using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiSistemaGestionEscolar.Models;
using Microsoft.AspNetCore.Authorization;

namespace ApiSistemaGestionEscolar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly SistemaGestionEscolarContext _context;

        public ProfesoresController(SistemaGestionEscolarContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesore>>> GetProfesores()
        {
            return await _context.Profesores.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Profesore>> GetProfesore(int id)
        {
            var profesore = await _context.Profesores.FindAsync(id);

            if (profesore == null)
            {
                return NotFound();
            }

            return profesore;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesore(int id, Profesore profesor)
        {
            if (id != profesor.Id)
            {
                return BadRequest();
            }

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesoreExists(id))
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
        public async Task<ActionResult<Profesore>> PostProfesore(Profesore profesor)
        {
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfesore", new { id = profesor.Id }, profesor);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesore(int id)
        {
            var profesore = await _context.Profesores.FindAsync(id);
            if (profesore == null)
            {
                return NotFound();
            }

            _context.Profesores.Remove(profesore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfesoreExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
