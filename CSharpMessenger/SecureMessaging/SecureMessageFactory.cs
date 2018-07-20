using SecureMessaging;
using SecureMessaging.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging
{
    /// <summary>
    /// SecureMessageFactory is a helper/convenience class for creating messages, whether that be to forward,
    /// reply or reply all
    /// </summary>
    public class SecureMessageFactory
    {
        /// <summary>
        /// CreateNewMessage precreates a new message
        /// </summary>
        /// <param name="messenger">The SecureMessenger instance</param>
        /// <returns></returns>
        public static Message CreateNewMessage(SecureMessenger messenger)
        {

            PreCreateConfiguration configuration = new PreCreateConfiguration();
            configuration.SetActionCode(ActionCodeEnum.New);

            return messenger.PreCreateMessage(configuration);

        }

        /// <summary>
        /// ForwardMessage creates a new message which is forwarding another message
        /// </summary>
        /// <param name="messenger">The SecureMessenger instance</param>
        /// <param name="forwardedMessage">The message being forwarded</param>
        /// <returns></returns>
        public static Message ForwardMessage(SecureMessenger messenger, Message forwardedMessage)
        {
            PreCreateConfiguration configuration = new PreCreateConfiguration();
            configuration.SetActionCode(ActionCodeEnum.Forward);
            configuration.SetParentGuid(forwardedMessage.MessageGuid);
            configuration.SetPassword(forwardedMessage.Password);

            return messenger.PreCreateMessage(configuration);
        }

        /// <summary>
        /// ReplyMessage creates a new message which is replying to another message
        /// </summary>
        /// <param name="messenger">The SecureMessenger instance</param>
        /// <param name="replyMessage">The message being replied to</param>
        /// <returns></returns>
        public static Message ReplyMessage(SecureMessenger messenger, Message replyMessage)
        {
            PreCreateConfiguration configuration = new PreCreateConfiguration();
            configuration.SetActionCode(ActionCodeEnum.Reply);
            configuration.SetParentGuid(replyMessage.MessageGuid);
            configuration.SetPassword(replyMessage.Password);

            return messenger.PreCreateMessage(configuration);
        }

        /// <summary>
        /// ReplyAllMessage creates a new message which reply alls to another message
        /// </summary>
        /// <param name="messenger">The SecureMessenger instance</param>
        /// <param name="replyAllMessage">The message being reply all'd to</param>
        /// <returns></returns>
        public static Message ReplyAllMessage(SecureMessenger messenger, Message replyAllMessage)
        {
            PreCreateConfiguration configuration = new PreCreateConfiguration();
            configuration.SetActionCode(ActionCodeEnum.ReplyAll);
            configuration.SetParentGuid(replyAllMessage.MessageGuid);
            configuration.SetPassword(replyAllMessage.Password);

            return messenger.PreCreateMessage(configuration);
        }


    }
}
