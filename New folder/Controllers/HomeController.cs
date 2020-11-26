using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;


namespace TriThucOnline_TTN.Controllers
{
    public class HomeController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();

        public ActionResult Index()
        {
            
            
            return View(db.DAUSACHes.Where(temp=>temp.MaTL==1).ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Search( string search="")
        {
            ViewBag.search = search;
            return View(db.DAUSACHes.Where(temp => temp.TenSach.Contains(search)).ToList());
        }
    }
}