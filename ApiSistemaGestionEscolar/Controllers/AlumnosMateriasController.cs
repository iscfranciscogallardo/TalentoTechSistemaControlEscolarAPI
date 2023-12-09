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
    public class AlumnosMateriasController : ControllerBase
    {
        private readonly SistemaGestionEscolarContext _context;

        public AlumnosMateriasController(SistemaGestionEscolarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlumnosMateria>>> GetAlumnosMaterias()
        {
            return await _context.AlumnosMaterias.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AlumnosMateria>> GetAlumnosMateria(long id)
        {
            var alumnosMateria = await _context.AlumnosMaterias.FindAsync(id);

            if (alumnosMateria == null)
            {
                return NotFound();
            }

            return alumnosMateria;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlumnosMateria(long id, AlumnosMateria alumnosMateria)
        {
            if (id != alumnosMateria.Id)
            {
                return BadRequest();
            }

            _context.Entry(alumnosMateria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlumnosMateriaExists(id))
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
        public async Task<ActionResult<AlumnosMateria>> PostAlumnosMateria(AlumnosMateria alumnosMateria)
        {
            _context.AlumnosMaterias.Add(alumnosMateria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlumnosMateria", new { id = alumnosMateria.Id }, alumnosMateria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlumnosMateria(long id)
        {
            var alumnosMateria = await _context.AlumnosMaterias.FindAsync(id);
            if (alumnosMateria == null)
            {
                return NotFound();
            }

            _context.AlumnosMaterias.Remove(alumnosMateria);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlumnosMateriaExists(long id)
        {
            return _context.AlumnosMaterias.Any(e => e.Id == id);
        }
    }
}
