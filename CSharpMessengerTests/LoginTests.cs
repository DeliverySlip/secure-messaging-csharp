using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpMessenger.SecureMessaging;
using System.Diagnostics;
using CSharpMessenger.SecureMessaging.CCC;
using ServiceStack;

namespace CSharpMessengerTests
{
    [TestClass]
    public class LoginTests: BaseTestCase
    {
        [ClassInitialize]
        public static void BeforeClass(TestContext context)
        {
            BaseTestCase.BeforeClassLoader(context);
        }

        [TestMethod]
        public void TestBasicLogin()
        {

            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);            
        }

        [TestMethod]
        public void TestLoginWithURL()
        {

            String messagingApiUrl = ServiceCodeResolver.Resolve(ServiceCode);
            SecureMessenger messenger = new SecureMessenger(messagingApiUrl);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);
        }

        [TestMethod]
        public void TestLoginWithClient()
        {
            String messagingApiUrl = ServiceCodeResolver.Resolve(ServiceCode);

            JsonServiceClient client = new JsonServiceClient(messagingApiUrl);

            SecureMessenger messenger = new SecureMessenger(client);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);
        }

        [TestMethod]
        [Ignore]
        public void TestLoginWithAutenticationToken()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            //TODO: Implement this functionality!

           
        }


    }
}
