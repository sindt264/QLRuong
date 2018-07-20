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
    public class ChamSocsController : Controller
    {
        private Datacontext db = new Datacontext();

        // GET: ChamSocs
        public ActionResult Index(int id)
        {
            Session["Ruong_ID"] = id;
            var chamSocs = db.ChamSocs.Include(c => c.Ruong).Where(c=>c.Ruong_ID== id).OrderByDescending(m=>m.CS_ID);
            return View(chamSocs.ToList());
        }

        // GET: ChamSocs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamSoc chamSoc = db.ChamSocs.Find(id);
            if (chamSoc == null)
            {
                return HttpNotFound();
            }
            return View(chamSoc);
        }

        // GET: ChamSocs/Create
        public ActionResult Create()
        {
            ViewBag.Ruong_ID = new SelectList(db.Ruongs, "Ruong_ID", "Ruong_Name");
            return View();
        }

        // POST: ChamSocs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CS_ID,Ruong_ID,CS_Name,CS_MoTa,CS_Mua")] ChamSoc chamSoc)
        {
            if (ModelState.IsValid)
            {
                chamSoc.CS_ID = autotang(db.ChamSocs.Count());
                chamSoc.Ruong_ID = Convert.ToInt32(Session["Ruong_ID"]);
                db.ChamSocs.Add(chamSoc);
                db.SaveChanges();
                return RedirectToAction("Index/"+ Session["Ruong_ID"]);
            }

            ViewBag.Ruong_ID = new SelectList(db.Ruongs, "Ruong_ID", "Ruong_Name", chamSoc.Ruong_ID);
            return View(chamSoc);
        }

        public int autotang(int sl)
        {
            var i = from p in db.ChamSocs where p.CS_ID == sl select p;
            if (i.Count() >= 1)
            {
                return autotang(sl + 1);
            }
            else return sl;
        }

        // GET: ChamSocs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamSoc chamSoc = db.ChamSocs.Find(id);
            if (chamSoc == null)
            {
                return HttpNotFound();
            }
            ViewBag.Ruong_ID = new SelectList(db.Ruongs, "Ruong_ID", "Ruong_Name", chamSoc.Ruong_ID);
            return View(chamSoc);
        }

        // POST: ChamSocs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CS_ID,Ruong_ID,CS_Name,CS_MoTa,CS_Mua")] ChamSoc chamSoc)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chamSoc).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + Session["Ruong_ID"]);
            }
            ViewBag.Ruong_ID = new SelectList(db.Ruongs, "Ruong_ID", "Ruong_Name", chamSoc.Ruong_ID);
            return View(chamSoc);
        }

        // GET: ChamSocs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamSoc chamSoc = db.ChamSocs.Find(id);
            if (chamSoc == null)
            {
                return HttpNotFound();
            }
            return View(chamSoc);
        }

        // POST: ChamSocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChamSoc chamSoc = db.ChamSocs.Find(id);
            db.ChamSocs.Remove(chamSoc);
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

        public int? tinhtongtien(int id)
        {
            return  db.Database.SqlQuery<int?>("select sum(CSCT_TongTien) as TongTien from ChamSocChiTiet where CS_ID ="+id).SingleOrDefault();
           
        }
    }
}
