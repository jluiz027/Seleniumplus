using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumPlus.Premise;
using SeleniumPlus.Exceptions;

namespace SeleniumPlus.Util
{
    public static class Extension
    {
        /// <summary>
        /// Tenta clicar no elemento web enquanto as condições (timeout, howManyTimes) forem satisfeitas
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="howManyTimes">Quantidade limite de tentativas (-1 para infinitas)</param>
        /// <param name="searchMecanism">Mecanismo de busca para buscar elemento (IWebElement) vide classe OpenQA.Selenium.By</param>
        /// <param name="timeBetweenTriesSeconds">Intervalo de tempo, em segundos, entre tentativas consecutivas</param>
        /// <param name="premissaWeb">Premissas para serem verificadas antes de realizar as tentativas</param>
        public static bool TryClick(this IWebDriver driver, int howManyTimes, int timeBetweenTriesSeconds, By searchMecanism, bool throwExceptions = true, IPremissaWeb premissaWeb = null)
        {
            try
            {
                var element = TryGetElement(driver, howManyTimes, timeBetweenTriesSeconds, searchMecanism, throwExceptions, premissaWeb);
                element.Click();
            }
            catch (TryEnoughTimesToGetException)
            {
                if (throwExceptions)
                {
                    throw;
                }

                return false;
            }
            catch (Exception e)
            {
                if (throwExceptions)
                {
                    throw;
                }

                return false;
            }
            return true;
        }

