using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;

namespace SeleniumTestSuite.Pages
{
    public class WebActions
    {
        private readonly IWebDriver _driver;
        private readonly Dictionary<string, string> _selectors;

        public WebActions(IWebDriver driver, string selectorFilePath)
        {
            _driver = driver;
            _selectors = LoadSelectors(selectorFilePath);
        }

        public void ClickElement(string key)
        {
            var element = _driver.FindElement(By.XPath(_selectors[key]));
            element.Click();
        }

        public void FillTextField(string key, string text)
        {
            var element = _driver.FindElement(By.XPath(_selectors[key]));
            element.Clear();
            element.SendKeys(text);
        }

        public void SelectCheckbox(string key, bool shouldSelect)
        {
            var checkbox = _driver.FindElement(By.XPath(_selectors[key]));
            if (checkbox.Selected != shouldSelect)
            {
                checkbox.Click();
            }
        }

        public void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }


         public void SelectRandomDate(string key)
        {
            // Locate the date input element
            var dateInput = _driver.FindElement(By.XPath(_selectors[key]));

            // Click on the date input to open the date picker
            dateInput.Click();

            // Wait for the date picker to appear by polling the DOM
            bool datePickerVisible = false;
            for (int i = 0; i < 10; i++) // Wait for up to 10 seconds
            {
                try
                {
                    var datePicker = _driver.FindElement(By.Id("ui-datepicker-div"));
                    if (datePicker.Displayed)
                    {
                        datePickerVisible = true;
                        break;
                    }
                }
                catch (NoSuchElementException)
                {
                    // Do nothing, just wait and retry
                }
                System.Threading.Thread.Sleep(1000); // Wait for 1 second before retrying
            }

            if (!datePickerVisible)
            {
                throw new Exception("Date picker did not appear within the expected time.");
            }

            // Identify available date cells
            var availableDates = _driver.FindElements(By.XPath("//td[not(contains(@class, 'ui-datepicker-unselectable')) and not(contains(@class, 'ui-datepicker-other-month')) and not(contains(@class, 'ui-datepicker-week-end'))]//a"));

            if (availableDates.Any())
            {
                // Randomly select one of the available dates
                var random = new Random();
                var randomDate = availableDates[random.Next(availableDates.Count)];

                // Scroll into view and click the date
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", randomDate);
                randomDate.Click();
            }
            else
            {
                throw new NoSuchElementException("No selectable dates available.");
            }
        }

        



        
       public void SelectDropdownOption(string key, string optionValue)
        {
            // Click on the select2 element to open the dropdown
            var dropdownElement = _driver.FindElement(By.XPath(_selectors[key]));
            dropdownElement.Click();

            // Wait until the dropdown options are visible
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.XPath($"//li[contains(@class, 'select2-results__option') and text()='{optionValue}']")));

            // Find the option by visible text and click it
            var optionElement = _driver.FindElement(By.XPath($"//li[contains(@class, 'select2-results__option') and text()='{optionValue}']"));
            optionElement.Click();
        }

        private Dictionary<string, string> LoadSelectors(string filePath)
        {
            var selectors = new Dictionary<string, string>();
            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split('=', 2);
                if (parts.Length == 2)
                {
                    selectors[parts[0].Trim()] = parts[1].Trim();
                }
            }
            return selectors;
        }
    }
}
