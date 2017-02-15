﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using NLog;

namespace Task2
{
    public class BaseElement
    {
        public By locator;
        public String name;
        static Logger logger = LogManager.GetCurrentClassLogger();
        public BaseElement(By locator, String name)
        {
            this.locator = locator;
            this.name = name;
        }
        public BaseElement(By locator) { }
        public BaseElement() { }

        public void myClick(IWebDriver driver)
        {
            logger.Info("Clicking on {0}. Its locator: {1}", name, locator.ToString());
            waitUntilElementVisible(driver).Click();
        }

        public IWebElement waitUntilElementVisible(IWebDriver driver)
        {
            int waitingtime = Int32.Parse(TestData.waitingTime);
            IWebElement tmp = /*Singleton.getInstance()*/driver.FindElement(locator);
            WebDriverWait wait = new WebDriverWait(/*Singleton.getInstance()*/driver, TimeSpan.FromSeconds(waitingtime));
            wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
            return tmp;
        }

        public IWebElement getElement(IWebDriver driver)
        {
            try
            {
                return /*Singleton.getInstance()*/driver.FindElement(locator);
            }
            catch (NoSuchElementException e)
            {
                logger.Error("Element named {0} was not found. Locator is: {1}. Exception: {2}", name, locator.ToString(), e);
                return null;
            }
        }

        public void FillLabel(string info, IWebDriver driver)
        {
            logger.Info("Filling label with info");
            waitUntilElementVisible(driver).SendKeys(info);
        }


    }
}
