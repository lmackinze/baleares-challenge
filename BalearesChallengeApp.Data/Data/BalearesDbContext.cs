using BalearesChallengeApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace BalearesChallengeApp.Data
{
    public class BalearesDbContext : DbContext
    {
        public DbSet<Contact> Contact { get; set; }
        public BalearesDbContext(DbContextOptions<BalearesDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Indexes
            modelBuilder.Entity<Contact>()
                .HasQueryFilter(c => !c.IsDeleted)
                .HasIndex(c => c.Email)
                .IsUnique();
            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();
            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.CityId);

            modelBuilder.Entity<City>()
                .HasIndex(c => c.ProvinceId);

            //Preloaded Data
            modelBuilder.Entity<Province>().HasData(
                new Province
                {
                    Id = 1,
                    Name = "Buenos Aires",
                },
                new Province
                {
                    Id = 2,
                    Name = "Cordoba",
                },
                new Province
                {
                    Id = 3,
                    Name = "Neuquen",
                }
            );

            modelBuilder.Entity<City>().HasData(
                new City
                {
                    Id = 1,
                    Name = "Villa Luzuriaga",
                    ProvinceId = 1
                },
                new City
                {
                    Id = 2,
                    Name = "La Plata",
                    ProvinceId = 1
                },
                new City
                {
                    Id = 3,
                    Name = "Tigre",
                    ProvinceId = 1
                },
                new City
                {
                    Id = 4,
                    Name = "Merlo",
                    ProvinceId = 1
                },
                new City
                {
                    Id = 5,
                    Name = "Cosquin",
                    ProvinceId = 2
                },
                new City
                {
                    Id = 6,
                    Name = "Villa General Belgrano",
                    ProvinceId = 2
                },
                new City
                {
                    Id = 7,
                    Name = "Mina Clavero",
                    ProvinceId = 2
                },
                new City
                {
                    Id = 8,
                    Name = "San Martin De Los Andes",
                    ProvinceId = 3
                },
                new City
                {
                    Id = 9,
                    Name = "Villa La Angostura",
                    ProvinceId = 3
                },
                new City
                {
                    Id = 10,
                    Name = "Centenario",
                    ProvinceId = 3
                }
            );
        }
    }
}
