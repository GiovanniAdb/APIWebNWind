using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Vwempleado
{
    public int Clave { get; set; }

    public string? Empleado { get; set; }

    public string? País { get; set; }

    public string? Jefe { get; set; }
}
