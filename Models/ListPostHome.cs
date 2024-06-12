namespace VnStockproxx.Models
{
    public class ListPostHome
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public string Image { get; set; } = null!;
        public int ViewCount { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
