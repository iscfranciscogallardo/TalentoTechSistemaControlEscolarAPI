using System;
using System.Collections.Generic;

namespace ApiSistemaGestionEscolar.Models;

public partial class Materia
{
    public int Id { get; set; }

    public int? ProfesorId { get; set; }

    public string? Nombre { get; set; }

    public decimal? MinimoAprobatorio { get; set; }

    public int? Estatus { get; set; }

    public virtual ICollection<AlumnosMateria> AlumnosMateria { get; set; } = new List<AlumnosMateria>();

    public virtual Profesore? Profesor { get; set; }
}
