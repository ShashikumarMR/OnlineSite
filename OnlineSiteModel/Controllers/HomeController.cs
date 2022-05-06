using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DbConnect;

namespace OnlineSiteModel.Controllers
{
    public class HomeController : Controller
    {
        Repository rep;
        public HomeController()
        {
            rep = new Repository();
        }
        public ActionResult Index(int PageId=0)
        {
            List< Models.Products > ProdList = new List< Models.Products >();
            int i = 0;
            foreach(var prod in rep.GetProdAll()) {
                var tempProd = Helper.ProdConverter(prod);
                if (i>= PageId*10 && i < (PageId + 1) * 10)
                {
                    ProdList.Add(tempProd);
                }
                i++;
            }
            int j = 0;
            var ls = new List<string>();
            foreach (var img in rep.GetImageData()) {
                string url = Helper.UrlMaker(img);
                if (j >= PageId*10 && j < (PageId + 1) * 10)
                {
                    ls.Add(url);
                }
                j++;
            }
          
          
            ViewBag.urls = ls;
            ViewBag.ProdList = ProdList;
            ViewBag.CategoryList = rep.GetCategories();
            if (Session["uid"] != null)
            {
                Session["CartCount"] = Helper.CartitemCount(rep, Session["uid"]);
            }
            else {
                ViewBag.CartItemCount = 0;
            }
            return View();
        }


        public ActionResult ProdDetail(int Prodid) {
            var prod = rep.GetProduct(Prodid);
            var ProdModel = Helper.ProdConverter(prod);
          
            foreach (var img in rep.GetImageData())
            {
                if (img.ProductID == Prodid)
                {
                    ViewBag.ProdUrl = Helper.UrlMaker(img);
                }
            }

            return View(ProdModel);
        }
        public ActionResult MyCart()
        {
            try
            {
                if (Session["uid"] == null) {
                    return Redirect("/Register/CreateAccount");
                }
                var carts = rep.GetAllCartOfUser(Convert.ToInt32(Session["uid"].ToString()));

                var prods = new List<Models.Products>();
                var urls = new List<string>();
                foreach (var c in carts)
                {
                    prods.Add(Helper.ProdConverter(rep.GetProduct(Convert.ToInt32(c.ProductID))));
                    urls.Add(Helper.UrlMaker(rep.GetProductIamgeData(Convert.ToInt32(c.ProductID))));
                }
                ViewBag.urls = urls;
                ViewBag.ProdList = prods;
                TempData["Cart"] = carts;
            }
            catch {
                return View();
            }

           
            return View();

        }
        [HttpGet]
        public ActionResult AddMyCart(int id) {
            if (Session["uid"] == null) {
                return Redirect("/Home/MyCart");
            }
            var c = new DbConnect.Cart() { ProductID = id, UserID = Convert.ToInt32(Session["uid"].ToString()) };

            Session["CartCount"] = rep.GetAllCartOfUser(Convert.ToInt32(Session["uid"].ToString())).Count();
            if (rep.AddToCart(c))
            {
                TempData["msg"] = "Added to cart";
                Session["CartCount"] = Helper.CartitemCount(rep, Session["uid"]);
            }
            else {
                TempData["msg"] = "Cannot add to cart";
            }
            var url = "/Home/ProdDetail?Prodid=" + Convert.ToString(id);
            return Redirect(url);
        }
        public ActionResult RemoveMyCart(int id) {
            if (rep.RemoveFromCart(id,Convert.ToInt32(Session["uid"])))
            {
                TempData["status"] = true;
                Session["CartCount"] = Helper.CartitemCount(rep, Session["uid"]);
            }
            else {
                TempData["status"] = false;
            }
            return Redirect("/Home/MyCart");
        }
        public ActionResult FiterResult() {
            return View();
        }
    }
    public class Helper
    {
        public static Models.Products ProdConverter(DbConnect.Product prod)
        {
            Models.Products ProdModel = new Models.Products();
            ProdModel.ProductID = prod.ProductID;
            ProdModel.productName = prod.productName;
            ProdModel.weight = prod.weight;
            ProdModel.stock = prod.stock;
            ProdModel.Description = prod.Description;
            ProdModel.CategoryId = prod.CategoryId;
            ProdModel.price = prod.price;
            return ProdModel;
        }
        public static string UrlMaker(DbConnect.ImageTable img)
        {
            byte[] bytes = (byte[])img.image;
            string strBase64 = Convert.ToBase64String(bytes);
            string url = "data:image/png;base64," + strBase64;
            return url;
        }
        public static int CartitemCount(Repository rep,object uid) {
            if (uid != null)
            {
              return rep.GetAllCartOfUser(Convert.ToInt32(uid.ToString())).Count();
            }
            else {
                return 0;
            }
        }
     
    }
}