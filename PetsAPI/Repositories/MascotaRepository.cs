using PetsAPI.Database;
using PetsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsAPI.Repositories
{
    public class MascotaRepository
    {
        public MascotasDbContext MascotasDbContext { get; set; }

        public MascotaRepository(MascotasDbContext mascotasDbContext)
        {
            this.MascotasDbContext = mascotasDbContext;
        }

        public void Create(Mascota mascota)
        {
            this.MascotasDbContext.Mascotas.Add(mascota);
            this.MascotasDbContext.SaveChanges();
        }

        public IEnumerable<Mascota> GetAll()
        {
            return this.MascotasDbContext.Mascotas.ToList();
        }

        public Mascota GetOne(int id)
        {
            Mascota mascota = this.MascotasDbContext.Mascotas.Find(id);
            if (mascota == null)
            {
                throw new Exception("Mascota con id:" + id + " no encontrado");
            }
            return mascota;
        }

        public void Edit(int id, Mascota newMascota)
        {
            Mascota mascota = this.GetOne(id);
            mascota.Nombre = newMascota.Nombre;
            this.MascotasDbContext.SaveChanges();
        }

        public void delete(int id)
        {
            this.MascotasDbContext.Remove(this.GetOne(id));
            this.MascotasDbContext.SaveChanges();
        }
    }
}
