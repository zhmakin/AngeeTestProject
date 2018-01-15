using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text;

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
            var nextButton = Driver.FindElementByXPath("//span[@class='quantumWizButtonPaperbuttonLabel exportLabel']");
            nextButton.Click();
            bool isDisplayed = Driver.FindElementByXPath("//div[@id='i.err.1133270322']").Displayed;
            Assert.IsTrue(isDisplayed);

            // 3) Fill third question and go to another step
            var thirdQuestionAnswerField = Driver.FindElementByXPath("//input[@name='entry.1864473569']");
            thirdQuestionAnswerField.SendKeys(DateTime.Today.ToString("MMMM"));
            System.Threading.Thread.Sleep(1000);
            nextButton.Click();

            // 4) Fill next questions
            List<string> favorites = new List<string>() {"The big band theory", "Suits", "Black list", "Elementary", "Sherlok"};
            Random randomIndex = new Random();
            string randomLine = 
                favorites[randomIndex.Next(0, 4)] + "\n"+ 
                favorites[randomIndex.Next(0, 4)] + "\n"+ 
                favorites[randomIndex.Next(0, 4)];

            var fourthQuestionAnswerField = Driver.FindElementByXPath("//textarea[@name='entry.1144061500']");
            fourthQuestionAnswerField.SendKeys(randomLine);

            var dropBoxButton = Driver.FindElementByXPath("//div[@class='quantumWizMenuPaperselectDropDown exportDropDown']");
            dropBoxButton.Click();
            System.Threading.Thread.Sleep(100);
            var redColorCase = Driver.FindElementByXPath("//form[@id='mG61Hd']/div/div[2]/div[2]/div[4]/div[2]/div[2]/div[3]/content");
            redColorCase.Click();

            // 5) Go back to first step
            var backButton = Driver.FindElementByXPath("//form[@id='mG61Hd']/div/div[2]/div[3]/div/div/div/content/span");
            backButton.Click();
            System.Threading.Thread.Sleep(1000);

            // 6) Reverse text in third question
            thirdQuestionAnswerField = Driver.FindElementByXPath("//input[@name='entry.1864473569']");
            char[] myArr = DateTime.Today.ToString("MMMM").ToCharArray();
            Array.Reverse(myArr);
            thirdQuestionAnswerField.Clear();
            thirdQuestionAnswerField.SendKeys(new string(myArr));

            // 7) Go to second step
            nextButton = Driver.FindElementByXPath("//span[@class='quantumWizButtonPaperbuttonLabel exportLabel']");
            nextButton.Click();

            // 8) Check that both questions are still filed
            fourthQuestionAnswerField = Driver.FindElementByXPath("//textarea[@name='entry.1144061500']");
            Assert.IsNotNull(fourthQuestionAnswerField.Text);
            var field = Driver.FindElementByXPath("//div[@aria-selected='true']");
            string selectedColor = field.Text;
            Assert.AreEqual(selectedColor, "Red");

            // 9) Go to last step
            nextButton = Driver.FindElementByXPath("//form[@id='mG61Hd']/div/div[2]/div[3]/div/div/div[2]/content/span");
            nextButton.Click();

            // 10) Fill last question and send form
            var radioButtonYes = Driver.FindElementByXPath("//div[@aria-label='Yes']");
            radioButtonYes.Click();
            var sendButton = Driver.FindElementByXPath("//form[@id='mG61Hd']/div/div[2]/div[3]/div/div/div[2]/content/span");
            sendButton.Click();
        }
    }
}
