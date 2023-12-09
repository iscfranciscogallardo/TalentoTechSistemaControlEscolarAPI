using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiSistemaGestionEscolar.Models;

public partial class SistemaGestionEscolarContext : DbContext
{
    public SistemaGestionEscolarContext()
    {
    }

    public SistemaGestionEscolarContext(DbContextOptions<SistemaGestionEscolarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<AlumnosMateria> AlumnosMaterias { get; set; }

    public virtual DbSet<Calificacione> Calificaciones { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; DataBase=SistemaGestionEscolar;Integrated Security=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(150);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(150);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(150);
            entity.Property(e => e.Especialidad)
                .HasMaxLength(150)
                .IsFixedLength();
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.PromedioTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Telefono).HasMaxLength(150);
        });

        modelBuilder.Entity<AlumnosMateria>(entity =>
        {
            entity.Property(e => e.Promedio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Resultado).HasMaxLength(50);

            entity.HasOne(d => d.Alumno).WithMany(p => p.AlumnosMateria)
                .HasForeignKey(d => d.AlumnoId)
                .HasConstraintName("FK_AlumnosMaterias_Alumnos");

            entity.HasOne(d => d.Materia).WithMany(p => p.AlumnosMateria)
                .HasForeignKey(d => d.MateriaId)
                .HasConstraintName("FK_AlumnosMaterias_Materias");
        });

        modelBuilder.Entity<Calificacione>(entity =>
        {
            entity.Property(e => e.Calificacion).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Observaciones).HasMaxLength(250);

            entity.HasOne(d => d.AlumnoMateria).WithMany(p => p.Calificaciones)
                .HasForeignKey(d => d.AlumnoMateriaId)
                .HasConstraintName("FK_Calificaciones_AlumnosMaterias");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.Property(e => e.MinimoAprobatorio).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(250);

            entity.HasOne(d => d.Profesor).WithMany(p => p.Materia)
                .HasForeignKey(d => d.ProfesorId)
                .HasConstraintName("FK_Materias_Profesores");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(150);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(150);
            entity.Property(e => e.CorreoElectronico).HasMaxLength(150);
            entity.Property(e => e.Especialidad)
                .HasMaxLength(150)
                .IsFixedLength();
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Telefono).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
