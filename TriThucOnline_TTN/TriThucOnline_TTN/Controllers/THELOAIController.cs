using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Controllers
{
    public class THELOAIController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();

        // GET: THELOAI
        public ActionResult Index()
        {
            return View(db.THELOAIs.ToList());
        }

        // GET: THELOAI/Details/5
        public ActionResult Details(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //THELOAI tHELOAI = db.THELOAIs.Find(id);
            //if (tHELOAI == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.TenTL = tHELOAI.TenTL;
            //return View(db.DAUSACHes.Where(temp => temp.MaTL == id).ToList());


            List<Category> categories = null;
            string jsonData = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");

                //get
                var responseTask = client.GetAsync("categories/"+id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    jsonData = readTask.Result;
                    Category category = JsonConvert.DeserializeObject<Category>(jsonData);
                    ViewBag.CacSachTheoTL = category.books;
                    ViewBag.TenTL = category.name;
                    //publishers = listPublisher;
                }
            }

            return View();









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
