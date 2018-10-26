using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Models
{
    public class Mascota
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Nombre no puede tener mas de 50 caracteres.")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Raza no puede tener mas de 50 caracteres.")]
        public string Raza { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Especie no puede tener mas de 50 caracteres.")]
        public string Especie { get; set; }
        [Required]
		[StringLength(50, ErrorMessage = "Edad no puede tener mas de 50 caracteres.")]
		public string Edad { get; set; }
        [Required]
		[StringLength(50, ErrorMessage = "Sexo no puede tener mas de 50 caracteres.")]
		public string Sexo { get; set; }
        [Required]
        public string Enfermedades { get; set; }
        public string Descripcion { get; set; }
        public string Procedencia { get; set; }
		[Required]
		[DataType(DataType.ImageUrl)]
		public string Imagen { get; set; }
        public bool Estado { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
    }
}
