namespace TQVaultAE.Model.Players
{
    public sealed class PlayerStatistics
    {
        /// <summary>
        /// Gets or sets the play time of a <see cref="Player"/>.
        /// </summary>
        public TimeSpan PlayTime { get; set; }

        /// <summary>
        /// Gets or sets the amount of kills of a <see cref="Player"/>.
        /// </summary>
        public int Kills { get; set; }

        /// <summary>
        /// Gets or sets the amount of deaths of a <see cref="Player"/>.
        /// </summary>
        public int Deaths { get; set; }

        /// <summary>
        /// Gets or sets the amount of experience of a <see cref="Player"/> earned by killing monsters.
        /// </summary>
        public int ExperienceFromKills { get; set; }

        /// <summary>
        /// Gets or sets the amount of used health potions by a <see cref="Player"/>.
        /// </summary>
        public int HealthPotionsUsed { get; set; }

        /// <summary>
        /// Gets or sets the amount of used energy potions by a <see cref="Player"/>.
        /// </summary>
        public int EnergyPotionsUsed { get; set; }

        /// <summary>
        /// Not exactly sure what this is
        /// </summary>
        public int MaxLevel { get; set; }

        /// <summary>
        /// Gets or sets the number of hits a <see cref="Player"/> has received.
        /// </summary>
        public int HitsReceived { get; set; }

        /// <summary>
        /// Gets or sets the number of critical hits a <see cref="Player"/> has received.
        /// </summary>
        public int CriticalHitsReceived { get; set; }

        /// <summary>
        /// Gets or sets the number of hits a <see cref="Player"/> has inflicted.
        /// </summary>
        public int HitsInflicted { get; set; }

        /// <summary>
        /// Gets or sets the number of critical hits a <see cref="Player"/> has inflicted.
        /// </summary>
        public int CriticalHitsInflicted { get; set; }

        /// <summary>
        /// Gets or sets the greatest damage a <see cref="Player"/> has inflicted.
        /// </summary>
        public int GreatestDamageInflicted { get; set; }
    }
}
