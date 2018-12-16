using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsAPI.Database;
using PetsAPI.Models;

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicasVeterianariasController : ControllerBase
    {
        private readonly MascotasDbContext _context;

        public ClinicasVeterianariasController(MascotasDbContext context)
        {
            _context = context;
        }

        // GET: api/ClinicasVeterianarias
        [HttpGet]
        public IEnumerable<ClinicaVeterianaria> GetClinicas()
        {
            return _context.Clinicas;
        }

        // GET: api/ClinicasVeterianarias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClinicaVeterianaria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clinicaVeterianaria = await _context.Clinicas.FindAsync(id);

            if (clinicaVeterianaria == null)
            {
                return NotFound();
            }

            return Ok(clinicaVeterianaria);
        }

        // PUT: api/ClinicasVeterianarias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinicaVeterianaria([FromRoute] int id, [FromBody] ClinicaVeterianaria clinicaVeterianaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clinicaVeterianaria.Id)
            {
                return BadRequest();
            }

            _context.Entry(clinicaVeterianaria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicaVeterianariaExists(id))
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

        // POST: api/ClinicasVeterianarias
        [HttpPost]
        public async Task<IActionResult> PostClinicaVeterianaria([FromBody] ClinicaVeterianaria clinicaVeterianaria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clinicas.Add(clinicaVeterianaria);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClinicaVeterianaria", new { id = clinicaVeterianaria.Id }, clinicaVeterianaria);
        }

        // DELETE: api/ClinicasVeterianarias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinicaVeterianaria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clinicaVeterianaria = await _context.Clinicas.FindAsync(id);
            if (clinicaVeterianaria == null)
            {
                return NotFound();
            }

            _context.Clinicas.Remove(clinicaVeterianaria);
            await _context.SaveChangesAsync();

            return Ok(clinicaVeterianaria);
        }

        private bool ClinicaVeterianariaExists(int id)
        {
            return _context.Clinicas.Any(e => e.Id == id);
        }
    }
}