using DatacomTests.Pages;
using OpenQA.Selenium;
using System;

namespace DatacomTests
{
    public class HomePage : BaseClass
    {
        private static WebDriver _driver;
        private By _overallLabel = By.XPath("//span[@class='js-overview total']/span");
        private By _menu = By.XPath("//div[@class='topbar-actions messages']/button");
        private By _menuSection = By.XPath("//section[@class='MainMenu']");
        private By _payeesMenuButton = By.XPath("//*[contains(text(),'Payees')]");
        private By _paymentsMenuButton = By.XPath("//*[contains(text(),'Pay or transfer')]");
        private By _notification = By.XPath("//*[contains(text(),'Transfer successful')]/ancestor::div[@class='inner js-notification show js-notificationShown']");
        public HomePage()
        {
            _driver = BaseHook.GetChromeDriver();
        }

        public void NavigateToPage()
        {
            _driver.Navigate().GoToUrl("https://www.demo.bnz.co.nz/client");
            WaitForElementToAppear(_overallLabel);
            WaitForElementToAppear(_menu);
        }

        public void WaitForHomePageToLoad()
        {
            WaitForElementToAppear(_overallLabel);
            WaitForElementToBeClickable(_overallLabel);
            WaitForElementToAppear(_menu);
            WaitForElementToBeClickable(_menu);
            base.WaitForPageToLoad();
        }

        public void ClickMenu()
        {
            _driver.FindElement(_menu).Click();
            WaitForElementToAppear(_menuSection);
            WaitForElementToAppear(_paymentsMenuButton);
        }

        public void ClickPayees()
        {
            _driver.FindElement(_payeesMenuButton).Click();
        }

        public void ClickPayments()
        {
            _driver.FindElement(_paymentsMenuButton).Click();
        }

        public double GetAmount(string account)
        {
            WaitForElementToAppear(By.XPath("//*[@data-testid='account-details-wrapper']/descendant::h3[contains(text(),'" + account + "')]/ancestor::div[@class='account-info']/descendant::span[@class='account-balance']"));
            WaitForElementToBeClickable(By.XPath("//*[@data-testid='account-details-wrapper']/descendant::h3[contains(text(),'" + account + "')]/ancestor::div[@class='account-info']/descendant::span[@class='account-balance']"));
            string amount = _driver.FindElement(By.XPath("//*[@data-testid='account-details-wrapper']/descendant::h3[contains(text(),'" + account + "')]/ancestor::div[@class='account-info']/descendant::span[@class='account-balance']")).Text;

            amount = amount.Replace(",", "");

            return Convert.ToDouble(amount);
        }

        public void WaitForTransferSuccessfulNotificationToAppear()
        {
            WaitForElementToAppear(_notification);
        }
    }
}
