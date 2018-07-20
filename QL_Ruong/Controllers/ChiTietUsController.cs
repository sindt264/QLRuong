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
    public class ChiTietUsController : Controller
    {
        private Datacontext db = new Datacontext();

        // GET: ChiTietUs
        public ActionResult Index()
        {
            return View(db.ChiTietUses.ToList());
        }

        // GET: ChiTietUs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietUs chiTietUs = db.ChiTietUses.Find(id);
            if (chiTietUs == null)
            {
                return HttpNotFound();
            }
            return View(chiTietUs);
        }

        // GET: ChiTietUs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ChiTietUs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CTUSER_ID,CTUSER_LEVEL,CTUSER_TRANGTHAI")] ChiTietUs chiTietUs)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietUses.Add(chiTietUs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(chiTietUs);
        }

        // GET: ChiTietUs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietUs chiTietUs = db.ChiTietUses.Find(id);
            if (chiTietUs == null)
            {
                return HttpNotFound();
            }
            return View(chiTietUs);
        }

        // POST: ChiTietUs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CTUSER_ID,CTUSER_LEVEL,CTUSER_TRANGTHAI")] ChiTietUs chiTietUs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietUs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(chiTietUs);
        }

        // GET: ChiTietUs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietUs chiTietUs = db.ChiTietUses.Find(id);
            if (chiTietUs == null)
            {
                return HttpNotFound();
            }
            return View(chiTietUs);
        }

        // POST: ChiTietUs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            ChiTietUs chiTietUs = db.ChiTietUses.Find(id);
            db.ChiTietUses.Remove(chiTietUs);
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
