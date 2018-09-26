using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureMessaging;
using SecureMessaging.Enums;
using System.IO;
using System.Collections.Generic;
using SecureMessaging.Attachment;
using SecureMessaging.CCC;
using SecureMessaging.Auth;

namespace CSharpMessengerTests
{
    [TestClass]
    public class AttachmentTests: BaseTestCase
    {

        [ClassInitialize]
        public static void BeforeClass(TestContext context)
        {
            BaseTestCase.BeforeClassLoader(context);
        }

        [TestMethod]
        public void TestSendMultipleAttachmentsMessengerWorkflow()
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

            SavedMessage savedMessage = messenger.SaveMessage(message);

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources");
            FileInfo file = new FileInfo(filePath + "/yellow.jpg");

            messenger.UploadAttachmentsForMessage(savedMessage, new List<FileInfo>() { file, file, file });

            savedMessage = messenger.SaveMessage(savedMessage);
            messenger.SendMessage(savedMessage);

        }

        [TestMethod]
        public void TestSendAttachmentMessengerWorkflow()
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

            SavedMessage savedMessage = messenger.SaveMessage(message);

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources");
            FileInfo file = new FileInfo(filePath + "/yellow.jpg");

            messenger.UploadAttachmentsForMessage(savedMessage, new List<FileInfo>() { file });

            savedMessage = messenger.SaveMessage(savedMessage);
            messenger.SendMessage(savedMessage);

        }

        [TestMethod]
        public void TestSendAttachmentManagerWorkflow()
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

            SavedMessage savedMessage = messenger.SaveMessage(message);

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources");
            FileInfo file = new FileInfo(filePath + "/yellow.jpg");

            AttachmentManager attachmentManager = messenger.CreateAttachmentManagerForMessage(savedMessage);

            attachmentManager.AddAttachmentFile(file);
            attachmentManager.PreCreateAllAttachments();
            attachmentManager.UploadAllAttachments();

            savedMessage = messenger.SaveMessage(savedMessage);
            messenger.SendMessage(savedMessage);

        }

        [TestMethod]
        public void TestSendAttachmentMostDecoupled()
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

            SavedMessage savedMessage = messenger.SaveMessage(message);

            AttachmentManager attachmentManager = new AttachmentManager(savedMessage, session);

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources");
            FileInfo file = new FileInfo(filePath + "/yellow.jpg");

            attachmentManager.AddAttachmentFile(file);
            attachmentManager.PreCreateAllAttachments();
            attachmentManager.UploadAllAttachments();

            savedMessage = messenger.SaveMessage(savedMessage);
            messenger.SendMessage(savedMessage);

        }

    }
}
