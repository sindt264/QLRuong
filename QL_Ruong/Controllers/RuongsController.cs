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
    public class RuongsController : Controller
    {
        private Datacontext db = new Datacontext();

        // GET: Ruongs
        public ActionResult Index(int id)
        {
            var ruongs = db.Ruongs.Include(r => r.User).Where(r=>r.User_ID == id);
            return View(ruongs.ToList());
        }

        // GET: Ruongs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ruong ruong = db.Ruongs.Find(id);
            if (ruong == null)
            {
                return HttpNotFound();
            }
            return View(ruong);
        }

        // GET: Ruongs/Create
        public ActionResult Create()
        {
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "User_Name");
            return View();
        }

        // POST: Ruongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ruong_ID,User_ID,Ruong_Name,Ruong_DienTich,Ruong_MoTa")] Ruong ruong)
        {
            if (ModelState.IsValid)
            {
                ruong.Ruong_ID = autotang(db.Ruongs.Count());
                ruong.User_ID = Convert.ToInt32(Session["User_ID"]);
                db.Ruongs.Add(ruong);
                db.SaveChanges();
                return RedirectToAction("Index/"+ Session["User_ID"]);
            }

            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "User_Name", ruong.User_ID);
            return View(ruong);
        }

        public int autotang(int sl)
        {
            var i = from p in db.Ruongs where p.Ruong_ID == sl select p;
            if (i.Count() >= 1)
            {
                return autotang(sl + 1);
            }
            else return sl;
        }
        // GET: Ruongs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ruong ruong = db.Ruongs.Find(id);
            if (ruong == null)
            {
                return HttpNotFound();
            }
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "User_Name", ruong.User_ID);
            return View(ruong);
        }

        // POST: Ruongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ruong_ID,User_ID,Ruong_Name,Ruong_DienTich,Ruong_MoTa")] Ruong ruong)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ruong).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + Session["User_ID"]);
            }
            ViewBag.User_ID = new SelectList(db.Users, "User_ID", "User_Name", ruong.User_ID);
            return View(ruong);
        }

        // GET: Ruongs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ruong ruong = db.Ruongs.Find(id);
            if (ruong == null)
            {
                return HttpNotFound();
            }
            return View(ruong);
        }

        // POST: Ruongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ruong ruong = db.Ruongs.Find(id);
            db.Ruongs.Remove(ruong);
            db.SaveChanges();
            return RedirectToAction("Index/" + Session["User_ID"]);
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
