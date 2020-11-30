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
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Controllers
{
    public class TACGIAController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();

        // GET: TACGIA
        public ActionResult Index()
        {
            return View(db.TACGIAs.ToList());
        }

        // GET: TACGIA/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //TACGIA tACGIA = db.TACGIAs.Find(id);
            //if (tACGIA == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.TenTG = tACGIA.TenTG;
            //return View(db.DAUSACHes.Where(temp => temp.MaTG == id).ToList());


            List<Author> authors = null;
            string jsonData = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");

                //get
                var responseTask = client.GetAsync("authors/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    jsonData = readTask.Result;
                    Author author = JsonConvert.DeserializeObject<Author>(jsonData);
                    ViewBag.CacSachTheoTG = author.books;
                    ViewBag.TenTG = author.name;
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
