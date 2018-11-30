using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Models
{
	public class SolicitudAdopcion
	{

		/*  'nombre': '',
        'cedula_identidad': '',
        'departamento': '',
        'provincia': '',
        'direccion': '',
        'ocupacion': '',
        'estado_civil': '',
        'estado_solicitud': '',
        'razon_adopcion': '',
        'mascotas_actuales': '',
        'razon_mascotas_esterilizadas': '',
        'mascotas_anteriomente': '',
        'estado_mascotas_anteriores': '',
        'visita_periodica_domicilio': '',
		*/


		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "Nombre no puede tener mas de 50 caracteres.")]
		public string Nombre { get; set; }
		[Required]
		public string CedulaIdentidad { get; set; }
		[Required]
		public string Correo { get; set; }
		public string Departamento { get; set; }
		public string Provincia { get; set; }
		public string Direccion { get; set; }
		public string Ocupacion { get; set; }
		public string EstadoCivil { get; set; }
		public string EstadoSolicitud { get; set; }
		public string RazonAdopcion { get; set; }
		public string MascotasActuales { get; set; }
		public string RazonMascotasEsterilizadas { get; set; }
		public string MascotasAnteriormente { get; set; }
		public string EstadoMascotasAnteriores { get; set; }
		public string VisitaPeriodicaDomicilio { get; set; }
		public string Estado { get; set; }



	}
}
