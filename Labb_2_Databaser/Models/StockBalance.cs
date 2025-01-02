using System;
using System.Collections.Generic;

namespace Labb_2_Databaser.Models;

public partial class StockBalance
{
    public int StoreId { get; set; }

    public string ISBN { get; set; } = null!;

    public int Quantity { get; set; }

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
