using System;
using System.Collections.Generic;

namespace ApiSistemaGestionEscolar.Models;

public partial class AlumnosMateria
{
    public long Id { get; set; }

    public int? AlumnoId { get; set; }

    public int? MateriaId { get; set; }

    public decimal? Promedio { get; set; }

    public string? Resultado { get; set; }

    public virtual Alumno? Alumno { get; set; }

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();

    public virtual Materia? Materia { get; set; }
}
