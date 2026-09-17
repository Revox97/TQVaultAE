using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Data
{
    /// <inheritdoc />
    public partial class AddIconIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_icon_sets_Icon_IconDownId",
                table: "icon_sets");

            migrationBuilder.DropForeignKey(
                name: "FK_icon_sets_Icon_IconHoverId",
                table: "icon_sets");

            migrationBuilder.DropForeignKey(
                name: "FK_icon_sets_Icon_IconUpId",
                table: "icon_sets");

            migrationBuilder.RenameColumn(
                name: "IconUpId",
                table: "icon_sets",
                newName: "icon_up_id");

            migrationBuilder.RenameColumn(
                name: "IconHoverId",
                table: "icon_sets",
                newName: "icon_hover_id");

            migrationBuilder.RenameColumn(
                name: "IconDownId",
                table: "icon_sets",
                newName: "icon_down_id");

            migrationBuilder.RenameIndex(
                name: "IX_icon_sets_IconUpId",
                table: "icon_sets",
                newName: "IX_icon_sets_icon_up_id");

            migrationBuilder.RenameIndex(
                name: "IX_icon_sets_IconHoverId",
                table: "icon_sets",
                newName: "IX_icon_sets_icon_hover_id");

            migrationBuilder.RenameIndex(
                name: "IX_icon_sets_IconDownId",
                table: "icon_sets",
                newName: "IX_icon_sets_icon_down_id");

            migrationBuilder.AddForeignKey(
                name: "FK_icon_sets_Icon_icon_down_id",
                table: "icon_sets",
                column: "icon_down_id",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_icon_sets_Icon_icon_hover_id",
                table: "icon_sets",
                column: "icon_hover_id",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_icon_sets_Icon_icon_up_id",
                table: "icon_sets",
                column: "icon_up_id",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_icon_sets_Icon_icon_down_id",
                table: "icon_sets");

            migrationBuilder.DropForeignKey(
                name: "FK_icon_sets_Icon_icon_hover_id",
                table: "icon_sets");

            migrationBuilder.DropForeignKey(
                name: "FK_icon_sets_Icon_icon_up_id",
                table: "icon_sets");

            migrationBuilder.RenameColumn(
                name: "icon_up_id",
                table: "icon_sets",
                newName: "IconUpId");

            migrationBuilder.RenameColumn(
                name: "icon_hover_id",
                table: "icon_sets",
                newName: "IconHoverId");

            migrationBuilder.RenameColumn(
                name: "icon_down_id",
                table: "icon_sets",
                newName: "IconDownId");

            migrationBuilder.RenameIndex(
                name: "IX_icon_sets_icon_up_id",
                table: "icon_sets",
                newName: "IX_icon_sets_IconUpId");

            migrationBuilder.RenameIndex(
                name: "IX_icon_sets_icon_hover_id",
                table: "icon_sets",
                newName: "IX_icon_sets_IconHoverId");

            migrationBuilder.RenameIndex(
                name: "IX_icon_sets_icon_down_id",
                table: "icon_sets",
                newName: "IX_icon_sets_IconDownId");

            migrationBuilder.AddForeignKey(
                name: "FK_icon_sets_Icon_IconDownId",
                table: "icon_sets",
                column: "IconDownId",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_icon_sets_Icon_IconHoverId",
                table: "icon_sets",
                column: "IconHoverId",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_icon_sets_Icon_IconUpId",
                table: "icon_sets",
                column: "IconUpId",
                principalTable: "Icon",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
