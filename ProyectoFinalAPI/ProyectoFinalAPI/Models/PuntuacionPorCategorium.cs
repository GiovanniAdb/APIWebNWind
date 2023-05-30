using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class PuntuacionPorCategorium
{
    public int CategoriaId { get; set; }

    public int AplicacionId { get; set; }

    public float? Puntuacion { get; set; }

    /// <summary>
    /// En segundos
    /// </summary>
    public int TiempoEjecucionRestante { get; set; }

    public DateTime? RegTimeStamp { get; set; }

    public virtual Aplicacione Aplicacion { get; set; } = null!;
}
