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
    public class SolicitudAdopcionsController : ControllerBase
    {
        private readonly MascotasDbContext _context;

        public SolicitudAdopcionsController(MascotasDbContext context)
        {
            _context = context;
        }

        // GET: api/SolicitudAdopcions
        [HttpGet]
        public IEnumerable<SolicitudAdopcion> GetSolicitudes(string estado="")
        {
			IEnumerable<SolicitudAdopcion> solicitudesDeAdopcion;
			if (estado != "")
			{
				solicitudesDeAdopcion= from solicitudAdopcion in this._context.Solicitudes where solicitudAdopcion.Estado == estado select solicitudAdopcion;

			}
			else {
				solicitudesDeAdopcion = this._context.Solicitudes;
			}
			return solicitudesDeAdopcion;
		}

        // GET: api/SolicitudAdopcions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSolicitudAdopcion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var solicitudAdopcion = await _context.Solicitudes.FindAsync(id);

            if (solicitudAdopcion == null)
            {
                return NotFound();
            }

            return Ok(solicitudAdopcion);
        }

        // PUT: api/SolicitudAdopcions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSolicitudAdopcion([FromRoute] int id, [FromBody] SolicitudAdopcion solicitudAdopcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != solicitudAdopcion.Id)
            {
                return BadRequest();
            }

            _context.Entry(solicitudAdopcion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SolicitudAdopcionExists(id))
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

        // POST: api/SolicitudAdopcions
        [HttpPost]
        public async Task<IActionResult> PostSolicitudAdopcion([FromBody] SolicitudAdopcion solicitudAdopcion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			solicitudAdopcion.Estado = "creado";
            _context.Solicitudes.Add(solicitudAdopcion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSolicitudAdopcion", new { id = solicitudAdopcion.Id }, solicitudAdopcion);
        }

        // DELETE: api/SolicitudAdopcions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSolicitudAdopcion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var solicitudAdopcion = await _context.Solicitudes.FindAsync(id);
            if (solicitudAdopcion == null)
            {
                return NotFound();
            }

            _context.Solicitudes.Remove(solicitudAdopcion);
            await _context.SaveChangesAsync();

            return Ok(solicitudAdopcion);
        }

        private bool SolicitudAdopcionExists(int id)
        {
            return _context.Solicitudes.Any(e => e.Id == id);
        }
    }
}