using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddApartmentToApartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApartmentTypeId",
                table: "Apartments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_ApartmentTypeId",
                table: "Apartments",
                column: "ApartmentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_ApartmentTypes_ApartmentTypeId",
                table: "Apartments",
                column: "ApartmentTypeId",
                principalTable: "ApartmentTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_ApartmentTypes_ApartmentTypeId",
                table: "Apartments");

            migrationBuilder.DropIndex(
                name: "IX_Apartments_ApartmentTypeId",
                table: "Apartments");

            migrationBuilder.DropColumn(
                name: "ApartmentTypeId",
                table: "Apartments");
        }
    }
}
