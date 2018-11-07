using SecureMessaging;
using SecureMessaging.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Powershell
{
    
    [Cmdlet(VerbsCommon.Get, "LoginSession")]
    [OutputType(typeof(Session))]
    public class GetLoginSessionCmdlet: Cmdlet
    {

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public String Username { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public String Password { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public String ServiceCode { get; set; }
        
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            //does initialization
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            //does execution

            Credentials credentials = new Credentials(Username, Password);
            MessagingApiClient client = MessagingApiClient.GetInstanceViaServiceCode(ServiceCode);
            client.SetClientName("secure-messenger-csharp-powershell");

            Session session = SessionFactory.CreateSession(credentials, client);
           
            WriteObject(session);
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
