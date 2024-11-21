using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TP2.Migrations
{
    /// <inheritdoc />
    public partial class seedGenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2a5b7ec1-8bce-488c-876d-5e11eb573b1f"), "Action" },
                    { new Guid("3b8c6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"), "Adventure" },
                    { new Guid("4c9b6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"), "Comedy" },
                    { new Guid("5d9b6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"), "Crime" },
                    { new Guid("6e9b6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"), "Drama" },
                    { new Guid("7f9b6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"), "Fantasy" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2a5b7ec1-8bce-488c-876d-5e11eb573b1f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3b8c6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4c9b6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5d9b6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("6e9b6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7f9b6f4c-4b7b-4f4b-8b5c-5e11eb573b1f"));
        }
    }
}
