using System;
using System.Collections.Generic;

namespace Labb_2_Databaser.Models;

public partial class Book
{
    public string Isbn { get; set; } = null!;

    public string Title { get; set; } = null!;

    public decimal? Price { get; set; }

    public int? PublishedYear { get; set; }

    public int? AuthorId { get; set; }

    public int? CategoryId { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Publisher> Publishers { get; set; } = new List<Publisher>();

    public virtual ICollection<SellOrder> SellOrders { get; set; } = new List<SellOrder>();

    public virtual ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();
}
