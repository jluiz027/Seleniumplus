using System;

namespace SeleniumPlus.Util
{
    public class TimeoutTimer
    {
        private int timeoutSegundos;
        private DateTime instanteInicial;
        private DateTime instanteAtual;

        public TimeoutTimer(int timeoutSegundos)
        {
            this.timeoutSegundos = timeoutSegundos * 1000;
            instanteInicial = DateTime.Now;
        }

        public bool esgotado()
        {
            instanteAtual = DateTime.Now;
            int intervalo = (instanteAtual - instanteInicial).Milliseconds;

            if (intervalo > this.timeoutSegundos)
                throw new TimeoutException();
            else
                return false;
        }
    }
}
//teste