using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class address_refactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.AuthorId);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    PostalCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.PostalCode);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    JMBG = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.JMBG);
                });

            migrationBuilder.CreateTable(
                name: "Rents",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rents", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Isbn = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    AuthorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    InStorage = table.Column<int>(type: "int", nullable: true),
                    Autograph = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Isbn);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "AuthorId");
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    number = table.Column<int>(type: "int", nullable: true),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityPostalCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.id);
                    table.ForeignKey(
                        name: "FK_Address_City_CityPostalCode",
                        column: x => x.CityPostalCode,
                        principalTable: "City",
                        principalColumn: "PostalCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminId", "DateCreated", "DateDeleted", "DateModified", "DateOfBirth", "FirstName", "IsDeleted", "LastName" },
                values: new object[,]
                {
                    { "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Admin1Name", null, "Admin1LastName" },
                    { "2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Admin2Name", null, "Admin2LastName" },
                    { "3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Admin3Name", null, "Admin3LastName" },
                    { "4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Admin4Name", null, "Admin4LastName" },
                    { "5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, "Admin5Name", null, "Admin5LastName" }
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "DateCreated", "DateDeleted", "DateModified", "DateOfBirth", "IsDeleted", "LastName", "Name" },
                values: new object[,]
                {
                    { "1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Author1LastName", "Author1Name" },
                    { "2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Author2LastName", "Author2Name" },
                    { "3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Author3LastName", "Author3Name" },
                    { "4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, "Author4LastName", "Author4Name" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Isbn", "AuthorId", "Available", "DateCreated", "DateDeleted", "DateModified", "Discriminator", "Genre", "IsDeleted", "Title" },
                values: new object[,]
                {
                    { "123456oadadadasf", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Book", "Avantura", null, "Le Petite Prince" },
                    { "123456oaihsf", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Book", "Avantura", null, "Alisa u zemlji cuda" },
                    { "127889asdihsf", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Book", "Avantura", null, "Harry Potter" },
                    { "189er56oaihsf", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Book", "Avantura", null, "The jungle book" },
                    { "asdffrghsf", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Book", "Avantura", null, "Lord of rings" },
                    { "deilgoihj2343", null, true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Book", "Avantura", null, "Murder on Nil" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "JMBG", "DateCreated", "DateDeleted", "DateModified", "FirstName", "IsDeleted", "LastName" },
                values: new object[,]
                {
                    { "123456", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Customer1Name", null, "Customer1LastName" },
                    { "238476", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Customer5Name", null, "Customer5LastName" },
                    { "239184762", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Customer2Name", null, "Customer2LastName" },
                    { "324857", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Customer4Name", null, "Customer4LastName" },
                    { "329456", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Customer3Name", null, "Customer3LastName" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_CityPostalCode",
                table: "Address",
                column: "CityPostalCode");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Rents");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "Authors");
        }
    }
}
