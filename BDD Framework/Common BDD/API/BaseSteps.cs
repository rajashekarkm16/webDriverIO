using Dnata.Automation.BDDFramework.Extensions;
using Dnata.Automation.BDDFramework.API;
using TechTalk.SpecFlow;
using ReportPortal.Shared.Execution.Logging;
using ReportPortal.Shared;
using System;
using Newtonsoft.Json;
using OpenQA.Selenium.Remote;
using Dnata.Automation.BDDFramework.Helpers;
using RestSharp;
using System.Net;
using System.Collections.Generic;

namespace Dnata.Automation.BDDFramework.API.BaseSteps
{

    public class BaseSteps
    { 
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;

        public BaseSteps(ScenarioContext injectedContext) 
        {
            context = injectedContext;
        }


        // Request
        public void SaveRequestToContext(HttpRequestWrapper restQuest)
        {
            ContextHelper.SaveRequestToContext(context, restQuest);
        }

        public HttpRequestWrapper GetRequestFromContext()
        {
            return ContextHelper.GetRequestFromContext(context);
        }

        // Response
        public void SaveResponseToContext(IRestResponse response)
        {
            ContextHelper.SaveResponseToContext(context, response);
        }

        public IRestResponse GetResponseFromContext()
        {
            return ContextHelper.GetResponseFromContext(context);
        }


        // Response content
        public void SaveResponseContentToContext(IRestResponse response)
        {
            ContextHelper.SaveResponseContentToContext(context, response);
        }

        public string GetResponseContentFromContext()
        {
            return ContextHelper.GetResponseContentFromContext(context);
        }

        public void SaveToContext<T>(string parameter, T value)
        {
            ContextHelper.SaveToContext(context, parameter, value);
        }
        
        public string GetFromContext(string parameter)
        {
           return context.GetValue<string>(parameter);
        }

        public T GetFromContext<T>(string paramter)
        {
           return  ContextHelper.GetFromContext<T>( context, paramter);  
        }

        public HttpStatusCode GetStatusCode()
        {
            var response = GetResponseFromContext();
            return response.StatusCode;
        }
        
        public string GetStatusDescription()
        {
            var response = GetResponseFromContext();
            return response.StatusDescription;
        }

        public string GetServer()
        {
            var response = GetResponseFromContext();
            return response.Server;
        }

        public Uri GetResponseURI()
        {
            var response = GetResponseFromContext();
            return response.ResponseUri;
        }

    }
}
