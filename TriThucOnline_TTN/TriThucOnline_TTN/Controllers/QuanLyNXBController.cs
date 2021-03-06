﻿using System;
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
    public class QuanLyNXBController : Controller
    {
        // GET: QuanLyNXB
        public ActionResult Index(int? page)
        {
            Publishers publishers = null;
            // GET

            Extensions.request = new RestRequest($"publishers?name", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                publishers = JsonConvert.DeserializeObject<Publishers>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(publishers.publishers.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public PartialViewResult IndexPartial(int? page)
        {
            Publishers publishers = null;
            // GET

            Extensions.request = new RestRequest($"publishers?name", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                publishers = JsonConvert.DeserializeObject<Publishers>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return PartialView(publishers.publishers.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult XemCTNXBPartial(int? id)
        {
            Publisher publisher = null;
            // GET

            Extensions.request = new RestRequest($"publishers/{id}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                publisher = JsonConvert.DeserializeObject<Publisher>(result.Content);
            }
            else
            {

            }
            return PartialView(publisher);
        }

        [HttpPost]
        public JsonResult Remove(int id)
        {
            Extensions.request = new RestRequest($"publishers/{id}", Method.DELETE);

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
            Extensions.request = new RestRequest($"publishers/{id}", Method.PUT);

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
            Extensions.request = new RestRequest($"publishers", Method.POST);

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