using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TQVaultAE.Model.Settings;

namespace TQVaultAE.Persistence.Application.Configurations
{
    public class SettingsConfiguration : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            builder.ToTable("settings");

            builder.Property(x => x.Id)
                   .IsRequired()
                   .HasColumnName("id");

            builder.HasKey(x => x.Id);

            builder.ComplexProperty(x => x.General, g =>
            {
                g.Property(g => g.IsBypassConfirmationMessagesEnabled)
                 .IsRequired()
                 .HasColumnName("bypass_confirmation_messages");

                g.Property(g => g.IsLoadAllVaultAndCharacterDataOnStartupEnabled)
                 .IsRequired()
                 .HasColumnName("load_all_vault_and_character_data_on_startup");

                g.Property(g => g.IsHotReloadEnabled)
                 .IsRequired()
                 .HasColumnName("hot_reload");

                g.Property(g => g.IsAutoStackingEnabled)
                 .IsRequired()
                 .HasColumnName("auto_stacking");

                g.Property(g => g.IsBypassTitleScreenEnabled)
                 .IsRequired()
                 .HasColumnName("bypass_title_screen");

                g.Property(g => g.IsDetailedTooltipViewEnabled)
                 .IsRequired()
                 .HasColumnName("detailed_tooltip_view");

                g.Property(g => g.IsLoadLastOpenedCharacterAutomaticallyEnabled)
                 .IsRequired()
                 .HasColumnName("load_last_opened_character");

                g.Property(g => g.IsLoadLastOpenedVaultAutomaticallyEnabled)
                 .IsRequired()
                 .HasColumnName("load_last_opened_vault");

                g.Property(g => g.IsPlayerEquipmentReadOnlyEnabled)
                 .IsRequired()
                 .HasColumnName("player_equipment_read_only");

                g.ComplexProperty(g => g.BackupSettings, b =>
                {
                    b.Property(b => b.IsBackupEnabled)
                     .IsRequired()
                     .HasColumnName("backup");

                    b.Property(b => b.ArePlayerSavesEnabled)
                     .IsRequired()
                     .HasColumnName("backup_player_saves");

                    b.Property(b => b.GitRepositoryUrl)
                     .IsRequired()
                     .HasColumnName("backup_repository_url");
                });
            });

            builder.ComplexProperty(x => x.UserInterface, u =>
            {
                u.Property(u => u.ItemBackgroundTransparencyLevel)
                 .IsRequired()
                 .HasColumnName("item_background_transparency_level");

                u.Property(u => u.Font)
                 .IsRequired()
                 .HasColumnName("font");

                u.Property(u => u.AreSoundsEnabled)
                 .IsRequired()
                 .HasColumnName("play_sounds");

                u.Property(u => u.IsItemRequirementRestrictionEnabled)
                 .IsRequired()
                 .HasColumnName("item_requirements_restriction");

                // TODO add conversion, if necessary
                u.Property(u => u.Language)
                 .IsRequired()
                 .HasColumnName("language");
            });

            builder.ComplexProperty(x => x.Game, g =>
            {
                g.Property(g => g.AreCustomMapsEnabled)
                 .IsRequired()
                 .HasColumnName("custom_maps");

                g.Property(g => g.IsOriginalTqSupportEnabled)
                 .IsRequired()
                 .HasColumnName("original_tq_support");

                g.ComplexProperty(g => g.GamePaths, p =>
                {
                    p.Property(p => p.IsAutoDetectionEnabled)
                     .IsRequired()
                     .HasColumnName("paths_auto_detection");

                    p.Property(p => p.TitanQuestGamePath)
                     .IsRequired()
                     .HasColumnName("paths_titan_quest");

                    p.Property(p => p.ImmortalThroneGamePath)
                     .IsRequired()
                     .HasColumnName("paths_immortal_thronw");
                });
            });

            builder.ComplexProperty(x => x.Cheats, c =>
            {
                c.Property(c => c.AreCheatsEnabled)
                 .IsRequired()
                 .HasColumnName("cheats_enable");

                c.Property(c => c.IsItemCopyingEnabled)
                 .IsRequired()
                 .HasColumnName("cheats_item_copying");

                c.Property(c => c.AreCharacterEditingFeaturesEnabled)
                 .IsRequired()
                 .HasColumnName("cheats_character_editing");

                c.Property(c => c.AreEpicAndLegendaryAffixesEnabled)
                 .IsRequired()
                 .HasColumnName("cheats_epic_legendary_affixes");

                c.Property(c => c.AreItemEditingFeaturesEnabled)
                 .IsRequired()
                 .HasColumnName("cheats_item_editing");
            });
        }
    }
}
