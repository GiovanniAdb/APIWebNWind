using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Reactivo
{
    public int Id { get; set; }

    public int CategoriaId { get; set; }

    public int ReactivoBancoId { get; set; }

    /// <summary>
    /// Peso en puntos, del reactivo, por default 1, para que todos los reactivos tengan el mismo peso.
    /// </summary>
    public int Ponderacion { get; set; }

    public virtual BancoReactivo ReactivoBanco { get; set; } = null!;

    public virtual ICollection<Respuesta> Respuesta { get; set; } = new List<Respuesta>();
}
