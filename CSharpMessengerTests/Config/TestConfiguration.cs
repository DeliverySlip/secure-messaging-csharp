using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Diagnostics;

namespace CSharpMessengerTests.Config
{
    public class TestConfiguration
    {
        public static String ServiceCode = "";
        public static String Username = "";
        public static String Password = "";
        public static String RecipientEmail = "";

        public static String ResolveUrl = null;

        public static void LoadConfiguration()
        {
            TestConfiguration.Username = ConfigurationManager.AppSettings["Username"];
            TestConfiguration.Password = ConfigurationManager.AppSettings["Password"];
            TestConfiguration.ServiceCode = ConfigurationManager.AppSettings["ServiceCode"];
            TestConfiguration.RecipientEmail = ConfigurationManager.AppSettings["RecipientEmail"];

            TestConfiguration.ResolveUrl = ConfigurationManager.AppSettings["ResolveUrl"]; //configuration manager returns null if not found
        }
    }
}
