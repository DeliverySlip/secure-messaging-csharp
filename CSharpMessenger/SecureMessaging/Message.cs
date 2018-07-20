using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecureMessaging.Enums;
using SecureMessaging.ServiceStack.Entities;
using SecureMessaging.ServiceStack.Services;

namespace SecureMessaging
{
	public class Message
	{

		public Guid MessageGuid { get; set; }
		public String Password { get; set; }
		public bool InviteNewUsers { get; set; } = true;
		public bool SendNotification { get; set; } = true;

		public List<String> To = new List<string>();
		public List<String> From = new List<string>();
		public List<String> Cc = new List<string>();
		public List<String> Bcc = new List<string>();

		public String Subject { get; set; }
		public String Body { get; set; }
		public BodyFormatEnum BodyFormat { get; set; }

		public Options MessageOptions = new Options();


		public class Options
		{
			public bool AllowForward { get; set; } = true;
			public bool AllowReply { get; set; } = true;
			public bool AllowTracking { get; set; } = true;
			public bool ShareTracking { get; set; } = true;
			public FyeoTypeEnum FyeoType { get; set; } = FyeoTypeEnum.Disabled;


		}

		public DateTime? ExpiryDate { get; set; }
		public String ExpiryGroup { get; set; }

		public String CraCode { get; set; }

		public List<AttachmentPlaceholder> UploadedAttachments { get; internal set; }

		public List<Extension> Extensions { get; internal set; }

		public bool AttachmentsAdded { get; internal set; } = false;
		public bool MessageSent { get; internal set; } = false;
		public bool MessageSaved { get; internal set; } = true;
	}
}
