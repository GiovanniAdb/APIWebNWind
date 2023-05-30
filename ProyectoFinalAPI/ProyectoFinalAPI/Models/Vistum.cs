using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Vistum
{
    public int Productid { get; set; }

    public string Productname { get; set; } = null!;

    public decimal? Unitprice { get; set; }

    public string Companyname { get; set; } = null!;

    public string Categoryname { get; set; } = null!;
}
