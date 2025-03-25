using System;
using System.Collections.Generic;

namespace ProdPracticeMob.Models;

public partial class HighestFrequencyInCalifornium
{
    public string ModelName { get; set; } = null!;

    public decimal Cpufrequency { get; set; }

    public string Location { get; set; } = null!;
}
