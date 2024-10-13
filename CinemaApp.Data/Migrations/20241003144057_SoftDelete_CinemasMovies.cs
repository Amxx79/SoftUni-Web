using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SoftDelete_CinemasMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("962385ae-a1d6-47d3-841c-6284cbd1e8b9"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("9758e9d3-19bc-4dc6-b3f6-391b701a2099"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("9b2b936a-d1cc-48a2-a15b-a7a9a0dbf726"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("96a76267-65d5-4789-b594-20b3ceab5adf"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("ed95aabf-d784-435a-bc78-ca5f542f3c0e"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CinemasMovies",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("2b843783-6a8c-4ac7-91bd-8b0e375e6511"), "Plovdiv", "Cinema City" },
                    { new Guid("2e26266e-05b3-49fb-a0ce-b98b86309a1a"), "Varna", "CineMax" },
                    { new Guid("750ce83a-91c7-4f39-bb76-40cc641817f0"), "Sofia", "Cinema City" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("67f4669f-c2cd-4eb0-ae63-0f6cd20bff1d"), "If you wanna be a better rider , you should watch multiple times and practice... There is no cheap advices in Twist of the Wrist ; so scientific and practicable.", "Cheef", 110, "Informative", new DateTime(2009, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Twist of the Wrist" },
                    { new Guid("a2594da9-4ae6-460e-8fdf-49d1c866d0e0"), "When two small-town rival racers are forced to work together to defeat a reigning champion, it becomes survival of the fastest in an ultimate race to the finish line. Stars Brett Davern.", "Alex Ranarivelo", 97, "Sport", new DateTime(2014, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Born to Race" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("2b843783-6a8c-4ac7-91bd-8b0e375e6511"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("2e26266e-05b3-49fb-a0ce-b98b86309a1a"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("750ce83a-91c7-4f39-bb76-40cc641817f0"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("67f4669f-c2cd-4eb0-ae63-0f6cd20bff1d"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a2594da9-4ae6-460e-8fdf-49d1c866d0e0"));

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CinemasMovies");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("962385ae-a1d6-47d3-841c-6284cbd1e8b9"), "Plovdiv", "Cinema City" },
                    { new Guid("9758e9d3-19bc-4dc6-b3f6-391b701a2099"), "Varna", "CineMax" },
                    { new Guid("9b2b936a-d1cc-48a2-a15b-a7a9a0dbf726"), "Sofia", "Cinema City" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("96a76267-65d5-4789-b594-20b3ceab5adf"), "If you wanna be a better rider , you should watch multiple times and practice... There is no cheap advices in Twist of the Wrist ; so scientific and practicable.", "Cheef", 110, "Informative", new DateTime(2009, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Twist of the Wrist" },
                    { new Guid("ed95aabf-d784-435a-bc78-ca5f542f3c0e"), "When two small-town rival racers are forced to work together to defeat a reigning champion, it becomes survival of the fastest in an ultimate race to the finish line. Stars Brett Davern.", "Alex Ranarivelo", 97, "Sport", new DateTime(2014, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Born to Race" }
                });
        }
    }
}
