using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace DatacomTests.Pages
{
    public class BaseClass
    {
        private static WebDriver _driver;
        private WebDriverWait _wait;

        public BaseClass()
        {
            _driver = BaseHook.GetChromeDriver();
            _wait  = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
        }

        public void WaitForPageToLoad()
        {
            Thread.Sleep(3000);
        }

        public void WaitForElementToAppear(By elementToWaitFor)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(elementToWaitFor));
        }

        public void WaitForElementToBeClickable(By elementToWaitFor)
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(elementToWaitFor));
        }

        public void WaitForElementToDisappear(By elementToWaitFor)
        {
            _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(elementToWaitFor));
        }
    }
}
