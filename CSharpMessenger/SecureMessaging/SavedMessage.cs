using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging
{
    /// <summary>
    /// SavedMessage represents a message that has been saved. Once a message has been saved, there is
    /// not much that can be directly done with it. The intention is to have the user save and then
    /// most likely send the message next. Some actions such as attachments and notifications though
    /// still can be configured after the message is saved
    /// </summary>
    public class SavedMessage
    {
        internal Message Message { get; set; }
        
    }
}
