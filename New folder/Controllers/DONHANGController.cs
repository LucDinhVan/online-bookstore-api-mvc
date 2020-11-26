using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TriThucOnline_TTN.Models;

namespace TriThucOnline_TTN.Controllers
{
    public class DONHANGController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();

        // GET: DONHANG
        public ActionResult Index()
        {
            var dONHANGs = db.DONHANGs.Include(d => d.KHACHHANG).Include(d => d.KHUYENMAI);
            return View(dONHANGs.ToList());
        }

        // GET: DONHANG/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // GET: DONHANG/Create
        public ActionResult Create()
        {
            ViewBag.TrangThaiThanhToan = new SelectList("01", "TrangThaiThanhToan");
            ViewBag.TrangThaiGiao = new SelectList("01","TrangThaiGiao");
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH");
            ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "MaKM", "MaKM");
            return View();
        }

        // POST: DONHANG/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDH,NgayMuaHang,NgayGiao,TrangThaiThanhToan,TrangThaiGiao,MaKH,MaKM")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.DONHANGs.Add(dONHANG);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrangThaiThanhToan = new SelectList("01", "TrangThaiThanhToan");
            ViewBag.TrangThaiGiao = new SelectList("01", "TrangThaiGiao");
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH");
            ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "MaKM", "MaKM");
            return View(dONHANG);
        }

        // GET: DONHANG/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrangThaiThanhToan = new SelectList("01", "TrangThaiThanhToan");
            ViewBag.TrangThaiGiao = new SelectList("01", "TrangThaiGiao");
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH", dONHANG.MaKH);
            ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "MaKM", "MaKM", dONHANG.MaKM);
            return View(dONHANG);
        }

        // POST: DONHANG/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDH,NgayMuaHang,NgayGiao,TrangThaiThanhToan,TrangThaiGiao,MaKH,MaKM")] DONHANG dONHANG)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dONHANG).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KHACHHANGs, "MaKH", "TenKH", dONHANG.MaKH);
            ViewBag.MaKM = new SelectList(db.KHUYENMAIs, "MaKM", "MaKM", dONHANG.MaKM);
            return View(dONHANG);
        }

        // GET: DONHANG/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DONHANG dONHANG = db.DONHANGs.Find(id);
            if (dONHANG == null)
            {
                return HttpNotFound();
            }
            return View(dONHANG);
        }

        // POST: DONHANG/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DONHANG dONHANG = db.DONHANGs.Find(id);
            db.DONHANGs.Remove(dONHANG);
            db.SaveChanges();
            return RedirectToAction("Index");
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