        /// <summary>
        /// Tenta recuperar o elemento web enquanto as condições (timeout, howManyTimes) forem satisfeitas
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="howManyTimes">Quantidade limite de tentativas (-1 para infinitas)</param>
        /// <param name="searchMechanism">Mecanismo de busca para buscar elemento (IWebElement) vide classe OpenQA.Selenium.By</param>
        /// <param name="timeBetweenTriesSeconds">Intervalo de tempo, em segundos, entre tentativas consecutivas</param>
        /// Set this value to false if you don't want to thrown known exceptions from this framework. In this case null value will be returned if the element was not found.</param>
        /// <param name="premissaWeb">Premissas para serem verificadas antes de realizar as tentativas</param>
        public static IWebElement TryGetElement(this IWebDriver driver, int howManyTimes, int timeBetweenTriesSeconds, By searchMechanism, bool throwExceptions = true, IPremissaWeb premissaWeb = null)
        {
            premissaWeb?.Verificar();

            var countTentativas = 0;
            var horarioInicio = DateTime.Now;
            while (true)
            {
                try
                {
                    CheckTimeOutAndTry(howManyTimes, horarioInicio, ref countTentativas);

                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeBetweenTriesSeconds));
                    //var myDynamicElement = wait.Until(d => d.FindElement(searchMechanism));
                    var myDynamicElement = driver.FindElement(searchMechanism);
                    return myDynamicElement;
                }
                catch (TryEnoughTimesToGetException)
                {
                    if (throwExceptions)
                    {
                        throw;
                    }

                    return null;
                }
                catch (Exception e)
                {
                    Thread.Sleep(timeBetweenTriesSeconds * 1000);
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Tenta recuperar o elemento web enquanto as condições (timeout, howManyTimes) forem satisfeitas
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="howManyTimes">Quantidade limite de tentativas (-1 para infinitas)</param>
        /// <param name="searchMechanism">Mecanismo de busca para buscar elemento (IWebElement) vide classe OpenQA.Selenium.By</param>
        /// <param name="throwExceptions">Indica se exceções conhecidas neste namespace devem ser disparadas. Se false retornará nulo caso não consiga encontrar o elemento. -</param>
        /// <param name="timeBetweenTriesSeconds">Intervalo de tempo, em segundos, entre tentativas consecutivas</param>
        /// <param name="premissaWeb">Premissas para serem verificadas antes de realizar as tentativas</param>
        public static ICollection<IWebElement> TryGetElements(this IWebDriver driver, int howManyTimes, int timeBetweenTriesSeconds, By searchMechanism, bool throwExceptions = true, IPremissaWeb premissaWeb = null)
        {
            premissaWeb?.Verificar();

            var countTentativas = 0;
            var horarioInicio = DateTime.Now;
            while (true)
            {
                try
                {
                    CheckTimeOutAndTry(howManyTimes, horarioInicio, ref countTentativas);

                    var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeBetweenTriesSeconds));
                    var myDynamicElement = wait.Until(d => d.FindElements(searchMechanism));
                    return myDynamicElement;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Tenta enviar caracteres para o elemento web enquanto as condições (timeout, howManyTimes) forem satisfeitas
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="howManyTimes">Quantidade limite de tentativas (-1 para infinitas)</param>
        /// <param name="searchMecanism">Mecanismo de busca para buscar elemento (IWebElement) vide classe OpenQA.Selenium.By</param>
        /// <param name="timeBetweenTriesSeconds">Intervalo de tempo, em segundos, entre tentativas consecutivas</param>
        /// <param name="premissaWeb">Premissas para serem verificadas antes de realizar as tentativas</param>
        public static bool TrySendKeys(this IWebDriver driver, int howManyTimes, int timeBetweenTriesSeconds, By searchMecanism, string text, bool throwExceptions = true, IPremissaWeb premissaWeb = null)
        {

            try
            {
                var element = TryGetElement(driver, howManyTimes, timeBetweenTriesSeconds, searchMecanism, throwExceptions, premissaWeb);
                element.Click();
                element.Clear();
                element.SendKeys(text);
                if (element.GetAttribute("value") == null || element.GetAttribute("value") != text)
                {
                    Thread.Sleep(timeBetweenTriesSeconds * 1000);
                    //throw new UnableToSendTextToElementException($"Campo '{element.GetAttribute("id")}' não estava disponivel para digitação do text '{text}' ");
                    throw new UnableToSendTextToElementException($"Unable to set value '{text}' to HTML element '{element.GetAttribute("id")}'");
                }
                return true;
            }
            catch (TryEnoughTimesToGetException)
            {
                if (throwExceptions)
                {
                    throw;
                }

                return false;
            }
            catch (Exception e)
            {
                if (throwExceptions)
                {
                    throw;
                }

                return false;
            }
            return true;
        }

        /// <summary>
        /// Tenta algumas vezes pegar o valor do elemento HTML
        /// </summary>
        /// <param name="driver">IWebDriver</param>
        /// <param name="howManyTimes">Quantidade limite de tentativas (-1 para infinitas)</param>
        /// <param name="searchMecanism">Mecanismo de busca para buscar elemento (IWebElement) vide classe OpenQA.Selenium.By</param>
        /// <param name="timeBetweenTriesSeconds">Intervalo de tempo, em segundos, entre tentativas consecutivas</param>
        /// <param name="premissaWeb">Premissas para serem verificadas antes de realizar as tentativas</param>
        /// <returns>text do campo</returns>
        public static string TryGetTextFromElement(this IWebDriver driver, int howManyTimes, int timeBetweenTriesSeconds, By searchMecanism, bool throwExceptions = true, IPremissaWeb premissaWeb = null)
        {

            try
            {
                var element = TryGetElement(driver, howManyTimes, timeBetweenTriesSeconds, searchMecanism, throwExceptions, premissaWeb);
                var text = element.Text == "" ? element.GetAttribute("value") : element.Text;
                return text;
            }
            catch (TryEnoughTimesToGetException)
            {
                if (throwExceptions)
                {
                    throw;
                }

                return null;
            }
            catch (Exception e)
            {
                if (throwExceptions)
                {
                    throw;
                }

                return null;
            }
        }

        /// <summary>
        /// Tenta pegar mensagem de popup (se houver)
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pressionaOk"></param>
        /// <returns></returns>
        public static string TryGetAlertText(this IWebDriver driver, bool pressionaOk)
        {
            try
            {
                //Verifica se a pagina já foi carregada ou já tem um alert na tela
                var timeout = new TimeoutTimer(120);
                //Se o timeout esgotar, o laço não repetira mais e uma exceção do tipo TimeoutException será lançada
                while (!timeout.esgotado())
                {
                    try
                    {
                        if (((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"))
                        {
                            break;
                        }
                        Thread.Sleep(500);
                    }
                    catch (UnhandledAlertException ex)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(500);
                    }
                }

                var alert = driver.SwitchTo().Alert();

                var alertText = alert.Text;

                if (pressionaOk)
                    alert.Accept();

                return alertText;

            }
            catch (Exceptions.TimeoutException) { throw; }
            catch (NoAlertPresentException) { return null; }
            catch (Exception) { return null; }
        }

        /// <summary>
        /// Vai para a próxima aba
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="currentWindowHandler">Aba atual</param>
        public static void SwithToNextWindow(this IWebDriver driver, string currentWindowHandler)
        {
            var nextWindow = currentWindowHandler;
            var next = false;
            var esperar = 0;

            if (driver.WindowHandles.Count < 2)
            {
                while (esperar < 5)
                {
                    esperar++;
                    Thread.Sleep(500);
                }
            }

            foreach (var windowHandle in driver.WindowHandles)
            {
                if (next)
                {
                    nextWindow = windowHandle;
                    break;
                }

                if (windowHandle == currentWindowHandler)
                    next = true;
            }

            driver.SwitchTo().Window(nextWindow);
        }

        /// <summary>
        /// Aguarda o carregamento da pagina e verifica se o elemento existe
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="elementLocator">elemento que será carregado </param>
        /// <param name="timeoutSegundos">tempo que fica aguardando o elemento carregar</param>
        /// <returns></returns>
        public static IWebElement WaitUntilElementExists(this IWebDriver driver, By elementLocator, int timeoutSegundos = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSegundos));
                return wait.Until(ExpectedConditions.ElementExists(elementLocator));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Aguarda o carregamento da pagina e verifica se o elemento esta visivel
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="elementLocator">elemento que será carregado </param>
        /// <param name="timeoutSegundos">tempo que fica aguardando o elemento carregar</param>
        /// <returns></returns>
        public static bool WaitUntilElementIsVisible(this IWebDriver driver, By elementLocator, int timeoutSegundos = 10)
        {
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSegundos));
                return wait.Until(ExpectedConditions.ElementIsVisible(elementLocator)) != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void CheckTimeOutAndTry(int timeoutSegundos, int quantasVezes, DateTime horarioInicio, ref int countTentativas)
        {
            var intervaloTotalSegundos = (DateTime.Now - horarioInicio).Seconds;
            countTentativas++;

            if (intervaloTotalSegundos > timeoutSegundos && timeoutSegundos != -1)
            {
                throw new TimeoutToClickException();
            }

            if (countTentativas > quantasVezes && quantasVezes != -1)
            {
                throw new TryEnoughTimesToGetException();
            }
        }

        private static void CheckTimeOutAndTry(int quantasVezes, DateTime horarioInicio, ref int countTentativas)
        {
            var intervaloTotalSegundos = (DateTime.Now - horarioInicio).Seconds;
            countTentativas++;

            if (countTentativas > quantasVezes && quantasVezes != -1)
            {
                throw new TryEnoughTimesToGetException();
            }
        }

        public static void TakeScreenshot(this IWebDriver driver, string screeshootFilePath)
        {
            try
            {
                Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
                //var image = ss.AsBase64EncodedString;
                ss.SaveAsFile(screeshootFilePath, OpenQA.Selenium.ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public static void scrollDown(this IWebDriver driver, int pixelsQuantity)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy(0,{pixelsQuantity})");
        }

        public static void wait(this IWebDriver driver, int timeSeconds)
        {
            Thread.Sleep(timeSeconds * 1000);
        }

        public static void TryNavigateToUrl(this IWebDriver driver, int howManyTimes, int timeBetweenTriesSeconds, string url)
        {
            for (int i = 0; i < howManyTimes; i++)
            {
                driver.Navigate().GoToUrl(url);
                wait(driver,timeBetweenTriesSeconds);
                if (driver.Url.Contains(url))
                {
                    return;
                }
            }
            throw new TryEnoughTimesToGetException();
        }
    }
}

