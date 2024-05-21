using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpineWise.Web.Migrations
{
    /// <inheritdoc />
    public partial class updatedChairUserRelationOnDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chairs_ChairId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "SpinePostureDataLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpperBackDistance = table.Column<float>(type: "real", nullable: false),
                    LegDistance = table.Column<float>(type: "real", nullable: false),
                    PressureSensor1 = table.Column<bool>(type: "bit", nullable: false),
                    PressureSensor2 = table.Column<bool>(type: "bit", nullable: false),
                    PressureSensor3 = table.Column<bool>(type: "bit", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChairId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpinePostureDataLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpinePostureDataLogs_Chairs_ChairId",
                        column: x => x.ChairId,
                        principalTable: "Chairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpinePostureDataLogs_ChairId",
                table: "SpinePostureDataLogs",
                column: "ChairId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chairs_ChairId",
                table: "Users",
                column: "ChairId",
                principalTable: "Chairs",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Chairs_ChairId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "SpinePostureDataLogs");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Chairs_ChairId",
                table: "Users",
                column: "ChairId",
                principalTable: "Chairs",
                principalColumn: "Id");
        }
    }
}
