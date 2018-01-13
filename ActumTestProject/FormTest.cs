using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;

namespace ActumTestProject
{
    [TestClass]
    public class FormTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            RemoteWebDriver Driver = new FirefoxDriver();
            Driver.Navigate().GoToUrl(@"https://docs.google.com/forms/d/e/1FAIpQLSeY_W-zSf2_JxR4drhbgIxdEwdbUbE4wXhxHZLgxZGiMcNl7g/viewform");

            // 1) Fill first and second question
            var uncheckedBoxes = Driver.FindElementsByXPath("//div[@aria-label='Check this']");
            foreach (var uncheckedBox in uncheckedBoxes) uncheckedBox.Click();

            DateTime dateNow = new DateTime();
            var dayLine = Driver.FindElementByXPath("//input[@max='31']");
            dayLine.SendKeys(DateTime.Today.AddDays(6).Date.Day.ToString());
            var monthLine = Driver.FindElementByXPath("//input[@max='12']");
            monthLine.SendKeys(DateTime.Today.AddDays(6).Date.Month.ToString());
            var yearLine = Driver.FindElementByXPath("//input[@max='2068']");
            yearLine.Clear();
            yearLine.SendKeys(DateTime.Today.AddDays(6).Date.Year.ToString());

            // 2) Validate that third question is mandatory
            Driver.FindElementByXPath("//span[@class='quantumWizButtonPaperbuttonLabel exportLabel']").Click();
            bool isDisplayed = Driver.FindElementByXPath("//div[@id='i.err.1133270322']").Displayed;
            Assert.IsTrue(isDisplayed);

            // 3) Fill third question and go to another step
            var thirdQuestionAnswerField = Driver.FindElementByXPath("//input[@name='entry.1864473569']");
            thirdQuestionAnswerField.SendKeys(DateTime.Today.ToString("MMMM"));
        }
    }
}
