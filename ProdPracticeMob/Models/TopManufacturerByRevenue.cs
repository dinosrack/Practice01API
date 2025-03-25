using System;
using System.Collections.Generic;

namespace ProdPracticeMob.Models;

public partial class TopManufacturerByRevenue
{
    public string ManufacturerName { get; set; } = null!;

    public decimal? TotalRevenue { get; set; }
}
