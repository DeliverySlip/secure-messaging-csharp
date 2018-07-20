using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureMessaging;
using SecureMessaging.Enums;
using System.Collections.Generic;
using SecureMessaging.CCC;
using SecureMessaging.Auth;

namespace CSharpMessengerTests
{
    [TestClass]
    public class SendMessageTests: BaseTestCase
    {

        [ClassInitialize]
        public static void BeforeClass(TestContext context)
        {
            BaseTestCase.BeforeClassLoader(context);
        }

        [TestMethod]
        public void TestSendBasicMessage()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            PreCreateConfiguration configuration = new PreCreateConfiguration();
            configuration.SetActionCode(ActionCodeEnum.New);

            Message message = messenger.PreCreateMessage(configuration);

            message.To = new List<String>()
            {
                RecipientEmail
            };
            message.Subject = "DeliverySlip C# Example";
            message.Body = "Hello Test Message From DeliverySlip C# Example";
            message.BodyFormat = BodyFormatEnum.Text;

            message = messenger.SaveMessage(message);
            messenger.SendMessage(message);

        }

        [TestMethod]
        public void TestSendBasicMessageMostDecoupled()
        {
            String messagingApiUrl = ServiceCodeResolver.Resolve(ServiceCode);
            MessagingApiClient client = new MessagingApiClient(messagingApiUrl);
            Credentials credentials = new Credentials(Username, Password);

            // this step is doing the logging in
            Session session = SessionFactory.CreateSession(credentials, client);

            // this is now to interact with your session
            SecureMessenger messenger = new SecureMessenger(session);

            Message message = SecureMessageFactory.CreateNewMessage(messenger);
            message.To = new List<String>()
            {
                RecipientEmail
            };
            message.Subject = "DeliverySlip C# Example";
            message.Body = "Hello Test Message From DeliverySlip C# Example";
            message.BodyFormat = BodyFormatEnum.Text;

            message = messenger.SaveMessage(message);
            messenger.SendMessage(message);
        }

        [TestMethod]
        public void TestSendBasicMessageWithFactory()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            Message message = SecureMessageFactory.CreateNewMessage(messenger);

            message.To = new List<String>()
            {
                RecipientEmail
            };
            message.Subject = "DeliverySlip C# Example";
            message.Body = "Hello Test Message From DeliverySlip C# Example";
            message.BodyFormat = BodyFormatEnum.Text;

            message = messenger.SaveMessage(message);
            messenger.SendMessage(message);
        }

        [TestMethod]
        public void TestSendFYEOMessageAccountPassword()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            PreCreateConfiguration configuration = new PreCreateConfiguration();
            configuration.SetActionCode(ActionCodeEnum.New);

            Message message = messenger.PreCreateMessage(configuration);

            message.To = new List<String>()
            {
                RecipientEmail
            };
            message.Subject = "DeliverySlip C# Example";
            message.Body = "Hello Test Message From DeliverySlip C# Example";
            message.BodyFormat = BodyFormatEnum.Text;

            message.MessageOptions.FyeoType = FyeoTypeEnum.AccountPassword;
            message.Password = Password;

            message = messenger.SaveMessage(message);
            messenger.SendMessage(message);
        }

        [TestMethod]
        public void TestSendFYEOMessageUniquePassword()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            PreCreateConfiguration configuration = new PreCreateConfiguration();
            configuration.SetActionCode(ActionCodeEnum.New);

            Message message = messenger.PreCreateMessage(configuration);

            message.To = new List<String>()
            {
                RecipientEmail
            };
            message.Subject = "DeliverySlip C# Example";
            message.Body = "Hello Test Message From DeliverySlip C# Example";
            message.BodyFormat = BodyFormatEnum.Text;

            message.MessageOptions.FyeoType = FyeoTypeEnum.UniquePassword;
            message.Password = "password";

            message = messenger.SaveMessage(message);
            messenger.SendMessage(message);
        }

        [TestMethod]
        public void TestSendMessageWithCRA()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            PreCreateConfiguration configuration = new PreCreateConfiguration();
            configuration.SetActionCode(ActionCodeEnum.New);

            Message message = messenger.PreCreateMessage(configuration);

            message.To = new List<String>()
            {
                RecipientEmail
            };
            message.Subject = "DeliverySlip C# Example";
            message.Body = "Hello Test Message From DeliverySlip C# Example";
            message.BodyFormat = BodyFormatEnum.Text;

            message.CraCode = "cracode";
            message.InviteNewUsers = true;
            message.SendNotification = true;

            message = messenger.SaveMessage(message);
            messenger.SendMessage(message);
        }
    }
}
