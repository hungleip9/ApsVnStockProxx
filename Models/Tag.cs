using System;
using System.Collections.Generic;

namespace VnStockproxx.Models;

public partial class Tag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Post> IdPost { get; set; } = new List<Post>();
}
