using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentalCar.Domain.Entities;

namespace RentalCar.Infrastructure.Persistence.EntityConfigurations;

public class VerificationCodeConfigurations: IEntityTypeConfiguration<VerificationCode>
{
    public void Configure(EntityTypeBuilder<VerificationCode> builder)
    {
        builder.HasKey(verificationCode => verificationCode.Id);
        builder.Property(verificationCode => verificationCode.Id).ValueGeneratedOnAdd();
        builder.Property(verificationCode => verificationCode.Code).IsRequired();
        builder.Property(verificationCode => verificationCode.Expiration).IsRequired();
        builder.Property(verificationCode => verificationCode.Email).IsRequired();
        builder.HasIndex(verificationCode => verificationCode.Email).IsUnique();
    }
}
