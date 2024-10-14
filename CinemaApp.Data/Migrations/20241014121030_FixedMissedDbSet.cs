using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixedMissedDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMovie_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserMovie");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserMovie_Movies_MovieId",
                table: "ApplicationUserMovie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationUserMovie",
                table: "ApplicationUserMovie");

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

            migrationBuilder.RenameTable(
                name: "ApplicationUserMovie",
                newName: "UsersMovies");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserMovie_MovieId",
                table: "UsersMovies",
                newName: "IX_UsersMovies_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersMovies",
                table: "UsersMovies",
                columns: new[] { "ApplicationUserId", "MovieId" });

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("3d1b4a75-85f2-4153-bb43-7f9d4c9b8964"), "Plovdiv", "Cinema City" },
                    { new Guid("c92d1446-3ef6-4e93-9547-e5a7c4da7d8f"), "Sofia", "Cinema City" },
                    { new Guid("e4572d9f-edba-4ed7-9b81-ef720ea485ec"), "Varna", "CineMax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("3da07a9d-1cda-4b81-8fe0-3896d080a498"), "When two small-town rival racers are forced to work together to defeat a reigning champion, it becomes survival of the fastest in an ultimate race to the finish line. Stars Brett Davern.", "Alex Ranarivelo", 97, "Sport", new DateTime(2014, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Born to Race" },
                    { new Guid("c94f8c1f-0398-427a-8f81-3de7b223847a"), "If you wanna be a better rider , you should watch multiple times and practice... There is no cheap advices in Twist of the Wrist ; so scientific and practicable.", "Cheef", 110, "Informative", new DateTime(2009, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Twist of the Wrist" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_AspNetUsers_ApplicationUserId",
                table: "UsersMovies",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersMovies_Movies_MovieId",
                table: "UsersMovies",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_AspNetUsers_ApplicationUserId",
                table: "UsersMovies");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersMovies_Movies_MovieId",
                table: "UsersMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersMovies",
                table: "UsersMovies");

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("3d1b4a75-85f2-4153-bb43-7f9d4c9b8964"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("c92d1446-3ef6-4e93-9547-e5a7c4da7d8f"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("e4572d9f-edba-4ed7-9b81-ef720ea485ec"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("3da07a9d-1cda-4b81-8fe0-3896d080a498"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("c94f8c1f-0398-427a-8f81-3de7b223847a"));

            migrationBuilder.RenameTable(
                name: "UsersMovies",
                newName: "ApplicationUserMovie");

            migrationBuilder.RenameIndex(
                name: "IX_UsersMovies_MovieId",
                table: "ApplicationUserMovie",
                newName: "IX_ApplicationUserMovie_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationUserMovie",
                table: "ApplicationUserMovie",
                columns: new[] { "ApplicationUserId", "MovieId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMovie_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserMovie",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserMovie_Movies_MovieId",
                table: "ApplicationUserMovie",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
