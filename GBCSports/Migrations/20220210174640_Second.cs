using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBCSports.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Code", "Name", "Price", "Release_Date" },
                values: new object[] { "TRNY10", "Tournament Master 1.0", 4L, new DateTime(2022, 2, 10, 17, 46, 40, 12, DateTimeKind.Local).AddTicks(4308) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Code",
                keyValue: "TRNY10");
        }
    }
}
