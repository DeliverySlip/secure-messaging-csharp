﻿using SecureMessaging.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging
{
	public class Session
	{

		//public String FirstName { get; set; }
		//public String LastName { get; set; }
		public String EmailAddress { get; set; }
		public String State { get; set; }
		public String SessionToken { get; set; }
        public MessagingApiClient Client { get; set; }
	}
}
