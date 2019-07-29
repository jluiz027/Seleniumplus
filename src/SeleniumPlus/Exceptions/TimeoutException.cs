using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumPlus.Exceptions
{
    public class TimeoutException : System.Exception
    {
        public override string Message => base.Message + " - A operação automatica não pode ser realizada porque o sistema não respondeu a tempo";
        //public override string Message { get { return base.Message + " - A operação automatica não pode ser realizada porque o sistema não respondeu a tempo"} }
    }
}
//teste
