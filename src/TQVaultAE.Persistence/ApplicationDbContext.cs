using Microsoft.EntityFrameworkCore;
using TQVaultAE.Model;
using TQVaultAE.Model.Settings;
using TQVaultAE.Model.UI;

namespace TQVaultAE.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Icon> Icons => Set<Icon>();

        public DbSet<IconSet> IconSets => Set<IconSet>();

        public DbSet<ApplicationInformation> ApplicationInformation => Set<ApplicationInformation>();

        public DbSet<User> Users => Set<User>();

        public DbSet<Settings> Settings => Set<Settings>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly,
                type => type.Namespace?.StartsWith("TQVaultAE.Persistence.Application") == true);

            base.OnModelCreating(modelBuilder);
        }

        public async Task CreateDatabaseAsync()
        {
            // Set up application information
            ApplicationInformation.Add(new ApplicationInformation());

            // ButtonMain Icon Set
            await CreateIconSetAsync(
                "ButtonMain",
                "ButtonMain_Down", "avares://TQVaultAE/Assets/Img/button_main_down.png",
                "ButtonMain_Up", "avares://TQVaultAE/Assets/Img/button_main_up.png",
                "ButtonMain_Hover", "avares://TQVaultAE/Assets/Img/button_main_hover.png"
            );

            // ButtonClose Icon Set
            await CreateIconSetAsync(
                "ButtonClose",
                "ButtonClose_Down", "avares://TQVaultAE/Assets/Img/button_close_down.png",
                "ButtonClose_Up", "avares://TQVaultAE/Assets/Img/button_close_up.png",
                "ButtonClose_Hover", "avares://TQVaultAE/Assets/Img/button_close_hover.png"
            );

            // ButtonMinimize Icon Set
            await CreateIconSetAsync(
                "ButtonMinimize",
                "ButtonMinimize_Down", "avares://TQVaultAE/Assets/Img/button_minimize_down.png",
                "ButtonMinimize_Up", "avares://TQVaultAE/Assets/Img/button_minimize_up.png",
                "ButtonMinimize_Hover", "avares://TQVaultAE/Assets/Img/button_minimize_hover.png"
            );

            // ButtonMaximize Icon Set
            await CreateIconSetAsync(
                "ButtonMaximize",
                "ButtonMaximize_Down", "avares://TQVaultAE/Assets/Img/button_maximize_down.png",
                "ButtonMaximize_Up", "avares://TQVaultAE/Assets/Img/button_maximize_up.png",
                "ButtonMaximize_Hover", "avares://TQVaultAE/Assets/Img/button_maximize_hover.png"
            );

            // InventoryBag Icon Set
            await CreateIconSetAsync(
                "InventoryBag",
                "InventoryBag_Down", "avares://TQVaultAE/Assets/Img/button_inventorybag_down.png",
                "InventoryBag_Up", "avares://TQVaultAE/Assets/Img/button_inventorybag_up.png",
                "InventoryBag_Hover", "avares://TQVaultAE/Assets/Img/button_inventorybag_hover.png"
            );

            // Autosort Icon Set
            await CreateIconSetAsync(
                "Autosort",
                "Autosort_Down", "avares://TQVaultAE/Assets/Img/button_autosort_down.png",
                "Autosort_Up", "avares://TQVaultAE/Assets/Img/button_autosort_up.png",
                "Autosort_Hover", "avares://TQVaultAE/Assets/Img/button_autosort_hover.png"
            );

            // AutosortRotated Icon Set
            await CreateIconSetAsync(
                "Autosort_Rotated",
                "Autosort_Rotated_Down", "avares://TQVaultAE/Assets/Img/button_autosort_rotated_down.png",
                "Autosort_Rotated_Up", "avares://TQVaultAE/Assets/Img/button_autosort_rotated_up.png",
                "Autosort_Rotated_Hover", "avares://TQVaultAE/Assets/Img/button_autosort_rotated_hover.png"
            );

            // Simple icons
            await CreateIconAsync("MajesticChest", "avares://TQVaultAE/Assets/Img/icon_majestic_chest.png");
            await CreateIconAsync("Character", "avares://TQVaultAE/Assets/Img/icon_character.png");

            await SaveChangesAsync();
        }

        public async Task<Icon> CreateIconAsync(string id, string iconResourcePath)
        {
            Icon icon = new(id, iconResourcePath);

            await Icons.AddAsync(icon);
            return icon;
        }

        public async Task CreateIconSetAsync(string id, string downId, string downUri, string upId, string upUri, string hoverId, string hoverUri)
        {
            try
            {
                Task<Icon> iconDown = CreateIconAsync(downId, downUri);
                Task<Icon> iconUp = CreateIconAsync(upId, upUri);
                Task<Icon> iconHover = CreateIconAsync(hoverId, hoverUri);

                await Icons.AddAsync(await iconDown);
                await Icons.AddAsync(await iconUp);
                await Icons.AddAsync(await iconHover);

                await IconSets.AddAsync(new IconSet()
                {
                    Id = id,
                    IconDownId = downId,
                    IconUpId = upId,
                    IconHoverId = hoverId
                });
            }
            catch(Exception ex)
            {

            }
        }
    }
}
