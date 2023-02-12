using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRenameArrDepDateInReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureDateTime",
                table: "Reservations",
                newName: "DepartureDate");

            migrationBuilder.RenameColumn(
                name: "ArrivalDateTime",
                table: "Reservations",
                newName: "ArrivalDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DepartureDate",
                table: "Reservations",
                newName: "DepartureDateTime");

            migrationBuilder.RenameColumn(
                name: "ArrivalDate",
                table: "Reservations",
                newName: "ArrivalDateTime");
        }
    }
}
