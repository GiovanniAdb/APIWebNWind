using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Ventasproducto1997
{
    public int? Productid { get; set; }

    public string? Productname { get; set; }

    public string Categoryname { get; set; } = null!;

    public decimal? VentasPorProducto { get; set; }
}
