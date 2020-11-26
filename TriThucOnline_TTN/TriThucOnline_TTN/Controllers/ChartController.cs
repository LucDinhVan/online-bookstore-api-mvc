using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Controllers
{
    [Authorize]
    public class ChartController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();

        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        // GET: Per Year
        public ActionResult _Partial_ThongKeDoanhThu_Nam_Chart()
        {
            return PartialView(db.Database.SqlQuery<ThongKeDoanhThuTheoNam>("SELECT * FROM ThongKeDoanhThuTheoNam_View").ToList());
        }

        // GET: Per Month
        public ActionResult _Partial_ThongKeDoanhThu_Thang_Chart(int year)
        {
            return PartialView(db.Database.SqlQuery<ThongKeDoanhThuTheoThang>("exec ThongKeDoanhThuTheoThang " + year).ToList());
        }
    }
}