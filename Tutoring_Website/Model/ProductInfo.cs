using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tutoring_Website.Model
{
    public class ProductInfo
    {
      
        public int ProductId { get; set; }
        public string ProductDescription { get; set; }
        public int ProductPrice { get; set; }
        public string ProductImg { get; set; }
        public double? ProductRating { get; set; }
        public DateTime DateAdded { get; set; }
        public int? RatingCount { get; set; }
    }
}
