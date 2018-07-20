using SecureMessaging.CCC;
using SecureMessaging.Utils;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Auth
{
    public class MessagingApiClient : JsonServiceClient
    {
        private String ClientName = "secure-messenger-csharp";
        private String ClientVersion = BuildVersion.GetBuildVersion();

        public static MessagingApiClient GetInstanceViaServiceCode(String serviceCode)
        {
            String messagingApiBaseUrl = ServiceCodeResolver.Resolve(serviceCode);
            return new MessagingApiClient(messagingApiBaseUrl);
        }

        public void SetClientName(String clientName)
        {
            this.ClientName = clientName;
            this.Headers.Add("x-sm-client-name", ClientName);
        }

        public void SetClientVersion(String clientVersion)
        {
            this.ClientVersion = clientVersion;
            this.Headers.Add("x-sm-client-version", ClientVersion);
        }

        public String GetClientName()
        {
            return this.ClientName;
        }

        public String GetClientVersion()
        {
            return this.ClientVersion;
        }

        public MessagingApiClient(String messagingApiBaseUrl): base(messagingApiBaseUrl)
        {
            global::ServiceStack.Text.JsConfig.DateHandler = global::ServiceStack.Text.DateHandler.ISO8601;
            global::ServiceStack.Text.JsConfig.AssumeUtc = true;
            global::ServiceStack.Text.JsConfig.AppendUtcOffset = false;

            Headers.Add("x-sm-client-name", ClientName);
            Headers.Add("x-sm-client-version", ClientVersion);

        }
    }
}
