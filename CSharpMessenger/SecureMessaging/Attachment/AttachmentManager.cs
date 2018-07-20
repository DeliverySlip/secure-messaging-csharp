using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using System.IO;
using SecureMessaging.ServiceStack.Services;
using SecureMessaging.ServiceStack.Entities;

namespace SecureMessaging.Attachment
{
    public class AttachmentManager
    {
        private Message message;
        private Session session;

        private List<FileInfo> attachmentList;
        private bool attachmentsHaveBeenPreCreated;

        private PreCreateAttachmentsResponse preCreateAttatchmentsResponse;

        public AttachmentManager(Message message, Session session)
        {
            this.message = message;
            this.session = session;

            this.attachmentList = new List<FileInfo>();
            this.attachmentsHaveBeenPreCreated = false;
        }

        public bool AddAttachmentFile(FileInfo file)
        {
            if(!this.attachmentsHaveBeenPreCreated && file.Exists)
            {
                this.attachmentList.Add(file);
                return true;
            }
            return false;
        }

        public bool AttachmentsHaveBeenPreCreated()
        {
            return this.attachmentsHaveBeenPreCreated;
        }

        public void PreCreateAllAttachments()
        {
            var client = this.session.Client;

            List<PreCreateAttachmentPlaceholder> attachmentPlaceholders = new List<PreCreateAttachmentPlaceholder>();

            foreach(FileInfo file in attachmentList)
            {
                var attachmentPlaceholder = new PreCreateAttachmentPlaceholder();
                attachmentPlaceholder.FileName = file.Name;
                attachmentPlaceholder.TotalBytesLength = file.Length;
                attachmentPlaceholders.Add(attachmentPlaceholder);
            }

            var request = new PreCreateAttachments();
            request.AttachmentPlaceholders = attachmentPlaceholders;
            request.MessageGuid = this.message.MessageGuid;

            this.preCreateAttatchmentsResponse = client.Post(request);

        }

        public void UploadAllAttachments()
        {
            var client = this.session.Client;

            if (this.attachmentsHaveBeenPreCreated)
            {
                foreach(var attachment in this.preCreateAttatchmentsResponse.AttachmentPlaceholders)
                {
                    UploadAttachment(attachment);
                }
            }
        }

        private void UploadAttachment(AttachmentPlaceholder attachment)
        {
            if (this.attachmentsHaveBeenPreCreated)
            {
                foreach (var fileInfoAttachment in this.attachmentList)
                {
                    if (fileInfoAttachment.Name.EqualsIgnoreCase(attachment.FileName))
                    {

                        UploadAttachmentChunk(attachment, this.message.MessageGuid, fileInfoAttachment);
                        return;
                    }
                }
                
                throw new FileNotFoundException("A File Contianing The Same Name As A Pre Created Attachment" +
                    "Could Not Be Found. Does The File Exist ? Has It Been Added PreCreateConfiguration ?");

            }else
            {
                throw new Exception("All Attachments Must Be PreCreated Before They Can Be Uploaded");
            }
            
        }

        private void UploadAttachmentChunk(AttachmentPlaceholder attachmentPlaceholder, Guid messageGuid, FileInfo attachmentFile)
        {
            var client = this.session.Client;
            //open the attachment and read it out and chunks to be uploaded
            using (var fs = File.OpenRead(attachmentFile.FullName))
            {
                foreach (var chunk in attachmentPlaceholder.Chunks)
                {
                    //create a buffer the same size as the chunk
                    byte[] buffer = new byte[chunk.BytesSize];
                    //use the chunks start index to position ourselves in the stream
                    fs.Position = chunk.ByteStartIndex;
                    //read enough data into the chunks buffer
                    fs.Read(buffer, 0, buffer.Length);

                    var chunkRequest = new UploadAttachmentChunk()
                    {
                        AttachmentGuid = attachmentPlaceholder.AttachmentGuid,
                        ChunkNumber = chunk.ChunkNumber
                    };

                    using (var ms = new MemoryStream(buffer))
                    {
                        client.PostFileWithRequest<UploadAttachmentChunk>(ms, attachmentPlaceholder.FileName, chunkRequest);
                    }
                }
            }
        }

        
        public List<AttachmentPlaceholder> GetAllPreCreatedAttachments()
        {
            return this.preCreateAttatchmentsResponse.AttachmentPlaceholders;
        }


        /*
        public void DownloadAllAttachments(String rootDownloadDirectory)
        {

        }

        public List<AttachmentSummary> GetAttachmentsInfo()
        {


            return null;
        }

        public void DownloadAttachment(AttachmentSummary attachmentSummary, String rootDownloadDirectory)
        {

        }*/




    }
}
