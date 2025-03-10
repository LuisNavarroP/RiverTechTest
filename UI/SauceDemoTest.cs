using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using OpenQA.Selenium;
using UI.Helpers;

namespace UI
{
    [FeatureDescription("As a user I want to log in into the site to buy a 'Sauce Labs Fleece Jacket'")]
    [TestFixture]
    public partial class WebTest : FeatureFixture
    {
        private PageHelper _pageHelper;

        [Scenario]
        public void JacketTest()
        {
            _pageHelper = new PageHelper();
            Runner.RunScenario(
                Given_User_Login,
                Then_Adds_The_Jacket_To_The_Cart,
                And_open_the_cart_to_assert_the_item_and_continue_to_the_checkout,
                Then_fill_the_requested_user_info_and_continue_the_checkout,
                And_assert_the_totals_in_the_checkout_overview_and_finish_the_order,
                Then_verify_the_order_dispatch
            );
        }
        // Login into the site with specific credentials
        private void Given_User_Login()
        {  
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("user-name"), "standard_user");
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("password"), "secret_sauce");
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("login-button"));
        }
        // Adding the jacket to the cart
        private void Then_Adds_The_Jacket_To_The_Cart()
        {
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("add-to-cart-sauce-labs-fleece-jacket"));
        }
        // Open the cart to check if the item is there and continue to the checkout
        private void And_open_the_cart_to_assert_the_item_and_continue_to_the_checkout()
        {   
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("shopping_cart_container"));
            var sauceJacket = WebDriverManager.Driver.FindElement(By.CssSelector("#cart_contents_container [data-test='item-5-title-link']"));
            sauceJacket.Text.Should().Be("Sauce Labs Fleece Jacket");
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("checkout"));
        }
        // Fill the requested user info and continue the checkout
        private void Then_fill_the_requested_user_info_and_continue_the_checkout()
        { 
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("first-name"), "Carlos");
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("last-name"), "Diaz");
            _pageHelper.FieldInputs(WebDriverManager.Driver, By.Id("postal-code"), "111125");
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("continue"));
        }
        // checking the totals in the checkout overview and finish the order
        private void And_assert_the_totals_in_the_checkout_overview_and_finish_the_order()
        {      
            _pageHelper.ElementValueTextCheck(WebDriverManager.Driver, By.ClassName("summary_subtotal_label"), "Item total 49.99");
            _pageHelper.ElementValueTextCheck(WebDriverManager.Driver, By.ClassName("summary_tax_label"), "Tax 4.00");
            _pageHelper.ElementValueTextCheck(WebDriverManager.Driver, By.ClassName("summary_total_label"), "Total 53.99");
            _pageHelper.ClickButton(WebDriverManager.Driver, By.Id("finish"));
        }
        // Verify the order dispatch
        private void Then_verify_the_order_dispatch()
        {
            _pageHelper.ElementValueTextCheck(WebDriverManager.Driver, By.ClassName("complete-text"), "Your order has been dispatched, and will arrive just as fast as the pony can get there!");
        }
    }
}
