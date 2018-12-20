using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PetsAPI.Models
{
    public class ClinicaVeterianaria
    {
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Especialidades { get; set; }
        [Required]
        public int Telefono { get; set; }
        [Required]
        public string Horario { get; set; }
        [Required]
        public string Dias { get; set; }
        public string Logo { get; set; }
    }
}
