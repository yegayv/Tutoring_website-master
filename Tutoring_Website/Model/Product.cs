using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tutoring_Website.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string VendorId { get; set; }
        public string ProductName { get; set; }
        public ProductInfo ProductInfo { get; set; }
    }

}
