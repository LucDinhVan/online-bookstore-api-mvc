using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Repository
{
    public class Repo_Cart
    {
        public static int SaveProductToCart(int id, int quantity)
        {
                var values = JsonConvert.SerializeObject(new
                {
                    quantity = quantity,
                    book_id = id,
                });
                var content = new StringContent(values, Encoding.UTF8, "application/json");
                var responseTask = Repo_Account.client.PostAsync("carts", content);
                responseTask.Wait();
                var result = responseTask.Result;
            return 1;
        }

        public static int CheckStore(int id,int sl)
        {
            string jsonData = "";
            var responseTask = Repo_Account.client.GetAsync("books/"+id);
            responseTask.Wait();
                
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                jsonData = readTask.Result;
                Book book = JsonConvert.DeserializeObject<Book>(jsonData);
                //publishers = listPublisher;
                if (book.quantity >= sl)
                    return 1;
                else
                    return -1;
            }
            return 0;
        }

    }
}