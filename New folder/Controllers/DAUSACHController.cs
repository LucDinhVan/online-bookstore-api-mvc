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
    public class DAUSACHController : Controller
    {
        private SQL_TriThucOnline_BanSachEntities1 db = new SQL_TriThucOnline_BanSachEntities1();

        // GET: DAUSACH
        public ActionResult Index()
        {
            var dAUSACHes = db.DAUSACHes.Include(d => d.TACGIA).Include(d => d.THELOAI).Include(d => d.NXB);
            return View(dAUSACHes.ToList());
        }

        // GET: DAUSACH/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAUSACH dAUSACH = db.DAUSACHes.Find(id);
            if (dAUSACH == null)
            {
                return HttpNotFound();
            }
            return View(dAUSACH);
        }

        // GET: DAUSACH/Create
        public ActionResult Create()
        {
            ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG");
            ViewBag.MaTL = new SelectList(db.THELOAIs, "MaTL", "TenTL");
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB");
            return View();
        }

        // POST: DAUSACH/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSach,TenSach,MaNXB,MaTL,MaTG,NamXuatBan,GiaTien,PicBook,SoTrang,BoCucSach,TrichDan,SoLuongTon,Moi")] DAUSACH dAUSACH)
        {
            if (ModelState.IsValid)
            {
                db.DAUSACHes.Add(dAUSACH);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG", dAUSACH.MaTG);
            ViewBag.MaTL = new SelectList(db.THELOAIs, "MaTL", "TenTL", dAUSACH.MaTL);
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB", dAUSACH.MaNXB);
            return View(dAUSACH);
        }

        // GET: DAUSACH/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAUSACH dAUSACH = db.DAUSACHes.Find(id);
            if (dAUSACH == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG", dAUSACH.MaTG);
            ViewBag.MaTL = new SelectList(db.THELOAIs, "MaTL", "TenTL", dAUSACH.MaTL);
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB", dAUSACH.MaNXB);
            return View(dAUSACH);
        }

        // POST: DAUSACH/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSach,TenSach,MaNXB,MaTL,MaTG,NamXuatBan,GiaTien,PicBook,SoTrang,BoCucSach,TrichDan,SoLuongTon,Moi")] DAUSACH dAUSACH)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dAUSACH).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaTG = new SelectList(db.TACGIAs, "MaTG", "TenTG", dAUSACH.MaTG);
            ViewBag.MaTL = new SelectList(db.THELOAIs, "MaTL", "TenTL", dAUSACH.MaTL);
            ViewBag.MaNXB = new SelectList(db.NXBs, "MaNXB", "TenNXB", dAUSACH.MaNXB);
            return View(dAUSACH);
        }

        // GET: DAUSACH/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DAUSACH dAUSACH = db.DAUSACHes.Find(id);
            if (dAUSACH == null)
            {
                return HttpNotFound();
            }
            return View(dAUSACH);
        }

        // POST: DAUSACH/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DAUSACH dAUSACH = db.DAUSACHes.Find(id);
            db.DAUSACHes.Remove(dAUSACH);
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
