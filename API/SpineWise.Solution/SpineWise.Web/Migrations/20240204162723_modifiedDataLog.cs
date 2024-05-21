using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpineWise.Web.Migrations
{
    /// <inheritdoc />
    public partial class modifiedDataLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpineWiseDataLogs_Users_UserId",
                table: "SpineWiseDataLogs");

            migrationBuilder.DropIndex(
                name: "IX_SpineWiseDataLogs_UserId",
                table: "SpineWiseDataLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SpineWiseDataLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SpineWiseDataLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SpineWiseDataLogs_UserId",
                table: "SpineWiseDataLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpineWiseDataLogs_Users_UserId",
                table: "SpineWiseDataLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
