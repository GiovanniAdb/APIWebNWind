using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Aplicacione
{
    public int Id { get; set; }

    public int ExamenId { get; set; }

    public string Finalizado { get; set; } = null!;

    public DateTime? FechaFinalizacion { get; set; }

    public float? PuntuacionTotal { get; set; }

    public DateTime? RegTimeStamp { get; set; }

    public int? UltimoReactivoAbiertoId { get; set; }

    public virtual ICollection<Aspirante> Aspirantes { get; set; } = new List<Aspirante>();

    public virtual Examene Examen { get; set; } = null!;

    public virtual ICollection<PuntuacionPorCategorium> PuntuacionPorCategoria { get; set; } = new List<PuntuacionPorCategorium>();

    public virtual ICollection<Respuesta> Respuesta { get; set; } = new List<Respuesta>();
}
