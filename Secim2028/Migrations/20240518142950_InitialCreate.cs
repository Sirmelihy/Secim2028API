using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Secim2028.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Iller",
                columns: table => new
                {
                    IlId = table.Column<int>(type: "int", nullable: false),
                    IlAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Iller", x => x.IlId);
                });

            migrationBuilder.CreateTable(
                name: "Ittifaklar",
                columns: table => new
                {
                    IttifakId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IttifakAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ittifaklar", x => x.IttifakId);
                });

            migrationBuilder.CreateTable(
                name: "Ilceler",
                columns: table => new
                {
                    IlceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IlceAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IlId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ilceler", x => x.IlceId);
                    table.ForeignKey(
                        name: "FK_Ilceler_Iller_IlId",
                        column: x => x.IlId,
                        principalTable: "Iller",
                        principalColumn: "IlId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partiler",
                columns: table => new
                {
                    SiyasiPartiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiyasiPartiAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiyasiPartiKisaltma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IttifakId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partiler", x => x.SiyasiPartiId);
                    table.ForeignKey(
                        name: "FK_Partiler_Ittifaklar_IttifakId",
                        column: x => x.IttifakId,
                        principalTable: "Ittifaklar",
                        principalColumn: "IttifakId");
                });

            migrationBuilder.CreateTable(
                name: "Sandiklar",
                columns: table => new
                {
                    SandikId = table.Column<int>(type: "int", nullable: false),
                    SandikNo = table.Column<int>(type: "int", nullable: false),
                    IlceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sandiklar", x => x.SandikId);
                    table.ForeignKey(
                        name: "FK_Sandiklar_Ilceler_IlceId",
                        column: x => x.IlceId,
                        principalTable: "Ilceler",
                        principalColumn: "IlceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Adaylar",
                columns: table => new
                {
                    AdayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdayAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SiyasiPartiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adaylar", x => x.AdayId);
                    table.ForeignKey(
                        name: "FK_Adaylar_Partiler_SiyasiPartiId",
                        column: x => x.SiyasiPartiId,
                        principalTable: "Partiler",
                        principalColumn: "SiyasiPartiId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PartiOylar",
                columns: table => new
                {
                    OyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SandikId = table.Column<int>(type: "int", nullable: false),
                    SiyasiPartiId = table.Column<int>(type: "int", nullable: false),
                    OySayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartiOylar", x => x.OyId);
                    table.ForeignKey(
                        name: "FK_PartiOylar_Partiler_SiyasiPartiId",
                        column: x => x.SiyasiPartiId,
                        principalTable: "Partiler",
                        principalColumn: "SiyasiPartiId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartiOylar_Sandiklar_SandikId",
                        column: x => x.SandikId,
                        principalTable: "Sandiklar",
                        principalColumn: "SandikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdayOylar",
                columns: table => new
                {
                    OyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SandikId = table.Column<int>(type: "int", nullable: false),
                    AdayId = table.Column<int>(type: "int", nullable: false),
                    OySayisi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdayOylar", x => x.OyId);
                    table.ForeignKey(
                        name: "FK_AdayOylar_Adaylar_AdayId",
                        column: x => x.AdayId,
                        principalTable: "Adaylar",
                        principalColumn: "AdayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdayOylar_Sandiklar_SandikId",
                        column: x => x.SandikId,
                        principalTable: "Sandiklar",
                        principalColumn: "SandikId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adaylar_SiyasiPartiId",
                table: "Adaylar",
                column: "SiyasiPartiId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayOylar_AdayId",
                table: "AdayOylar",
                column: "AdayId");

            migrationBuilder.CreateIndex(
                name: "IX_AdayOylar_SandikId",
                table: "AdayOylar",
                column: "SandikId");

            migrationBuilder.CreateIndex(
                name: "IX_Ilceler_IlId",
                table: "Ilceler",
                column: "IlId");

            migrationBuilder.CreateIndex(
                name: "IX_Partiler_IttifakId",
                table: "Partiler",
                column: "IttifakId");

            migrationBuilder.CreateIndex(
                name: "IX_PartiOylar_SandikId",
                table: "PartiOylar",
                column: "SandikId");

            migrationBuilder.CreateIndex(
                name: "IX_PartiOylar_SiyasiPartiId",
                table: "PartiOylar",
                column: "SiyasiPartiId");

            migrationBuilder.CreateIndex(
                name: "IX_Sandiklar_IlceId",
                table: "Sandiklar",
                column: "IlceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdayOylar");

            migrationBuilder.DropTable(
                name: "PartiOylar");

            migrationBuilder.DropTable(
                name: "Adaylar");

            migrationBuilder.DropTable(
                name: "Sandiklar");

            migrationBuilder.DropTable(
                name: "Partiler");

            migrationBuilder.DropTable(
                name: "Ilceler");

            migrationBuilder.DropTable(
                name: "Ittifaklar");

            migrationBuilder.DropTable(
                name: "Iller");
        }
    }
}
