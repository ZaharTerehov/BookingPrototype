using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Booking.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableWithPicturePaths : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Apartments");

            migrationBuilder.CreateTable(
                name: "ApartmentPicture",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApartmentId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApartmentPicture", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApartmentPicture_Apartments_ApartmentId",
                        column: x => x.ApartmentId,
                        principalTable: "Apartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ApartmentId",
                table: "Reservations",
                column: "ApartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentPicture_ApartmentId",
                table: "ApartmentPicture",
                column: "ApartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Apartments_ApartmentId",
                table: "Reservations",
                column: "ApartmentId",
                principalTable: "Apartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Apartments_ApartmentId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "ApartmentPicture");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ApartmentId",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Apartments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
