using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Web;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Repository
{
    public class Repo_Account
    {
        public static string access;
        public static HttpClient client;
        public static int CheckLogin(string username, string pass)
        {
            Extensions.request = new RestRequest($"login", Method.POST);

            var data = JsonConvert.SerializeObject(new { username = username, password = pass });
            Extensions.request.AddParameter("application/json", data, ParameterType.RequestBody);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                dynamic obj = JsonConvert.DeserializeObject<Object>(result.Content);
                return obj.id;
            }
            else
            {
                return -1;
            }
        }
        public static User GetAccount(int id)
        {
            Extensions.request = new RestRequest($"user/{id}", Method.GET);

            Extensions.request.AddParameter("application/json", ParameterType.RequestBody);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                User obj = JsonConvert.DeserializeObject<User>(result.Content);
                return obj;
            }
            else
            {
                return null;
            }
        }
    }
}