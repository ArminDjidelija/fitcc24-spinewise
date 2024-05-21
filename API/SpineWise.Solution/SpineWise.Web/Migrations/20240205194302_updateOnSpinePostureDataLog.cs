using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpineWise.Web.Migrations
{
    /// <inheritdoc />
    public partial class updateOnSpinePostureDataLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Key",
                table: "SpinePostureDataLogs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "SpinePostureDataLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
