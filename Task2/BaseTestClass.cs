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
    public class BaseTestClass
    {
        public IWebDriver driverForJournals = BrowserFactory.getBrowser(TestData.browser);
        public IWebDriver driverForLogin = BrowserFactory.getBrowser(TestData.browser);//testBrowser2);
    }
}
