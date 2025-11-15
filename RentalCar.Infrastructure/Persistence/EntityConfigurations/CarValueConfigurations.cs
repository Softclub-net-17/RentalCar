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
    public class CarValueConfigurations : IEntityTypeConfiguration<CarValue>
    {
        public void Configure(EntityTypeBuilder<CarValue> builder)
        {
            builder.ToTable("CarValues");

            builder.HasKey(cv => new { cv.CarId, cv.ValueId });

            builder.Property(x => x.CarId)
                .IsRequired();

            builder.Property(x => x.ValueId)
                .IsRequired();

            builder.HasOne(x => x.Car)
                .WithMany(c => c.CarValues)
                .HasForeignKey(x => x.CarId);

            builder.HasOne(x => x.Value)
               .WithMany(v => v.CarValues)
               .HasForeignKey(x => x.ValueId);


        }
    }
}
