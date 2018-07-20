using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SecureMessaging.ServiceStack.Entities;
using SecureMessaging.ServiceStack.Services;
using ServiceStack;

namespace SecureMessaging.ServiceStack.Clients
{
    public class MsgApiClient : JsonServiceClient
    {
        public string SessionToken { get; private set; }
        private string _baseApiUri { get; set; }
        private int _apiVersion { get; set; }

        private string _clientName { get; set; }
        private string _clientVersion { get; set; }

        private string _fyeoRequestHeaderPassword { get; set; }

	    public static MsgApiClient InstantiateFromServiceCode(string cccApiBaseUrl, string serviceCode, int apiVersion, string clientName, string clientVersion)
	    {
			var baseMsgApiUrl = new CccApiClient(cccApiBaseUrl).DiscoverServiceApiUrl(serviceCode);

			if (string.IsNullOrWhiteSpace(baseMsgApiUrl)) { throw new Exception("Failed To Resolve Service From ServiceCode. Must resolve a valid baseMsgApiUrl"); }

			return new MsgApiClient(baseMsgApiUrl, apiVersion, clientName, clientVersion);
	    }

        public MsgApiClient(string baseApiUri, int apiVersion, string clientName, string clientVersion) : 
            base(string.Format("{0}", baseApiUri))//make the default apiUrl point to the versioned api operations. Any unversioned calls must do so explicitly.  
        {
            global::ServiceStack.Text.JsConfig.DateHandler = global::ServiceStack.Text.DateHandler.ISO8601;
            global::ServiceStack.Text.JsConfig.AssumeUtc = true;
            global::ServiceStack.Text.JsConfig.AppendUtcOffset = false;
            _baseApiUri = baseApiUri;
            _apiVersion = apiVersion;
            _clientName = clientName;
            _clientVersion = _clientVersion;
            
            this.Timeout = TimeSpan.FromSeconds(60);

            //add outgoing headers to every request (MsgApi requires that clients declare what type of client they are ie. WebApp, Outlook, Custom Integration A, etc)
            this.RequestFilter = httpReq =>
            {
                //add headers meant for every outbound request
                httpReq.Headers.Add(HeaderConstants.RequestClientNameHeader, this._clientName);
                httpReq.Headers.Add(HeaderConstants.RequestClientVersionHeader, this._clientVersion);
                httpReq.Headers.Add(HeaderConstants.RequestSessionTokenHeader, this.SessionToken);

                //not ideal - but the only way we can add headers easily to a single outgoing request. The idea here is to assign a value to _fyeoRequestHeaderPassword, make a call, and then assign a null value to _fyeoRequestHeaderPassword. 
                if (!string.IsNullOrWhiteSpace(_fyeoRequestHeaderPassword))
                {
                    var encodedPass = EncodeStringToBase64String(_fyeoRequestHeaderPassword);//base64 encode the password as per BasicAuth requirements
                    httpReq.Headers.Add(HeaderConstants.RequestPasswordHeader, encodedPass);
                }

            };

            this.ResponseFilter = httpResp =>
            {
                Console.WriteLine((int)httpResp.StatusCode + " " + httpResp.StatusCode.ToString());
            };
        }

        #region overrides to the base ServiceStack client verbs to display outputs


        public override TResponse Put<TResponse>(IReturn<TResponse> requestDto)
        {
            try
            {
                Console.WriteLine("PUT " + this.BaseUri + requestDto.ToPutUrl());
                Console.WriteLine(requestDto.GetType().Name + " Request: " + requestDto.ToPrettyString());
                var response = base.Put(requestDto);
                Console.WriteLine(requestDto.GetType().Name + " Response: " + response.ToPrettyString());
                return response;
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine(ex.StatusCode + " " + ex.StatusDescription + " ERROR: " + ex.ResponseDto.ToPrettyString());
                throw;
            }
        }
        public override TResponse Get<TResponse>(IReturn<TResponse> requestDto)
        {
            try
            {

                Console.WriteLine("GET " + this.BaseUri + requestDto.ToGetUrl());
                Console.WriteLine(requestDto.GetType().Name + " Request: " + requestDto.ToPrettyString());
                var response = base.Get(requestDto);
                Console.WriteLine(requestDto.GetType().Name + " Response: " + response.ToPrettyString());
                return response;
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine(ex.StatusCode + " " + ex.StatusDescription + " ERROR: " + ex.ResponseDto.ToPrettyString());
                throw;
            }
        }
        public override TResponse Delete<TResponse>(IReturn<TResponse> requestDto)
        {
            try
            {
                Console.WriteLine("DELETE " + this.BaseUri + requestDto.ToDeleteUrl());
                Console.WriteLine(requestDto.GetType().Name + " Request: " + requestDto.ToPrettyString());
                var response = base.Delete(requestDto);
                Console.WriteLine(requestDto.GetType().Name + " Response: " + response.ToPrettyString());
                return response;
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine(ex.StatusCode + " " + ex.StatusDescription + " ERROR: " + ex.ResponseDto.ToPrettyString());
                throw;
            }
        }

