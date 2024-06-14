using System;
using System.Collections.Generic;

namespace VnStockproxx.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NameMap { get; set; }

    public virtual ICollection<Post> Post { get; set; } = new List<Post>();
}
