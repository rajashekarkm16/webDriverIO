using System;
using System.Collections.Generic;
using Dnata.Automation.BDDFramework.Configuration;
using Dnata.Automation.BDDFramework.Helpers;
using Newtonsoft.Json;
using RestSharp;

namespace Dnata.Automation.BDDFramework.API
{
    public class HttpRequestWrapper
    {
        private RestRequest _restRequest;
        private RestClient _restClient;
        private Object requestBody;
        private readonly string _url = AtConfiguration.GetConfiguration("PreProdUrl");

        public HttpRequestWrapper()
        {
            _restRequest = new RestRequest();
        }
        
        public HttpRequestWrapper(string url)
        {
            _restRequest = new RestRequest();
            _url = url;
        }

        public HttpRequestWrapper SetResourse(string resource)
        {
            _restRequest.Resource = resource;
            return this;
        }

        public HttpRequestWrapper SetMethod(Method method)
        {
            _restRequest.Method = method;
            return this;
        }

        public HttpRequestWrapper AddUrlSegment(string name, string value)
        {
            _restRequest.AddUrlSegment(name, value);
            return this;
        }

        public HttpRequestWrapper AddHeaders(IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                _restRequest.AddParameter(header.Key, header.Value, ParameterType.HttpHeader);
            }
            return this;
        }

        public HttpRequestWrapper AddJsonContent(object data)
        {
            requestBody = data;
            _restRequest.RequestFormat = DataFormat.Json;
            _restRequest.AddHeader("Content-Type", "application/json");
            _restRequest.AddJsonBody(data);
            return this;
        }

        public Object GetRequestBody()
        {
            return requestBody;
        }

        public string GetRequestBodyAsString()
        {
            return JsonHelper.ConvertJsonToString(requestBody);
        }

        public HttpRequestWrapper AddEtagHeader(string value)
        {
            _restRequest.AddHeader("If-None-Match", value);
            return this;
        }


        public HttpRequestWrapper AddParameter(string name, object value)
        {
            _restRequest.AddParameter(name, value);
            return this;
        }

        public HttpRequestWrapper AddParameter(string name, object value, ParameterType parameterType)
        {
            _restRequest.AddParameter(name, value, parameterType);
            return this;
        }

        public HttpRequestWrapper AddParameters(IDictionary<string, object> parameters)
        {
            foreach (var item in parameters)
            {
                _restRequest.AddParameter(item.Key, item.Value);
            }
            return this;
        }
        public HttpRequestWrapper AddParameters(IDictionary<string, object> parameters, ParameterType parameterType)
        {
            foreach (var item in parameters)
            {
                _restRequest.AddParameter(item.Key, item.Value, parameterType);
            }
            return this;
        }

        public IRestResponse Execute()
        {
            try
            {
                _restClient = new RestClient(_url);
                var response = _restClient.Execute(_restRequest);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public T Execute<T>()
        {
            _restClient = new RestClient(_url);
            var response = _restClient.Execute<T>(_restRequest);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

    }
}
