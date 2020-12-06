using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Text;

namespace TriThucOnline_TTN
{
    public static class Extensions
    {
        public static Uri root_domain = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1");

        public static RestClient client = new RestClient("https://bookstore-api-v1.herokuapp.com/api/v1");

        public static RestRequest request = new RestRequest();

        public static void CreateCookie()
        {
            client.CookieContainer = new System.Net.CookieContainer();
        }
    }
}