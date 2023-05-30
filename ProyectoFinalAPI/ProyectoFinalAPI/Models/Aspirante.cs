using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Aspirante
{
    public int Id { get; set; }

    /// <summary>
    /// AD2019
    /// EJ2019
    /// AD2020
    /// EJ2020
    /// ...
    /// ..
    /// 
    /// </summary>
    public string Periodo { get; set; } = null!;

    public int Folio { get; set; }

    public string Password { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string Sexo { get; set; } = null!;

    public string PreparatoriaProcedencia { get; set; } = null!;

    public string? Bachillerato { get; set; }

    public decimal? PromedioBachillerato { get; set; }

    public string CarreraSolicitada { get; set; } = null!;

    /// <summary>
    /// No se tiene desde SICE, pero se puede incorporar mas adelante para tener la rastreabilidad completa del alumno.
    /// </summary>
    public string? NoControlItsur { get; set; }

    public DateTime? RegTimeStamp { get; set; }

    public int? AplicacionId { get; set; }

    public int? CarClave { get; set; }

    public virtual Aplicacione? Aplicacion { get; set; }
}
