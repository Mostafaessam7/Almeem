namespace AlmeemDashboard.Models
{
    public class Pagination<Product>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<Product> Data { get; set; }
    }
}
