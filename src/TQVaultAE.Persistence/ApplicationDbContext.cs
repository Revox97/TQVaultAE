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
            List<Task> tasks = [];

            // Set up application information
            tasks.Add(Task.Run(async () => ApplicationInformation.Add(new ApplicationInformation())));

            // ButtonMain Icon Set
            tasks.Add(Task.Run(async () => await CreateIconSetAsync("ButtonMain",
                "ButtonMain_Down", new Uri("avares://TQVaultAE/Assets/Img/button_main_down.png"),
                "ButtonMain_Up", new Uri("avares://TQVaultAE/Assets/Img/button_main_up.png"),
                "ButtonMain_Hover", new Uri("avares://TQVaultAE/Assets/Img/button_main_hover.png")
            )));

            // ButtonClose Icon Set
            tasks.Add(Task.Run(async () => await CreateIconSetAsync("ButtonClose",
                "ButtonClose_Down", new Uri("avares://TQVaultAE/Assets/Img/button_close_down.png"),
                "ButtonClose_Up", new Uri("avares://TQVaultAE/Assets/Img/button_close_up.png"),
                "ButtonClose_Hover", new Uri("avares://TQVaultAE/Assets/Img/button_close_hover.png")
            )));

            // ButtonMinimize Icon Set
            tasks.Add(Task.Run(async () => await CreateIconSetAsync("ButtonMinimize",
                "ButtonMinimize_Down", new Uri("avares://TQVaultAE/Assets/Img/button_minimize_down.png"),
                "ButtonMinimize_Up", new Uri("avares://TQVaultAE/Assets/Img/button_minimize_up.png"),
                "ButtonMinimize_Hover", new Uri("avares://TQVaultAE/Assets/Img/button_minimize_hover.png")
            )));

            // ButtonMaximize Icon Set
            tasks.Add(Task.Run(async () => await CreateIconSetAsync("ButtonMaximize",
                "ButtonMaximize_Down", new Uri("avares://TQVaultAE/Assets/Img/button_maximize_down.png"),
                "ButtonMaximize_Up", new Uri("avares://TQVaultAE/Assets/Img/button_maximize_up.png"),
                "ButtonMaximize_Hover", new Uri("avares://TQVaultAE/Assets/Img/button_maximize_hover.png")
            )));

            // InventoryBag Icon Set
            tasks.Add(Task.Run(async () => await CreateIconSetAsync("InventoryBag",
                "InventoryBag_Down", new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_down.png"),
                "InventoryBag_Up", new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_up.png"),
                "InventoryBag_Hover", new Uri("avares://TQVaultAE/Assets/Img/button_inventorybag_hover.png")
            )));

            // Autosort Icon Set
            tasks.Add(Task.Run(async () => await CreateIconSetAsync("Autosort",
                "Autosort_Down", new Uri("avares://TQVaultAE/Assets/Img/button_autosort_down.png"),
                "Autosort_Up", new Uri("avares://TQVaultAE/Assets/Img/button_autosort_up.png"),
                "Autosort_Hover", new Uri("avares://TQVaultAE/Assets/Img/button_autosort_hover.png")
            )));

            // AutosortRotated Icon Set
            tasks.Add(Task.Run(async () => await CreateIconSetAsync("Autosort_Rotated",
                "Autosort_Rotated_Down", new Uri("avares://TQVaultAE/Assets/Img/button_autosort_rotated_down.png"),
                "Autosort_Rotated_Up", new Uri("avares://TQVaultAE/Assets/Img/button_autosort_rotated_up.png"),
                "Autosort_Rotated_Hover", new Uri("avares://TQVaultAE/Assets/Img/button_autosort_rotated_hover.png")
            )));

            // Simple icons
            tasks.Add(Task.Run(async () => await CreateIconAsync("MajesticChest", new Uri("avares://TQVaultAE/Assets/Img/icon_majestic_chest.png"))));
            tasks.Add(Task.Run(async () => await CreateIconAsync("Character", new Uri("avares://TQVaultAE/Assets/Img/icon_character.png"))));

            Task.WaitAll(tasks);
            await SaveChangesAsync();
        }

        public async Task<Icon> CreateIconAsync(string id, Uri uri)
        {
            Icon icon = new(id, uri);

            await Icons.AddAsync(icon);
            return icon;
        }

        public async Task CreateIconSetAsync(string id, string downId, Uri downUri, string upId, Uri upUri, string hoverId, Uri hoverUri)
        {
            Task<Icon> iconDown = CreateIconAsync(downId, downUri);
            Task<Icon> iconUp = CreateIconAsync(upId, upUri);
            Task<Icon> iconHover = CreateIconAsync(hoverId, hoverUri);

            await Icons.AddAsync(await iconDown);
            await Icons.AddAsync(await iconUp);
            await Icons.AddAsync(await iconHover);

            await CreateIconSetAsync(id, await iconDown, await iconUp, await iconHover);
        }

        public async Task CreateIconSetAsync(string id, Icon down, Icon up, Icon hover)
        {
            await IconSets.AddAsync(new IconSet(id, down, up, hover));
        }
    }
}
