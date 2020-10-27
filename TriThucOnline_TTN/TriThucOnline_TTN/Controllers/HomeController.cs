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
       
        public ActionResult Index(int PageNo = 1)
        {

            // phân loại sách theo thể loại (Home)
            ViewBag.TheLoai = db.THELOAIs.ToList();

            ViewBag.CacTheLoai = db.THELOAIs.OrderBy(n => n.TenTL).ToList();
            //Paging
            List<DAUSACH> dausach = db.DAUSACHes.ToList();
            int NoOfRecordPerPage = 6;
            int NoOfPages = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dausach.Count) / Convert.ToDouble(NoOfRecordPerPage)));
            int NoOfRecordToSkip = (PageNo - 1) * NoOfRecordPerPage;
            ViewBag.PageNo = PageNo;
            ViewBag.NoOfPage = NoOfPages;
            dausach = dausach.Skip(NoOfRecordToSkip).Take(NoOfRecordPerPage).ToList();

            /// giới thiệu sách mới
            ViewBag.SachMoi = dausach;
            /// sách bán chạy 
            ViewBag.SachBanChay = dausach;

            return View();
        }
        public ActionResult Paging()
        {
            return PartialView("_Paging");
        }
        public ActionResult Detail(int? MaTL)
        {
            ViewBag.TenKhoaHoc = db.THELOAIs.Find(MaTL).TenTL;
            List<DAUSACH> Sach = db.DAUSACHes.Where(x => x.MaTL == MaTL).ToList();
            return PartialView(Sach);
        }

        public ActionResult SachMoi()
        {
            return PartialView("_PartitalView_SachMoi");
        }
        public ActionResult SachBanChay()
        {
            return PartialView("_PartitalView_SachBanChay");
        }

        public ActionResult Search(string search = "")
        {
            ViewBag.search = search;

            ViewBag.TheLoai = db.THELOAIs.ToList();
            ViewBag.NXB = db.NXBs.ToList();
            ViewBag.TacGia = db.TACGIAs.ToList();

            return View(db.DAUSACHes.Where(temp => temp.TenSach.Contains(search)).ToList());

        }
    }
}