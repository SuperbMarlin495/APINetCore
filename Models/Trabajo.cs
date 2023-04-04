using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace APICoreWeb.Models;

public partial class Trabajo
{
    public int IdTrabajo { get; set; }

    public string? Empresa { get; set; }

    public string? Turno { get; set; }

    public string? Puesto { get; set; }

    public int? IdInformacion { get; set; }

    [JsonIgnore]
    public virtual Informacion? oTrabajo { get; set; }
}
