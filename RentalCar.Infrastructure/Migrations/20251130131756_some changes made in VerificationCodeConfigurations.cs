using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class somechangesmadeinVerificationCodeConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VerificationCodes_NewEmail",
                table: "VerificationCodes");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCodes_UserId_NewEmail",
                table: "VerificationCodes",
                columns: new[] { "UserId", "NewEmail" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VerificationCodes_UserId_NewEmail",
                table: "VerificationCodes");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationCodes_NewEmail",
                table: "VerificationCodes",
                column: "NewEmail",
                unique: true);
        }
    }
}
