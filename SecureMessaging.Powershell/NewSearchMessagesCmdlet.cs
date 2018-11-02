using SecureMessaging.Search;
using SecureMessaging.ServiceStack.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Powershell
{

    [Cmdlet(VerbsCommon.New, "SearchMessages")]
    [OutputType(typeof(IEnumerable<MessageSummary>))]
    class NewSearchMessagesCmdlet : Cmdlet
    {

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public Session Session { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public SearchMessagesFilter SearchMessagesFilter { get; set; }

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
            SearchMessagesResults results = messenger.SearchMessages(SearchMessagesFilter);

            WriteObject(results.GetEnumerator());
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