        public override TResponse Post<TResponse>(IReturn<TResponse> requestDto)
        {
            try
            {
                Console.WriteLine("POST " + this.BaseUri + requestDto.ToPostUrl());
                Console.WriteLine(requestDto.GetType().Name + " Request: " + requestDto.ToPrettyString());
                var response = base.Post(requestDto);
                Console.WriteLine(requestDto.GetType().Name + " Response: " + response.ToPrettyString());
                return response;
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine(ex.StatusCode + " " + ex.StatusDescription + " ERROR: " + ex.ResponseDto.ToPrettyString());
                throw;
            }
        }

        public override TResponse PostFileWithRequest<TResponse>(Stream fileToUpload, string fileName, object request,
            string fieldName = "upload")
        {
            try
            {
                Console.WriteLine("PostFileWithRequest " + this.BaseUri + request.ToPostUrl());
                Console.WriteLine(request.GetType().Name + " Request: " + request.ToPrettyString());
                var response = base.PostFileWithRequest<TResponse>(fileToUpload, fileName, request, fieldName);
                Console.WriteLine(request.GetType().Name + " Response: " + response.ToPrettyString());
                return response;
            }
            catch (WebServiceException ex)
            {
                Console.WriteLine(ex.StatusCode + " " + ex.StatusDescription + " ERROR: " + ex.ResponseDto.ToPrettyString());
                throw;
            }


        }
        #endregion

        #region Authentication

        public LoginResponse Login(string email, string pass, bool? cookieless = null)
        {
            var req = new Login()
            {
                Username = email,
                Password = pass,
                Cookieless = cookieless
            };
            var resp = Post(req);
            SessionToken = resp.SessionToken;
            return resp;
        }

        public LogoutResponse Logout()
        {
            var resp = Post(new Logout() { });
            SessionToken = null;
            return resp;
        }



        #endregion

        #region Messages 
        public PreCreateMessageResponse PreCreateMessage(string actionCode, Guid? parentGuid = null, string password = null, Guid? campaignGuid = null)
        {
            var req = new PreCreateMessage()
            {
                ActionCode = actionCode,
                ParentGuid = parentGuid,               
                Password = password,
                CampaignGuid = campaignGuid
            };
            return Post(req);
        }


        public SaveMessageResponse SaveMessage(Guid messageGuid, string bodyFormat, List<string> to = null, List<string> cc = null, List<string> bcc = null, string subject = null,
                                string body = null, MessageOptions messageOptions = null, DateTime? expiryDate = null, string expiryGroup = null)
        {
            var req = new SaveMessage()
            {
                MessageGuid = messageGuid,
                BodyFormat = bodyFormat,
                To = to,
                Cc = cc,
                Bcc = bcc,
                Subject = subject,
                Body = body,
                MessageOptions = messageOptions,
                ExpiryDate = expiryDate,
                ExpiryGroup = expiryGroup
            };
            return Put(req);
        }

        public SendMessageResponse SendMessage(Guid messageGuid, string password = null, bool? inviteNewUsers = null, bool? sendNotification = null,
                                string craCode = null, List<string> notificationFormats = null)
        {
            var req = new SendMessage()
            {
                MessageGuid = messageGuid,
                Password = password,
                InviteNewUsers = inviteNewUsers,
                CraCode = craCode,
                NotificationFormats = notificationFormats
            };
            return Put(req);
        }

        //RetrieveBody in v6.50.16330.2 and above
        public GetMessageResponse GetMessage(Guid messageGuid, string password = null, bool? retrieveBody = null)
        {
            try
            {
                var req = new GetMessage()
                {
                    MessageGuid = messageGuid,
                    RetrieveBody = retrieveBody
                };
                _fyeoRequestHeaderPassword = password;//assign a local variable the FYEO password we want to send with the headers for THIS REQUEST ONLY (the RequestFilter in the ctor intercept all requests and check if it should be adding this password). This operation is not thread safe and would need a concurrency model applied to it to run properly in a multithreaded environment
                return Get(req);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                _fyeoRequestHeaderPassword = null;//always set the password var to null after the request is completed
            }
        }


        #endregion

        #region Attachments

        public PreCreateAttachmentsResponse PreCreateAttachments(Guid messageGuid, List<PreCreateAttachmentPlaceholder> preCreateAttachmentPlaceholders)
        {
            var placeholders = new List<AttachmentPlaceholder>();

            var req = new PreCreateAttachments()
            {
                MessageGuid = messageGuid,
                AttachmentPlaceholders = preCreateAttachmentPlaceholders
            };

            return Post(req);
        }

