using System.Net;
using API.Helpers;
using API.Model;
using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;

namespace API
{
    /// <summary>
    /// This test automates the user get proccess.
    /// It includes the steps of send the get user request, assert the status code of the request and assert the content of the response,
    /// The tests use LightBDD for the driven testing,RestSharp for the API request, Newtonsoft.Json for the response deserialization,
    /// and FluentAssertions for assertions.
    /// </summary>
    [FeatureDescription(
    @"In order to retrieve user details
    As an API consumer
    I want to fetch user information from the endpoint")]
    public partial class UserApiTests : FeatureFixture
    {
        // Variables to store the response status code and the response body
        private HttpStatusCode _statusCode;
        private userResponse _responseBody;

        // Scenarios to be run in the test
        [Scenario]
        public void Retrieving_a_user()
        {
            Runner.RunScenario(
                Given_the_get_user_endpoint_and_the_get_request_sent_then_store_the_response,
                And_assert_the_call_response,
                Then_assert_the_fetched_data_with_the_expected_user
                );
        }

        // Sending a GET request to fetch user details,the response code and assign each one to a variable
        private void Given_the_get_user_endpoint_and_the_get_request_sent_then_store_the_response()
        {
            var endpoint = "https://jsonplaceholder.typicode.com/users/1";
            var (statusCode, responseBody) = ApiHelper.ExecuteGetRequest<userResponse>(endpoint);

            _statusCode = statusCode;
            _responseBody = responseBody;
        }

        // Asserting the status code of the response
        private void And_assert_the_call_response()
        {
            _statusCode.Should().Be(HttpStatusCode.OK);
        }

        // Setting the expected user details and asserting the response body comparing it with the expected user
        private void Then_assert_the_fetched_data_with_the_expected_user()
        {
            var expectedUser = new userResponse
            {
                id = 1,
                name = "Leanne Graham",
                username = "Bret",
                email = "Sincere@april.biz",
                phone = "1-770-736-8031 x56442",
                website = "hildegard.org",
                address = new Address
                {
                    street = "Kulas Light",
                    suite = "Apt. 556",
                    city = "Gwenborough",
                    zipcode = "92998-3874",
                    geo = new Geo
                    {
                        lat = "-37.3159",
                        lng = "81.1496"
                    }
                },
                company = new Company
                {
                    name = "Romaguera-Crona",
                    catchPhrase = "Multi-layered client-server neural-net",
                    bs = "harness real-time e-markets"
                }
            };
            _responseBody.Should().BeEquivalentTo(expectedUser);
        }
    }
}
