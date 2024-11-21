using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TP2.Migrations
{
    /// <inheritdoc />
    public partial class AddAddedDateAndPhotoToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AddedDate",
                table: "Movies",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Movies",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddedDate",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Movies");
        }
    }
}
