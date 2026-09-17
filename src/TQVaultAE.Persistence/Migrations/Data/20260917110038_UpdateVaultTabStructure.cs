using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Data
{
    /// <inheritdoc />
    public partial class UpdateVaultTabStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vault_tabs_icon_sets_IconId1",
                table: "vault_tabs");

            migrationBuilder.DropIndex(
                name: "IX_vault_tabs_IconId1",
                table: "vault_tabs");

            migrationBuilder.DropColumn(
                name: "IconId1",
                table: "vault_tabs");

            migrationBuilder.CreateIndex(
                name: "IX_vault_tabs_IconId",
                table: "vault_tabs",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_vault_tabs_icon_sets_IconId",
                table: "vault_tabs",
                column: "IconId",
                principalTable: "icon_sets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vault_tabs_icon_sets_IconId",
                table: "vault_tabs");

            migrationBuilder.DropIndex(
                name: "IX_vault_tabs_IconId",
                table: "vault_tabs");

            migrationBuilder.AddColumn<string>(
                name: "IconId1",
                table: "vault_tabs",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_vault_tabs_IconId1",
                table: "vault_tabs",
                column: "IconId1");

            migrationBuilder.AddForeignKey(
                name: "FK_vault_tabs_icon_sets_IconId1",
                table: "vault_tabs",
                column: "IconId1",
                principalTable: "icon_sets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
