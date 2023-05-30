using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Configuracione
{
    public string? PasswordGeneral { get; set; }

    /// <summary>
    /// AD2019
    /// EJ2019
    /// AD2020
    /// EJ2020
    /// ...
    /// ..
    /// </summary>
    public string PeriodoActual { get; set; } = null!;
}
