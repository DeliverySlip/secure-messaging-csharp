using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;

namespace CSharpMessenger.SecureMessaging.Attachment
{
    class AttachmentManager
    {
        private Message message;
        private JsonServiceClient client;

        public AttachmentManager(Message message, JsonServiceClient client)
        {
            this.message = message;
            this.client = client;
        }
    }
}
