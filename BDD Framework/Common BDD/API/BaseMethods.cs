using Dnata.Automation.BDDFramework.Helpers;
using Newtonsoft.Json;
using RestSharp;

namespace Dnata.Automation.BDDFramework.API
{
    public class BaseMethods
    {
        public HttpRequestWrapper InitializeRequestForEndPoint(string resource )
        {
            HttpRequestWrapper request = new HttpRequestWrapper();
            request.SetResourse(resource);
            return request;
        }

        public HttpRequestWrapper InitializeRequestForEndPoint(string resource, string url)
        {
            HttpRequestWrapper request = new HttpRequestWrapper(url);
            request.SetResourse(resource);
            return request;
        }

        public T CreateRequestFromFile<T>(string fileLocation)
        {
            return JsonHelper.ReadFromJson<T>(fileLocation);
        }

        public IRestResponse Execute(HttpRequestWrapper request, Method method = Method.POST)
        {
            var response = request.SetMethod(method).Execute();
            LogHelper.LogRequestBody(request);
            LogHelper.LogResponseContent(response);
            return response;
        }
        
        public T Execute<T>(HttpRequestWrapper request, Method method = Method.POST)
        {
            var response = request.SetMethod(method).Execute();
            LogHelper.LogRequestBody(request);
            LogHelper.LogResponseContent(response);
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

    }
}
