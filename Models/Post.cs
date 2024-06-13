namespace VnStockproxx.Models;
public partial class Post
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string Content { get; set; } = null!;

    public string ImageContent { get; set; } = null!;

    public int ViewCount { get; set; }

    public int? CateId { get; set; }

    public string Image { get; set; } = null!;

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual ICollection<Tag> IdTag { get; set; } = new List<Tag>();
}
