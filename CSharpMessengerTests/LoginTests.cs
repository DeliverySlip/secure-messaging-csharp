using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureMessaging;
using System.Diagnostics;
using SecureMessaging.CCC;
using ServiceStack;
using SecureMessaging.Auth;

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
        public void TestLoginMostDecoupled()
        {
            String messagingApiUrl = ServiceCodeResolver.Resolve(ServiceCode);
            MessagingApiClient client = new MessagingApiClient(messagingApiUrl);
            Credentials credentials = new Credentials(Username, Password);

            // this step is doing the logging in
            Session session = SessionFactory.CreateSession(credentials, client);

            // this is now to interact with your session
            SecureMessenger messenger = new SecureMessenger(session);

            //you are already logged in now at this point

        }

        [TestMethod]
        public void TestLoginMostDecoupled2()
        {
            MessagingApiClient client = MessagingApiClient.GetInstanceViaServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);

            // this step is doing the logging in
            Session session = SessionFactory.CreateSession(credentials, client);

            // this is now to interact with your session
            SecureMessenger messenger = new SecureMessenger(session);

            //you are already logged in now at this point

        }

        [TestMethod]
        public void TestLoginWithClient()
        {
            String messagingApiUrl = ServiceCodeResolver.Resolve(ServiceCode);

            MessagingApiClient client = new MessagingApiClient(messagingApiUrl);

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
