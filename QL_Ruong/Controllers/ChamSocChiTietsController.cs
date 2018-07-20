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
    public class ChamSocChiTietsController : Controller
    {
        private Datacontext db = new Datacontext();

        // GET: ChamSocChiTiets
        public ActionResult Index(int id)
        {
            Session["CS_ID"] = id;
            var chamSocChiTiets = db.ChamSocChiTiets.Include(c => c.ChamSoc).Where(ct=>ct.CS_ID == id).OrderByDescending(ct=>ct.CSCT_Ngay);
            return View(chamSocChiTiets.ToList());
        }

        // GET: ChamSocChiTiets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamSocChiTiet chamSocChiTiet = db.ChamSocChiTiets.Find(id);
            if (chamSocChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(chamSocChiTiet);
        }

        // GET: ChamSocChiTiets/Create
        public ActionResult Create()
        {
            ViewBag.CS_ID = new SelectList(db.ChamSocs, "CS_ID", "CS_Name");
            return View();
        }

        // POST: ChamSocChiTiets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CSCT_ID,CSCT_TenThuoc,CSCT_SoLuong,CSCT_Ngay,CS_ID,CSCT_TongTien")] ChamSocChiTiet chamSocChiTiet)
        {
            if (ModelState.IsValid)
            {
                chamSocChiTiet.CSCT_ID = autotang(db.ChamSocChiTiets.Count());
                chamSocChiTiet.CS_ID = Convert.ToInt32(Session["CS_ID"]);
                db.ChamSocChiTiets.Add(chamSocChiTiet);
                db.SaveChanges();
                return RedirectToAction("Index/"+ Session["CS_ID"]);
            }

            ViewBag.CS_ID = new SelectList(db.ChamSocs, "CS_ID", "CS_Name", chamSocChiTiet.CS_ID);
            return View(chamSocChiTiet);
        }

        public int autotang(int sl)
        {
            var i = from p in db.ChamSocChiTiets where p.CSCT_ID == sl select p;
            if (i.Count() >= 1)
            {
                return autotang(sl + 1);
            }
            else return sl;
        }

        // GET: ChamSocChiTiets/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamSocChiTiet chamSocChiTiet = db.ChamSocChiTiets.Find(id);
            if (chamSocChiTiet == null)
            {
                return HttpNotFound();
            }
            ViewBag.CS_ID = new SelectList(db.ChamSocs, "CS_ID", "CS_Name", chamSocChiTiet.CS_ID);
            return View(chamSocChiTiet);
        }

        // POST: ChamSocChiTiets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CSCT_ID,CSCT_TenThuoc,CSCT_SoLuong,CSCT_Ngay,CS_ID,CSCT_TongTien")] ChamSocChiTiet chamSocChiTiet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chamSocChiTiet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/" + Session["CS_ID"]);
            }
            ViewBag.CS_ID = new SelectList(db.ChamSocs, "CS_ID", "CS_Name", chamSocChiTiet.CS_ID);
            return View(chamSocChiTiet);
        }

        // GET: ChamSocChiTiets/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChamSocChiTiet chamSocChiTiet = db.ChamSocChiTiets.Find(id);
            if (chamSocChiTiet == null)
            {
                return HttpNotFound();
            }
            return View(chamSocChiTiet);
        }

        // POST: ChamSocChiTiets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChamSocChiTiet chamSocChiTiet = db.ChamSocChiTiets.Find(id);
            db.ChamSocChiTiets.Remove(chamSocChiTiet);
            db.SaveChanges();
            return RedirectToAction("Index/" + Session["CS_ID"]);
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
