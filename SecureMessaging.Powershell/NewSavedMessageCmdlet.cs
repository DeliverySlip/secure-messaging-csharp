using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Powershell
{
    [Cmdlet(VerbsCommon.New, "SavedMessage")]
    [OutputType(typeof(SavedMessage))]
    class NewSavedMessageCmdlet : Cmdlet
    {

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Message Message { get; set; }

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
            SavedMessage message = messenger.SaveMessage(Message);

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
