using Newtonsoft.Json;
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
		[Key]
		[Column(Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }
		[Required]
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
        public int IdMascota { get; set; }
        [ForeignKey("IdMascota")]
        [JsonIgnore]
        public Mascota Mascota { get; set; }


	}
}
