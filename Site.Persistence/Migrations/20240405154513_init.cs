using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Site.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(900)", maxLength: 900, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 5, 19, 15, 10, 197, DateTimeKind.Local).AddTicks(4545)),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoryBrands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 5, 19, 15, 10, 197, DateTimeKind.Local).AddTicks(7584)),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryBrands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2024, 4, 5, 19, 15, 10, 198, DateTimeKind.Local).AddTicks(360)),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { 1, "سامسونگ", null, null },
                    { 2, "شیائومی ", null, null },
                    { 3, "اپل", null, null },
                    { 4, "هوآوی", null, null },
                    { 5, "نوکیا ", null, null },
                    { 6, "ال جی", null, null }
                });

            migrationBuilder.InsertData(
                table: "CategoryBrands",
                columns: new[] { "Id", "Brand", "RemoveTime", "UpdateTime" },
                values: new object[,]
                {
                    { 1, "اسنوا", null, null },
                    { 2, "ایکس ویژن", null, null }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "Count", "Price", "PurchaseDate", "RemoveTime", "Title", "UpdateTime" },
                values: new object[,]
                {
                    { 1, 3, 1900m, new DateTime(2024, 4, 5, 19, 15, 10, 198, DateTimeKind.Local).AddTicks(4846), null, "تلویزیون", null },
                    { 2, 1, 980m, new DateTime(2024, 4, 5, 19, 15, 10, 198, DateTimeKind.Local).AddTicks(4854), null, "یخچال", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "CategoryBrands");

            migrationBuilder.DropTable(
                name: "Invoices");
        }
    }
}
