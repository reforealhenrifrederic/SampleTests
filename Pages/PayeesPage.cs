using DatacomTests.Pages;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DatacomTests
{
    public class PayeesPage : BaseClass
    {
        private static WebDriver _driver;
        private By _header = By.XPath("//h1[@class='CustomPage-heading']/span");
        private By _addPayeeButton = By.XPath("//*[@class='Button Button--sub Button--translucid js-add-payee']");
        private By _payeeForm = By.Id("apm-form");
        private By _payeeName = By.Id("ComboboxInput-apm-name");
        private By _payeeNameConfirmation = By.XPath("//*[contains(text(),'Someone new:')]");
        private By _bank = By.Id("apm-bank");
        private By _branch = By.Id("apm-branch");
        private By _account = By.Id("apm-account");
        private By _suffix = By.Id("apm-suffix");
        private By _payeeFormAddButton = By.XPath("//form[@id='apm-form']/descendant::button[text()='Add']");
        private By _notification = By.XPath("//*[contains(text(),'Payee added')]/ancestor::div[@class='inner js-notification show js-notificationShown']");
        private By _errorToolTip = By.XPath("//*[contains(text(), 'Payee Name is a required field. Please complete to continue.')]/ancestor::div[@class='tooltip-panel general-tooltip js-tooltip-view']");
        private By _errorHeaderInForm = By.XPath("//*[@class='error-header']");
        private By _nameHeader = By.XPath("//*[text()='Name']");
        private By _payeesList = By.XPath("//*[@class='js-payee-name']");

        public PayeesPage()
        {
            _driver = BaseHook.GetChromeDriver();
        }

        public string GetPageHeader()
        {
            WaitForElementToAppear(_header);
            return _driver.FindElement(_header).Text;
        }

        public void AddNewPayee()
        {
            WaitForElementToAppear(_addPayeeButton);
            _driver.FindElement(_addPayeeButton).Click();
            WaitForElementToAppear(_payeeForm);
        }

        public void InputPayeeName(string payeeName)
        {
            WaitForElementToAppear(_payeeName);
            _driver.FindElement(_payeeName).SendKeys(payeeName);
            WaitForElementToAppear(_payeeNameConfirmation);
            _driver.FindElement(_payeeNameConfirmation).Click();
            WaitForElementToAppear(_bank);
        }

        public void InputPayeeNumber(string bank, string branch, string account, string suffix)
        {
            _driver.FindElement(_bank).SendKeys(bank);
            _driver.FindElement(_branch).SendKeys(branch);
            _driver.FindElement(_account).SendKeys(account);
            _driver.FindElement(_suffix).SendKeys(suffix + "\t");
        }

        public void ClickAddButtonInForm()
        {
            WaitForElementToAppear(_payeeFormAddButton);
            _driver.FindElement(_payeeFormAddButton).Click();
        }

        public void WaitForPayeeAddedNotificationToAppear()
        {
            WaitForElementToAppear(_notification);
        }
        
        public ICollection<IWebElement> GetAccountInPage(string accountNumber)
        {
            return _driver.FindElements(By.XPath("//*[text()='" + accountNumber + "']"));
        }

        public string GetErrorHeaderMessage()
        {
            try
            {
                return _driver.FindElement(_errorHeaderInForm).Text;
            }
            catch(Exception ex)
            {
                return String.Empty;
            }
        }

        public bool IsPayeeNameRequiredErrorToolTipVisible()
        {
            try
            {
                _driver.FindElement(_errorToolTip);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public void ClickNameHeader()
        {
            WaitForElementToAppear(_nameHeader);
            _driver.FindElement(_nameHeader).Click();
        }

        public List<string> GetListOfPayees()
        {
            var payeesList = _driver.FindElements(_payeesList).ToList();

            List<string> retVal = new List<string>();
            foreach(var p in payeesList)
            {
                retVal.Add(p.Text);
            }

            return retVal;
        }
    }
}
