using System;
using System.Collections.Generic;

namespace Labb_2_Databaser.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? City { get; set; }

    public virtual ICollection<SellOrder> SellOrders { get; set; } = new List<SellOrder>();

    public virtual ICollection<StockBalance> StockBalances { get; set; } = new List<StockBalance>();
}
