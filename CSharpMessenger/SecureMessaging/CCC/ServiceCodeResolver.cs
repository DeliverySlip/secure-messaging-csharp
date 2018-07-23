using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Text;
using SecureMessaging.Auth;
using System.Net;

namespace SecureMessaging.CCC
{
	public class ServiceCodeResolver
	{

        private static String cccApiBaseUrl = Endpoints.CCCAPI;

		public static void SetResolveURL(String resolveURL)
		{
			ServiceCodeResolver.cccApiBaseUrl = resolveURL;
		}

        /// <summary>
        /// Resolve executes the resolving of the passed in serviceCode to matching messaging api. It uses the
        /// CCC to search for the passed in service code and returns the Secure Messaging API base url
        /// </summary>
        /// <param name="serviceCode">The servicecode of the service being resolved</param>
        /// <returns></returns>
		public static String Resolve(String serviceCode)
		{
			var client = new JsonServiceClient(ServiceCodeResolver.cccApiBaseUrl);
            var publicGetServiceResponse = client.Get<HttpWebResponse>($"/public/services/single?serviceCode={serviceCode}");

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
