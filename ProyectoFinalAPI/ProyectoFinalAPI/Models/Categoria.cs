using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Categoria
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    /// <summary>
    /// En segundos
    /// </summary>
    public int? TiempoEjecucion { get; set; }

    public int ExamenId { get; set; }

    public virtual Examene Examen { get; set; } = null!;
}
