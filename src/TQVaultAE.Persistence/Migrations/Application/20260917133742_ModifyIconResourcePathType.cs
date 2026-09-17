using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class ModifyIconResourcePathType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uri",
                table: "icons",
                newName: "resource_path");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "resource_path",
                table: "icons",
                newName: "uri");
        }
    }
}
