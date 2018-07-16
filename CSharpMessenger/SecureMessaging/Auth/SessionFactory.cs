using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMessenger.ServiceStack.Services;
using ServiceStack;

namespace CSharpMessenger.SecureMessaging.Auth
{
	public class SessionFactory
	{

		public static Session createSession(Credentials credentials, JsonServiceClient client)
		{
			if (credentials.GetCredentialsType() == Credentials.CredentialsTypeEnum.UsernamePassword)
			{

				var req = new Login()
				{
					Username = credentials.GetUsername(),
					Password = credentials.GetPassword(),
					Cookieless = true
				};

				var response = client.Post(req);

				client.Headers.Add("x-sm-session-token", response.SessionToken);

				var req2 = new GetUserSettings();
				var response2 = client.Get(req2);

				return new Session()
				{
					SessionToken = response.SessionToken,
					FirstName = response2.FirstName,
					LastName = response2.LastName,
					State = response2.State,
					EmailAddress = response2.EmailAddress
				};

			}
			else
			{

				var req = new CSharpMessenger.ServiceStack.Services.Authenticate()
				{
					AuthenticationToken = credentials.GetAuthenticationToken(),
					Cookieless = true
				};

				var response = client.Post(req);

				var req2 = new GetUserSettings();
				var response2 = client.Get(req2);

				return new Session()
				{
					SessionToken = response.SessionToken,
					FirstName = response2.FirstName,
					LastName = response2.LastName,
					State = response2.State,
					EmailAddress = response2.EmailAddress
				};

			}
		}
	}
}
