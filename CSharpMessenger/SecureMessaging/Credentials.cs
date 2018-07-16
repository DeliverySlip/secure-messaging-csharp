using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpMessenger.SecureMessaging
{
	public class Credentials
	{
		public enum CredentialsTypeEnum
		{
			AuthenticationToken,
			UsernamePassword
		}

		private String Username;
		private String Password;
		private String AuthenticationToken;

		public Credentials(String username, String password)
		{
			this.Username = username;
			this.Password = password;
		}

		public Credentials(String authenticationToken)
		{
			this.AuthenticationToken = authenticationToken;
		}

		public CredentialsTypeEnum GetCredentialsType()
		{
			if (Username == null && Password == null && AuthenticationToken != null)
			{
				return CredentialsTypeEnum.AuthenticationToken;
			}

			return CredentialsTypeEnum.UsernamePassword;
			
		}

		public String GetUsername()
		{
			return Username;
		}

		public String GetPassword()
		{
			return Password;
		}

		public String GetAuthenticationToken()
		{
			return AuthenticationToken;
		}
	}
}
