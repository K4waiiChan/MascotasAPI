﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetsAPI.Database;
using PetsAPI.Models;
using PetsAPI.Services;

namespace PetsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudAdopcionsController : ControllerBase
    {
        private readonly MascotasDbContext _context;
        private readonly EmailService emailService;

        public SolicitudAdopcionsController(MascotasDbContext context)
        {
            this.emailService = new EmailService();
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

			solicitudAdopcion.Estado = "creada";
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

        [HttpPut("{id}/estado")]
        public IActionResult PutEstadoSolicitud([FromRoute] int id, [FromBody] EditarEstadoSolicitudModel data)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != data.IdSolicitud)
            {
                return BadRequest();
            }
            try
            {
                SolicitudAdopcion solicitudAdopcion = this._context.Solicitudes.Find(data.IdSolicitud);
                if (data.Estado == "aceptada")
                {
                    this._context.Mascotas.Find(solicitudAdopcion.IdMascota).Estado = "reservada";
                    this.CambiarEstadoDeSolicitudes(solicitudAdopcion.IdMascota, "espera");
                    this.emailService.SendnEmail(solicitudAdopcion.Correo, "Felicidades tu solicitud de adopcion a sido aceptada, la mascota que solicitaste estara reservada para ti durante 1 semana. Recoge a tu nueva mascota lo mas antes posible Saludos.");
                }
                else if (data.Estado == "rechazada" && solicitudAdopcion.Estado == "creada")
                {
                    this.RechazarSolicitud(solicitudAdopcion.Correo);
                }
                else if (data.Estado == "rechazada" && solicitudAdopcion.Estado == "aceptada")
                {
                    this._context.Mascotas.Find(solicitudAdopcion.IdMascota).Estado = "disponible";
                    this.CambiarEstadoDeSolicitudes(solicitudAdopcion.IdMascota, "creada");
                    this.RechazarSolicitud(solicitudAdopcion.Correo);
                }
                else if (data.Estado == "compleatada")
                {
                    this._context.Mascotas.Find(solicitudAdopcion.IdMascota).Estado = "adoptada";
                    this.CambiarEstadoDeSolicitudes(solicitudAdopcion.IdMascota, "rechazada");
                }
                solicitudAdopcion.Estado = data.Estado;
                this._context.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        private void CambiarEstadoDeSolicitudes(int idMascota, string estado)
        {
            foreach (SolicitudAdopcion solicitud in (from solicitud in this._context.Solicitudes where solicitud.IdMascota == idMascota && solicitud.Estado != "rechazada" select solicitud))
            {
                solicitud.Estado = estado;
                if (estado == "rechazada")
                {
                    this.RechazarSolicitud(solicitud.Correo);
                }
            }
        }

        private void RechazarSolicitud(string correo)
        {
            this.emailService.SendnEmail(correo, "Sentimos mucho darte esta noticia pero tu solicitud de adopcion a sido rechazada, quizas llenaste algun campo mal. Intenta mandar una nueva solicitud  o adoptar otra mascotas. Saludos ");
        }
    }
}