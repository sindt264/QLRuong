using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QL_Ruong.Models;

namespace QL_Ruong.Controllers
{
    public class LoginController : Controller
    {
        Datacontext db = new Datacontext();

        public ActionResult Index()
        {
            Session["User_ID"] = null;
            Session["Ruong_ID"] = null;
            return View();
        }
            
        [HttpPost]
        public ActionResult Index(string TK,string MK)
        {
             TK = Request["User_TK"];
            MK = Request["User_MK"];
            string MK1 = PassMD5.TakeMD5(MK);
            var result = from p in db.Users where p.User_TK == TK && p.User_MK == MK1 select p;
            if (result.Count() ==1)
            {
                foreach(var item in result)
                {
                    Session["User_ID"] = item.User_ID;
                    Session["User_Level"] = item.CTUSER_ID;
                    
                    return Redirect("/Ruongs/Index/" + item.User_ID);
                }
                
            }
            else
            {
                ModelState.AddModelError("", "TK or MK sai");
            }
            return View();
        }
    }
}