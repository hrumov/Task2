using System.IO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace Task2
{
    public class JournalPage
    {
        AssertsAccumulator assertsAccumulator = new AssertsAccumulator();
        static BaseElement tempNavElement = new BaseElement();
        static BaseElement tempSmallElement = new BaseElement();
        //static BaseElement error = new BaseElement();
        static BaseElement loginLink = new BaseElement(By.XPath("//a[@id = 'ctl00_ucUserActionsToolbar_lnkLogin']"), "Login Link");
        static BaseElement logoutButton = new BaseElement(By.XPath("//a[@id = 'ctl00_ucUserActionsToolbar_lnkLogout']"), "Logout Button");
        static BaseElement searchBar = new BaseElement(By.XPath("//input[@class = 'form-control']"), "Simple Search Bar");
        static BaseElement searchButton = new BaseElement(By.XPath("//button[@type = 'submit']"), "Search Button");
        static BaseElement advancedSearchLink = new BaseElement(By.XPath("//div[@class = 'advance-search']"), "Advanced Search Link");
        static string url;
        static string tempBigXpath;
        static string tempSmallXpath;

        static Logger logger = LogManager.GetCurrentClassLogger();


        public bool CheckItemExist(BaseElement obj, IWebDriver driver)
        {
            if (obj.getElement(driver) != null)
            {
                return true;
            }
            else return false;

        }

        public void GoToTheJournal(string journalName, IWebDriver driver)
        {
            try
            {
                url = string.Concat(TestData.baseUrl, journalName);
                logger.Debug("Going to the journal page {0}", url);
                /*Singleton.getInstance()*/driver.Navigate().GoToUrl(url);
            }
            catch (Exception e)
            {
                string message = string.Format("Tried to go to the {0} page", url);
                logger.Error(e, message);
            }
        }




        public void CheckEverythingForExist(Journals obj, IWebDriver driver)
        {
            foreach (var navs in obj.navigation)
            {
                tempBigXpath = "//a[contains(text(), \"" + navs.bigItem + "\")]";
                tempNavElement.name = navs.bigItem + " in " + obj.journalName;
                tempNavElement.locator = By.XPath(tempBigXpath);
                logger.Info("Checking if element exists {0}, its XPath: {1}", tempNavElement.name, tempBigXpath);
                assertsAccumulator.Accumulate(() => Assert.True(CheckItemExist(tempNavElement, driver), "The Element " + tempNavElement.name + " does not exist"));

                tempSmallXpath = "//span[contains(text(), \"" + navs.item + "\")]";
                tempSmallElement.name = navs.item + " in " + obj.journalName;
                tempSmallElement.locator = By.XPath(tempSmallXpath);
                logger.Info("Checking if element exists {0}, its XPath: {1}", tempSmallElement.name, tempSmallXpath);
                assertsAccumulator.Accumulate(() => Assert.True(CheckItemExist(tempSmallElement, driver), "The Element " + tempSmallElement.name + " does not exist"));
            }
            assertsAccumulator.Release();

        }

        public void GoToLogin(IWebDriver driver)
        {
            logger.Info("Moving to the Login Page");
            loginLink.myClick(driver);
        }
        public void LogOut(IWebDriver driver)
        {
            Assert.True(CheckItemExist(logoutButton, driver), "Login Does not Work!");
            logoutButton.myClick(driver);
        }

        public void SearchAndGo(IWebDriver driver)
        {
            searchBar.myClick(driver);
            searchBar.FillLabel(TestData.simpleSearchInfo, driver);
            searchButton.myClick(driver);
        }

        public void GoToTheAdvancedSearch(IWebDriver driver)
        {
            advancedSearchLink.myClick(driver);
        }
    }

}

