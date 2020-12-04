using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;
using PagedList;
using PagedList.Mvc;
using RestSharp;
using Newtonsoft.Json;

namespace TriThucOnline_TTN.Controllers
{
    [Authorize]
    public class QuanLyKhuyenMaiController : Controller
    {
        public ActionResult Index(int? page)
        {
            Coupons coupons = null;
            // GET

            Extensions.request = new RestRequest($"coupons?name", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                coupons = JsonConvert.DeserializeObject<Coupons>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return View(coupons.coupons.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        [HttpGet]
        public PartialViewResult IndexPartial(int? page)
        {
            Coupons coupons = null;
            // GET

            Extensions.request = new RestRequest($"coupons?name", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                coupons = JsonConvert.DeserializeObject<Coupons>(result.Content);
            }
            else
            {

            }
            int pageNumber = (page ?? 1);
            int pageSize = 10;
            return PartialView(coupons.coupons.ToList().OrderBy(n => n.id).ToPagedList(pageNumber, pageSize));
        }
        public PartialViewResult XemCTKMPartial(string code)
        {
            Coupon coupon = null;
            // GET

            Extensions.request = new RestRequest($"coupons/{code}", Method.GET);

            var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessful)
            {
                coupon = JsonConvert.DeserializeObject<Coupon>(result.Content);
            }
            else
            {

            }
            return PartialView(coupon);
        }

        [HttpPost]
        public JsonResult Remove(string code)
        {
            Extensions.request = new RestRequest($"coupons/{code}", Method.DELETE);

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
            Extensions.request = new RestRequest($"coupons/{f["code"]}", Method.PUT);
            DateTime from = Convert.ToDateTime(f["valid_from"]);
            DateTime until = Convert.ToDateTime(f["valid_until"]);
            var data = JsonConvert.SerializeObject(new
            {
                code = f["code"],
                description = f["description"],
                discount = f["discount"],
                count = f["count"],
                valid_from = String.Format("{0:dd/MM/yyyy HH:mm:ss}", from),
                valid_until = String.Format("{0:dd/MM/yyyy HH:mm:ss}", until),
                is_enable = f["is_enable"]=="1"?true:false
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
            Extensions.request = new RestRequest($"coupons", Method.POST);

            DateTime from = Convert.ToDateTime(f["valid_from"]);
            DateTime until = Convert.ToDateTime(f["valid_until"]);
            var data = JsonConvert.SerializeObject(new
            {
                code = f["code"],
                description = f["description"],
                discount = f["discount"],
                count = f["count"],
                valid_from = String.Format("{0:dd/MM/yyyy HH:mm:ss}", from),
                valid_until = String.Format("{0:dd/MM/yyyy HH:mm:ss}", until),
                is_enable = f["is_enable"] == "1" ? true : false
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