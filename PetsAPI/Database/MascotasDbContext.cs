using Microsoft.EntityFrameworkCore;
using PetsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Database
{
    public class MascotasDbContext: DbContext
    {
        /// <summary>
        /// DataBase initilizador
        /// </summary>
        /// <param name="options"></param>
        public MascotasDbContext(DbContextOptions<MascotasDbContext> options) : base(options)
        {

        }

        public DbSet<Mascota> Mascotas { get; set; }

        /// <summary>
        /// DataBase configuration
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
