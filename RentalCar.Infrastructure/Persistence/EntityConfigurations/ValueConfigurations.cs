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
    public class ValueConfigurations : IEntityTypeConfiguration<Value>
    {
        public void Configure(EntityTypeBuilder<Value> builder)
        {
            builder.ToTable("Values");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id)
                .ValueGeneratedOnAdd();

            builder.Property(v => v.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.Property(v => v.CarAttributeId)
                .IsRequired();

            builder.HasOne(v => v.CarAttribute)
                .WithMany(c => c.Values)
                .HasForeignKey(v => v.CarAttributeId);
        }
    }
}
