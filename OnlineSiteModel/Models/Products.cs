using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSiteModel.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string productName { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<double> price { get; set; }
        public Nullable<double> weight { get; set; }
        public Nullable<int> stock { get; set; }
        public string Description { get; set; }
    }
}