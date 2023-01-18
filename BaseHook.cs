using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace DatacomTests
{
    [Binding]
    public static class BaseHook
    {
        private static WebDriver _driver;

        [BeforeScenario]
        public static void InitializeWebDriver()
        {
            _driver = new ChromeDriver("\\drivers\\chromedriver.exe");
        }

        [AfterScenario]
        public static void DisposeWebdriver()
        {
            _driver.Quit();
        }

        public static WebDriver GetChromeDriver()
        {
            return _driver;
        }
    }
}
