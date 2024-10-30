using System.ComponentModel.DataAnnotations;

namespace AlmeemDashboard.Models
{
    public class ProductVariant
    {
        [Required]
        public string Size { get; set; } = string.Empty;

        [Required]
        public string Color { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock must be non-negative")]
        public int QuantityInStock { get; set; }

    }
}
