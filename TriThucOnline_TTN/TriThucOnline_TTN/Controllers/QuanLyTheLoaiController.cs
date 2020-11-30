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
        SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();
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
        /// <summary>
        /// Tao moi
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(THELOAI cd)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                db.THELOAIs.Add(cd);
                db.SaveChanges();
                ViewBag.ThongBao = "Thêm mới thành công";
            }
            else {
                ViewBag.ThongBao = "Thêm mới thất bại";
                return View(cd);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int MaTheLoai)
        {
            //Lấy ra đối tượng sách theo mã 
            THELOAI cd = db.THELOAIs.SingleOrDefault(n => n.MaTL == MaTheLoai);
            if (cd == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            
            return View(cd);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(THELOAI cd, FormCollection f)
        {
            //Thêm vào cơ sở dữ liệu
            if (ModelState.IsValid)
            {
                //Thực hiện cập nhận trong model
                db.Entry(cd).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ViewBag.ThongBao = "Lỗi";
                return View(cd);
            }

            return RedirectToAction("Index");

        }
        
        /////////////////////
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

    }
}