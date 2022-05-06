using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineSiteModel.Models
{
    public class images
    {
        public int ImgId { get; set; }
        public string image_name { get; set; }
        public byte[] image { get; set; }
        public Nullable<int> ProductID { get; set; }
    }
}