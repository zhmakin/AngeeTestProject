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
    public class ThirdPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;

        public ThirdPage(IWebDriver browser, WebDriverWait _wait)
        {
            this.wait = _wait;
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        [FindsBy(How = How.XPath, Using = "(//div[@aria-label='Yes'])")]
        public IWebElement YesRadioButtons { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[@class='quantumWizButtonPaperbuttonLabel exportLabel'])[1]")]
        public IWebElement BackButton { get; set; }

        [FindsBy(How = How.XPath, Using = "(//span[@class='quantumWizButtonPaperbuttonLabel exportLabel'])[2]")]
        public IWebElement NextButton { get; set; }


        public void SelectYes()
        {
            YesRadioButtons.Click();
        }

        public void GoToNextPage()
        {
            this.NextButton.Click();
        }
    }
}
