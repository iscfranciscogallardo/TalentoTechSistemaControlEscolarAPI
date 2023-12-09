using System;
using System.Collections.Generic;

namespace ApiSistemaGestionEscolar.Models;

public partial class Alumno
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Especialidad { get; set; }

    public string? CorreoElectronico { get; set; }

    public string? Telefono { get; set; }

    public decimal? PromedioTotal { get; set; }

    public int? Estatus { get; set; }

    public virtual ICollection<AlumnosMateria> AlumnosMateria { get; set; } = new List<AlumnosMateria>();
}
