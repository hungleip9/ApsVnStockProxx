using System.ComponentModel.DataAnnotations.Schema;

namespace VnStockproxx.Models;
public partial class Post
{
    [NotMapped]
    public List<int> ListIdTagInt { get; set; } = new();

    [NotMapped]
    public string CategoryName { get; set; } = null!;

}
