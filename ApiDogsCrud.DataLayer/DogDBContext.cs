using ApiDogsCrud.DataLayer.EnitityConfiguration;
using ApiDogsCrud.DataLayer.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDogsCrud.DataLayer
{
    public class DogDBContext : DbContext
    {
        public DbSet<Dog> Dogs { get; set; }
        public DogDBContext(DbContextOptions<DogDBContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DogConfiguration());
        }
    }
}