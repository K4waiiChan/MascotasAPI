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
    [ApiController]
    public class MascotasController : ControllerBase
    {
        private readonly MascotasDbContext _context;

        public MascotasController(MascotasDbContext context)
        {
            _context = context;
        }

        // GET: api/Mascotas
        [HttpGet]
        public IEnumerable<Mascota> GetMascotas(string especie = "", string raza = "", string sexo = "", int? edadMax = 100, int? edadMin = 0)
        {

            return from mascota in this._context.Mascotas where mascota.Raza.Contains(raza) && mascota.Especie.Contains(especie) && mascota.Sexo.Contains(sexo) && mascota.Edad >= edadMin && mascota.Edad < edadMax select mascota;
        }

        // GET: api/Mascotas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMascota([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mascota = await _context.Mascotas.FindAsync(id);

            if (mascota == null)
            {
                return NotFound();
            }

            return Ok(mascota);
        }

        // PUT: api/Mascotas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMascota([FromRoute] int id, [FromBody] Mascota mascota)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mascota.Id)
            {
                return BadRequest();
            }

            _context.Entry(mascota).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MascotaExists(id))
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
        public async Task<IActionResult> PostMascota([FromBody] Mascota mascota)
        {
            mascota.Estado = "Disponible";
            mascota.Created_at = DateTime.Today;
            mascota.Updated_at = DateTime.Today;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMascota", new { id = mascota.Id }, mascota);
        }

        // DELETE: api/Mascotas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMascota([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var mascota = await _context.Mascotas.FindAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }

            _context.Mascotas.Remove(mascota);
            await _context.SaveChangesAsync();

            return Ok(mascota);
        }

        private bool MascotaExists(int id)
        {
            return _context.Mascotas.Any(e => e.Id == id);
        }
    }
}