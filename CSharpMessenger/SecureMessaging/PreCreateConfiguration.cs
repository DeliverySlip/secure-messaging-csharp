using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpMessenger.SecureMessaging.Enums;

namespace CSharpMessenger.SecureMessaging
{
    public class PreCreateConfiguration
    {
        private String actionCode;
        private String parentGuid;
        private String password;
        private String campaignGuid;

        public ActionCodeEnum GetActionCode()
        {

            return (ActionCodeEnum)Enum.Parse(typeof(ActionCodeEnum), this.actionCode);
        }

        public void SetActionCode(ActionCodeEnum actionCode)
        {
            this.actionCode = actionCode.ToString();
        }

        public void SetParentGuid(Guid? parentGuid)
        {
            if (parentGuid.HasValue)
            {
                this.parentGuid = parentGuid.ToString();
            }
            else
            {
                this.parentGuid = null;
            }
            
        }

        public Guid? GetParentGuid()
        {
            Guid result;
            if(Guid.TryParse(this.parentGuid, out result))
            {
                return result;
            }
            else
            {
                return null;
            }            
        }

        public void SetPassword(String password)
        {
            this.password = password;
        }

        public String GetPassword()
        {
            return this.password;
        }

        public void SetCampaignGuid(Guid? campaignGuid)
        {
            if (campaignGuid.HasValue)
            {
                this.campaignGuid = campaignGuid.ToString();
            }
            else
            {
                this.campaignGuid = null;
            }

            
        }

        public Guid? GetCampaignGuid()
        {
            Guid result;
            if(Guid.TryParse(this.campaignGuid, out result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }


    }
}
