using System.Drawing;
using TQVaultAE.Domain.Contracts.Providers;
using TQVaultAE.Domain.Helpers;
using TQVaultAE.Domain.Results;

namespace TQVaultAE.Domain.Entities
{
	/// <summary>
	/// Shared data context for all services
	/// </summary>
	/// <remarks>must be agnostic so no Winform references. Only data</remarks>
	public class SessionContext(IItemProvider itemProvider)
	{
		private readonly IItemProvider ItemProvider = itemProvider;

		/// <summary>
		/// Currently selected player
		/// </summary>
		public PlayerCollection CurrentPlayer { get; set; }

		private BagButtonIconInfo _iconInfoCopy;

		/// <summary>
		/// Last icon info copied
		/// </summary>
		public BagButtonIconInfo IconInfoCopy
		{
			get => _iconInfoCopy;
			set
			{
				_iconInfoCopy = value;
				_iconInfoCopied = true;
			}
		}

		private bool _iconInfoCopied;

		/// <summary>
		/// Is there any IconInfo copied
		/// </summary>
		/// <remarks>this allow <see cref="IconInfoCopy"/> to have null relevant</remarks>
		public bool IconInfoCopied => _iconInfoCopied;

		/// <summary>
		/// Dictionary of all loaded player files
		/// </summary>
		public readonly LazyConcurrentDictionary<string, PlayerCollection> Players = new();

		/// <summary>
		/// Dictionary of all loaded vault files
		/// </summary>
		public readonly LazyConcurrentDictionary<string, PlayerCollection> Vaults = new();

		/// <summary>
		/// Dictionary of all loaded player stash files
		/// </summary>
		public readonly LazyConcurrentDictionary<string, Stash> Stashes = new();

		/// <summary>
		/// Gets or sets the background Color for item highlight.
		/// </summary>
		public Color HighlightSearchItemColor { get; set; } = TQColor.Indigo.Color();

		/// <summary>
		/// Gets or sets the border color for item highlight.
		/// </summary>
		public Color HighlightSearchItemBorderColor { get; set; } = TQColor.Red.Color();

		/// <summary>
		/// Hightlight search string
		/// </summary>
		public string? HighlightSearch { get; set; }

		/// <summary>
		/// Hightlight search filters
		/// </summary>
		public HighlightFilterValues? HighlightFilter { get; set; }

		/// <summary>
		/// Hightlight search items
		/// </summary>
		public readonly List<Item> HighlightedItems = [];

