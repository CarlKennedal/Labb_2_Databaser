using System;
using System.Collections.Generic;

namespace Labb_2_Databaser.Models;

public partial class PurchaseOrder
{
    public int PurchaseOrderId { get; set; }

    public int StoreId { get; set; }

    public int PublisherId { get; set; }

    public string ISBN { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public int Quantity { get; set; }

    public virtual Book IsbnNavigation { get; set; } = null!;

    public virtual Publisher Publisher { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
