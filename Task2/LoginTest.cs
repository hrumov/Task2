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
using Task2.Pages;

namespace Task2
{
    [TestFixture]
    [Parallelizable(ParallelScope.Fixtures)]
    class LoginTest : BaseTestClass
    {
        static JournalPage journPage = new JournalPage();
        static Logger logger = LogManager.GetCurrentClassLogger();

        [OneTimeSetUp]
        public void SetUpEverything()
        {            
            //Singleton.getInstance().Navigate().GoToUrl(TestData.baseUrl);
            driverForLogin.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            driverForLogin.Manage().Window.Maximize();
            driverForLogin/*.getInstance(TestData.testBrowser2)*/.Navigate().GoToUrl(TestData.baseUrl);
        }

        [Test]
        public void TestLogin()
        {           
            journPage.GoToLogin(driverForLogin/*.getInstance(TestData.testBrowser2)*/);
            LoginPage.Login(driverForLogin/*.getInstance(TestData.testBrowser2)*/);
            journPage.LogOut(driverForLogin/*.getInstance(TestData.testBrowser2)*/);
        }

        [OneTimeTearDown]
        public void CleanAll()
        {
            driverForLogin/*.getInstance(TestData.testBrowser2)*/.Quit();
        }
    }
}
