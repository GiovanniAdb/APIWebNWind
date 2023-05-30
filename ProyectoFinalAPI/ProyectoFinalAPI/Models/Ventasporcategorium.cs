using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Ventasporcategorium
{
    public string Categoryname { get; set; } = null!;

    public decimal? SumVentasporproducto { get; set; }
}
