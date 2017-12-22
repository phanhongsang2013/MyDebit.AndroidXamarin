using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;

namespace MyDebit
{
    public class SangUtil
    {
        public class RestApiRequest
        {
            // insert plugin SDK RestSharp to use

            static readonly string SERVER_ADDRESS = "http://mic.duytan.edu.vn:82/SmartMushroom/api/";
            //static readonly string SERVER_ADDRESS = "http://localhost:53123/SmartMushroom/api/";

            private readonly RestClient _client;

            public RestApiRequest()
            {
                _client = new RestClient(SERVER_ADDRESS);
            }

            // get data with url, param will add condition
            // example : request.AddParameter("id", "sang@gmail.com");
            // param = null : get all
            public List<T> GetMethod<T>(string actionName, string value)
            {
                var request = new RestRequest(actionName, Method.GET);

                if (!string.IsNullOrEmpty(value))
                    request.AddParameter("id", value);

                var queryResult = _client.Execute<List<T>>(request).Data;
                return queryResult;
            }
            public List<T> GetMethodCustom<T>(string actionName, string value)
            {
                var request = new RestRequest(actionName + value, Method.GET);
                var queryResult = _client.Execute<List<T>>(request).Data;
                return queryResult;
            }

            // post data to create new data to database
            // with a data model
            public string PostMethod(string tableName, object dataModel)
            {
                var request = new RestRequest(tableName, Method.POST);
                request.RequestFormat = DataFormat.Json;

                request.AddBody(dataModel);
                var resStatus = _client.Execute(request).StatusCode.ToString();
                return resStatus;
            }

            // delete data 
            // param is condition 
            // if param = null, remove all (not test)
            public void DeleteMethod(string tableName, object dataModel, Parameter param)
            {
                var client = new RestClient(SERVER_ADDRESS);
                var request = new RestRequest(tableName, Method.DELETE);

                if (param != null)
                    request.AddParameter(param);

                client.Execute(request);
            }

            //put method to update data
            public string PutMethod(string action, string id, Object dataModel)
            {
                var request = new RestRequest(action + id, Method.PUT);
                request.AddJsonBody(dataModel);
                var respone = _client.Execute(request).StatusDescription.ToString();
                return respone;
            }
        }

        public string ConvertIntToCurrency(int money)
        {
            string yourValue = (money / 1m).ToString("C0");
            return yourValue;
        }
    }
}