using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SecureMessaging.CCC;

namespace CSharpMessengerTests
{

    public class BaseTestCase
    {

        protected static String ServiceCode;
        protected static String Username;
        protected static String Password;
        protected static String RecipientEmail;

        
        public static void BeforeClassLoader(TestContext context)
        {
            Config.TestConfiguration.LoadConfiguration();
            ServiceCode = Config.TestConfiguration.ServiceCode;
            Username = Config.TestConfiguration.Username;
            Password = Config.TestConfiguration.Password;
            RecipientEmail = Config.TestConfiguration.RecipientEmail;

            if (Config.TestConfiguration.ResolveUrl != null)
            {
                ServiceCodeResolver.SetResolveURL(Config.TestConfiguration.ResolveUrl);
            }

        }


    }
}
