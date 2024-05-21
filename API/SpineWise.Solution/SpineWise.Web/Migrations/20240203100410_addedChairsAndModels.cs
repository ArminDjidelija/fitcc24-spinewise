using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpineWise.Web.Migrations
{
    /// <inheritdoc />
    public partial class addedChairsAndModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChairModelId",
                table: "Chairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreating",
                table: "Chairs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "ChairModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfCreating = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChairModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SignInLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountID = table.Column<int>(type: "int", nullable: false),
                    TimeOfSignIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuccessfullSignIn = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignInLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignInLogs_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SignOutLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAccountID = table.Column<int>(type: "int", nullable: false),
                    TimeOfSignOut = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignOutLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignOutLogs_UserAccounts_UserAccountID",
                        column: x => x.UserAccountID,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chairs_ChairModelId",
                table: "Chairs",
                column: "ChairModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SignInLogs_UserAccountID",
                table: "SignInLogs",
                column: "UserAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_SignOutLogs_UserAccountID",
                table: "SignOutLogs",
                column: "UserAccountID");

            migrationBuilder.AddForeignKey(
                name: "FK_Chairs_ChairModels_ChairModelId",
                table: "Chairs",
                column: "ChairModelId",
                principalTable: "ChairModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chairs_ChairModels_ChairModelId",
                table: "Chairs");

            migrationBuilder.DropTable(
                name: "ChairModels");

            migrationBuilder.DropTable(
                name: "SignInLogs");

            migrationBuilder.DropTable(
                name: "SignOutLogs");

            migrationBuilder.DropIndex(
                name: "IX_Chairs_ChairModelId",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "ChairModelId",
                table: "Chairs");

            migrationBuilder.DropColumn(
                name: "DateOfCreating",
                table: "Chairs");
        }
    }
}
