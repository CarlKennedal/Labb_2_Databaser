using System;
using System.Collections.Generic;

namespace Labb_2_Databaser.Models;

public partial class TitlesPerAuthor
{
    public string? Name { get; set; }

    public int? Age { get; set; }

    public int? Titles { get; set; }

    public decimal? StockValue { get; set; }
}
