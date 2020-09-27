using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Linq;

namespace VoteCompass
{
    public class QuestionPage : IDisposable
    {
        private readonly IWebDriver _webDriver;
        public QuestionPage()
        {
            _webDriver = new ChromeDriver();
            _webDriver.Url = "https://votecompass.tvnz.co.nz/survey";
        }

        public string Question
        {
            get
            {
                try
                {
                    return _webDriver.FindElement(By.ClassName("Label-root"))?.Text;
                }catch(NoSuchElementException)
                {
                    return null;
                }
            }
        }

        public void Answer(int answerNumber) {
            var answerChoices = _webDriver.FindElements(By.ClassName("Radio-choice"));
            answerChoices.Skip(answerNumber-1).FirstOrDefault().Click();
        }

        public void Dispose()
        {
            _webDriver.Dispose();
        }

        public void SkipIfPossible()
        {
            var navigationButtons = _webDriver.FindElements(By.ClassName("Button-root"));
            navigationButtons.FirstOrDefault(b => string.Equals(b.Text, "Skip", System.StringComparison.OrdinalIgnoreCase))?.Click();
        }
    }
}
