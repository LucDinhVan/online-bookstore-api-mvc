using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using RestSharp;

namespace TriThucOnline_TTN.Controllers
{
    [Authorize]
    public class QuanLyTheLoaiController : Controller
    {
        // GET: QuanLyChuDe
        public ActionResult Index(int? page)
        {
            Categories categories = null;
            string data = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");

                //get
                var responseTask = client.GetAsync("categories?name");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    data = readTask.Result;
                    categories = JsonConvert.DeserializeObject<Categories>(data);
                    
                    //ViewBag.CacTheLoai = categories.categories;
                }
            }

            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(categories.categories.OrderBy(n => n.name).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult IndexPartial(int ? page)
        {
            Categories categories = null;
            string data = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");

                //get
                var responseTask = client.GetAsync("categories?name");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    data = readTask.Result;
                    categories = JsonConvert.DeserializeObject<Categories>(data);

                    //ViewBag.CacTheLoai = categories.categories;
                }
            }

            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return PartialView(categories.categories.OrderBy(n => n.name).ToPagedList(pageNumber, pageSize));
        }
        
        [HttpPost]
        public JsonResult Remove(int id)
        {
            // GET
            Extensions.request = new RestRequest($"categories/{id}", Method.DELETE);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                return Json(new { Url = Url.Action("IndexPartial") });
            }
            else
            {
                Response.StatusCode = 500;
                return null;
            }
        }

        [HttpPost]
        public JsonResult Update(int id, string name)
        {
            Extensions.request = new RestRequest($"categories/{id}", Method.PUT);

            var data = JsonConvert.SerializeObject(new { id = id, name = name });
            Extensions.request.AddParameter("application/json", data, ParameterType.RequestBody);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                return Json(new { Url = Url.Action("IndexPartial") });
            }
            else
            {
                Response.StatusCode = 500;
                Response.Write(result.Content);
                return null;
            }

        }

        [HttpPost]
        public JsonResult Add(string name)
        {
            Extensions.request = new RestRequest($"categories", Method.POST);

            var data = JsonConvert.SerializeObject(new {name = name });
            Extensions.request.AddParameter("application/json", data, ParameterType.RequestBody);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                return Json(new { Url = Url.Action("IndexPartial") });
            }
            else
            {
                Response.StatusCode = 500;
                Response.Write(result.Content);
                return null;
            }

        }
    }
}