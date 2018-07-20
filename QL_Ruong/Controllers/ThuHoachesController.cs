using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QL_Ruong.Models;

namespace QL_Ruong.Controllers
{
    public class ThuHoachesController : Controller
    {
        private Datacontext db = new Datacontext();

        // GET: ThuHoaches
        public ActionResult Index(int id)
        {
            Session["Ruong_ID"] = id;
            var thuHoaches = db.ThuHoaches.Include(t => t.Ruong).Where(r=>r.Ruong_ID==id).OrderByDescending(m=>m.TH_ID);
            return View(thuHoaches.ToList());
        }

        // GET: ThuHoaches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuHoach thuHoach = db.ThuHoaches.Find(id);
            if (thuHoach == null)
            {
                return HttpNotFound();
            }
            return View(thuHoach);
        }

        // GET: ThuHoaches/Create
        public ActionResult Create()
        {
            ViewBag.Ruong_ID = new SelectList(db.Ruongs, "Ruong_ID", "Ruong_Name");
            return View();
        }

        // POST: ThuHoaches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TH_ID,Ruong_ID,TH_Name,TH_ChiSo,TH_TienTrenDa,TH_TongTien,TH_MoTa")] ThuHoach thuHoach)
        {
            int SoDa = Convert.ToInt32(Request["TH_ChiSo"]);
            int tientrenda = Convert.ToInt32(Request["TH_TienTrenDa"]);
            if (ModelState.IsValid)
            {
                thuHoach.TH_ID = autotang(db.ThuHoaches.Count());
                thuHoach.Ruong_ID = Convert.ToInt32(Session["Ruong_ID"]);
                thuHoach.TH_TongTien = SoDa * tientrenda;
                db.ThuHoaches.Add(thuHoach);
                db.SaveChanges();
                return RedirectToAction("Index/"+ Session["Ruong_ID"]);
            }

            ViewBag.Ruong_ID = new SelectList(db.Ruongs, "Ruong_ID", "Ruong_Name", thuHoach.Ruong_ID);
            return View(thuHoach);
        }

        public int autotang(int sl)
        {
            var i = from p in db.ThuHoaches where p.TH_ID == sl select p;
            if (i.Count() >= 1)
            {
                return autotang(sl + 1);
            }
            else return sl;
        }


        // GET: ThuHoaches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuHoach thuHoach = db.ThuHoaches.Find(id);
            if (thuHoach == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ruong_ID = new SelectList(db.Ruongs, "Ruong_ID", "Ruong_Name", thuHoach.Ruong_ID);
            return View(thuHoach);
        }

        // POST: ThuHoaches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TH_ID,Ruong_ID,TH_Name,TH_ChiSo,TH_TienTrenDa,TH_TongTien,TH_MoTa")] ThuHoach thuHoach)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thuHoach).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + Session["Ruong_ID"]);
            }
            ViewBag.Ruong_ID = new SelectList(db.Ruongs, "Ruong_ID", "Ruong_Name", thuHoach.Ruong_ID);
            return View(thuHoach);
        }

        // GET: ThuHoaches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuHoach thuHoach = db.ThuHoaches.Find(id);
            if (thuHoach == null)
            {
                return HttpNotFound();
            }
            return View(thuHoach);
        }

        // POST: ThuHoaches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThuHoach thuHoach = db.ThuHoaches.Find(id);
            db.ThuHoaches.Remove(thuHoach);
            db.SaveChanges();
            return RedirectToAction("Index/" + Session["Ruong_ID"]);
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
