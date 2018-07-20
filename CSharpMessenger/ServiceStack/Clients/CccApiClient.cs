using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Text;

namespace SecureMessaging.ServiceStack.Clients
{
    public class CccApiClient
    {
        private string baseCccApiUrl { get; set; }

        public CccApiClient(string baseCccApiUrl)
        {
            this.baseCccApiUrl = baseCccApiUrl;
        }

        /// <summary>
        /// Need this to resolve the Services MsgApi URL. 
        /// </summary>
        /// <param name="serviceCode"></param>
        /// <returns></returns>
        public string DiscoverServiceApiUrl(string serviceCode)
        {

            var client = new JsonServiceClient(this.baseCccApiUrl);
            var publicGetServiceResponse = client.Get($"/public/services/single?serviceCode={serviceCode}");

            var json = publicGetServiceResponse.ReadToEnd();//get the JSON

            var jsonObj = (Dictionary<string, string>)JsonObject.Parse(json);//parse the first level of JSON results into a dictionary
            string urlsJson;
            if (jsonObj.TryGetValue("urls", out urlsJson))
            {

                var urls = (Dictionary<string, string>)JsonObject.Parse(urlsJson);//parse the second level and get the MsgAPI Url we need.
                string url;
                if (urls.TryGetValue("SecMsgAPI", out url))
                {
                    return url;
                }

            }

            return null;
        }
    }
}
