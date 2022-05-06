using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConnect
{
    public class Repository
    {
        EcomEntities con;
        public Repository()
        {
            con = new EcomEntities();
        }

        public bool AddUser(Registration u)
        {
            con.Registrations.Add(u);
            if (con.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<ImageTable> GetImageData()
        {
            return con.ImageTables.ToList();
        }

        public List<Product> GetProdAll()
        {
            return con.Products.ToList();
        }

        public Product GetProduct(int pid)
        {
            return con.Products.Find(pid);
        }

        public List<Registration> GetRegistration()
        {
            return con.Registrations.ToList();
        }
        public List<Cart> GetAllCartOfUser(int id = 0)
        {
            var ls = (from c in con.Carts where (c.UserID == id) select c).ToList();
            return ls;
        }

        public Registration IndividualDetails(int id)
        {
            return con.Registrations.Find(id);
        }

        public bool AddToCart(Cart c)
        {
            con.Carts.Add(c);
            if (con.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ProductToDb(Product prod, ImageTable img)
        {
            bool flag = false;
            try
            {
                con.Products.Add(prod);
                con.ImageTables.Add(img);
                if (con.SaveChanges() > 0)
                {
                    flag = true;
                }
                return flag;
            }
            catch
            {
                return flag;
            }

        }

        public List<Category> GetCategories()
        {
            return con.Categories.ToList();
        }

        public bool RemoveFromCart(int id, int uid)
        {
            var c = (Cart)(from crt in con.Carts where (crt.ProductID == id && crt.UserID == uid) select crt).First();
            con.Carts.Remove(c);
            if (con.SaveChanges() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ImageTable GetProductIamgeData(int pid)
        {
            var img = (from b in con.ImageTables where b.ProductID == pid select b).ToList();
            return img[0];
        }

        public int GetProductId(string pname)
        {
            var pid = from id in GetProdAll() where (id.productName == pname) select id.ProductID;
            return Convert.ToInt32(pid.ToList()[0]);
        }
    }
}