using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Examene
{
    public int Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public sbyte? CarClave { get; set; }

    public string? Activo { get; set; }

    public DateTime RegTimeStamp { get; set; }

    public virtual ICollection<Aplicacione> Aplicaciones { get; set; } = new List<Aplicacione>();

    public virtual ICollection<Categoria> Categoria { get; set; } = new List<Categoria>();
}
