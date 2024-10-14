using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMovieImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("658430f1-8c04-493c-bce3-8f0951e249cb"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("938228be-f1a6-4dd1-aead-a2fb82a53a44"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("e3f91001-c143-4ad9-8ffe-19eff2f10505"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("78a69bb5-0bdb-497d-aa6a-7f4aa23fde73"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a6dfb6b7-83e6-4f99-affd-cb712ad5b23e"));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Movies",
                type: "nvarchar(2083)",
                maxLength: 2083,
                nullable: true,
                defaultValue: "~/images/no-image.jpg");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("387a77c4-68eb-4f58-9a6d-d5d2cfca6710"), "Varna", "CineMax" },
                    { new Guid("8c64d8e9-7e69-4586-8f16-a126b6f929d8"), "Sofia", "Cinema City" },
                    { new Guid("d410b84f-e59b-42ab-9942-bd45ef88975b"), "Plovdiv", "Cinema City" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("0ccbc969-9e1e-4f60-a353-43bb54590040"), "If you wanna be a better rider , you should watch multiple times and practice... There is no cheap advices in Twist of the Wrist ; so scientific and practicable.", "Cheef", 110, "Informative", new DateTime(2009, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Twist of the Wrist" },
                    { new Guid("409cdcb1-b1a8-4f92-8ab3-372263ad9265"), "When two small-town rival racers are forced to work together to defeat a reigning champion, it becomes survival of the fastest in an ultimate race to the finish line. Stars Brett Davern.", "Alex Ranarivelo", 97, "Sport", new DateTime(2014, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Born to Race" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("387a77c4-68eb-4f58-9a6d-d5d2cfca6710"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("8c64d8e9-7e69-4586-8f16-a126b6f929d8"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("d410b84f-e59b-42ab-9942-bd45ef88975b"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("0ccbc969-9e1e-4f60-a353-43bb54590040"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("409cdcb1-b1a8-4f92-8ab3-372263ad9265"));

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Movies");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("658430f1-8c04-493c-bce3-8f0951e249cb"), "Sofia", "Cinema City" },
                    { new Guid("938228be-f1a6-4dd1-aead-a2fb82a53a44"), "Plovdiv", "Cinema City" },
                    { new Guid("e3f91001-c143-4ad9-8ffe-19eff2f10505"), "Varna", "CineMax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("78a69bb5-0bdb-497d-aa6a-7f4aa23fde73"), "When two small-town rival racers are forced to work together to defeat a reigning champion, it becomes survival of the fastest in an ultimate race to the finish line. Stars Brett Davern.", "Alex Ranarivelo", 97, "Sport", new DateTime(2014, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Born to Race" },
                    { new Guid("a6dfb6b7-83e6-4f99-affd-cb712ad5b23e"), "If you wanna be a better rider , you should watch multiple times and practice... There is no cheap advices in Twist of the Wrist ; so scientific and practicable.", "Cheef", 110, "Informative", new DateTime(2009, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Twist of the Wrist" }
                });
        }
    }
}
