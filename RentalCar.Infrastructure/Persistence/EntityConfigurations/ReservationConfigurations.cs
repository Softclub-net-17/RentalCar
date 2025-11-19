using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCar.Domain.Entities;

namespace RentalCar.Infrastructure.Persistence.EntityConfigurations
{
    public class ReservationConfigurations : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservations");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();
            
            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.CarId)
                .IsRequired();

            builder.Property(r => r.StartDate)
                .IsRequired();

            builder.Property(r => r.EndDate)
                .IsRequired();

            builder.Property(r => r.TotalPrice)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            builder.HasOne(r => r.Car)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CarId);
        }
    }
}