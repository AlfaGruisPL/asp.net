using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zpnet.Migrations
{
    public partial class start2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "dataStworzenia",
                table: "elementy",
                newName: "opis");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data_stworzenia",
                table: "elementy",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data_stworzenia",
                table: "elementy");

            migrationBuilder.RenameColumn(
                name: "opis",
                table: "elementy",
                newName: "dataStworzenia");
        }
    }
}
