using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Data
{
    /// <inheritdoc />
    public partial class TempRemoveIconFromVaultTab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vault_tabs_icon_sets_icon_set",
                table: "vault_tabs");

            migrationBuilder.DropIndex(
                name: "IX_vault_tabs_icon_set",
                table: "vault_tabs");

            migrationBuilder.RenameColumn(
                name: "icon_set",
                table: "vault_tabs",
                newName: "IconId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "vault_tabs",
                newName: "icon_set");

            migrationBuilder.CreateIndex(
                name: "IX_vault_tabs_icon_set",
                table: "vault_tabs",
                column: "icon_set");

            migrationBuilder.AddForeignKey(
                name: "FK_vault_tabs_icon_sets_icon_set",
                table: "vault_tabs",
                column: "icon_set",
                principalTable: "icon_sets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
