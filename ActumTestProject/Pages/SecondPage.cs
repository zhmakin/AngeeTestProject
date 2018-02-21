using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace ActumTestProject.Pages
{
    public class SecondPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public SecondPage(IWebDriver browser, WebDriverWait _wait)
        {
            this.wait = _wait;
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = "(//textarea[@jsname='YPqjbf'])")]
        public IWebElement FirstQuestionInputField { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='quantumWizMenuPaperselectDropDown exportDropDown']")]
        public IWebElement ColorDropbox { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[@class='quantumWizButtonPaperbuttonLabel exportLabel'])[1]")]
        public IWebElement BackButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[@class='quantumWizButtonPaperbuttonLabel exportLabel'])[2]")]
        public IWebElement NextButton { get; set; }


        public void FillFirstQuestion()
        {
            List<string> favorites = new List<string>() { "The big band theory", "Suits", "Black list", "Elementary", "Sherlok" };
            Random randomIndex = new Random();
            string randomLine =
                favorites[randomIndex.Next(0, 4)] + "\n" +
                favorites[randomIndex.Next(0, 4)] + "\n" +
                favorites[randomIndex.Next(0, 4)];

            FirstQuestionInputField.SendKeys(randomLine);
        }

        public void ChoseColor()
        {
            ColorDropbox.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div[@class='exportSelectPopup quantumWizMenuPaperselectPopup']")));
            var redbtn = driver.FindElement(By.XPath("//div[@class='exportSelectPopup quantumWizMenuPaperselectPopup']/div[3]"));
            redbtn.Click();
        }


        public void GoToPrevPage()
        {
            this.BackButton.Click();
        }

        public void GoToNextPage()
        {
            this.NextButton.Click();
        }

        public void CheckFirstAnswerIsFilled()
        {
            Assert.IsNotNull(FirstQuestionInputField.GetAttribute("data-initial-value"));
        }

        public void CheckSecondAnswerIsFilled()
        {
            var field = driver.FindElement(By.XPath("//div[@aria-selected='true']"));
            string selectedColor = field.Text;
            Assert.AreEqual(selectedColor, "Red");
        }
    }
}