        public Dictionary<string, AttachmentPlaceholder> CreatePlaceHolderDictionary(IEnumerable<FileInfo> fileInfos, Guid messageGuid)
        {
            var preCreateAttachmentPlaceholders = new List<PreCreateAttachmentPlaceholder>();

            foreach (var fileInfo in fileInfos)
            {
                preCreateAttachmentPlaceholders.Add(new PreCreateAttachmentPlaceholder() { FileName = fileInfo.Name, TotalBytesLength = fileInfo.Length });
            }

            //load some of the attachment information into the pre create DTOs 
            var attachmentPlaceholders = PreCreateAttachments(messageGuid, preCreateAttachmentPlaceholders).AttachmentPlaceholders;
            var attachmentDict = new Dictionary<string, AttachmentPlaceholder>();


            foreach (var attachPlaceholder in attachmentPlaceholders)
            {
                var fileInfo = fileInfos.First(x => x.Name == attachPlaceholder.FileName);
                attachmentDict.Add(fileInfo.FullName, attachPlaceholder);
            }
            return attachmentDict;
        }

        //pass in a Dictionary<string, AttachmentPlaceholder> where the 'string' is the physical file path, and the 'AttachmentPlaceholder' is the request DTO to be sent in the outgoing request. By passing in the full path AND the DTO, it allows this method to encapulate the complexity of opening files, closing streams, dealing with chunks, etc 
        public void UploadAttachments(Dictionary<string, AttachmentPlaceholder> attachmentPlaceholders)
        {
            foreach (var attachment in attachmentPlaceholders)
            {
                var fi = new FileInfo(attachment.Key);
                var attachmentPlaceholder = attachment.Value;

                using (var fs = File.OpenRead(fi.FullName))
                {
                    //iterate through all the expected chunks
                    foreach (var chunkPlaceholder in attachmentPlaceholder.Chunks)
                    {
                        //create the chunk DTO
                        var req = new UploadAttachmentChunk()
                        {
                            AttachmentGuid = attachmentPlaceholder.AttachmentGuid,
                            ChunkNumber = chunkPlaceholder.ChunkNumber
                        };
                        //create a buffer the same size as the chunk
                        byte[] buffer = new byte[chunkPlaceholder.BytesSize];

                        //use the chunks start index to position ourselves in the stream
                        fs.Position = chunkPlaceholder.ByteStartIndex;

                        //read enough data into the chunks buffer
                        fs.Read(buffer, 0, buffer.Length);

                        using (var ms = new MemoryStream(buffer))
                        {
                            try
                            {
                                //post the DTO, along with the memorystream (which wraps the byte[] buffer)
                                PostFileWithRequest<UploadAttachmentChunk>(ms, "", req);
                                Console.WriteLine(string.Format("Upload Chunk {0} Success", chunkPlaceholder.ChunkNumber));
                            }
                            catch (WebServiceException ex)
                            {
                                Console.WriteLine("ERROR: " + ex.ResponseDto.ToPrettyString());
                                throw;
                            }

                        }
                    }
                }
            }
        }

        public void DownloadAttachment(Guid attachmentGuid, string localFolderPath = null, bool? preview = null, string downloadType = null, string downloadFileType = null)
        {
            var req = new DownloadAttachment()
            {
                AttachmentGuid = attachmentGuid,
                DownloadType = downloadType,
                DownloadFileType = downloadFileType,
                Preview = preview
            };

            var resp = Get<HttpWebResponse>(req);
            var stream = resp.GetResponseStream();
            var bytes = stream.ToBytes();

            var localFileName = resp.Headers["content-disposition"]
                .Replace("attachment; filename=", "").Replace("\"", "").Replace(";", ""); //prob a better way to do this

            File.WriteAllBytes(string.Format("{0}/{1}", localFolderPath, localFileName), bytes);            
        }


    
        #endregion

        #region Search

  
        public SearchMessagesPagedResponse SearchMessages(int? page = null, int? pageSize = null, SearchMessagesFilter filter = null, SearchMessagesSort sort = null)
        {
            var req = new SearchMessages()
            {
                Page = page,
                PageSize = pageSize,
                Filter = filter,
                Sort = sort
            };
            return Get(req);
        }



        #endregion

        #region Utils

        public PingResponse Ping()
        {
            var req = new Ping();

            return Get<PingResponse>(string.Format("{0}{1}", _baseApiUri, req.ToGetUrl()));//ping is unversioned... so it cannot use the versioned url
        }

        public static string EncodeStringToBase64String(string value)
        {
        var bytes = System.Text.Encoding.UTF8.GetBytes(value);
        var base64EncodedString = System.Convert.ToBase64String(bytes);
            return base64EncodedString;
        }

        #endregion

    }

    public static class ExtentionMethods
    {
        public static string ToPrettyString(this object value)
        {
            return value.ToJson();
        }
    }



}
