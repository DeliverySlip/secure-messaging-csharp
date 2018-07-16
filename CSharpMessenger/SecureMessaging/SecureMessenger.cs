using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CSharpMessenger.SecureMessaging.Auth;
using CSharpMessenger.SecureMessaging.CCC;
using CSharpMessenger.SecureMessaging.Enums;
using CSharpMessenger.ServiceStack.Entities;
using CSharpMessenger.ServiceStack.Services;
using ServiceStack;
using CSharpMessenger.SecureMessaging.Search;

namespace CSharpMessenger.SecureMessaging
{
	public class SecureMessenger
	{

		private JsonServiceClient client;
		private Session Session;

		private String ClientName = "csharp-secure-messenger";
		private String ClientVersion = "2.1.0";

		private Mutex GetMessageMutex = new Mutex();

		public static SecureMessenger ResolveFromServiceCode(String serviceCode)
		{
			String baseURL = ServiceCodeResolver.Resolve(serviceCode);
			return new SecureMessenger(baseURL);
		}

		private void ConfigureClient()
		{
            global::ServiceStack.Text.JsConfig.DateHandler = global::ServiceStack.Text.DateHandler.ISO8601;
            global::ServiceStack.Text.JsConfig.AssumeUtc = true;
            global::ServiceStack.Text.JsConfig.AppendUtcOffset = false;

			this.client.Headers.Add("x-sm-client-name", ClientName);
			this.client.Headers.Add("x-sm-client-version", ClientVersion);
			
		}

		public void SetClientName(String clientName)
		{
			this.ClientName = clientName;
			this.client.Headers.Add("x-sm-client-name", ClientName);
		}

		public void SetClientVersion(String clientVersion)
		{
			this.ClientVersion = clientVersion;
			this.client.Headers.Add("x-sm-client-version", ClientVersion);
		}

		public SecureMessenger(String baseURL)
		{
			this.client = new JsonServiceClient(baseURL);
			ConfigureClient();
		}

		public SecureMessenger(JsonServiceClient client)
		{
			
			this.client = client;
			ConfigureClient();
		}

		public void Login(Credentials credentials)
		{
			this.Session = SessionFactory.createSession(credentials, this.client);
			//this.client.Headers.Add("x-sm-session-token", this.Session.SessionToken);
		}

		public void Logout()
		{
			var resp = client.Post(new Logout() { });
			this.Session = null;
			this.client.Headers.Remove("x-sm-session-token");
		}

        public Message PreCreateMessage(PreCreateConfiguration configuration)
        {
            var req = new PreCreateMessage()
            {
                ActionCode = configuration.GetActionCode().ToString(),
                ParentGuid = configuration.GetParentGuid(),
                CampaignGuid = configuration.GetCampaignGuid()
            };

            if (configuration.GetPassword() != null)
            {
                req.Password = configuration.GetPassword();
            }

            //this.client.Headers.Add("x-sm-session-token", this.Session.SessionToken);
            var response = this.client.Post(req);

            Message message = new Message();
            message.MessageGuid = response.MessageGuid;
            message.From.Add(this.Session.EmailAddress);

            return message;
        }

        [Obsolete("PreCreateMessage With Configuration Parameters Is Being Deprecated For PreCreateConfiguration Parameter. Method Will Be Removed In v4.0.0")]
        public Message PreCreateMessage(ActionCodeEnum actionCode = ActionCodeEnum.New, Guid? parentGuid = null, String password = null, Guid? campaignGuid = null)
		{
			var req = new PreCreateMessage()
			{
				ActionCode = actionCode.ToString(),
				ParentGuid = parentGuid,
				CampaignGuid = campaignGuid,
			};

			if (password != null)
			{
				req.Password = password;
			}

			//this.client.Headers.Add("x-sm-session-token", this.Session.SessionToken);
			var response = this.client.Post(req);

			Message message = new Message();
			message.MessageGuid = response.MessageGuid;
			message.From.Add(this.Session.EmailAddress);

			return message;
		}

		public PingResponse Ping()
		{
			var req = new Ping();
			var response = client.Get(req);
			
			//return client.Get<PingResponse>(string.Format("{0}{1}", _baseApiUri, req.ToGetUrl()));//ping is unversioned... so it cannot use the versioned url
			return response;
		}

		public Message SaveMessage(Message message)
		{

			MessageOptions messageOptions = new MessageOptions()
			{
				AllowForward = message.MessageOptions.AllowForward,
				AllowReply = message.MessageOptions.AllowReply,
				AllowTracking = message.MessageOptions.AllowTracking,
				FYEOType = message.MessageOptions.FyeoType.ToString(),
				ShareTracking = message.MessageOptions.ShareTracking
			};

			var req = new SaveMessage()
			{
				MessageGuid = message.MessageGuid,
				BodyFormat = message.BodyFormat.ToString(),
				To = message.To,
				Cc = message.Cc,
				Bcc = message.Bcc,
				Subject = message.Subject,
				Body = message.Body,
				MessageOptions = messageOptions,
				ExpiryDate = message.ExpiryDate,
				ExpiryGroup = message.ExpiryGroup
			};

			var response = client.Put(req);
			message.MessageSaved = true;

			return message;

		}

		public Message SendMessage(Message message)
		{

			var req = new SendMessage()
			{
				MessageGuid = message.MessageGuid,
				Password = message.Password,
				InviteNewUsers = message.InviteNewUsers,
				CraCode = message.CraCode,
				SendEmailNotification = message.SendNotification
			};

			var response = client.Put(req);
			message.MessageSent = true;

			return message;

		}

