using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using SecureMessaging.ServiceStack.Entities;

namespace SecureMessaging.Powershell
{
    [Cmdlet(VerbsCommon.New, "SearchMessagesFilter")]
    [OutputType(typeof(SearchMessagesFilter))]
    class NewSearchMessagesFilterCmdlet : Cmdlet
    {

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            //does initialization
        }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            //does execution
                
            WriteObject(new SearchMessagesFilter());
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
