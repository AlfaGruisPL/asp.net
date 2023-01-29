using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zpnet.Migrations
{
    public partial class start : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "kategorie",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kategorie", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "miejsca",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_miejsca", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "elementy",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    dataStworzenia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    miejsceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_elementy", x => x.id);
                    table.ForeignKey(
                        name: "FK_elementy_miejsca_miejsceId",
                        column: x => x.miejsceId,
                        principalTable: "miejsca",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "elementkategoria",
                columns: table => new
                {
                    elementyid = table.Column<int>(type: "int", nullable: false),
                    kategorieid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_elementkategoria", x => new { x.elementyid, x.kategorieid });
                    table.ForeignKey(
                        name: "FK_elementkategoria_elementy_elementyid",
                        column: x => x.elementyid,
                        principalTable: "elementy",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_elementkategoria_kategorie_kategorieid",
                        column: x => x.kategorieid,
                        principalTable: "kategorie",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_elementkategoria_kategorieid",
                table: "elementkategoria",
                column: "kategorieid");

            migrationBuilder.CreateIndex(
                name: "IX_elementy_miejsceId",
                table: "elementy",
                column: "miejsceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "elementkategoria");

            migrationBuilder.DropTable(
                name: "elementy");

            migrationBuilder.DropTable(
                name: "kategorie");

            migrationBuilder.DropTable(
                name: "miejsca");
        }
    }
}
