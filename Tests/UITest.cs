using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeleniumTestSuite.Pages;

namespace SeleniumTestSuite.Tests
{
    [TestClass]
    public class UITest : BaseTest
    {
        [TestMethod]
        public void PerformActionsTest()
        {
            // Navigate to the test page
            driver.Navigate().GoToUrl("https://miacademy.co/#/");

            var actions = new WebActions(driver, "Selectors/selectors.txt");

            // Example usage of WebActions methods
            actions.ClickElement("miaPrepOHS");
            actions.ClickElement("applyMOHS");
            actions.ClickElement("nextMOHS");
            actions.FillTextField("nameMOHS","Umit");
            actions.FillTextField("lastNameMOHS","Yilmaz");
            actions.FillTextField("emailMOHS","umit@gmail.com");
            actions.FillTextField("phoneMOHS","9773337722");
            actions.SelectDropdownOption("dropdownMOHS","No");
            actions.SelectCheckbox("checkBoxMOHSsearchEngine", true);
            actions.SelectCheckbox("checkBoxMOHSmiaPrep", true);
            actions.SelectRandomDate("datePickerMOHS");
            //actions.Wait(5000);
            actions.ClickElement("nextToStudentInfoMOHS");



            
        }
    }
}