		public Message UploadAttachmentsForMessage(Message message, IEnumerable<FileInfo> attachments)
		{

			//allocate server resources for passed in attachments needing uploading
			var preCreateAttachmentPlaceholders = new List<PreCreateAttachmentPlaceholder>();

			foreach (var attachment in attachments)
			{
				preCreateAttachmentPlaceholders.Add(new PreCreateAttachmentPlaceholder() { FileName = attachment.Name, TotalBytesLength = attachment.Length });
			}

			//api call to allocate
			var preCreateRequest = new PreCreateAttachments()
			{
				MessageGuid = message.MessageGuid,
				AttachmentPlaceholders = preCreateAttachmentPlaceholders
			};

			var response = client.Post(preCreateRequest);
			var attachmentPlaceholders = response.AttachmentPlaceholders;

			//process each placeholder, find the correct matching file, and upload
			foreach (var attachmentPlaceholder in attachmentPlaceholders)
			{
				FileInfo attachmentFileInfo = attachments.Where(a => a.Name.Equals(attachmentPlaceholder.FileName)).First();
				UploadAttachment(attachmentPlaceholder, attachmentFileInfo);

			}

			message.UploadedAttachments = attachmentPlaceholders;
			message.AttachmentsAdded = true;
			return message;

		}

		private void UploadAttachment(AttachmentPlaceholder attachmentPlaceholder, FileInfo attachment )
		{

			//open the attachment and read it out and chunks to be uploaded
			using (var fs = File.OpenRead(attachment.FullName))
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
						try
						{
							client.PostFileWithRequest<UploadAttachmentChunk>(ms, attachmentPlaceholder.FileName, chunkRequest);
						}
						catch (WebServiceException ex)
						{
							Console.WriteLine("ERROR: " + ex.ResponseDto.ToString());
							throw;
						}

					}
				}

			}
		}

		public void DownloadAttachment(Guid attachmentGuid, String localFolderPath = null, bool? preview = null, String downloadType = null, String downloadFileType = null)
		{
			var req = new DownloadAttachment()
			{
				AttachmentGuid = attachmentGuid,
				DownloadType = downloadType,
				DownloadFileType = downloadFileType,
				Preview = preview
			};

			var resp = client.Get<HttpWebResponse>(req);
			var stream = resp.GetResponseStream();
			var bytes = stream.ToBytes();

			var localFileName = resp.Headers["content-disposition"]
				.Replace("attachment; filename=", "").Replace("\"", "").Replace(";", ""); //prob a better way to do this

			File.WriteAllBytes(string.Format("{0}/{1}", localFolderPath, localFileName), bytes);
		}

		public Message SetMessageExtensions(Message message, List<Extension> extensions)
		{
			var req = new SetMessageExtensions()
			{
				Extensions = extensions,
				MessageGuid = message.MessageGuid
			};

			var response = client.Put(req);

			message.Extensions = extensions;
			return message;
		}

        public SearchMessagesResults SearchMessages(SearchMessagesFilter searchMessageFilter)
        {

            var req = new SearchMessages()
            {
                Filter = searchMessageFilter
            };

            SearchMessagesPagedResponse response = client.Get(req);

            return new SearchMessagesResults(response, searchMessageFilter, this.client);


        }

        [Obsolete("SearchMessage Returning List Is Deprecated. Please use SearchMessages returning SearchMessagesResults instead. Method Will Be Removed In v4.0.0")]
        public List<MessageSummary> SearchMessage(SearchMessagesFilter searchCriteria)
		{

			List<MessageSummary> messageSummaries = new List<MessageSummary>();


			var req = new SearchMessages()
			{
				Filter = searchCriteria
			};

			SearchMessagesPagedResponse response = client.Get(req);
			messageSummaries.AddRange(response.Results);

			if (response.TotalPages > 1)
			{
				for (var i = 2; i <= response.TotalPages; i++)
				{
					var nextPageReq = new SearchMessages()
					{
						Filter = searchCriteria,
						Page = i
					};

					var nextPageResp = client.Get(nextPageReq);
					messageSummaries.AddRange(nextPageResp.Results);

				}
			}

			return messageSummaries;
		}

        public Message GetMessage(Guid messageGuid, bool retrieveBody = true, String password = null)
		{
			GetMessageMutex.WaitOne();
			try
			{

				var req = new GetMessage()
				{
					MessageGuid = messageGuid,
					RetrieveBody = retrieveBody
				};

				if (password != null)
				{
					var bytes = System.Text.Encoding.UTF8.GetBytes(password);
					var base64EncodedString = System.Convert.ToBase64String(bytes);
					this.client.Headers.Add("x-sm-password", base64EncodedString);
				}

				var response = this.client.Get(req);

                Message message = new Message();
                message.Bcc = response.Bcc.Select(s => s.Email).ToList();
                message.Body = response.Body;
                message.BodyFormat = (BodyFormatEnum)Enum.Parse(typeof(BodyFormatEnum), response.BodyFormat);
                message.Cc = response.Cc.Select(s => s.Email).ToList();
                message.MessageGuid = response.Guid;
                message.To = response.To.Select(s => s.Email).ToList();
                message.Extensions = response.Extensions;
                message.ExpiryDate = response.ExpiryDate;
                message.ExpiryGroup = response.ExpiryGroup;
                message.From = new List<String>() { response.Sender.Email };
                message.MessageSaved = true;
                message.MessageSent = true;
                message.AttachmentsAdded = (response.Attachments.Count > 0);

                return message;

			}
			catch (Exception ex)
			{
				throw;
			}
			finally
			{
				this.client.Headers.Remove("x-sm-password");
				GetMessageMutex.ReleaseMutex();
			}
		}

	}
}
