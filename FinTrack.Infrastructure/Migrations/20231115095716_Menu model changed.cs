using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinTrack.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Menumodelchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CanCreate",
                table: "MenusGroupPrivileges");

            migrationBuilder.DropColumn(
                name: "CanDelete",
                table: "MenusGroupPrivileges");

            migrationBuilder.DropColumn(
                name: "CanRead",
                table: "MenusGroupPrivileges");

            migrationBuilder.DropColumn(
                name: "CanUpdate",
                table: "MenusGroupPrivileges");

            migrationBuilder.AddColumn<long>(
                name: "MenuUrlId",
                table: "Menus",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MenusUrlsId",
                table: "Menus",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenusUrlsId",
                table: "Menus",
                column: "MenusUrlsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_MenusUrls_MenusUrlsId",
                table: "Menus",
                column: "MenusUrlsId",
                principalTable: "MenusUrls",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_MenusUrls_MenusUrlsId",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Menus_MenusUrlsId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "MenuUrlId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "MenusUrlsId",
                table: "Menus");

            migrationBuilder.AddColumn<bool>(
                name: "CanCreate",
                table: "MenusGroupPrivileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanDelete",
                table: "MenusGroupPrivileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanRead",
                table: "MenusGroupPrivileges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanUpdate",
                table: "MenusGroupPrivileges",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
