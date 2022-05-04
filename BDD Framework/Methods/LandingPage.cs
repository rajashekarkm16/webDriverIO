using Dnata.Automation.BDDFramework.API;
using Dnata.TravelRepublic.MobileWeb.UI.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dnata.TravelRepublic.MobileWeb.UI.Methods
{
    public class LandingPage : BaseMethods
    {
        IRestResponse iFacadeResponse;
        HttpRequestWrapper facadeRequest;

        
        public IDictionary<string, Object> GetParamtersToRequest( int destinationID, int productType, int themeID = 0, bool isPreview = false, int tabID = 1, bool tabOnly = true)
        {
            IDictionary<string, Object> paramters = new Dictionary<string, Object>();
            paramters.Add("destinationId", destinationID);
            paramters.Add("ProductType", productType);
            paramters.Add("themeId", themeID);
            paramters.Add("culture", HelperFunctions.GetCulture());
            paramters.Add("domainId", HelperFunctions.GetDomainID());
            paramters.Add("ispreview", isPreview);
            paramters.Add("tabId", tabID);
            paramters.Add("tabOnly", tabOnly);
            return paramters;
        }


        public IRestResponse GetFacadeResponse(int destinationID, int productType, int themeID = 0, bool isPreview = false, int tabID = 1, bool tabOnly = true)
        {
            facadeRequest = new HttpRequestWrapper(HelperFunctions.GetFacadeURL()).SetResourse(ResourceEndPoints.EndPoints["FacadeContent"]);
            var paramters = GetParamtersToRequest( destinationID, productType, themeID, isPreview, tabID, tabOnly);
            facadeRequest.AddParameters(paramters,ParameterType.GetOrPost);
            iFacadeResponse = Execute(facadeRequest, Method.GET);
            return iFacadeResponse;
        }


    }
}
    
