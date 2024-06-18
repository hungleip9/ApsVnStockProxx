using System;
using System.Collections.Generic;

namespace VnStockproxx.Models;

public partial class Post
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public int? ViewCount { get; set; }

    public int? CateId { get; set; }

    public string? Image { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public virtual Category? Cate { get; set; }

    public virtual ICollection<Tag> IdTag { get; set; } = new List<Tag>();
}
