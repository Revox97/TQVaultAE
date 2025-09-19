namespace TQVaultAE.Models.Game
{
	public static class ItemExtensions
	{
		public static bool IsLocationOverlap(this Item item, Item other)
		{
			if (other == item)
				return false;

			for (int i = other.Location.X; i < other.Location.X + other.Size.Width - 1; ++i)
			{
				for (int k = item.Location.X; k < item.Location.X + item.Size.Width - 1; ++k)
				{
					if (i == k)
						return true;
				}
			}

			for (int i = other.Location.Y; i < other.Location.Y + other.Size.Height - 1; ++i)
			{
				for (int k = item.Location.Y; k < item.Location.Y + item.Size.Height - 1; ++k)
				{
					if (i == k)
						return true;
				}
			}

			return false;
		}

		public static bool CanPlayerEquip(this Item item, Player player, int percentageStrength = 100, int percentageDexterity = 100, int percentageIntelligence = 100)
		{
			if (player.Statistics.Level < item.Requirements.Level)
				return false;

			int actualStrengthRequirement = item.Requirements.Strength / 100 * percentageStrength;
			if (player.Statistics.Attributes.Strength < actualStrengthRequirement) 
				return false;

			int actualDexterityRequirement = item.Requirements.Dexterity / 100 * percentageDexterity;
			if (player.Statistics.Attributes.Dexterity < actualDexterityRequirement) 
				return false;

			int actualIntelligenceRequirement = item.Requirements.Intelligence / 100 * percentageIntelligence;
			if (player.Statistics.Attributes.Intelligence < actualIntelligenceRequirement)
				return false;

			return true;
		}
	}
}
