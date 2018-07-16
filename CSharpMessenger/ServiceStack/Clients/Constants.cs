using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMessenger.ServiceStack.Clients
{
    public class HeaderConstants
    {
        public const string RequestSessionTokenHeader = "x-sm-session-token";
        public const string RequestClientNameHeader = "x-sm-client-name";
        public const string RequestClientVersionHeader = "x-sm-client-version";
        public const string RequestPasswordHeader = "x-sm-password";

        public const string ResponseMsgApiVersionHeader = "X-sm-msgapi-version";
        public const string ResponseServiceSettingsVersionHeader = "X-sm-service-settings-version";
        public const string ResponseUserSettingsVersionHeader = "X-sm-user-settings-version";
    }
}
