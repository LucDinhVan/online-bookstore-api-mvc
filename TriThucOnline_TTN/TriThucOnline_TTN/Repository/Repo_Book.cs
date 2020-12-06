using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Repository
{
    public class Repo_Book
    {
        public static Book GetBook(int id)
        {
            string jsonData = "";
            var responseTask = Repo_Account.client.GetAsync("books/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                jsonData = readTask.Result;
                Book book = JsonConvert.DeserializeObject<Book>(jsonData);
                return book;
            }
            return null;
        }

        public static Categories GetAllCate()
        {
            string jsonData = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");
                var responseTask = client.GetAsync("categories");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    jsonData = readTask.Result;
                    Categories categories = JsonConvert.DeserializeObject<Categories>(jsonData);
                    return categories;
                }
                return null;
            }                
            
        }

        public static Publishers GetAllPublisher()
        {
            string jsonData = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");
                var responseTask = client.GetAsync("publishers");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    jsonData = readTask.Result;
                    Publishers publishers = JsonConvert.DeserializeObject<Publishers>(jsonData);
                    return publishers;
                }
                return null;
            }
        }
        public static Authors GetAllAuthor()
        {
            string jsonData = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");
                var responseTask = client.GetAsync("authors");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    jsonData = readTask.Result;
                    Authors authors = JsonConvert.DeserializeObject<Authors>(jsonData);
                    return authors;
                }
                return null;
            }
        }

        public static Books GetAllBook()
        {
            string jsonData = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");
                var responseTask = client.GetAsync("books");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    jsonData = readTask.Result;
                    Books books = JsonConvert.DeserializeObject<Books>(jsonData);
                    return books;
                }
                return null;
            }
        }
    }
}