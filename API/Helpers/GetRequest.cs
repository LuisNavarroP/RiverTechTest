using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace API.Helpers
{
    public class ApiHelper
    {
        //Execute the GET request and deserialize the response
        public static (HttpStatusCode, T) ExecuteGetRequest<T>(string endpoint)
        {
            var client = new RestClient(endpoint);
            var request = new RestRequest();
            request.Method = Method.Get;

            var response = client.Execute(request);
            var responseBody = JsonConvert.DeserializeObject<T>(response.Content);

            return (response.StatusCode, responseBody);
        }
    }
}
