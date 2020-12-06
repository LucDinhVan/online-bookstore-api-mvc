using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;
using System.IO;
using PagedList;
using Newtonsoft.Json;
using RestSharp;

namespace TriThucOnline_TTN.Controllers
{
    [Authorize]
    public class QuanLySachController : Controller
    {
        // GET: QuanLyDAUSACH

        public ActionResult Index(int? page)
        {
            Books books = null;
            int pageNumber = (page ?? 1);
            // GET

            Extensions.request = new RestRequest($"books?page={pageNumber}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                books = JsonConvert.DeserializeObject<Books>(result.Content);
            }
            else
            {

            }

            return View(books);
        }

        public PartialViewResult Search(int? page, string keyword)
        {
            Books books = null;
            int pageNumber = (page ?? 1);
            // GET

            Extensions.request = new RestRequest($"books?q={keyword}&page={pageNumber}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                books = JsonConvert.DeserializeObject<Books>(result.Content);
            }
            else
            {

            }
            int pageSize = 10;
            return PartialView(books.items.OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public PartialViewResult IndexPartial(int? page)
        {
            Books books = null;
            int pageNumber = (page ?? 1);
            // GET

            Extensions.request = new RestRequest($"books?page={pageNumber}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                books = JsonConvert.DeserializeObject<Books>(result.Content);
            }
            else
            {

            }

            return PartialView(books);
        }

        public PartialViewResult XemCTSACHPartial(int? id)
        {
            Book book = null;
            // GET

            Extensions.request = new RestRequest($"books/{id}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                book = JsonConvert.DeserializeObject<Book>(result.Content);
            }
            else
            {

            }

            return PartialView(book);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            Extensions.request = new RestRequest($"books/{id}", Method.DELETE);

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
        public JsonResult Update(FormCollection f)
        {
            Extensions.request = new RestRequest($"books/{f["id"]}", Method.PUT);

            var data = JsonConvert.SerializeObject(new
            {
                title = f["name"],
                price = f["price"],
                publish_year = f["publish_year"],
                picture = f["picture"],
                page_number = f["page_number"],
                quantity = f["quantity"],
                quotes_about = f["quotes_about"],
                author_id = f["author_id"],
                publisher_id = f["publisher_id"],
                category_id = f["category_id"],
            });
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
        public JsonResult Add(FormCollection f)
        {
            Extensions.request = new RestRequest($"books", Method.POST);

            var data = JsonConvert.SerializeObject(new
            {
                title = f["name"],
                price = f["price"],
                publish_year = f["publish_year"],
                picture = f["picture"],
                page_number = f["page_number"],
                quantity = f["quantity"],
                quotes_about = f["quotes_about"],
                author_id = f["author_id"],
                publisher_id = f["publisher_id"],
                category_id = f["category_id"],
            });

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