using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalCar.Infrastructure.Persistence.EntityConfigurations
{
    public class ImageConfigurations : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

            builder.Property(i => i.PhotoUrl)
                .IsRequired();

            builder.Property(i => i.CarId)
                .IsRequired();

            builder.HasOne(i => i.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(i => i.CarId);

            
        }
    }
}
