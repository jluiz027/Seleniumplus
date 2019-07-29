using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumPlus.Exceptions
{
    class UnableToSendTextToElementException : Exception
    {
        public UnableToSendTextToElementException(string message): base(message)
        {
            
        }
    }
}
