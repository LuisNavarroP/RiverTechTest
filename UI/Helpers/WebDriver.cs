using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace RiverUITests.Helpers
{
    public static class WebDriverManager
    {
        // This class is used to hold the WebDriver instance
        private static IWebDriver driver;
        // Check the webdriver instance if its initiallized or not, if not then it will initiallize it
        public static IWebDriver Driver
        {
            get
            {
                if (driver == null)
                {
                    SetupTest();
                }
                return driver;
            }
        }

        // This method is used to setup the WebDriver instance
        public static void SetupTest()
        {
            if (driver == null)
            {
                driver = new ChromeDriver();
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.Navigate().GoToUrl("https://www.saucedemo.com/");
            }
        }
        // This method is used to close and dispose the WebDriver instance
        public static void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();  
            }
        }
    }
}
