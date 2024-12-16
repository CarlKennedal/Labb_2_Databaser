﻿using System;
using System.Collections.Generic;

namespace Labb_2_Databaser.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string CustomerName { get; set; } = null!;

    public string? Address { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<SellOrder> SellOrders { get; set; } = new List<SellOrder>();
}
