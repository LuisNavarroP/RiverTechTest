using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace UI.Helpers
{
    public static class WebDriverManager
    {
        private static IWebDriver driver;
        // To check and initiallize the driver
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

        // Setup configuration 
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
        // Closing and disposing the driver
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
