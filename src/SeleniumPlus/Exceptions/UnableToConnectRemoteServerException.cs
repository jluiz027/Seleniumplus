using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumPlus.Exceptions
{
    class UnableToConnectRemoteServerException : System.Exception
    {
        public override string Message {
            get { return "Unable connect to Selenium grid server. Check your network connections"; }
        }
    }
}
