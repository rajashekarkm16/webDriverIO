using Dnata.Automation.BDDFramework.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ReportPortal.Shared;
using ReportPortal.Shared.Execution.Logging;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dnata.Automation.BDDFramework.Helpers
{
    public static class LogHelper
    {

        public static void LogRequestBody(HttpRequestWrapper request)
        {
            var jsonBody = JsonConvert.SerializeObject(request.GetRequestBody());
            ILogMessage logMessage = new LogMessage(jsonBody)
            {
                Message = "Request:" + jsonBody,
                Level = LogMessageLevel.Info
            };
            Context.Launch.Log.Message(logMessage);
            CommonFunctions.LogToConsole("Request: \n" + jsonBody);
        }

        public static void LogResponseContent(IRestResponse response)
        {

            var jsonResponse = response.Content;
            ILogMessage logMessage = new LogMessage(jsonResponse)
            {
                Message = "Response:" + jsonResponse,
                Level = LogMessageLevel.Info
            };
            Context.Launch.Log.Message(logMessage);
            CommonFunctions.LogToConsole("Request URL : \n" + response.ResponseUri);
            CommonFunctions.LogToConsole("Response: \n" + jsonResponse);
        }

        public static void LogContent(string message)
        {
            ILogMessage logMessage = new LogMessage(message)
            {
                Message =  message,
                Level = LogMessageLevel.Info
            };
            Context.Launch.Log.Message(logMessage);
            CommonFunctions.LogToConsole(message);
        }
    }
}

