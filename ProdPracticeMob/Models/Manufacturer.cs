using System;
using System.Collections.Generic;

namespace ProdPracticeMob.Models;

public partial class Manufacturer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Computer> Computers { get; set; } = new List<Computer>();
}
