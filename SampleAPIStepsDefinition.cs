using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System.IO;
using System.Net;
using TechTalk.SpecFlow;

namespace DatacomTests
{
    [Scope(Feature = "SampleTestsAPI")]
    [Binding]
    public class SampleAPIStepsDefinition
    {
        RestClient client;
        RestRequest request;
        RestResponse response;

        [Given(@"I am a user")]
        public void GivenIAmAUser()
        {
            client = new RestClient("https://jsonplaceholder.typicode.com/");
            
        }

        [When(@"I get the list of users")]
        public void WhenIGetTheListOfUsers()
        {
            request = new RestRequest("users", Method.Get);
            response = client.Execute(request);
        }

        [When(@"I get user with id '(.*)'")]
        public void WhenIGetUserById(int id)
        {
            request = new RestRequest($"users/{id}", Method.Get);
            response = client.Execute(request);
        }

        [When(@"I post a user")]
        public void WhenIPostAUser()
        {
            request = new RestRequest("users", Method.Post);

            string text = File.ReadAllText(@"JsonToSend.json");
            var jsonToSend = JsonConvert.DeserializeObject(text);

            request.AddParameter("application/json", jsonToSend, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;

            response = client.Execute(request);
        }

        [Then(@"there are '(.*)' users returned")]
        public void ThenThereAreNUsersReturned(int userCount)
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(userCount, JArray.Parse(response.Content).Count);
        }

        [Then(@"user '(.*)' is returned")]
        public void ThenUserIsReturned(string userReturned)
        {
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(userReturned, JObject.Parse(response.Content)["name"].ToString());
        }

        [Then(@"user is created")]
        public void ThenUserIsCreated()
        {
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }
    }
}
