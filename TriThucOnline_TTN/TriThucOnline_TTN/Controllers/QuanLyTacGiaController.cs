using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TriThucOnline_TTN.Models;
using PagedList;
using PagedList.Mvc;
using System.IO;
using RestSharp;
using Newtonsoft.Json;

namespace TriThucOnline_TTN.Controllers
{
    [Authorize]
    public class QuanLyTacGiaController : Controller
    {
        // GET: QuanLyTacGia
        public ActionResult Index(int? page)
        {
            Authors authors = null;
            // GET

            Extensions.request = new RestRequest($"authors", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                authors = JsonConvert.DeserializeObject<Authors>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(authors.authors.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult IndexPartial(int? page)
        {
            Authors authors = null;
            // GET

            Extensions.request = new RestRequest($"authors", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                authors = JsonConvert.DeserializeObject<Authors>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return PartialView(authors.authors.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult XemCTTGPartial(int? id)
        {
            Author publisher = null;
            // GET

            Extensions.request = new RestRequest($"authors/{id}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                publisher = JsonConvert.DeserializeObject<Author>(result.Content);
            }
            else
            {

            }
            return PartialView(publisher);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            Extensions.request = new RestRequest($"authors/{id}", Method.DELETE);

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
        public JsonResult Update(int id, string name, string picture, string info)
        {
            Extensions.request = new RestRequest($"authors/{id}", Method.PUT);

            var data = JsonConvert.SerializeObject(new { id = id, name = name, picture=picture, info=info });
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
        public JsonResult Add(string name, string picture, string info)
        {
            Extensions.request = new RestRequest($"authors", Method.POST);

            var data = JsonConvert.SerializeObject(new {name = name, picture=picture, info=info });
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