using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpineWise.Web.Migrations
{
    /// <inheritdoc />
    public partial class testCHairUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChairId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FingerprintLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LogDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Successful = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FingerprintLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FingerprintLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpineWiseDataLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegDistance = table.Column<float>(type: "real", nullable: false),
                    LumbarBackDistance = table.Column<float>(type: "real", nullable: false),
                    ThoracicBackDistance = table.Column<float>(type: "real", nullable: false),
                    LogDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChairId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpineWiseDataLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpineWiseDataLogs_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpineWiseDataLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChairId",
                table: "Users",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_FingerprintLogs_UserId",
                table: "FingerprintLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SpineWiseDataLogs_ChairId",
                table: "SpineWiseDataLogs",
                column: "ChairId");

            migrationBuilder.CreateIndex(
                name: "IX_SpineWiseDataLogs_UserId",
                table: "SpineWiseDataLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chairs_ChairId",
                table: "Users",
                column: "ChairId",
                principalTable: "Chairs",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chairs_ChairId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "FingerprintLogs");

            migrationBuilder.DropTable(
                name: "SpineWiseDataLogs");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChairId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChairId",
                table: "Users");
        }
    }
}
