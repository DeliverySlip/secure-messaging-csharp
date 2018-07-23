using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Auth
{
    /// <summary>
    /// Endpoints stores and contains baseurl Endpoint information so that it can
    /// be accessed from anywhere within the SDK. This is also so that changes
    /// only need to be made in a single file to change them for the whole SDK
    /// </summary>
    class Endpoints
    {
        public static readonly String CCCAPI = "https://api.secure-messaging.com/api";
    }
}
