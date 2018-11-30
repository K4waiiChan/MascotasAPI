using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Models
{
    public class Adoption
    {
		[Key]
		public int Id { get; set; }
		[Required]
		public int Id_mascota_adopcion { set; get; }
		[Required]
		[StringLength(60, ErrorMessage = "Nombre no puede tener mas de 60 caracteres.")]
		public string Nombre { get; set; }
		[Required]
		[StringLength(200, ErrorMessage = "Dirección no puede tener mas de 200 caracteres.")]
		public string Direccion { get; set; }
		[Required]
		[StringLength(15, ErrorMessage = "Ci no puede tener mas de 15 caracteres.")]
		public int Ci { get; set; }
		[Required]
		[StringLength(200, ErrorMessage = "Correo Electrónico no puede tener mas de 200 caracteres.")]
		public string Correo_electronico { get; set; }
		[Required]
		[StringLength(20, ErrorMessage = "Departamento no puede tener mas de 20 caracteres.")]
		public string Departamento { get; set; }
		[Required]
		[StringLength(20, ErrorMessage = "Provincia no puede tener mas de 20 caracteres.")]
		public string Provincia { get; set; }
		[Required]
		[StringLength(30, ErrorMessage = "Ocupación no puede tener mas de 30 caracteres.")]
		public string Ocupacion { get; set; }
		[Required]
		[StringLength(15, ErrorMessage = "Estado civil no puede tener mas de 15 caracteres.")]
		public string Estado_civil { get; set; }
		[Required]
		[StringLength(20, ErrorMessage = "Estado de solicitud no puede tener mas de 20 caracteres.")]
		public string Estado_solicitud { get; set; }
		[Required]
		[StringLength(200, ErrorMessage = "Razon de adopcion no puede tener mas de 200 caracteres.")]
		public string Razon_adopcion { get; set; }
		[Required]
		[StringLength(20, ErrorMessage = "Mascota actual no puede tener mas de 20 caracteres.")]
		public string Mascotas_actuales { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "Razon de mascotas esterilizadas no puede tener mas de 50 caracteres.")]
		public string Razon_mascotas_esterilizadas { get; set; }
		[Required]
		[StringLength(20, ErrorMessage = "Mascota anteriormente no puede tener mas de 20 caracteres.")]
		public string Mascotas_anteriormente { get; set; }
		[Required]
		[StringLength(50, ErrorMessage = "Estado de mascotas anteriores no puede tener mas de 50 caracteres.")]
		public string Estado_mascotas_anteriores { get; set; }
		[Required]
		[StringLength(3, ErrorMessage = "Visita periodica no puede tener mas de 3 caracteres.")]
		public string Visita_periodica_domicilio { get; set; }
		public string Imagen { get; set; }
		public DateTime Creacion_formulacio { get; set; }
	}
}
