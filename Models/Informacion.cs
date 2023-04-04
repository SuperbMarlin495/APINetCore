using System;
using System.Collections.Generic;

namespace APICoreWeb.Models;

public partial class Informacion
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public virtual ICollection<Trabajo> Trabajos { get; } = new List<Trabajo>();
}
