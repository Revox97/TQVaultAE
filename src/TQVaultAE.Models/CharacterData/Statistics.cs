using TQVaultAE.Models.PlayerData;

namespace TQVaultAE.Models.CharacterData
{
	public class Statistics
	{
		public GameDifficulty Difficulty { get; set; }

		public byte Level { get; set; }

		public TimeSpan TimePlayed { get; set; }

		public Mastery FirstMastery { get; set; }

		public Mastery SecondMastery { get; set; }

		public string Class { get; }

		public Attributes Attributes { get; set; }

		public int AvailableSkillPoints { get; set; }

		public int AvailableAttributePoints { get; set; }

		public long TotalXp { get; set; }

		public long XpFromKills { get; set; }

		public long Money { get; set; }

		public long Kills { get; set; }

		public long Deaths { get; set; }

		public long CriticalHitsDealt { get; set; }

		public long CriticalHitsReceived { get; set; }

		public long HitsDealt { get; set; }

		public long HitsReceived { get; set; }

		public long HealthPotsUsed { get; set; }

		public long EnergyPotsUsed { get; set; }

		public long EternalEmbersPotsUsed { get; set; }
	}

	public record struct Attributes(int Health, int Energy, int Strength, int Dexterity, int Intelligence);
}
