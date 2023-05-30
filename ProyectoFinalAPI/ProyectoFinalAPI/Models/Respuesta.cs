using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Respuesta
{
    public int AplicacionId { get; set; }

    public int ReactivoId { get; set; }

    public int? OpcionId { get; set; }

    public string? EsAcierto { get; set; }

    public DateTime? RespuestaTimeStamp { get; set; }

    public virtual Aplicacione Aplicacion { get; set; } = null!;

    public virtual Opcione? Opcion { get; set; }

    public virtual Reactivo Reactivo { get; set; } = null!;
}
