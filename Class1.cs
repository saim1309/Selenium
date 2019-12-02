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


        /*Test Case 3: Check the happy path i.e., If all the netries to the form are correct*/
        [Test]
        public void CheckHappyPathfForFilledForm()
        {
            /**********************ARRANGE***********************/

            /*Initializing the Iwebdriver*/
            driver = new FirefoxDriver();
            /*Implicit pageload timeout*/
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Boolean dataEntered = false;

            /**************************ACT***************/
            /* Navigating to the homepage url*/
            driver.Navigate().GoToUrl(homePageLoc);
            Thread.Sleep(2000);
            /* Finding the xpath of Add button to click */
            driver.FindElement(By.XPath("//*[@id='search']/a[1]")).Click();
            /* Enter all the required details*/
            driver.FindElement(By.Id("fname")).SendKeys("Rachel");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("lname")).SendKeys("Green");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("address")).SendKeys("653 Albert Street");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("city")).SendKeys("Waterloo");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("phone")).SendKeys("519-781-9206");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("email")).SendKeys("rachelgreen@gmail.com");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("model")).SendKeys("2012 BMW 328i");
            Thread.Sleep(1000);
            /* Click on Submit button after filling all the field*/
            driver.FindElement(By.XPath("//*[@id='myform']/input[8]")).Click();
            /*Get the data displayed after registering*/
            string urlConstructed = driver.FindElement(By.XPath("/html/body/div[1]/a")).Text;

            Console.WriteLine("Url constructed : {0}" + urlConstructed);
            /* Check if the person is registered successfully and the url to the JD power
                site is constructed successfully*/
            if (urlConstructed.Contains("http://www.jdpower.com/cars/BMW/328i/2012"))
            {
                dataEntered = true;
            }
            /***********************ASSERT*********************/
            Assert.AreEqual(true, dataEntered);
            Thread.Sleep(3000);
            driver.Close();
        }

        /*Test Case 4: To check the system behaviour for invalid telephone number entry*/
        [Test]
        public void CheckWrongTelephoneEntry()
        {
            /**********************ARRANGE***********************/

            /*Initializing the Iwebdriver*/
            driver = new FirefoxDriver();
            /*Implicit pageload timeout*/
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            Boolean phoneErrorPresent = false;

            /**************************ACT***************/
            /* Navigating to the homepage url*/
            driver.Navigate().GoToUrl(homePageLoc);
            Thread.Sleep(2000);
            /* Finding the xpath of Add button to click */
            driver.FindElement(By.XPath("//*[@id='search']/a[1]")).Click();
            /* Enter all the required details*/
            driver.FindElement(By.Id("fname")).SendKeys("Rachel");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("lname")).SendKeys("Green");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("address")).SendKeys("653 Albert Street");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("city")).SendKeys("Waterloo");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("phone")).SendKeys("519-6");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("email")).SendKeys("rachelgreen@gmail.com");
            Thread.Sleep(1000);
            driver.FindElement(By.Id("model")).SendKeys("2012 BMW 328i");
            Thread.Sleep(1000);
            /* Click on Submit button after filling all the field*/
            driver.FindElement(By.XPath("//*[@id='myform']/input[8]")).Click();


            string phoneError = driver.FindElement(By.Id("phoneError")).Text;
            if (!phoneError.Equals(""))
            {
                phoneErrorPresent = true;
            }

            /***********************ASSERT*********************/
            Assert.AreEqual(true, phoneErrorPresent);
            Thread.Sleep(3000);
            driver.Close();
        }




    }
}




