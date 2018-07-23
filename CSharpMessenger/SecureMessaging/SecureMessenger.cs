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

        /// <summary>
        /// SetClientName sets the client identification name for the Secure Messaging API
        /// </summary>
        /// <param name="clientName">The name passed to the API identifying this client</param>
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

        /// <summary>
        /// SetClientVersion sets the clients version for the Secure Messaging API
        /// </summary>
        /// <param name="clientVersion">The version passed ot the API identifying the
        /// client</param>
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

        /// <summary>
        /// Logout of the Secure Messaging API. This simply terminates your session with the
        /// API. All sessions expire automatically from the server side. This is a helper
        /// method to explicitly terminate the session
        /// </summary>
		public void Logout()
		{
			var resp = this.Session.Client.Post(new Logout() { });
			this.Session = null;
			this.Session.Client.Headers.Remove("x-sm-session-token");
		}

        /// <summary>
        /// CreateAttachmentManagerForMessage instantiates an AttachmentManager for the
        /// passed in SavedMessage
        /// </summary>
        /// <param name="savedMessage">The saved message which an attachment
        /// manager is being created for</param>
        /// <returns></returns>
        public AttachmentManager CreateAttachmentManagerForMessage(SavedMessage savedMessage)
        {
            return new AttachmentManager(savedMessage, this.Session);
        }

        /// <summary>
        /// CreateAttachmentManagerForMessage instatiates an AttachmentManager for the
        /// passed in Message
        /// </summary>
        /// <param name="message">The message which an attachment manager is being
        /// created for</param>
        /// <returns></returns>
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

        /// <summary>
        /// Ping is a helper method for Pinging the Messaging API. This is primarily a debugging call
        /// but can be used to determine if the Messaging API is currently up and running, aswell
        /// as retrieve additional meta information about the API
        /// </summary>
        /// <returns></returns>
		public PingResponse Ping()
		{
			var req = new Ping();
			var response = this.Session.Client.Get(req);
			
			return response;
		}

        /// <summary>
        /// SaveMessage resaves the passed in SavedMessage
        /// </summary>
        /// <param name="savedMessage"></param>
        /// <returns></returns>
        public SavedMessage SaveMessage(SavedMessage savedMessage)
        {
            return this.SaveMessage(savedMessage.Message);
        }

        /// <summary>
        /// SaveMessage saves the passed in message. This is mandatory before sending a message and
        /// is recommended before uploading any attachments
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
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

        /// <summary>
        /// SendMessage sends the passed in SavedMessage
        /// </summary>
        /// <param name="savedMessage">A message that has been saved already</param>
        /// <returns></returns>
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

        /// <summary>
        /// UploadAttachmentsForMessage is a helper method which uploads the passed in list of 
        /// FileInfo attachments to the passed in SavedMessage. This method does not return until
        /// all attachments have been uploaded. For more control of attachment uploads, use the
        /// AttachmentManager
        /// </summary>
        /// <param name="savedMessage">The already saved message the attachments belong to</param>
        /// <param name="attachments">A list of attachments to be uploaded to the message</param>
        /// <returns></returns>
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

        /// <summary>
        /// Sets the passed in extensions for the passed in message
        /// </summary>
        /// <param name="message">The message the extensions are being applied to</param>
        /// <param name="extensions">The extensions being applied to the message</param>
        /// <returns></returns>
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

        /// <summary>
        /// SearchMessages takes the passed in filter and searches for all messages matching.
        /// </summary>
        /// <param name="searchMessageFilter">Filter with search parameters</param>
        /// <returns></returns>
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

        /// <summary>
        /// GetMessage fetches a single message from the API. Optional parameters allow retrieval of the
        /// body of the message and the password if the message has FYEO enabled
        /// </summary>
        /// <param name="messageGuid">ID of the message being fetched</param>
        /// <param name="retrieveBody">Whether to retrieve the body of the message or not</param>
        /// <param name="password">The password, if this message has FYEO or Confidential enabled</param>
        /// <returns></returns>
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
