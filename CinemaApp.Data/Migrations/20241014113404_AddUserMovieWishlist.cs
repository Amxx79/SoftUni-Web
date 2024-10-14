using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CinemaApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserMovieWishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("1fdcaa64-795f-48b8-aa63-eec779a42c96"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("bbe6acbe-2b16-44c3-9348-2c30ecfb7216"));

            migrationBuilder.DeleteData(
                table: "Cinemas",
                keyColumn: "Id",
                keyValue: new Guid("e6886b2c-6b6f-424e-b9e8-e769223774c9"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("1f4af55d-8d70-40d4-85d4-68b452890769"));

            migrationBuilder.DeleteData(
                table: "Movies",
                keyColumn: "Id",
                keyValue: new Guid("a14d0fab-bf79-4cc3-9f4d-96cce54f56b6"));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.CreateTable(
                name: "ApplicationUserMovie",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MovieId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserMovie", x => new { x.ApplicationUserId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserMovie_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserMovie_Movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserMovie_MovieId",
                table: "ApplicationUserMovie",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserMovie");

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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.InsertData(
                table: "Cinemas",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { new Guid("1fdcaa64-795f-48b8-aa63-eec779a42c96"), "Sofia", "Cinema City" },
                    { new Guid("bbe6acbe-2b16-44c3-9348-2c30ecfb7216"), "Plovdiv", "Cinema City" },
                    { new Guid("e6886b2c-6b6f-424e-b9e8-e769223774c9"), "Varna", "CineMax" }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "Id", "Description", "Director", "Duration", "Genre", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { new Guid("1f4af55d-8d70-40d4-85d4-68b452890769"), "If you wanna be a better rider , you should watch multiple times and practice... There is no cheap advices in Twist of the Wrist ; so scientific and practicable.", "Cheef", 110, "Informative", new DateTime(2009, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "A Twist of the Wrist" },
                    { new Guid("a14d0fab-bf79-4cc3-9f4d-96cce54f56b6"), "When two small-town rival racers are forced to work together to defeat a reigning champion, it becomes survival of the fastest in an ultimate race to the finish line. Stars Brett Davern.", "Alex Ranarivelo", 97, "Sport", new DateTime(2014, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Born to Race" }
                });
        }
    }
}
