using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DbConnect;

namespace OnlineSiteModel.Controllers
{
    public class AddUserController : Controller
    {
        Repository rep;
        public AddUserController()
        {
            rep = new Repository();
        }
        [HttpGet]
        public ActionResult NewAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewAccount(string uname, int mnumber, string pw, string cpw, string address)
        {
            DbConnect.Registration reg = new DbConnect.Registration()
            {
              UserName=uname,
              Phone=mnumber,
              Password=pw,
              Address=address
            };
            if (cpw != pw)
            {
                ViewBag.FailedRegister = "Password did'nt match";
                return View();
            }
            try
            {
                if (rep.AddUser(reg))
                {
                    ViewBag.FailedRegister = "Successful!";
                    return Redirect("/Register/CreateAccount/");
                }
                else
                {
                    ViewBag.FailedRegister = "Something went wrong..";
                    return View();
                }
            }
            catch(Exception e)
            {
                ViewBag.FailedRegister = e.Message;
                //"Registration Failed!\n Try again.."
                return View();
            }

        }
    }
}