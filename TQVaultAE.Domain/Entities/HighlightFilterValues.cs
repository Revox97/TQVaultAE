namespace TQVaultAE.Domain.Entities
{
	public class HighlightFilterValues
	{
		public int MaxStr { get; set; }

		public int MaxDex { get; set; }

		public int MaxInt { get; set; }

		public int MinStr { get; set; }

		public int MinDex {get; set; }

		public int MinInt {get; set; }

		public bool MinRequierement { get; set; }

		public bool MaxRequierement { get; set; }

		public List<string> ClassItem = [];

		public List<Rarity> Rarity = [];

		public List<GameDlc> Origin = [];

		public int MaxLvl {get; set; }

		public int MinLvl {get; set; }

		public bool HavingPrefix {get; set; }

		public bool HavingSuffix {get; set; }

		public bool HavingRelic {get; set; }

		public bool HavingCharm {get; set; }

		public bool IsSetItem {get; set; }
	}
}
