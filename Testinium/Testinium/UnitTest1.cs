
using Docker.DotNet.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Testinium
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            #region Beymen Web Panel Open
            var chromeOptions = new ChromeOptions();
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
            IWebDriver webDriver = new ChromeDriver(chromeOptions);
            webDriver.Navigate().GoToUrl("https://www.beymen.com/");

            Thread.Sleep(1000);
            #endregion
            #region Gender and Cookies Accepted
            webDriver.FindElement(By.Id("onetrust-accept-btn-handler")).Click();
            webDriver.FindElement(By.Id("genderManButton")).Click();
            #endregion
            #region Entering Product
            webDriver.FindElement(By.CssSelector("body > header > div > div > div.col-4.col-sm-4.col-md-4.col-lg-4.col-xl-6 > div > div > input")).SendKeys("Þort");
            Thread.Sleep(500);
            for (int i = 0; i < 4; i++)
            {
                webDriver.FindElement(By.Id("o-searchSuggestion__input")).SendKeys(Keys.Backspace);
            }
            Thread.Sleep(250);
            webDriver.FindElement(By.Id("o-searchSuggestion__input")).SendKeys("Gömlek");
            webDriver.FindElement(By.Id("o-searchSuggestion__input")).SendKeys(Keys.Enter);
            #endregion
            #region Product Selection
            chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
            webDriver.FindElement(By.XPath("//*[@id=\"productList\"]/div[2]/div/div/div[1]/a/div[2]/div[2]")).Click();
            Thread.Sleep(250);
            #endregion
            #region Product Information and Adding to Cart
            
            string productInfo = webDriver.FindElement(By.XPath("/html/body/div[6]/div[2]/div/div[2]/div/div/div[1]/div/h3/a[2]/span")).Text;
            IWebElement amount = webDriver.FindElement(By.XPath("/html/body/div[6]/div[2]/div/div[2]/div/div/div[1]/div/div/span/span"));
            
            Assert.True(amount.Displayed);
            string productAmount = amount.Text;
            
            webDriver.FindElement(By.XPath("//*[@id=\"sizes\"]/div/span[2]")).Click();
            webDriver.FindElement(By.Id("addBasket")).Click();
            Thread.Sleep(1000);
            webDriver.FindElement(By.CssSelector("body > header > div > div > div.col.col-xl-3.d-flex.justify-content-end > div > a.o-header__userInfo--item.bwi-cart-o.-cart > span")).Click();
            #endregion
            #region Select Product Quantity
            Thread.Sleep(3000);
            webDriver.FindElement(By.Id("quantitySelect0-key-0")).Click();
            webDriver.FindElement(By.CssSelector("#quantitySelect0-key-0 > option:nth-child(2)")).Click();
            Thread.Sleep(3000);
          
            IWebElement totalAmount = webDriver.FindElement(By.CssSelector("body > div.wrapper > div.container.m-basket > div > div > div.col-12.col-md-12.col-lg-4 > div.m-orderSummary > div.m-orderSummary__body > ul > li:nth-child(1) > span.m-orderSummary__value"));
            Assert.True(totalAmount.Displayed);
            string totalProductAmount = totalAmount.Text+(",00 TL");
           
            Assert.Equal(productAmount, totalProductAmount);
            #endregion
        }
    }
}