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
    public class FavoriteConfigurations : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorites");

            builder.HasKey(cv => new { cv.UserId, cv.CarId });

            builder.Property(x => x.CarId)
             .IsRequired();

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasOne(fav => fav.Car)
                .WithMany(car => car.Favorites)
                .HasForeignKey(fav => fav.CarId);

            builder.HasOne(fav => fav.User)
                .WithMany(user => user.Favorites)
                .HasForeignKey(fav => fav.UserId);
        }
    }
}
