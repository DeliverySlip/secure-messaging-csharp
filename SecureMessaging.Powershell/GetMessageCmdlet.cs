using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Powershell
{

    [Cmdlet(VerbsCommon.Get, "Message")]
    [OutputType(typeof(Message))]
    class GetMessageCmdlet: Cmdlet
    {

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Session Session { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Guid MessageGuid { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = false)]
        public String Password { get; set; } = null;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            //does initialization
        }

        
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            //does execution

            SecureMessenger messenger = new SecureMessenger(Session);
            Message message = messenger.GetMessage(MessageGuid, true, Password);

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
