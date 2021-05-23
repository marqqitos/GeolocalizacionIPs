using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace GeolocalizacionIPs.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IPInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Pais = table.Column<string>(type: "text", nullable: true),
                    CodigoISOPais = table.Column<string>(type: "text", nullable: true),
                    FranjasHorarias = table.Column<string>(type: "text", nullable: true),
                    Idiomas = table.Column<string>(type: "text", nullable: true),
                    DistanciaABuenosAires = table.Column<double>(type: "double precision", nullable: false),
                    Moneda = table.Column<string>(type: "text", nullable: true),
                    Invocaciones = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IPInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IPInfos");
        }
    }
}
