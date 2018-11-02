using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Powershell
{
    [Cmdlet(VerbsCommon.New, "Message")]
    [OutputType(typeof(Message))]
    public class NewMessageCmdlet: Cmdlet
    {

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Session Session { get; set; }

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
            Message message = SecureMessageFactory.CreateNewMessage(messenger);

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
