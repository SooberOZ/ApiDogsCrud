using ApiDogsCrud.DataLayer.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiDogsCrud.DataLayer.EnitityConfiguration
{
    public class DogConfiguration : IEntityTypeConfiguration<Dog>
    {
        public void Configure(EntityTypeBuilder<Dog> builder)
        {
            builder
                .Property(d => d.Name)
                .IsRequired();

            builder
                .Property(d => d.Color)
                .IsRequired();

            builder
                .Property(d => d.TailLength)
                .IsRequired()
                .HasDefaultValue(0);

            builder
                .Property(d => d.Weight)
                .IsRequired()
                .HasDefaultValue(0);
        }
    }
}