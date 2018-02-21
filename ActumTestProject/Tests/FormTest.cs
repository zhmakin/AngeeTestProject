using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Text;

namespace ActumTestProject
{
    [TestClass]
    public class FormTest
    {
        public IWebDriver Driver { get; set; }
        public WebDriverWait Wait { get; set; }

        [TestInitialize]
        public void SetupTest()
        {
            this.Driver = new FirefoxDriver();
            this.Wait = new WebDriverWait(this.Driver, TimeSpan.FromSeconds(5));
        }
        
        [TestCleanup]
        public void TeardownTest()
        {
            this.Driver.Quit();
        }
        
        [TestMethod]
        public void TestMethod1()
        {
            Pages.MainPage testPage = new Pages.MainPage(this.Driver, this.Wait);
            testPage.Navigate();

            // 1) Fill first and second question
            testPage.CheckBoxesAction();
            testPage.FillDate(DateTime.Today.AddDays(6));

            // 2) Validate that third question is mandatory
            testPage.GoToNextPage();
            Assert.IsTrue(testPage.AssertErrorMsgExist());

            // 3) Fill third question and go to another step
            testPage.FillAnswerField(DateTime.Today.ToString("MMMM"));
            //System.Threading.Thread.Sleep(200);
            testPage.GoToNextPage();
            Pages.SecondPage secondTestPage = new Pages.SecondPage(this.Driver, this.Wait);

            // 4) Fill next questions
            secondTestPage.FillFirstQuestion();
            secondTestPage.ChoseColor();

            // 5) Go back to first step
            secondTestPage.GoToPrevPage();

            // 6) Reverse text in third question
            testPage = new Pages.MainPage(this.Driver, this.Wait);
            testPage.ReverseAnswerField();

            // 7) Go to second step
            testPage.GoToNextPage();

            // 8) Check that both questions are still filed
            secondTestPage = new Pages.SecondPage(this.Driver, this.Wait);
            secondTestPage.CheckFirstAnswerIsFilled();
            secondTestPage.CheckSecondAnswerIsFilled();

            // 9) Go to last step
            secondTestPage.GoToNextPage();

            // 10) Fill last question and send form
            Pages.ThirdPage thirdTestPage = new Pages.ThirdPage(this.Driver, this.Wait);
            thirdTestPage.SelectYes();
            thirdTestPage.GoToNextPage();
        }
    }
}
