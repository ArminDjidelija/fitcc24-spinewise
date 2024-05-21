using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpineWise.Web.Migrations
{
    /// <inheritdoc />
    public partial class addedChangesToDbCOntext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperAdmins_UserAccount_Id",
                table: "SuperAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActionLogs_UserAccount_UserAccountID",
                table: "UserActionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_UserAccount_UserAccountId",
                table: "UserTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccount",
                table: "UserAccount");

            migrationBuilder.RenameTable(
                name: "UserAccount",
                newName: "UserAccounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserAccounts_Id",
                        column: x => x.Id,
                        principalTable: "UserAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_SuperAdmins_UserAccounts_Id",
                table: "SuperAdmins",
                column: "Id",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActionLogs_UserAccounts_UserAccountID",
                table: "UserActionLogs",
                column: "UserAccountID",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_UserAccounts_UserAccountId",
                table: "UserTokens",
                column: "UserAccountId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperAdmins_UserAccounts_Id",
                table: "SuperAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_UserActionLogs_UserAccounts_UserAccountID",
                table: "UserActionLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_UserTokens_UserAccounts_UserAccountId",
                table: "UserTokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAccounts",
                table: "UserAccounts");

            migrationBuilder.RenameTable(
                name: "UserAccounts",
                newName: "UserAccount");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAccount",
                table: "UserAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperAdmins_UserAccount_Id",
                table: "SuperAdmins",
                column: "Id",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserActionLogs_UserAccount_UserAccountID",
                table: "UserActionLogs",
                column: "UserAccountID",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserTokens_UserAccount_UserAccountId",
                table: "UserTokens",
                column: "UserAccountId",
                principalTable: "UserAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
