using System.Net;
using API.Helpers;
using API.Model;
using FluentAssertions;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;

namespace API
{
    [FeatureDescription(
    @"In order to retrieve user details
    As an API consumer
    I want to fetch user information from the endpoint")]
    public partial class UserApiTests : FeatureFixture
    {
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

        // Setting the endpoint and sending a get request
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

        // Setting the expected user details and asserting it
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
