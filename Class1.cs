using NUnit.Framework;
using NUnit.Framework.Internal;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace QAAssignment3
{
    [TestFixture]
    public class Class1
    {

        private IWebDriver driver;
        private string baseURL;
        private bool acceptNextAlert = true;
        private string homePageLoc = "";//"file:///C:/Users/ZEEFA/Desktop/QAProject/home.html";
        private string formPageLoc = ""; //"file:///C:/Users/ZEEFA/Desktop/QAProject/page1.html";
        private string formPageName = "page1";

        [SetUp]
        public void ReadXML()
        {
            string directory = "file:///" + Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\")).ToString();
            homePageLoc = directory + "home.html";
            formPageLoc = directory + "page1.html";

        }

        /* Test Case 1 : To test if the Add button on home page navigates to the Form page*/
        [Test]
        public void CheckAddButtonFunctionality()
        {
            /**************************ARRANGE********************/

            /*Initializing the Iwebdriver*/
            driver = new FirefoxDriver();
            /*Implicit pageload timeout*/
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);

            /**************************ACT********************/

            /* Navigating to the homepage url*/
            driver.Navigate().GoToUrl(homePageLoc);
            Thread.Sleep(2000);
            /* Finding the xpath of Add button to click */
            driver.FindElement(By.XPath("//*[@id='search']/a[1]")).Click();
            /* Checks if the form page is loaded or not by checking the label of the first text field*/
            IWebElement label = driver.FindElement(By.XPath("//*[@id='myform']/label[1]"));
            string actualLabelName = label.Text;

            /*************************ASSERT***********************/
            Assert.AreEqual("First Name", actualLabelName);
            Thread.Sleep(3000);
            driver.Close();
        }

        /*Test Case 2 : Click on submit button when the form is empty. Handle the 
            alert box if appears and the control should remain on the same page*/
        [Test]
        public void CheckFormPage()
        {
            /**********************ARRANGE*********************/

            /*Initializing the Iwebdriver*/
            driver = new FirefoxDriver();
            /*Implicit pageload timeout*/
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Boolean nextPageLoaded = true;

            /************************ACT************************/
            /* Navigating to the homepage url*/
            driver.Navigate().GoToUrl(homePageLoc);
            Thread.Sleep(2000);
            /* Finding the xpath of Add button to click */
            driver.FindElement(By.XPath("//*[@id='search']/a[1]")).Click();
            /* Click on Submit button without filling any of the field*/
            driver.FindElement(By.XPath("//*[@id='myform']/input[8]")).Click();
            /* Check if the alert box is present*/
            IAlert alert = new WebDriverWait(driver, TimeSpan.FromTicks(10)).Until(ExpectedConditions.AlertIsPresent());
            /* Accept the alert box if present*/
            if (alert != null)
            {

                driver.SwitchTo().Alert();
                alert.Accept();
            }
            IWebElement label = driver.FindElement(By.XPath("//*[@id='myform']/label[1]"));
            string labelName = label.Text;

            /* After clicking on submit the page should remain the same. It should not 
                move to a different page. This is check by the labl used in the form*/
            if (labelName.Contains("First Name"))
            {
                nextPageLoaded = false;
            }
            /***********************ASSERT*************************/

            /* Check if the control remains on the same page using Assert*/
            Assert.AreEqual(false, nextPageLoaded);
            Thread.Sleep(3000);
            driver.Close();
        }



        


    }
}




