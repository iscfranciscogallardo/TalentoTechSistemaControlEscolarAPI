using System;
using System.Collections.Generic;

namespace ApiSistemaGestionEscolar.Models;

public partial class Calificacione
{
    public long Id { get; set; }

    public long? AlumnoMateriaId { get; set; }

    public decimal? Calificacion { get; set; }

    public string? Observaciones { get; set; }

    public virtual AlumnosMateria? AlumnoMateria { get; set; }
}
