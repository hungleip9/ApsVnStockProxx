using System;
using System.Collections.Generic;

namespace VnStockproxx.Models;

public partial class Post
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public string? Teaser { get; set; }

    public string? ViewCount { get; set; }

    public int? CateId { get; set; }

    public string Image { get; set; } = null!;

    public virtual Category? Cate { get; set; }
}
