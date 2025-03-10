using FluentAssertions;
using OpenQA.Selenium;

namespace UI.Helpers
{
    public class PageHelper
    {
        // this method search for the input field by its locator, clear its value and allows to set a desired value
        public void FieldInputs(IWebDriver driver, By elementLocation, string value)
        {
            var element = driver.FindElement(elementLocation);
            element.Clear();
            element.SendKeys(value);
        }

        // this method find the button by its locator and click it
        public void ClickButton(IWebDriver driver, By elementLocation)
        {
            var button = driver.FindElement(elementLocation);
            button.Click();
        }

        // this method find the element by its locator and assert the text with an expected text
        public void ElementTextCheck(IWebDriver driver, By elementLocation, string expectedText)
        {
            var element = driver.FindElement(elementLocation);
            var elementText = element.Text;
            elementText.Should().Be(expectedText);
        }

        // this method find the element by its locator, grab the text, remove the unwanted characters and assert the text with the expected text
        public void ElementContainsTextCheck(IWebDriver driver, By locator, string expectedText, string failureMessage = null)
        {
            var elementText = driver.FindElement(locator).Text;
            string normalizedText = elementText.Replace(":", "").Replace("$", "").Trim();
            normalizedText.Should().Contain(expectedText, failureMessage ?? $"Expected element '{locator}' to contain '{expectedText}', but found '{elementText}'");
        }
    }
}
