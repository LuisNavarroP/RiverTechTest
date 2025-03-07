using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using OpenQA.Selenium;
using RiverUITests.Helpers;

namespace JacketExercise
{
    /// <summary>
    /// This test automates the purchase flow of a 'Sauce Labs Fleece Jacket'.
    /// It includes the steps of user login, adding the item to the cart, asserts the cart contents,
    /// filling out checkout details, reviewing and asserting the order summary, and confirming order dispatch.
    /// The tests use Selenium WebDriver for UI interaction, LightBDD for the driven testing,
    /// NUnit for test framework, and FluentAssertions for assertions.
    /// </summary>
    [FeatureDescription("As a user i want to log in into the site to buy a 'Sauce Labs Fleece Jacket'")]
    [TestFixture]
    public partial class WebTest : FeatureFixture
    {
        // initiallizing the PageHelper class to use it for the tests
        private PageHelper _pageHelper;

        // This method intializes the WebDriver from the webdriver helper
        [SetUp]
        public void SetupTest()
        {
            WebDriverManager.SetupTest();
            _pageHelper = new PageHelper();
        }

        // Scenarios to be run in the test
        [Scenario]
        public void JacketTest()
        {
            Runner.RunScenario(
                Given_User_Login,
                Then_Adds_The_Jacket_To_The_Cart,
                And_open_the_cart_to_assert_the_item_and_continue_to_the_checkout,
                Then_fill_the_requesteed_user_info_and_continue_the_checkout,
                And_assert_the_totals_in_the_checkout_overview_and_finish_the_order,
                Then_verifie_the_order_dispatch
            );
        }

        // Finding username & password fields by its ID, enter credentials, and clicking the login button
        private void Given_User_Login()
        {
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("user-name"), "standard_user");
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("password"), "secret_sauce");
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("login-button"));
        }

        // Finding the button for the "Sauce Labs Fleece Jacket" by its ID and clicking it to add it to the cart
        private void Then_Adds_The_Jacket_To_The_Cart()
        {
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("add-to-cart-sauce-labs-fleece-jacket"));

        }

        // Opening the cart by clicking on the cart icon, Finding the container of the "Sauce Labs Fleece Jacket" item and assert the item found is the desired jacket by it name, and clicking the checkout button if the jacket is found
        private void And_open_the_cart_to_assert_the_item_and_continue_to_the_checkout()
        {
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("shopping_cart_container"));
            var sauceJacket = WebDriverManager.Driver.FindElement(By.CssSelector("#cart_contents_container [data-test='item-5-title-link']"));
            sauceJacket.Text.Should().Be("Sauce Labs Fleece Jacket");
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("checkout"));
        }

        // Filling the checkout form with the specified data and clicking the continue button
        private void Then_fill_the_requesteed_user_info_and_continue_the_checkout()
        {
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("first-name"), "Carlos");
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("last-name"), "Diaz");
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("postal-code"), "111125");
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("continue"));
        }

        // Consuming the "ElementContainsTextCheck" method to find the the total price, Tax , and subtotal elements and assert its values and clicking the finish button after verify the values
        private void And_assert_the_totals_in_the_checkout_overview_and_finish_the_order()
        {
            _pageHelper.ElementContainsTextCheck(WebDriverManager.Driver, By.ClassName("summary_subtotal_label"), "Item total 49.99");
            _pageHelper.ElementContainsTextCheck(WebDriverManager.Driver, By.ClassName("summary_tax_label"), "Tax 4.00");
            _pageHelper.ElementContainsTextCheck(WebDriverManager.Driver, By.ClassName("summary_total_label"), "Total 53.99");
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("finish"));
        }

        // Consuming the "ElementContainsTextCheck" method to find the div class "complete-text" and assert the text
        private void Then_verifie_the_order_dispatch()
        {
            _pageHelper.ElementContainsTextCheck(WebDriverManager.Driver, By.ClassName("complete-text"), "Your order has been dispatched, and will arrive just as fast as the pony can get there!");
        }

        // This method closes  and dispose the WebDriver
        [TearDown]
        public void TearDownTest()
        {
            WebDriverManager.TearDown();
        }
    }
}
