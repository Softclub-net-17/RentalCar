using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class somechangesmadeintableverificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "VerificationCodes",
                newName: "NewEmail");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationCodes_Email",
                table: "VerificationCodes",
                newName: "IX_VerificationCodes_NewEmail");

            migrationBuilder.AddColumn<string>(
                name: "CodeHash",
                table: "VerificationCodes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "VerificationCodes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "VerificationCodes",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodeHash",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "VerificationCodes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "VerificationCodes");

            migrationBuilder.RenameColumn(
                name: "NewEmail",
                table: "VerificationCodes",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_VerificationCodes_NewEmail",
                table: "VerificationCodes",
                newName: "IX_VerificationCodes_Email");
        }
    }
}
