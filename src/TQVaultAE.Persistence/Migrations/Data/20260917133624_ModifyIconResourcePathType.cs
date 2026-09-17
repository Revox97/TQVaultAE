using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Data
{
    /// <inheritdoc />
    public partial class ModifyIconResourcePathType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Uri",
                table: "Icon",
                newName: "ResourcePath");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResourcePath",
                table: "Icon",
                newName: "Uri");
        }
    }
}
