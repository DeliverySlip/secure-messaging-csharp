using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Powershell
{

    [Cmdlet(VerbsCommon.New, "MessageAttachment")]
    [OutputType(typeof(Message))]
    class NewMessageAttachmentCmdlet: Cmdlet
    {
        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Session Session { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Message Message { get; set;  }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string[] Attachments { get; set; }


        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            //does initialization
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            //does execution

            //login and get the message
            SecureMessenger messenger = new SecureMessenger(Session);
            SavedMessage savedMessage = messenger.SaveMessage(Message);

            // wrap paths in FileInfo objects
            List<FileInfo> attachmentFiles = new List<FileInfo>();
            foreach(var attachment in Attachments)
            {
                FileInfo file = new FileInfo(attachment);

                // Only add the attachment if it exists
                if (file.Exists)
                {
                    attachmentFiles.Add(file);
                }
                else
                {
                    throw new FileNotFoundException("Path To File: >" + attachment + "< Does Not Exist Or Is Not A Valid File To Upload As An Attachment");
                }
            }

            // upload the attachments
            Message message = messenger.UploadAttachmentsForMessage(savedMessage, attachmentFiles);
            WriteObject(message);
        }

        protected override void EndProcessing()
        {
            base.EndProcessing();
            //clean up after processing
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();
            //handles abnormal termination
        }
    }
}
