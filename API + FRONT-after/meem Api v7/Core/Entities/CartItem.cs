using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public required string NameEn { get; set; }
        public required string NameAr { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public required string ImageUrl { get; set; }
        public required string CategoryNameEn { get; set; }
        public required string CategoryNameAr { get; set; }
        public required string Size { get; set; }
        public required string Color { get; set; }
    }
}
