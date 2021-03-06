﻿using Newtonsoft.Json;
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
    public class NXBController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();


        // GET: NXB
        public ActionResult Index()
        {
            return View(db.NXBs.ToList());
        }

        // GET: NXB/Details/5
        public ActionResult Details(int? id)
        {

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //NXB nXB = db.NXBs.Find(id);
            //if (nXB == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.TenNXB = nXB.TenNXB;
            //return View(db.DAUSACHes.Where(temp => temp.MaNXB == id).ToList());



            List<Publisher> publishers = null;
            string jsonData = "";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://bookstore-api-v1.herokuapp.com/api/v1/");

                //get
                var responseTask = client.GetAsync("publishers/" + id);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsStringAsync();
                    readTask.Wait();

                    jsonData = readTask.Result;
                    Publisher publisher = JsonConvert.DeserializeObject<Publisher>(jsonData);
                    ViewBag.CacSachTheoNXB = publisher.books;
                    ViewBag.TenNXB = publisher.name;
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
