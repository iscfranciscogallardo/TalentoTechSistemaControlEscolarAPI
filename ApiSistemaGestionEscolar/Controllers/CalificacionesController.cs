using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSistemaGestionEscolar.Models;

namespace ApiSistemaGestionEscolar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalificacionesController : ControllerBase
    {
        private readonly SistemaGestionEscolarContext _context;

        public CalificacionesController(SistemaGestionEscolarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calificacione>>> GetCalificaciones()
        {
            return await _context.Calificaciones.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Calificacione>> GetCalificacione(long id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);

            if (calificacion == null)
            {
                return NotFound();
            }

            return calificacion;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalificacione(long id, Calificacione calificacion)
        {
            if (id != calificacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(calificacion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                var alumnomateria = _context.AlumnosMaterias.Where(am => am.Id == calificacion.AlumnoMateriaId).FirstOrDefault();
                var promedio = _context.Calificaciones.Where(c => c.AlumnoMateriaId == calificacion.AlumnoMateriaId).Average(x => x.Calificacion);
                alumnomateria.Promedio = promedio;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacioneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Calificacione>> PostCalificacione(Calificacione calificacion)
        {
            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();
            var alumnomateria = _context.AlumnosMaterias.Where(am => am.Id == calificacion.AlumnoMateriaId).FirstOrDefault();
            var promedio = _context.Calificaciones.Where(c => c.AlumnoMateriaId == calificacion.AlumnoMateriaId).Average(x => x.Calificacion);
            alumnomateria.Promedio = promedio ?? 0;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalificacione(long id)
        {
            var calificacion = await _context.Calificaciones.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }

            _context.Calificaciones.Remove(calificacion);
            await _context.SaveChangesAsync();
            var alumnomateria = _context.AlumnosMaterias.Where(am => am.Id == calificacion.AlumnoMateriaId).FirstOrDefault();
            var promedio = _context.Calificaciones.Where(c => c.AlumnoMateriaId == calificacion.AlumnoMateriaId).Average(x => x.Calificacion);
            alumnomateria.Promedio = promedio ?? 0;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalificacioneExists(long id)
        {
            return _context.Calificaciones.Any(e => e.Id == id);
        }
    }
}
