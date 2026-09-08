namespace TQVaultAE.Model.Players
{
    /// <summary>
    /// Represents a <see cref="Player"/>.
    /// </summary>
    public sealed class Player
    {
        /// <summary>
        /// Gets the id of the <see cref="Player"/>.
        /// </summary>
        public Guid Id { get; init; }

        /// <summary>
        /// Gets the version of the <see cref="Player"/>.
        /// </summary>
        public int Version { get; init; }

        /// <summary>
        /// Gets the name of the <see cref="Player"/>.
        /// </summary>
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Gets the level of the <see cref="Player"/>.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets the class of the <see cref="Player"/>.
        /// </summary>
        public string Class { get; init; } = string.Empty;

        /// <summary>
        /// Gets the class tag of the <see cref="Player"/>.
        /// </summary>
        public string ClassTag { get; set; } = string.Empty;

        /// <summary>
        /// Gets the money of the <see cref="Player"/>.
        /// </summary>
        public int Money { get; set; } 

        /// <summary>
        /// Gets the ??wie auch immer die resource gleich nochmal hier?? of the <see cref="Player"/>.
        /// </summary>
        public int AltMoney { get; set; }

        /// <summary>
        /// Gets the experience of the <see cref="Player"/>.
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Gets the available attribute points of the <see cref="Player"/>.
        /// </summary>
        public int AvailableAttributePoints { get; set; }

        /// <summary>
        /// Gets the available skill points of the <see cref="Player"/>.
        /// </summary>
        public int AvailableSkillPoints { get; set; }

        /// <summary>
        /// Gets the statistics of the <see cref="Player"/>.
        /// </summary>
        public PlayerStatistics Statistics { get; set; } = new();

        /// <summary>
        /// Gets the <see cref="Sack"/>s of the <see cref="Player"/>.
        /// </summary>
        public List<Sack> Sacks { get; init; } = [];

        /// <summary>
        /// Gets the amount of <see cref="Sack"/>s of the <see cref="Player"/>.
        /// </summary>
        public int SackCount => Sacks.Count;

        /// <summary>
        /// Gets or sets the <see cref="Equipment"/> of the <see cref="Player"/>.
        /// </summary>
        public Equipment Equipment { get; set; } = new();
    }
}
