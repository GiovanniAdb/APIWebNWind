using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class ResultadoDetallado
{
    public int Folio { get; set; }

    public string Nombre { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string ApellidoMaterno { get; set; } = null!;

    public string PreparatoriaProcedencia { get; set; } = null!;

    public float? PuntuacionTotal { get; set; }

    public double? Aritmetica { get; set; }

    public double? Algebra { get; set; }

    public double? Perfil { get; set; }

    public double? ComprensionLectora { get; set; }
}
