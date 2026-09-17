using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Data
{
    /// <inheritdoc />
    public partial class AddIconToVaultTab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vault_tabs_icon_sets_IconId",
                table: "vault_tabs");

            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "vault_tabs",
                newName: "icon_set");

            migrationBuilder.RenameIndex(
                name: "IX_vault_tabs_IconId",
                table: "vault_tabs",
                newName: "IX_vault_tabs_icon_set");

            migrationBuilder.AddForeignKey(
                name: "FK_vault_tabs_icon_sets_icon_set",
                table: "vault_tabs",
                column: "icon_set",
                principalTable: "icon_sets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vault_tabs_icon_sets_icon_set",
                table: "vault_tabs");

            migrationBuilder.RenameColumn(
                name: "icon_set",
                table: "vault_tabs",
                newName: "IconId");

            migrationBuilder.RenameIndex(
                name: "IX_vault_tabs_icon_set",
                table: "vault_tabs",
                newName: "IX_vault_tabs_IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_vault_tabs_icon_sets_IconId",
                table: "vault_tabs",
                column: "IconId",
                principalTable: "icon_sets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
