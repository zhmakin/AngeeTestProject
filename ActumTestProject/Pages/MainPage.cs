using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace ActumTestProject.Pages
{
    public class MainPage
    {
        private readonly IWebDriver driver;
        private readonly WebDriverWait wait;
        private readonly string url = @"https://docs.google.com/forms/d/e/1FAIpQLSeY_W-zSf2_JxR4drhbgIxdEwdbUbE4wXhxHZLgxZGiMcNl7g/viewform";

        public MainPage(IWebDriver browser, WebDriverWait _wait)
        {
            this.wait = _wait;
            this.driver = browser;
            PageFactory.InitElements(browser, this);
        }

        public void Navigate()
        {
            this.driver.Navigate().GoToUrl(this.url);
        }

        [FindsBy(How = How.XPath, Using = "(//input[@jsname='YPqjbf'])[1]")]
        public IWebElement DayInputField { get; set; }

        [FindsBy(How = How.XPath, Using = "(//input[@jsname='YPqjbf'])[2]")]
        public IWebElement MonthInputField { get; set; }

        [FindsBy(How = How.XPath, Using = "(//input[@jsname='YPqjbf'])[3]")]
        public IWebElement YearInputField { get; set; }

        [FindsBy(How = How.XPath, Using = "//input[@name='entry.1864473569']")]
        public IWebElement AnswerInputField { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@aria-label='Check this']")]
        public IList<IWebElement> UncheckedBoxes { get; set; }

        [FindsBy(How = How.XPath, Using = "//span[@class='quantumWizButtonPaperbuttonLabel exportLabel']")]
        public IWebElement NextButton { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@id='i.err.1133270322']")]
        public IWebElement ErrorMessage { get; set; }

        public void CheckBoxesAction()
        {
            foreach (var uncheckedBox in UncheckedBoxes) uncheckedBox.Click();
        }

        public void FillDate(DateTime date)
        {
            this.DayInputField.Clear();
            this.DayInputField.SendKeys(date.Day.ToString());

            this.MonthInputField.Clear();
            this.MonthInputField.SendKeys(date.Month.ToString());

            this.YearInputField.Clear();
            this.YearInputField.SendKeys(date.Year.ToString());
        }

        public bool AssertErrorMsgExist()
        {
            return ErrorMessage.Displayed;
            
        }

        public void FillAnswerField(string text)
        {
            this.AnswerInputField.Clear();
            this.AnswerInputField.SendKeys(text);
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@id='i.err.1133270322']")));
        }

        public void GoToNextPage()
        {
            this.NextButton.Click();
        }


        public void ReverseAnswerField()
        {
            char[] myArr = AnswerInputField.GetAttribute("data-initial-value").ToCharArray();
            Array.Reverse(myArr);
            AnswerInputField.Clear();
            AnswerInputField.SendKeys(new string(myArr));
        }


    }
}
