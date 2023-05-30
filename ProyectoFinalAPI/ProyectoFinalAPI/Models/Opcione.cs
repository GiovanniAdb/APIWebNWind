using System;
using System.Collections.Generic;

namespace ProyectoFinalAPI.Models;

public partial class Opcione
{
    public int Id { get; set; }

    public string Texto { get; set; } = null!;

    public string EsCorrecta { get; set; } = null!;

    public int ReactivoBancoId { get; set; }

    public virtual BancoReactivo ReactivoBanco { get; set; } = null!;

    public virtual ICollection<Respuesta> Respuesta { get; set; } = new List<Respuesta>();
}
