using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using SeleniumPlus.Exceptions;
using System;

namespace SeleniumPlus
{
    public class DriverFactory
    {
        /// <summary>
        /// Returns a local selenium Chromedriver (IWebDriver) by passing its current file location path (*.exe)
        /// </summary>
        /// <param name="webDriverFilePath">Current Chromedriver file location path</param>
        /// <returns>Selenium Chromedriver (IWebDriver)</returns>
        public static IWebDriver GetLocalChromeDriver(string webDriverFilePath)
        {
            IWebDriver webDriver = new ChromeDriver(webDriverFilePath);
            return webDriver;
        }

        public static IWebDriver GetLocalFirefoxDriver(string webDriverFilePath)
        {
            return new FirefoxDriver(webDriverFilePath);
        }

        public static IWebDriver GetRemoteChromeDriver(string seleniumGridServerUri)
        {
            var uri = new Uri(seleniumGridServerUri);
            ChromeOptions cap = new ChromeOptions();
            //cap.AddArguments("--test-type", "--start-maximized");
            cap.AddArguments("--start-maximized");
            try
            {
                return new RemoteWebDriver(uri, cap.ToCapabilities(), new TimeSpan(0, 5, 0));
            }
            catch (Exception e)
            {
                throw new UnableToConnectRemoteServerException();
            }
            
        }
        
        private IWebDriver GetRemoteFirefoxDriver(string seleniumGridServerUri)
        {
            var uri = new Uri(seleniumGridServerUri);
            FirefoxOptions cap = new FirefoxOptions();
            cap.AddArguments("--test-type", "--start-maximized");
            try
            {
                return new RemoteWebDriver(uri, cap.ToCapabilities(), new TimeSpan(0, 5, 0));
            }
            catch (Exception e)
            {
                throw new UnableToConnectRemoteServerException();
            }
        }

        

    }
}
