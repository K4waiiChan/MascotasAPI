using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Models
{
	public class EditarEstadoMascotaModel
	{
		public string Estado { get; set; }
		public int IdMascota { get; set; }
		public int IdSolicitud { get; set; }
	}
}
