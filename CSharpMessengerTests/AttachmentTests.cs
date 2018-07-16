using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpMessenger.SecureMessaging;
using CSharpMessenger.SecureMessaging.Enums;
using System.IO;
using System.Collections.Generic;

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

            message = messenger.SaveMessage(message);

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(projectPath, "Resources");
            FileInfo file = new FileInfo(filePath + "/yellow.jpg");

            messenger.UploadAttachmentsForMessage(message, new List<FileInfo>() { file });

            message = messenger.SaveMessage(message);
            messenger.SendMessage(message);

        }

    }
}
