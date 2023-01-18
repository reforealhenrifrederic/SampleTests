using DatacomTests.Pages;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace DatacomTests
{
    [Scope(Feature = "SampleTestsUI")]
    [Binding]
    public class SampleUIStepDefinitions
    {
        private HomePage _homePage;
        private PayeesPage _payeesPage;
        private PaymentsPage _paymentsPage;
        private string _accountNumber;
        private List<string> _payeesList;
        private double _originalFromAmount;
        private double _originalToAmount;
        private string _fromAccount;
        private string _toAccount;
        private double _transferAmount;

        public SampleUIStepDefinitions()
        {
            BaseClass baseClass = new BaseClass();

            _homePage = new HomePage();
            _payeesPage = new PayeesPage();
            _paymentsPage = new PaymentsPage();
        }

        [Given(@"I am in the Demo page")]
        public void GivenIAmInTheDemoPage()
        {
            _homePage.NavigateToPage();
        }

        [Given(@"I get the original amount of '(.*)' and '(.*)'")]
        public void GivenIGetTheOriginalAmounts(string fromAccount, string toAccount)
        {
            _homePage.NavigateToPage();

            _fromAccount = fromAccount;
            _toAccount = toAccount;

            _originalFromAmount = _homePage.GetAmount(fromAccount);
            _originalToAmount = _homePage.GetAmount(toAccount);
        }

        [When(@"I navigate to the Payees page")]
        public void WhenINavigateToThePayeesPage()
        {
            _homePage.ClickMenu();
            _homePage.ClickPayees();
        }

        [When(@"I navigate to the Payments page")]
        public void WhenINavigateToThePaymentsPage()
        {
            _homePage.ClickMenu();
            _homePage.ClickPayments();
        }

        [When(@"I input payee '(.*)' with account number '(.*)'")]
        public void WhenIInputPayeeWithAccountNumber(string payee, string accountNumber)
        {
            _payeesPage.InputPayeeName(payee);
            char[] separator = { '-' };

            _accountNumber = accountNumber;
            List<string> accountDetails = accountNumber.Split(separator, 4, StringSplitOptions.None).ToList();

            _payeesPage.InputPayeeNumber(accountDetails[0], accountDetails[1], accountDetails[2], accountDetails[3]);
        }

        [When(@"I add a payee")]
        public void WhenIAddAPayee()
        {
            _payeesPage.AddNewPayee();
        }

        [When(@"I submit")]
        public void WhenISubmit()
        {
            _payeesPage.ClickAddButtonInForm();
        }

        [When(@"I click the Name header")]
        public void WhenIClickTheNameHeader()
        {
            _payeesPage.ClickNameHeader();
        }

        [When(@"I transfer '(.*)' from '(.*)' to '(.*)'")]
        public void WhenITransferAmount(string amount, string fromAccount, string toAccount)
        {
            _paymentsPage.ChooseFromAccount(fromAccount);
            _paymentsPage.ChooseToAccount(toAccount);
            _paymentsPage.InputTransferAmount(amount);
            _paymentsPage.Transfer();
            _homePage.WaitForHomePageToLoad();

            _transferAmount = Convert.ToDouble(amount);
        }

        [Then(@"Verify Payees is loaded")]
        public void ThenVerifyPayeesIsLoaded()
        {
            Assert.AreEqual("Payees", _payeesPage.GetPageHeader());
        }

        [Then(@"the new payee is successfully added")]
        public void ThenTheNewPayeeIsSuccessfullyAdded()
        {
            _payeesPage.WaitForPayeeAddedNotificationToAppear();
            Assert.NotZero(_payeesPage.GetAccountInPage(_accountNumber).Count);
        }

        [Then(@"errors are shown indicating that Payee Name is required")]
        public void ThenErrorsAreShownIndicatingThatPayeeNameIsRequired()
        {
            Assert.AreEqual("A problem was found. Please correct the field highlighted below.", _payeesPage.GetErrorHeaderMessage());
            Assert.True(_payeesPage.IsPayeeNameRequiredErrorToolTipVisible());
        }

        [Then(@"the errors indicating that Payee Name is required is not visible anymore")]
        public void ThenTheErrorsIndicatingThatPayeeNameIsRequiredIsNotVisibleAnymore()
        {
            Assert.AreEqual(String.Empty, _payeesPage.GetErrorHeaderMessage());
            Assert.False(_payeesPage.IsPayeeNameRequiredErrorToolTipVisible());
        }

        [Then(@"payeees are assorted in ascending order")]
        public void ThenPayeesAreAssortedInASC()
        {
            _payeesList = _payeesPage.GetListOfPayees();

            List<string> sortedPayees = _payeesList.OrderBy(p => p).ToList();

            Assert.True(_payeesList.SequenceEqual(sortedPayees));
        }

        [Then(@"payeees are assorted in descending order")]
        public void ThenPayeesAreAssortedInDSC()
        {
            List<string> sortedPayees = _payeesList.OrderByDescending(p => p).ToList();

            Assert.False(_payeesList.SequenceEqual(sortedPayees));
        }

        [Then(@"the amount is successfully transferred")]
        public void ThenTheAmountIsSuccessfullyTransferred()
        {
            _homePage.WaitForHomePageToLoad();
            var newFromAmount = _homePage.GetAmount(_fromAccount);
            var newToAmount = _homePage.GetAmount(_toAccount);

            Assert.AreEqual(_originalFromAmount - _transferAmount, newFromAmount);
            Assert.AreEqual(_originalToAmount + _transferAmount, newToAmount);
        }
    }
}
