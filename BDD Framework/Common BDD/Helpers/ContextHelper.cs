using Dnata.Automation.BDDFramework.API;
using Dnata.Automation.BDDFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using RestSharp;

namespace Dnata.Automation.BDDFramework.Helpers
{
   public class ContextHelper
    {

        // Request
        public static void SaveRequestToContext(ScenarioContext context, HttpRequestWrapper restQuest)
        {
            context.SaveValue("request", restQuest);
        }

        public static HttpRequestWrapper GetRequestFromContext(ScenarioContext context)
        {
            return context.Get<HttpRequestWrapper>("request");
        }

        // Response
        public static void SaveResponseToContext(ScenarioContext context, IRestResponse reponse)
        {
            context.SaveValue("response", reponse);
        }

        public static IRestResponse GetResponseFromContext(ScenarioContext context)
        {
            return context.Get<IRestResponse>("response");
        }

        // Response Content
        public static void SaveResponseContentToContext(ScenarioContext context, IRestResponse reponse)
        {
            context.SaveValue("responseContent", reponse.Content);
        }

        public static string GetResponseContentFromContext(ScenarioContext context)
        {
            return context.Get<string>("responseContent");
        }

        // Save paramters to context
        public static void SaveToContext<T>(ScenarioContext context, string parameter, T value)
        {
            context.SaveValue<T>(parameter, value);
        }

        public static T GetFromContext<T>(ScenarioContext context, string parameter)
        {
            return context.GetValue<T>(parameter);
        }

    }
}
