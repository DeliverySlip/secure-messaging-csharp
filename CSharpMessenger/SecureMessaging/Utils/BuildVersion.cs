using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecureMessaging.Utils
{
    /// <summary>
    /// BuildVersion stores and contains SDK version information so that it can be retrieved from anywhere in the
    /// app. This is also so that only a single spot needs to be changed to update the build version
    /// </summary>
    class BuildVersion
    {

        public static String GetBuildVersion()
        {
            return "2.2.0";
        }
    }
}
