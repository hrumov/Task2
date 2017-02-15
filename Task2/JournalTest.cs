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
    public class JournalTest : BaseTestClass
    {
        static JournalPage journPage = new JournalPage();
        static AssertsAccumulator accumulator = new AssertsAccumulator();
        static Logger logger = LogManager.GetCurrentClassLogger();        
        //static IWebDriver somedriver = driverForJournals.getInstance(TestData.browser);
        static List<Journals> dataForParams = DataFromFile.MakeParamsData(TestData.journal);

        [OneTimeSetUp]
        public void SetUpEverything()
        {
            driverForJournals.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            driverForJournals.Manage().Window.Maximize();
            //Singleton.getInstance().Navigate().GoToUrl(TestData.baseUrl); 
            //driverForJournals/*.getInstance(TestData.browser)*/.Navigate().GoToUrl(TestData.baseUrl);
        }


        //[Ignore("asd")]
        [Test]
        [TestCaseSource("dataForParams")]
        public void TestJournals(Journals journ)
        {
            journPage.GoToTheJournal(journ.journalName, driverForJournals/*.getInstance(TestData.browser)*/);
            Assert.False(driverForJournals/*.getInstance(TestData.browser)*/.Url.Contains("PageNotFoundError"), "There is no journal named " + journ.journalName);
            journPage.SearchAndGo(driverForJournals/*.getInstance(TestData.browser)*/);
            accumulator.Accumulate(() => Assert.True(SearchPage.CheckSearch(driverForJournals/*.getInstance(TestData.browser)*/), "No results"));
            journPage.GoToTheAdvancedSearch(driverForJournals/*.getInstance(TestData.browser)*/);
            AdvancedSearchPage.MakeSearch(driverForJournals/*.getInstance(TestData.browser)*/);
            accumulator.Accumulate(() => Assert.False(AdvancedSearchPage.CheckSearch(driverForJournals/*.getInstance(TestData.browser)*/), "No results"));
            journPage.GoToTheJournal(journ.journalName, driverForJournals/*.getInstance(TestData.browser)*/);
            journPage.CheckEverythingForExist(journ, driverForJournals/*.getInstance(TestData.browser)*/);
            accumulator.Release();           
        }

        [OneTimeTearDown]
        public void CleanAll()
        {
            driverForJournals/*.getInstance(TestData.browser)*/.Quit();
        }
    }
}
