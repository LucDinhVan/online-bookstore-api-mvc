using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Repository;
using TriThucOnline_TTN.Models;
using RestSharp;

namespace TriThucOnline_TTN.Controllers
{
    public class DAUSACHController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();

        // GET: DAUSACH/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //DAUSACH dAUSACH = db.DAUSACHes.Find(id);
            //if (dAUSACH == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(dAUSACH);

            List<Book> books = null;
            string jsonData = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");

                //get
                var responseTask = client.GetAsync("books/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    jsonData = readTask.Result;
                    Book book = JsonConvert.DeserializeObject<Book>(jsonData);
                    ViewBag.CTSach = book;
                    //publishers = listPublisher;
                }
            }

            return View();



        }
        public ActionResult SachLienQuan(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            THELOAI tl = db.THELOAIs.Find(id);
            if (tl == null)
            {
                return HttpNotFound();
            }
            ViewBag.sachtheotheloai = tl.DAUSACHes.ToList();
            return PartialView("_PartitalView_SachLienQuan");
        }
        [HttpPost]
        public JsonResult AddToCart(int? id, int quantity)
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return Json(new { Success = false, ItemAmount = 10, ErrorMsg = "Bạn chưa đăng nhập" });
            }
            bool success = true;
            string error = "";
            if(id != null)
            {
                Cart cart = null;
                // GET

                Extensions.request = new RestRequest($"carts", Method.POST);
                var data = JsonConvert.SerializeObject(new { book_id = id, quantity = quantity });
                Extensions.request.AddParameter("application/json", data, ParameterType.RequestBody);
                var responseTask = Extensions.client.ExecuteAsync(Extensions.request);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessful)
                {
                    cart = JsonConvert.DeserializeObject<Cart>(result.Content);
                }
                else
                {
                    success = false;
                    dynamic obj = JsonConvert.DeserializeObject<Object>(result.Content);
                    error = obj.message.ToString();
                }
            }

            return Json(new { Success = success, ItemAmount = 10, ErrorMsg = error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}