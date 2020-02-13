using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test
{
    public class SeleniumPlusFixture
    {
        public IWebDriver seleniumWebDriver;
        public SeleniumPlusFixture()
        {
            seleniumWebDriver = SeleniumPlus.DriverFactory.GetLocalChromeDriver();
        }
    }
}
