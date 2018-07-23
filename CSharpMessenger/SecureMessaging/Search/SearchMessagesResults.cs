using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureMessaging.ServiceStack.Entities;
using SecureMessaging.ServiceStack.Services;
using ServiceStack;

namespace SecureMessaging.Search
{
    public class SearchMessagesResults : IReadOnlyCollection<MessageSummary>
    {
        private int count;
        private int currentPage;
        private int pageSize;

        private SearchMessagesFilter searchMessageFilter;
        private List<MessageSummary> localMessageStore = new List<MessageSummary>();
        private JsonServiceClient client;

        public int Count
        {
            get
            {
                return count;
            }
        }


        public SearchMessagesResults(SearchMessagesPagedResponse initialData, SearchMessagesFilter searchMessageFilter, JsonServiceClient client)
        {
            this.searchMessageFilter = searchMessageFilter;
            this.count = initialData.TotalItems;
            this.currentPage = initialData.CurrentPage;
            this.pageSize = initialData.PageSize;
            this.client = client;

            localMessageStore.AddRange(initialData.Results);
        }

        /// <summary>
        /// LoadNextPageOfDataIntoLocalList fetches the next page of data from the API and caches it localy
        /// so that it can be retrieved by the client
        /// </summary>
        private void LoadNextPageOfDataIntoLocalList()
        {
            this.currentPage++;

            var nextPageReq = new SearchMessages()
            {
                Filter = this.searchMessageFilter,
                Page = this.currentPage
            };

            SearchMessagesPagedResponse nextPageResponse = client.Get(nextPageReq);
            this.localMessageStore.AddRange(nextPageResponse.Results);
        }


        public IEnumerator<MessageSummary> GetEnumerator()
        {
            return new SearchMessageResultsIterator(this, 0);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// SearchMessageResultsIterator handles iterating through the search
        /// results returned by the Secure Messaging API
        /// </summary>
        private class SearchMessageResultsIterator : IEnumerator<MessageSummary>
        {
            private int index = 0;
            SearchMessagesResults container;

            public MessageSummary Current
            {
                get
                {
                    return container.localMessageStore.ElementAt(this.index);
                }
            }

            object IEnumerator.Current
            {
                get
                {
                    return container.localMessageStore.ElementAt(this.index);
                }
            }


            public SearchMessageResultsIterator(SearchMessagesResults container, int index)
            {
                this.index = index;
                this.container = container;
            }

            public void Dispose()
            {
                //https://stackoverflow.com/questions/3061612/ienumerator-is-it-normal-to-have-an-empty-dispose-method
            }

            public bool MoveNext()
            {
                index++;
                while(index >= container.localMessageStore.Count)
                {
                    container.LoadNextPageOfDataIntoLocalList();
                }

                return true;
            }

            public void Reset()
            {
                this.index = 0;
            }
        }
    }
}
