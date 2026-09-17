using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Application
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    version = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application", x => x.name);
                });

            migrationBuilder.CreateTable(
                name: "icons",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    uri = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_icons", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    cheats_character_editing = table.Column<bool>(type: "INTEGER", nullable: false),
                    cheats_enable = table.Column<bool>(type: "INTEGER", nullable: false),
                    cheats_epic_legendary_affixes = table.Column<bool>(type: "INTEGER", nullable: false),
                    cheats_item_editing = table.Column<bool>(type: "INTEGER", nullable: false),
                    cheats_item_copying = table.Column<bool>(type: "INTEGER", nullable: false),
                    custom_maps = table.Column<bool>(type: "INTEGER", nullable: false),
                    original_tq_support = table.Column<bool>(type: "INTEGER", nullable: false),
                    paths_immortal_thronw = table.Column<string>(type: "TEXT", nullable: false),
                    paths_auto_detection = table.Column<bool>(type: "INTEGER", nullable: false),
                    paths_titan_quest = table.Column<string>(type: "TEXT", nullable: false),
                    auto_stacking = table.Column<bool>(type: "INTEGER", nullable: false),
                    bypass_confirmation_messages = table.Column<bool>(type: "INTEGER", nullable: false),
                    bypass_title_screen = table.Column<bool>(type: "INTEGER", nullable: false),
                    detailed_tooltip_view = table.Column<bool>(type: "INTEGER", nullable: false),
                    hot_reload = table.Column<bool>(type: "INTEGER", nullable: false),
                    load_all_vault_and_character_data_on_startup = table.Column<bool>(type: "INTEGER", nullable: false),
                    load_last_opened_character = table.Column<bool>(type: "INTEGER", nullable: false),
                    load_last_opened_vault = table.Column<bool>(type: "INTEGER", nullable: false),
                    player_equipment_read_only = table.Column<bool>(type: "INTEGER", nullable: false),
                    backup_player_saves = table.Column<bool>(type: "INTEGER", nullable: false),
                    backup_repository_url = table.Column<string>(type: "TEXT", nullable: false),
                    backup = table.Column<bool>(type: "INTEGER", nullable: false),
                    play_sounds = table.Column<bool>(type: "INTEGER", nullable: false),
                    font = table.Column<string>(type: "TEXT", nullable: false),
                    item_requirements_restriction = table.Column<bool>(type: "INTEGER", nullable: false),
                    item_background_transparency_level = table.Column<double>(type: "REAL", nullable: false),
                    language = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_settings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "icon_sets",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    icon_down = table.Column<string>(type: "TEXT", nullable: false),
                    icon_up = table.Column<string>(type: "TEXT", nullable: false),
                    icon_hover = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_icon_sets", x => x.id);
                    table.ForeignKey(
                        name: "FK_icon_sets_icons_icon_down",
                        column: x => x.icon_down,
                        principalTable: "icons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_icon_sets_icons_icon_hover",
                        column: x => x.icon_hover,
                        principalTable: "icons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_icon_sets_icons_icon_up",
                        column: x => x.icon_up,
                        principalTable: "icons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    settings_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_settings_settings_id",
                        column: x => x.settings_id,
                        principalTable: "settings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_icon_sets_icon_down",
                table: "icon_sets",
                column: "icon_down");

            migrationBuilder.CreateIndex(
                name: "IX_icon_sets_icon_hover",
                table: "icon_sets",
                column: "icon_hover");

            migrationBuilder.CreateIndex(
                name: "IX_icon_sets_icon_up",
                table: "icon_sets",
                column: "icon_up");

            migrationBuilder.CreateIndex(
                name: "IX_users_settings_id",
                table: "users",
                column: "settings_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "application");

            migrationBuilder.DropTable(
                name: "icon_sets");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "icons");

            migrationBuilder.DropTable(
                name: "settings");
        }
    }
}
