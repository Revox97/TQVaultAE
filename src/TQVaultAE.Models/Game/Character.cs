using System.ComponentModel.DataAnnotations;
using TQVaultAE.Models.Game.Enumerations;

namespace TQVaultAE.Models.Game
{
    public class Character
    {
        public GameVersion GameVersion { get; set; }

        public bool IsArchived { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Range(0, 4)]
        public int NumberOfSacks { get; set; } = 1;

        public Dictionary<int, List<Item>> Sacks { get; set; } = [];

        [Required]
        public Statistics Statistics { get; set; }

        [Required]
        public Equipment Equipment { get; set; }

        public Stash StorageArea { get; set; }
    }
}
