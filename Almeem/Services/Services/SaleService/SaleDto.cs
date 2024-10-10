using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.SaleService
{
    public class SaleDto
    {
        public int Id { get; set; }  // Maps to the BaseEntity Id
        public required string Name { get; set; }  // Sale Name
        public decimal DiscountPercentage { get; set; }  // Discount
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsGlobal { get; set; }

        // List of related Product Names (simplified view) delete this
       // public List<string>? ProductNames { get; set; }

        // List of related Category Names (simplified view)
       /// public List<string>? CategoryNames { get; set; }
    }
}
