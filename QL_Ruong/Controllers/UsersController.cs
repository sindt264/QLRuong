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
    public class UsersController : Controller
    {
        private Datacontext db = new Datacontext();

        // GET: Users
        public ActionResult Index(int id)
        {
            var users = db.Users.Include(u => u.ChiTietUs).Where(u=>u.User_ID == id);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.CTUSER_ID = new SelectList(db.ChiTietUses, "CTUSER_ID", "CTUSER_ID");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_ID,CTUSER_ID,User_Name,User_Detail,User_TK,User_MK")] User user)
        {
            string TK = Request["User_TK"];
            string MK = Request["User_MK"];
            string MK1 = Request["User_MK1"];
            string UserName = Request["User_Name"];
            string TenTK = db.Database.SqlQuery<string>("select User_TK from Users where User_TK ='" + TK + "'").SingleOrDefault();
            if (UserName.Length <= 0)
            {
                ModelState.AddModelError("", "Tên người dùng không được để trống");
            }
            else if(TK.Length <= 0)
            {
                ModelState.AddModelError("", "Tài khoản không được để trống");
            }
            if(TenTK != null)
            {
                ModelState.AddModelError("", "Tài khoản đã tồn tại");
            }else
          
            if(MK.Length==0 || MK1.Length == 0)
            {
                ModelState.AddModelError("", "Mật khẩu không được để trống");
            }
            else if(MK != MK1)
            {
                ModelState.AddModelError("", "Mật khẩu xác nhận không giống nhau !@!");
            }
            if (ModelState.IsValid)
            {
                user.User_ID = autotang(db.Users.Count());
                user.CTUSER_ID = 1;
                user.User_MK = PassMD5.GetMD5(MK);
                db.Users.Add(user);
                db.SaveChanges();
                ModelState.AddModelError("", "Đăng ký thành công !!!");
            }

            ViewBag.CTUSER_ID = new SelectList(db.ChiTietUses, "CTUSER_ID", "CTUSER_ID", user.CTUSER_ID);
            return View(user);
        }

        public int autotang(int sl)
        {
            var i = from p in db.Users where p.User_ID == sl select p;
            if (i.Count() >= 1)
            {
                return autotang(sl + 1);
            }
            else return sl;
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CTUSER_ID = new SelectList(db.ChiTietUses, "CTUSER_ID", "CTUSER_ID", user.CTUSER_ID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_ID,CTUSER_ID,User_Name,User_Detail,User_TK,User_MK")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CTUSER_ID = new SelectList(db.ChiTietUses, "CTUSER_ID", "CTUSER_ID", user.CTUSER_ID);
            return View(user);
        }
        public ActionResult EditMK(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CTUSER_ID = new SelectList(db.ChiTietUses, "CTUSER_ID", "CTUSER_ID", user.CTUSER_ID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditMK([Bind(Include = "User_ID,CTUSER_ID,User_Name,User_Detail,User_TK,User_MK")] User user)
        {
            string MK = Request["User_MK"];
            string MK1 = Request["User_MK1"];
            if (MK.Length <= 0 || MK1.Length<=0)
            {
                ModelState.AddModelError("", "Vui lòng điền mật khẩu và mật khẩu xác nhận");
            }
            else if(MK != MK1)
            {
                ModelState.AddModelError("", "Mậu khẩu xác nhận chưa đúng");
            }
            if (ModelState.IsValid)
            {
                user.User_MK = PassMD5.GetMD5(MK);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index/"+Session["User_ID"]);
            }
            //ViewBag.CTUSER_ID = new SelectList(db.ChiTietUses, "CTUSER_ID", "CTUSER_ID", user.CTUSER_ID);
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
