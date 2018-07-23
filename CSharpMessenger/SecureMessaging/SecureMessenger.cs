using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SecureMessaging.Auth;
using SecureMessaging.CCC;
using SecureMessaging.Enums;
using SecureMessaging.ServiceStack.Entities;
using SecureMessaging.ServiceStack.Services;
using ServiceStack;
using SecureMessaging.Search;
using SecureMessaging.Utils;
using SecureMessaging.Attachment;

namespace SecureMessaging
{
	public class SecureMessenger
	{

		private Session Session;
        private MessagingApiClient client;

		private Mutex GetMessageMutex = new Mutex();

		public static SecureMessenger ResolveFromServiceCode(String serviceCode)
		{
			String baseURL = ServiceCodeResolver.Resolve(serviceCode);
            return new SecureMessenger(baseURL);
		}

        /// <summary>
        /// Create a SecureMessenger with a Messaging API endpoint. You will login using the 
        /// SecureMessenger instance
        /// </summary>
        /// <param name="baseURL"></param>
		public SecureMessenger(String baseURL)
		{
            this.client = new MessagingApiClient(baseURL);
		}

        /// <summary>
        /// Create a SecureMessenger using a MessagingApiClient. You will login using the
        /// SecureMessenger instance
        /// </summary>
        /// <param name="client"></param>
		public SecureMessenger(MessagingApiClient client)
		{
			this.client = client;
		}

        /// <summary>
        /// Create a SecureMessenger with an existing session. This means you have logged in independently
        /// of the SecureMessenger class
        /// </summary>
        /// <param name="session">The Session object representing your connection with the
        /// Messaging API</param>
        public SecureMessenger(Session session)
        {
            this.Session = session;
            this.client = session.Client;
        }

        public void SetClientName(String clientName)
        {

            if(this.Session == null)
            {
                this.client.SetClientName(clientName);
            }else
            {
                this.Session.Client.SetClientName(clientName);
            }
            
        }

        public void SetClientVersion(String clientVersion)
        {
            if(this.Session == null)
            {
                this.client.SetClientVersion(clientVersion);
            }else
            {
                this.Session.Client.SetClientVersion(clientVersion);
            }
        }

        /// <summary>
        /// Login to the Secure Messaging API, this assumes you have not logged in previously
        /// before instantiating the SecureMessenger. Calling login is not necessary if
        /// the SecureMessenger was instantiated with a Session object. If you call login,
        /// and you have logged in already, your previous session will be overwritten with
        /// the new one which the SecureMessenger will create
        /// </summary>
        /// <param name="credentials"></param>
        public void Login(Credentials credentials)
		{
			this.Session = SessionFactory.CreateSession(credentials, this.client);
        }

		public void Logout()
		{
			var resp = this.Session.Client.Post(new Logout() { });
			this.Session = null;
			this.Session.Client.Headers.Remove("x-sm-session-token");
		}

        public AttachmentManager CreateAttachmentManagerForMessage(SavedMessage savedMessage)
        {
            return new AttachmentManager(savedMessage, this.Session);
        }

        public AttachmentManager CreateAttachmentManagerForMessage(Message message)
        {
            return new AttachmentManager(message, this.Session);
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


            var response = this.Session.Client.Post(req);

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

            var response = this.Session.Client.Post(req);

			Message message = new Message();
			message.MessageGuid = response.MessageGuid;
			message.From.Add(this.Session.EmailAddress);

			return message;
		}

		public PingResponse Ping()
		{
			var req = new Ping();
			var response = this.Session.Client.Get(req);
			
			return response;
		}

        public SavedMessage SaveMessage(SavedMessage savedMessage)
        {
            return this.SaveMessage(savedMessage.Message);
        }

		public SavedMessage SaveMessage(Message message)
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

			var response = this.Session.Client.Put(req);
			message.MessageSaved = true;

			return new SavedMessage() { Message = message };

		}

		public Message SendMessage(SavedMessage savedMessage)
		{

            var message = savedMessage.Message;

			var req = new SendMessage()
			{
				MessageGuid = message.MessageGuid,
				Password = message.Password,
				InviteNewUsers = message.InviteNewUsers,
				CraCode = message.CraCode,
				SendEmailNotification = message.SendNotification
			};

			var response = this.Session.Client.Put(req);
			message.MessageSent = true;

			return message;

		}

		public Message UploadAttachmentsForMessage(SavedMessage savedMessage, IEnumerable<FileInfo> attachments)
		{

            AttachmentManager attachmentManager = new AttachmentManager(savedMessage, this.Session);

            foreach(var attachment in attachments)
            {
                attachmentManager.AddAttachmentFile(attachment);
            }

            attachmentManager.PreCreateAllAttachments();
            attachmentManager.UploadAllAttachments();

            savedMessage.Message.UploadedAttachments = attachmentManager.GetAllPreCreatedAttachments();
            savedMessage.Message.AttachmentsAdded = true;

            return savedMessage.Message;

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

			var resp = this.Session.Client.Get<HttpWebResponse>(req);
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

			var response = this.Session.Client.Put(req);

			message.Extensions = extensions;
			return message;
		}

        public SearchMessagesResults SearchMessages(SearchMessagesFilter searchMessageFilter)
        {

            var req = new SearchMessages()
            {
                Filter = searchMessageFilter
            };

            SearchMessagesPagedResponse response = this.Session.Client.Get(req);

            return new SearchMessagesResults(response, searchMessageFilter, this.Session.Client);


        }

        [Obsolete("SearchMessage Returning List Is Deprecated. Please use SearchMessages returning SearchMessagesResults instead. Method Will Be Removed In v4.0.0")]
        public List<MessageSummary> SearchMessage(SearchMessagesFilter searchCriteria)
		{

			List<MessageSummary> messageSummaries = new List<MessageSummary>();


			var req = new SearchMessages()
			{
				Filter = searchCriteria
			};

			SearchMessagesPagedResponse response = this.Session.Client.Get(req);
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

					var nextPageResp = this.Session.Client.Get(nextPageReq);
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
					this.Session.Client.Headers.Add("x-sm-password", base64EncodedString);
				}

				var response = this.Session.Client.Get(req);

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
				this.Session.Client.Headers.Remove("x-sm-password");
				GetMessageMutex.ReleaseMutex();
			}
		}

	}
}
