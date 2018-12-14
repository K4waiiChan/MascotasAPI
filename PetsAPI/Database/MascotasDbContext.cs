using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetsAPI.Models;


namespace PetsAPI.Database
{
    public class MascotasDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        /// <summary>
        /// DataBase initilizador
        /// </summary>
        /// <param name="options"></param>
        public MascotasDbContext(DbContextOptions<MascotasDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>(b =>
            {
                b.ToTable("Users");
                b.HasMany(u => u.UserRoles)
                 .WithOne(ur => ur.User)
                 .HasForeignKey(ur => ur.UserId)
                 .IsRequired();
            });

            builder.Entity<Role>(role =>
            {
                role.ToTable("Roles");
                role.HasKey(r => r.Id);
                role.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
                role.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

                role.Property(u => u.Name).HasMaxLength(256);
                role.Property(u => u.NormalizedName).HasMaxLength(256);

                role.HasMany<UserRole>()
                    .WithOne(ur => ur.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                role.HasMany<IdentityRoleClaim<string>>()
                    .WithOne()
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();
            });

            builder.Entity<IdentityRoleClaim<string>>(roleClaim =>
            {
                roleClaim.HasKey(rc => rc.Id);
                roleClaim.ToTable("RoleClaims");
            });

            builder.Entity<UserRole>(userRole =>
            {
                userRole.ToTable("UserRoles");
                userRole.HasKey(r => new { r.UserId, r.RoleId });
            });

            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");

        }

        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<ClinicaVeterianaria> Clinicas { get; set; }
		public DbSet<SolicitudAdopcion> Solicitudes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
