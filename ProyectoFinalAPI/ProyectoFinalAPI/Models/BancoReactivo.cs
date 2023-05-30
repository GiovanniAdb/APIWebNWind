using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class BancoReactivo
{
    public int Id { get; set; }

    public string Texto { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public string? CategoriasSugeridas { get; set; }

    public virtual ICollection<Opcione> Opciones { get; set; } = new List<Opcione>();

    public virtual ICollection<Reactivo> Reactivos { get; set; } = new List<Reactivo>();
}
