using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SeleniumPlus.Util;

namespace Test
{
    public class AoExcecutarComandoSelenium : IClassFixture<SeleniumPlusFixture>
    {

        private IWebDriver _webDriver;

        public AoExcecutarComandoSelenium(SeleniumPlusFixture fixture)
        {
            _webDriver = fixture.seleniumWebDriver;
        }

        //[Fact]
        public void AbreONavegadorEAcessaPagina()
        {
            try
            {
                _webDriver.TryNavigateToUrl(5, 1, "http://www.google.com", "www.google.com");
                Assert.Contains("google.com", _webDriver.Url);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _webDriver.Quit();
            }
        }

        [Fact]
        public void AbreONavegadorAcessaPaginaEExecutaBusca()
        {
            var byCampoBusca = By.XPath("/html/body[@id='gsr']/div[@id='viewport']/div[@id='searchform']/form[@id='tsf']/div[2]/div[@class='A8SBwf sbfc']/div[@class='RNNXgb']/div[@class='SDkEP']/div[@class='a4bIc']/input[@class='gLFyf gsfi']");
            try
            {
                _webDriver.TryNavigateToUrl(5, 1, "http://www.google.com", "www.google.com");
                _webDriver.TrySendKeys(5, 1, byCampoBusca, "carro");
                Assert.Contains("google.com", _webDriver.Url);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _webDriver.Quit();
            }
        }
    }
}
