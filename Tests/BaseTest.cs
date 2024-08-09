using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;

namespace SeleniumTestSuite.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected IWebDriver driver;

        [TestInitialize]
        public void SetUp()
        {
            var driverPath = Path.Combine(Directory.GetCurrentDirectory(), "Drivers");
            driver = new ChromeDriver(driverPath);
            driver.Manage().Window.Maximize();
        }

        [TestCleanup]
        public void TearDown()
        {
            if (driver != null)
            {
                driver.Quit();
            }
        }
    }
}