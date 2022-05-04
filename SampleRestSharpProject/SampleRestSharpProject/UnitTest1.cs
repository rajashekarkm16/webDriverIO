using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace Tests
{
    public class Tests
    {
        IRestRequest request;
        IRestResponse response;
        IRestClient client;


            public class Profile
            {
                public int userId;
                public int id;
                public string title;
               public string body;
                public string content;
            }


        [SetUp]
        public void Setup()
        {
            request = new RestRequest();
            response = new RestResponse();
            client = new RestClient("https://jsonplaceholder.typicode.com");
            
        }


        [Test]
        public void GetRequest()
        {
            string resource = "posts";
            request.Method = Method.GET;
            request.Resource = resource;
            response = client.Execute(request);

        }

        [Test]
        public void PostRequest()
        {
            string resource = "posts";
            request.Method = Method.POST;

            Profile p = new Profile()
            {
                id = 101,
                title = "Title",
                //body = "Body",
                userId = 1
            };
            request.AddJsonBody(p);
            request.Resource = resource;
            response = client.Execute(request);
            
        }

        [Test]
        public void DeserializeRespose()
        {

            string resource = "posts/1";
            request.Method = Method.GET;
            request.Resource = resource;
            response = client.Execute(request);

            Dictionary<string,string> json = JsonConvert.DeserializeObject<Dictionary<string,string>>(response.Content);
            Assert.AreEqual(1, Convert.ToInt16(json["id"]));

            var json1 = JObject.Parse(response.Content);
            Assert.AreEqual(1, Convert.ToInt16(json1["id"].ToString()));

        }

        [Test]
        public void DeserializeResposeToSpecificClass()
        {

            string resource = "posts/1";
            request.Method = Method.GET;
            request.Resource = resource;
            response = client.Execute(request);

            //List<Profile> listOfProfiles = JsonConvert.DeserializeObject<List<Profile>>(response.Content)
                  Profile profile = JsonConvert.DeserializeObject<Profile>(response.Content);
            Assert.AreEqual(1, profile.id, "");
        }

        [Test]
        public void DeserializeResposeToSpecificListOfClass()
        {

            string resource = "posts";
            request.Method = Method.GET;
            request.Resource = resource;
            response = client.Execute(request);

            List<Profile> listOfProfiles = JsonConvert.DeserializeObject<List<Profile>>(response.Content);
            
            Assert.AreEqual(100, listOfProfiles.Count, "");
        }

        [Test]
        public void UrlSegement()
        {
            string resource = "posts/{postId}";
            request.Method = Method.GET;
            request.AddUrlSegment("postId", 2);
            request.Resource = resource;
            response = client.Execute(request);
            JObject jObject = JObject.Parse(response.Content);
            Assert.AreEqual(2, (int)jObject["id"]);

        }

        [Test]
        public void AddingCookiesAndCookies()
        {
            string resource = "posts";
            request.Method = Method.GET;
            request.Resource = resource;
            request.AddCookie("C1", "C1Value");
            request.AddHeader("Accept", "text/html");
            response = client.Execute(request);
            List<Profile> listOfProfiles = JsonConvert.DeserializeObject<List<Profile>>(response.Content);
            Assert.AreEqual(100, listOfProfiles.Count, "");
            

        }

        [Test]
        public void ValidatingResponse()
        {
            string resource = "posts";
            request.Method = Method.GET;
            request.Resource = resource;
            request.AddCookie("C1", "C1Value");
            request.AddHeader("Accept", "text/html");
            response = client.Execute(request);
            IList<Parameter> headers = response.Headers;
            IList<RestResponseCookie> cookies = response.Cookies;
            Assert.IsTrue(response.IsSuccessful);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(ResponseStatus.Completed, response.ResponseStatus);
            
        }





        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}