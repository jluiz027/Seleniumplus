using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace SeleniumPlus.Exceptions
{
    class ElementNotFoundException : Exception
    {
        public sealed override string Message { get; }
        public ElementNotFoundException(string elementId)
        {
            Message = $"Element not found. (ElementId: {elementId})";
        }
    }
}