		/// <summary>
		/// Find items to highlight
		/// </summary>
		public void FindHighlight()
		{
			bool hasSearch = !string.IsNullOrWhiteSpace(HighlightSearch);
			bool hasFilter = HighlightFilter is not null;

			if (hasSearch || hasFilter)
			{
				HighlightedItems.Clear();
				
				// Check for players
				IEnumerable<SackCollection> sacksplayers = Players.Select(p => p.Value.Value)
					.SelectMany(p => {
						List<SackCollection> retval = [];

						if (p.EquipmentSack is not null)
							retval.Add(p.EquipmentSack);

						if (p.Sacks is not null)
							retval.AddRange(p.Sacks);

						return retval;
					})
					.Where(s => s.Count > 0);

				// Check for Vaults
				IEnumerable<SackCollection> sacksVault = Vaults.Select(p => p.Value.Value)
					.SelectMany(p => p.Sacks)
					.Where(s => s is not null && s.Count > 0);

				// Check for Stash
				IEnumerable<SackCollection> sacksStash = Stashes.Select(p => p.Value.Value)
					.Select(p => p.Sack)
					.Where(s => s is not null && s.Count > 0);

				var availableItems = sacksplayers.Concat(sacksVault).Concat(sacksStash).SelectMany(i => i)
					.Select(i =>
					{
						ToFriendlyNameResult? fnr = ItemProvider.GetFriendlyNames(i, FriendlyNamesExtraScopes.ItemFullDisplay);

						return new
						{
							Item = i,
							FriendlyNames = fnr,
							Info = fnr.RequirementInfo,
						};
					}).AsQueryable();

				if (hasSearch)
				{
					var (isRegex, _, regex, regexIsValid) = StringHelper.IsTQVaultSearchRegEx(this.HighlightSearch);

					availableItems = availableItems.Where(i =>
						isRegex && regexIsValid
							? i.FriendlyNames.FulltextIsMatchRegex(regex)
							: i.FriendlyNames.FulltextIsMatchIndexOf(this.HighlightSearch)
					);
				}

				if (hasFilter)
				{
					if (HighlightFilter.MinRequierement)
					{
						// Min Lvl
						if (HighlightFilter.MinLvl != 0)
						{
							availableItems = availableItems.Where(i =>
								!i.Info.Lvl.HasValue // Item doesn't have requirement
								|| i.Info.Lvl >= HighlightFilter.MinLvl
							);
						}
						// Min Dex
						if (HighlightFilter.MinDex != 0)
						{
							availableItems = availableItems.Where(i =>
								!i.Info.Dex.HasValue
								|| i.Info.Dex >= HighlightFilter.MinDex
							);
						}
						// Min Str
						if (HighlightFilter.MinStr != 0)
						{
							availableItems = availableItems.Where(i =>
								!i.Info.Str.HasValue
								|| i.Info.Str >= HighlightFilter.MinStr
							);
						}
						// Min Int
						if (HighlightFilter.MinInt != 0)
						{
							availableItems = availableItems.Where(i =>
								!i.Info.Int.HasValue
								|| i.Info.Int >= HighlightFilter.MinInt
							);
						}
					}

					if (HighlightFilter.MaxRequierement)
					{
						// Max Lvl
						if (HighlightFilter.MaxLvl != 0)
						{
							availableItems = availableItems.Where(i =>
								!i.Info.Lvl.HasValue // Item doesn't have requirement
								|| i.Info.Lvl <= HighlightFilter.MaxLvl
							);
						}
						// Max Dex
						if (HighlightFilter.MaxDex != 0)
						{
							availableItems = availableItems.Where(i =>
								!i.Info.Dex.HasValue
								|| i.Info.Dex <= HighlightFilter.MaxDex
							);
						}
						// Max Str
						if (HighlightFilter.MaxStr != 0)
						{
							availableItems = availableItems.Where(i =>
								!i.Info.Str.HasValue
								|| i.Info.Str <= HighlightFilter.MaxStr
							);
						}
						// Max Int
						if (HighlightFilter.MaxInt != 0)
						{
							availableItems = availableItems.Where(i =>
								!i.Info.Int.HasValue
								|| i.Info.Int <= HighlightFilter.MaxInt
							);
						}
					}

					if (HighlightFilter.ClassItem.Count != 0)
					{
						availableItems = availableItems.Where(i =>
							HighlightFilter.ClassItem
							.Any(ci => ci.Equals(i.Item.ItemClass, StringComparison.OrdinalIgnoreCase))
						);
					}

					if (HighlightFilter.Rarity.Count != 0)
					{
						availableItems = availableItems.Where(i =>
							HighlightFilter.Rarity.Contains(i.Item.Rarity)
						);
					}

					if (HighlightFilter.Origin.Count != 0)
					{
						availableItems = availableItems.Where(i =>
							HighlightFilter.Origin.Contains(i.Item.GameDlc)
						);
					}

					if (HighlightFilter.HavingPrefix)
						availableItems = availableItems.Where(i => i.Item.HasPrefix);

					if (HighlightFilter.HavingSuffix)
						availableItems = availableItems.Where(i => i.Item.HasSuffix);
					
					if (HighlightFilter.HavingRelic)
						availableItems = availableItems.Where(i => i.Item.HasRelic);

					if (HighlightFilter.HavingCharm)
						availableItems = availableItems.Where(i => i.Item.HasCharm);

					if (HighlightFilter.IsSetItem)
						availableItems = availableItems.Where(i => i.FriendlyNames.ItemSet != null);
				}

				HighlightedItems.AddRange([.. availableItems.Select(i => i.Item)]);
				return;
			}

			ResetHighlight();
		}

		/// <summary>
		/// Reset Hightlight search
		/// </summary>
		public void ResetHighlight()
		{
			HighlightedItems.Clear();
			HighlightSearch = null;
			HighlightFilter = null;
		}
	}
}
