using System;
using System.Collections.Generic;

namespace VnStockproxx.Models;

public partial class Category
{
    public int Id { get; set; }

    public string? CategoryName { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
