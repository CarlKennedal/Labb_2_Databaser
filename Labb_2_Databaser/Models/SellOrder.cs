using System;
using System.Collections.Generic;

namespace Labb_2_Databaser.Models;

public partial class SellOrder
{
    public int SellOrderId { get; set; }

    public int StoreId { get; set; }

    public string Isbn { get; set; } = null!;

    public int CustomerId { get; set; }

    public DateOnly OrderDate { get; set; }

    public int Quantity { get; set; }

    public decimal TotalPrice { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
