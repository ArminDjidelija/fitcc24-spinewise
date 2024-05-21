using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpineWise.Web.Migrations
{
    /// <inheritdoc />
    public partial class dodatPressureSenzor4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PressureSensor4",
                table: "SpinePostureDataLogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PressureSensor4",
                table: "SpinePostureDataLogs");
        }
    }
}
