using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TQVaultAE.Persistence.Migrations.Data
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "affix",
                columns: table => new
                {
                    path = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_affix", x => x.path);
                });

            migrationBuilder.CreateTable(
                name: "Icon",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Uri = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icon", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vaults",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    type = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vaults", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    position = table.Column<string>(type: "TEXT", nullable: false),
                    resource_path = table.Column<string>(type: "TEXT", nullable: false),
                    seed = table.Column<int>(type: "INTEGER", nullable: false),
                    prefix = table.Column<string>(type: "TEXT", nullable: false),
                    suffix = table.Column<string>(type: "TEXT", nullable: false),
                    talisman_one = table.Column<Guid>(type: "TEXT", nullable: false),
                    talisman_two = table.Column<Guid>(type: "TEXT", nullable: false),
                    var1 = table.Column<int>(type: "INTEGER", nullable: false),
                    var2 = table.Column<int>(type: "INTEGER", nullable: false),
                    classification = table.Column<int>(type: "INTEGER", nullable: false),
                    cost = table.Column<int>(type: "INTEGER", nullable: false),
                    level = table.Column<int>(type: "INTEGER", nullable: false),
                    max_transparency = table.Column<int>(type: "INTEGER", nullable: false),
                    scale = table.Column<float>(type: "REAL", nullable: false),
                    template_name = table.Column<string>(type: "TEXT", nullable: false),
                    size = table.Column<string>(type: "TEXT", nullable: false),
                    @class = table.Column<int>(name: "class", type: "INTEGER", nullable: false),
                    stack_count = table.Column<int>(type: "INTEGER", nullable: false),
                    can_stack = table.Column<bool>(type: "INTEGER", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    TalismanType = table.Column<int>(type: "INTEGER", nullable: true),
                    Bonus = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_items_affix_prefix",
                        column: x => x.prefix,
                        principalTable: "affix",
                        principalColumn: "path");
                    table.ForeignKey(
                        name: "FK_items_affix_suffix",
                        column: x => x.suffix,
                        principalTable: "affix",
                        principalColumn: "path");
                    table.ForeignKey(
                        name: "FK_items_items_talisman_one",
                        column: x => x.talisman_one,
                        principalTable: "items",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_items_items_talisman_two",
                        column: x => x.talisman_two,
                        principalTable: "items",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "icon_sets",
                columns: table => new
                {
                    id = table.Column<string>(type: "TEXT", nullable: false),
                    IconDownId = table.Column<string>(type: "TEXT", nullable: false),
                    IconUpId = table.Column<string>(type: "TEXT", nullable: false),
                    IconHoverId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_icon_sets", x => x.id);
                    table.ForeignKey(
                        name: "FK_icon_sets_Icon_IconDownId",
                        column: x => x.IconDownId,
                        principalTable: "Icon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_icon_sets_Icon_IconHoverId",
                        column: x => x.IconHoverId,
                        principalTable: "Icon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_icon_sets_Icon_IconUpId",
                        column: x => x.IconUpId,
                        principalTable: "Icon",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vault_tabs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    vault_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    IconId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IconId1 = table.Column<string>(type: "TEXT", nullable: false),
                    VaultId1 = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vault_tabs", x => x.id);
                    table.ForeignKey(
                        name: "FK_vault_tabs_icon_sets_IconId1",
                        column: x => x.IconId1,
                        principalTable: "icon_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vault_tabs_vaults_VaultId1",
                        column: x => x.VaultId1,
                        principalTable: "vaults",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_vault_tabs_vaults_vault_id",
                        column: x => x.vault_id,
                        principalTable: "vaults",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vault_tab_item",
                columns: table => new
                {
                    vault_tab_id = table.Column<Guid>(type: "TEXT", nullable: false),
                    item_id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vault_tab_item", x => new { x.vault_tab_id, x.item_id });
                    table.ForeignKey(
                        name: "FK_vault_tab_item_items_item_id",
                        column: x => x.item_id,
                        principalTable: "items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vault_tab_item_vault_tabs_vault_tab_id",
                        column: x => x.vault_tab_id,
                        principalTable: "vault_tabs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_icon_sets_IconDownId",
                table: "icon_sets",
                column: "IconDownId");

            migrationBuilder.CreateIndex(
                name: "IX_icon_sets_IconHoverId",
                table: "icon_sets",
                column: "IconHoverId");

            migrationBuilder.CreateIndex(
                name: "IX_icon_sets_IconUpId",
                table: "icon_sets",
                column: "IconUpId");

            migrationBuilder.CreateIndex(
                name: "IX_items_prefix",
                table: "items",
                column: "prefix");

            migrationBuilder.CreateIndex(
                name: "IX_items_suffix",
                table: "items",
                column: "suffix");

            migrationBuilder.CreateIndex(
                name: "IX_items_talisman_one",
                table: "items",
                column: "talisman_one");

            migrationBuilder.CreateIndex(
                name: "IX_items_talisman_two",
                table: "items",
                column: "talisman_two");

            migrationBuilder.CreateIndex(
                name: "IX_vault_tab_item_item_id",
                table: "vault_tab_item",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_vault_tabs_IconId1",
                table: "vault_tabs",
                column: "IconId1");

            migrationBuilder.CreateIndex(
                name: "IX_vault_tabs_vault_id",
                table: "vault_tabs",
                column: "vault_id");

            migrationBuilder.CreateIndex(
                name: "IX_vault_tabs_VaultId1",
                table: "vault_tabs",
                column: "VaultId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vault_tab_item");

            migrationBuilder.DropTable(
                name: "items");

            migrationBuilder.DropTable(
                name: "vault_tabs");

            migrationBuilder.DropTable(
                name: "affix");

            migrationBuilder.DropTable(
                name: "icon_sets");

            migrationBuilder.DropTable(
                name: "vaults");

            migrationBuilder.DropTable(
                name: "Icon");
        }
    }
}
