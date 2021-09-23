using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class ActualizandolaBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagen",
                table: "Generos");

            migrationBuilder.AddColumn<int>(
                name: "IdGenero",
                table: "Personajes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdGenero",
                table: "Generos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Generos_IdGenero",
                table: "Generos",
                column: "IdGenero");

            migrationBuilder.AddForeignKey(
                name: "FK_Generos_Personajes_IdGenero",
                table: "Generos",
                column: "IdGenero",
                principalTable: "Personajes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Generos_Personajes_IdGenero",
                table: "Generos");

            migrationBuilder.DropIndex(
                name: "IX_Generos_IdGenero",
                table: "Generos");

            migrationBuilder.DropColumn(
                name: "IdGenero",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "IdGenero",
                table: "Generos");

            migrationBuilder.AddColumn<string>(
                name: "imagen",
                table: "Generos",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
