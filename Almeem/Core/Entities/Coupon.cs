namespace Core.Entities
{
    public class Coupon : Discount
    {
        public int? MaxUsageCount { get; set; } // Maximum times a coupon can be used
        public int CurrentUsageCount { get; set; } // Track how many times it has been used
    }
}
