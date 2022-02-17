using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GBCSports.Migrations
{
    public partial class CreatedCountryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Canada" },
                    { 2, "VietNam" },
                    { 3, "United States of America" },
                    { 4, "Russia" },
                    { 5, "Italy" },
                    { 6, "France" },
                    { 7, "Spain" },
                    { 8, "England" },
                    { 9, "Germany" },
                    { 10, "Netherlands" },
                    { 11, "Poland" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
