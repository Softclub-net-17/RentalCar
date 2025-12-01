using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCar.Domain.Entities;

namespace RentalCar.Infrastructure.Persistence.EntityConfigurations;

public class VerificationCodeConfigurations: IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.HasKey(vc => vc.Id);

        builder.Property(vc => vc.Id).ValueGeneratedOnAdd();
        builder.Property(vc => vc.Code).IsRequired();
        builder.Property(vc => vc.Expiration).IsRequired();
        builder.Property(vc => vc.NewEmail).IsRequired();
        builder.HasIndex(vc => new { vc.UserId, vc.NewEmail }).IsUnique();
    }
}
