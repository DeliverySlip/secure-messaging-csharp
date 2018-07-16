using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSharpMessenger.SecureMessaging;
using CSharpMessenger.ServiceStack.Entities;
using CSharpMessenger.ServiceStack.Services;
using CSharpMessenger.SecureMessaging.Search;
using System.Collections.Generic;

namespace CSharpMessengerTests
{
    [TestClass]
    public class SearchMessageTests: BaseTestCase
    {

        [ClassInitialize]
        public static void BeforeClass(TestContext context)
        {
            BaseTestCase.BeforeClassLoader(context);
        }

        [TestMethod]
        public void TestSearchEverything()
        {

            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            SearchMessagesFilter filter = new SearchMessagesFilter();
            SearchMessagesResults results = messenger.SearchMessages(filter);
            
        }

        [TestMethod]
        public void TestSearchEverythingIterator()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            SearchMessagesFilter filter = new SearchMessagesFilter();
            SearchMessagesResults results = messenger.SearchMessages(filter);

            IEnumerator<MessageSummary> enumerator = results.GetEnumerator();

        }

        [TestMethod]
        public void TestSearchEverythingIteratorFirst25()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            SearchMessagesFilter filter = new SearchMessagesFilter();
            SearchMessagesResults results = messenger.SearchMessages(filter);

            int index = 0;
            foreach(var searchResult in results)
            {
                index++;
                if(index > 25)
                {
                    break;
                }
            }
        }

        [TestMethod]
        public void TestSearchEverythingIteratorFirst25FetchBody()
        {
            SecureMessenger messenger = SecureMessenger.ResolveFromServiceCode(ServiceCode);
            Credentials credentials = new Credentials(Username, Password);
            messenger.Login(credentials);

            SearchMessagesFilter filter = new SearchMessagesFilter();
            SearchMessagesResults results = messenger.SearchMessages(filter);

            int index = 0;
            foreach (var searchResult in results)
            {
                index++;
                if (index > 25)
                {
                    break;
                }else
                {
                    // Search results do not contain the message body so you have to explicity call it to fetch it
                    String messageBody = messenger.GetMessage(searchResult.Guid).Body;
                }
            }
        }
    }
}
