using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservasiRuangApp.Migrations
{
    /// <inheritdoc />
    public partial class InitReservasi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservasi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamaPemesan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NamaRuangan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaktuMulai = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WaktuSelesai = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Keterangan = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservasi", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservasi");
        }
    }
}
