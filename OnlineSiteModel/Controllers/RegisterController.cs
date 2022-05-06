using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbConnect;

namespace OnlineSiteModel.Controllers
{
    public class RegisterController : Controller
    {
        Repository rep;
        public RegisterController()
        {
            rep = new Repository(); 
        }
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount(string uname,string pw)
        {
            var reg = rep.GetRegistration();
            bool flag = false;
            foreach(var r in reg)
            {
                if (r.UserName == uname && r.Password == pw) {
                    flag = true;
                    Session["uid"] = r.UserID;
                    break;
                        }
            }
            if (flag)
            {
                Session["uname"] = uname;
 
                return Redirect("/Home/index");
            }
            else
            {
                ViewBag.FailedLogin = "login failed";
                return View();
            }
            
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return Redirect("/Home/index");
        }

        public ActionResult AccountDetail() {
            var reg = rep.IndividualDetails(Convert.ToInt32(Session["uid"].ToString()));
            ViewBag.reg = reg;
            return View();
        }
    }
}