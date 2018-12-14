using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsAPI.Database;
using PetsAPI.Models;


namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClinicasVeterinariasController : Controller
    {
        private readonly MascotasDbContext _context;

        public ClinicasVeterinariasController(MascotasDbContext context)
        {
            _context = context;
        }

        // GET: api/Mascotas
        [HttpGet]
        public IEnumerable<ClinicaVeterianaria> GetClinicasVeterianarias()
        {
            return this._context.Clinicas;

        }

        // GET: api/Mascotas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClinicaVeterinaria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clinica = await _context.Clinicas.FindAsync(id);

            if (clinica == null)
            {
                return NotFound();
            }

            return Ok(clinica);
        }

        // PUT: api/Mascotas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinicaVeterinaria([FromRoute] int id, [FromBody] ClinicaVeterianaria clinica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != clinica.Id)
            {
                return BadRequest();
            }

            _context.Entry(clinica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClinicaExists(id))
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

        // POST: api/Mascotas
        [HttpPost]
        public async Task<IActionResult> PostClinica([FromBody] ClinicaVeterianaria clinica)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Clinicas.Add(clinica);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMascota", new { id = clinica.Id }, clinica);
        }

        // DELETE: api/Mascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClinica([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var clinica = await _context.Clinicas.FindAsync(id);
            if (clinica == null)
            {
                return NotFound();
            }

            _context.Clinicas.Remove(clinica);
            await _context.SaveChangesAsync();

            return Ok(clinica);
        }

        private bool ClinicaExists(int id)
        {
            return _context.Clinicas.Any(e => e.Id == id);
        }

    }
}
