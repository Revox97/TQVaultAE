using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Data
{
    /// <inheritdoc />
    public partial class AddIconToVaultTabAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IconId",
                table: "vault_tabs",
                newName: "icon_id");

            migrationBuilder.CreateIndex(
                name: "IX_vault_tabs_icon_id",
                table: "vault_tabs",
                column: "icon_id");

            migrationBuilder.AddForeignKey(
                name: "FK_vault_tabs_icon_sets_icon_id",
                table: "vault_tabs",
                column: "icon_id",
                principalTable: "icon_sets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vault_tabs_icon_sets_icon_id",
                table: "vault_tabs");

            migrationBuilder.DropIndex(
                name: "IX_vault_tabs_icon_id",
                table: "vault_tabs");

            migrationBuilder.RenameColumn(
                name: "icon_id",
                table: "vault_tabs",
                newName: "IconId");
        }
    }
}
