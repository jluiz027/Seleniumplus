using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumPlus.Premise
{
    public class PremissasBase : IPremissaWeb
    {
        private List<IPremissaWeb> premissas;

        public PremissasBase()
        {
            premissas = new List<IPremissaWeb>();
        }

        public void Add(IPremissaWeb premissa)
        {
            premissas.Add(premissa);
        }
        public IPremissaWeb Verificar()
        {
            premissas.ForEach(p => p.Verificar());
            return this;
        }
    }
}
