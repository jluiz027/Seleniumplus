using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace SeleniumPlus.Util
{
    public class Web
    {
        public static void ForceClick(By by, IWebDriver driver)
        {

            IJavaScriptExecutor executor = (IJavaScriptExecutor)driver;
            executor.ExecuteScript("arguments[0].focus();", driver.FindElement(by));
            executor.ExecuteScript("arguments[0].click();", driver.FindElement(by));

        }
        
        public static String switchToWindowByTitle(IWebDriver driver, String windowTitle)
        {
            var handles = driver.WindowHandles;
            String currentHandle = driver.CurrentWindowHandle;
            foreach (String handle in handles)
            {
                driver.SwitchTo().Window(handle);
                if (windowTitle.Contains(driver.Title))
                {
                    break;
                }
            }

            return currentHandle;
        }

        public static String switchToWindowByUrl(IWebDriver driver, String windowUrl, bool fecharOutrasJanelas = false)
        {
            var handles = driver.WindowHandles;
            String currentHandle = driver.CurrentWindowHandle;
            String correctHandle = driver.CurrentWindowHandle;

            foreach (String handle in handles)
            {
                driver.SwitchTo().Window(handle);
                if (windowUrl.Contains(driver.Url))
                {
                    correctHandle = driver.CurrentWindowHandle;
                    //break;
                }
                else if(fecharOutrasJanelas)
                {
                    driver.Close();
                }
            }
            driver.SwitchTo().Window(correctHandle);

            return currentHandle;
        }
    }
}
