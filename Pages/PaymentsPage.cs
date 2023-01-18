using OpenQA.Selenium;

namespace DatacomTests.Pages
{
    public class PaymentsPage : BaseClass
    {
        private static WebDriver _driver;
        private By _paymentModal = By.XPath("//*[@data-testid='PaymentModal']");
        private By _fromAccount = By.XPath("//*[text()='Choose account']");
        private By _toAccount = By.XPath("//*[text()='Choose account, payee, or someone new']");
        private By _transferAmountField = By.XPath("//*[@data-monitoring-label='Transfer Form Amount']");
        private By _transferButton = By.XPath("//*[@data-monitoring-label='Transfer Form Submit']");
        private By _search = By.XPath("//*[@data-monitoring-label='Transfer Form Search']");

        public PaymentsPage()
        {
            _driver = BaseHook.GetChromeDriver();
        }

        public void ChooseFromAccount(string account)
        {
            WaitForElementToAppear(_paymentModal);
            _driver.FindElement(_fromAccount).Click();
            WaitForElementToAppear(By.XPath("//*[@data-testid='PaymentModal']/div[2]/descendant::p[text()='" + account + "']"));
            WaitForElementToBeClickable(By.XPath("//*[@data-testid='PaymentModal']/div[2]/descendant::p[text()='" + account + "']"));
            _driver.FindElement(By.XPath("//*[@data-testid='PaymentModal']/div[2]/descendant::p[text()='" + account + "']")).Click();
        }

        public void ChooseToAccount(string account)
        {
            WaitForElementToAppear(_toAccount);
            WaitForElementToBeClickable(_toAccount);
            _driver.FindElement(_toAccount).Click();
            WaitForElementToAppear(_search);
            WaitForElementToBeClickable(_search);
            _driver.FindElement(_search).SendKeys(account);
            WaitForElementToAppear(By.XPath("//*[@data-testid='PaymentModal']/div[2]/descendant::p[contains(text(), '" + account + "')][2]"));
            WaitForElementToBeClickable(By.XPath("//*[@data-testid='PaymentModal']/div[2]/descendant::p[contains(text(), '" + account + "')][2]"));
            _driver.FindElement(By.XPath("//*[@data-testid='PaymentModal']/div[2]/descendant::p[contains(text(), '" + account + "')][2]")).Click();
        }

        public void InputTransferAmount(string amount)
        {
            _driver.FindElement(_transferAmountField).SendKeys(amount);
        }

        public void Transfer()
        {
            WaitForElementToBeClickable(_transferButton);
            _driver.FindElement(_transferButton).Click();
            WaitForElementToDisappear(_paymentModal);
        }
    }
}
