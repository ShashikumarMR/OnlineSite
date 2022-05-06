using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.IO;
using DbConnect;

namespace OnlineSiteModel.Controllers
{
    public class AdminController : Controller
    {
        Repository rep;
        public AdminController()
        {
            rep = new Repository();  
        }
        // GET: Admin
        public ActionResult ProductAdd()
        {
            ViewBag.Category = rep.GetCategories();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(string pname, Nullable<double> price, Nullable<int> Category, Nullable<double> weight, Nullable<int> stock, string desc,string ImgPath)
        {
            ViewBag.Category = rep.GetCategories();
            var prod = new DbConnect.Product() {
                productName = pname,
                price = price,
                CategoryId = Category,
                weight = weight,
                stock = stock,
                Description = desc,
            };

            Image img = Image.FromFile(@"C:\Users\ADMIN\Downloads\iimage\OIP.jfif");
            var Img = new DbConnect.ImageTable();
            Img.image_name = pname;
            Img.ProductID = rep.GetProductId(pname);
            using (var ms = new MemoryStream()) {
                img.Save(ms, img.RawFormat);
                Img.image = ms.ToArray();
            }

            if (rep.ProductToDb(prod, Img))
            {
                ViewBag.status = true;
            }
            else {
                ViewBag.status = false;
            }
                return View();
        }
    }
}