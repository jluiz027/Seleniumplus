using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumPlus.Premise
{
    public class PremissaNula : IPremissaWeb
    {
        public IPremissaWeb Verificar()
        {
            return this;
        }
    }
}
