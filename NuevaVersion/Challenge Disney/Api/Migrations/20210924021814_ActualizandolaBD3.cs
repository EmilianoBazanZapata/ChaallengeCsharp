using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class ActualizandolaBD3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeliculaPersonaje");

            migrationBuilder.AddColumn<int>(
                name: "IdPersonaje",
                table: "Personajes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdPelicula",
                table: "Peliculas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PeliculaPorPersonajes",
                columns: table => new
                {
                    IdPeliculaPorPersonaje = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPersonaje = table.Column<int>(type: "int", nullable: false),
                    IdPelicula = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaPorPersonajes", x => x.IdPeliculaPorPersonaje);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personajes_IdPersonaje",
                table: "Personajes",
                column: "IdPersonaje");

            migrationBuilder.CreateIndex(
                name: "IX_Peliculas_IdPelicula",
                table: "Peliculas",
                column: "IdPelicula");

            migrationBuilder.AddForeignKey(
                name: "FK_Peliculas_PeliculaPorPersonajes_IdPelicula",
                table: "Peliculas",
                column: "IdPelicula",
                principalTable: "PeliculaPorPersonajes",
                principalColumn: "IdPeliculaPorPersonaje",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Personajes_PeliculaPorPersonajes_IdPersonaje",
                table: "Personajes",
                column: "IdPersonaje",
                principalTable: "PeliculaPorPersonajes",
                principalColumn: "IdPeliculaPorPersonaje",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Peliculas_PeliculaPorPersonajes_IdPelicula",
                table: "Peliculas");

            migrationBuilder.DropForeignKey(
                name: "FK_Personajes_PeliculaPorPersonajes_IdPersonaje",
                table: "Personajes");

            migrationBuilder.DropTable(
                name: "PeliculaPorPersonajes");

            migrationBuilder.DropIndex(
                name: "IX_Personajes_IdPersonaje",
                table: "Personajes");

            migrationBuilder.DropIndex(
                name: "IX_Peliculas_IdPelicula",
                table: "Peliculas");

            migrationBuilder.DropColumn(
                name: "IdPersonaje",
                table: "Personajes");

            migrationBuilder.DropColumn(
                name: "IdPelicula",
                table: "Peliculas");

            migrationBuilder.CreateTable(
                name: "PeliculaPersonaje",
                columns: table => new
                {
                    PeliculasId = table.Column<int>(type: "int", nullable: false),
                    PersonajesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeliculaPersonaje", x => new { x.PeliculasId, x.PersonajesId });
                    table.ForeignKey(
                        name: "FK_PeliculaPersonaje_Peliculas_PeliculasId",
                        column: x => x.PeliculasId,
                        principalTable: "Peliculas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeliculaPersonaje_Personajes_PersonajesId",
                        column: x => x.PersonajesId,
                        principalTable: "Personajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeliculaPersonaje_PersonajesId",
                table: "PeliculaPersonaje",
                column: "PersonajesId");
        }
    }
}
