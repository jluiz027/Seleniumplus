using System;
using OpenQA.Selenium;

namespace SeleniumPlus.Exceptions
{
    public class TryEnoughTimesToGetException : Exception
    {
        public sealed override string Message { get; }

        public TryEnoughTimesToGetException()
        {
            Message = "quantidade de tentativas de click excedido";
        }
    }

    public class TimeoutToClickException : Exception
    {
        public sealed override string Message { get; }

        public TimeoutToClickException()
        {
            Message = "Timeout de tentativas de click excedido";
        }
    }

    public class ElementNotImputValueFoundException : Exception
    {
        public sealed override string Message { get; }

        public ElementNotImputValueFoundException(IWebElement myDynamicElement, string value)
        {
            Message = $"Campo '{myDynamicElement.GetAttribute("id")}' não estava disponivel para digitação do texto '{value}' ";
        }
    }

    public class AutomacaoException : Exception 
    {
        public sealed override string Message { get; }

        public AutomacaoException(string message)
        {
            Message = message;
        }
    }
}
