using System.Text.RegularExpressions;
using TQVaultAE.Domain.Entities;
using TQVaultAE.Domain.Helpers;

namespace TQVaultAE.Domain.Results;

public partial class ToFriendlyNameResult(Item itm)
{

	private IEnumerable<string?>? _fullText = null;

	private void FulltextBuild()
	{
		// No mutation, just a comprehensive list of string ref 
		_fullText ??= new[] {
			[
				FullName
				, ItemThrown
				, ArtifactClass
				, RelicCompletionFormat
				, RelicBonusFormat
				, ItemSeed
				, ItemOrigin 
				// Socketed Relic name
				, Item.HasRelicOrCharmSlot1 ? RelicInfo1Description : null
				, Item.HasRelicOrCharmSlot2 ? RelicInfo2Description : null
				// Socketed Relic Completion label
				, Item.HasRelicOrCharmSlot1 ? RelicInfo1CompletionResolved : null
				, Item.HasRelicOrCharmSlot2 ? RelicInfo2CompletionResolved : null
				// Socketed Relic Completion Bonus label
				, Item.HasRelicOrCharmSlot1 ? RelicInfo1CompletionBonusResolved : null
				, Item.HasRelicOrCharmSlot2 ? RelicInfo2CompletionBonusResolved : null
			]
			, AttributesAll
			, FlavorText
			, ItemSet?.Translations.Select(si => si.Value).ToArray() ?? []
			, Requirements
		}.SelectMany(s => s).Where(s => !string.IsNullOrEmpty(s));
	}


	public bool FulltextIsMatch(string search)
	{
		if (string.IsNullOrWhiteSpace(search)) 
			return false;

		(bool IsRegex, string Pattern, Regex? Regex, bool RegexIsValid) isrex = StringHelper.IsTQVaultSearchRegEx(search);

		return isrex.IsRegex 
			? FulltextIsMatchRegex(isrex.Pattern) 
			: FulltextIsMatchIndexOf(search);
	}

	public bool FulltextIsMatchIndexOf(string search)
	{
		if (string.IsNullOrWhiteSpace(search)) 
			return false;

		FulltextBuild();

		if (_fullText is null)
			return false;

		foreach (string? str in _fullText)
		{
			if (str is not null && str.ContainsIgnoreCase(search)) 
				return true;
		}

		return false;
	}

	public bool FulltextIsMatchRegex(string pattern)
	{
		if (string.IsNullOrWhiteSpace(pattern)) 
			return false;

		FulltextBuild();

		try
		{
			Regex rex = new(pattern, RegexOptions.IgnoreCase);
			return FulltextIsMatchRegex(rex);
		}
		catch (ArgumentException) { }
		return false;
	}

	public bool FulltextIsMatchRegex(Regex pattern)
	{
		if (pattern is null) 
			return false;

		FulltextBuild();

		if (_fullText is null)
			return false;

		foreach (string? str in _fullText)
		{
			if (str is not null && pattern.IsMatch(str))
				return true;
		}

		return false;
	}

	public string FullNameClean => FullName.RemoveAllTQTags();

	public string FullName => new string[] {
			PrefixInfoDescription
			, BaseItemInfoQuality
			, BaseItemInfoDescription
			, Item.IsThrownWeapon ? null : BaseItemInfoStyle
			, SuffixInfoDescription
			, Item.DoesStack ? NumberFormat : null
		}.RemoveEmptyAndSanitize()
	.JoinWithoutStartingSpaces(" ");

	public string FullNameBagTooltipClean => FullNameBagTooltip.RemoveAllTQTags();

	public string FullNameBagTooltip => new string[] {
				PrefixInfoDescription
				, BaseItemInfoQuality
				, Item.IsRelicOrCharm && !Item.IsCharmOnly ? TQColor.Silver.ColorTag() : null // Make a color diff for Relic & Charm
				, BaseItemInfoDescription
				, Item.IsThrownWeapon ? null : BaseItemInfoStyle
				, SuffixInfoDescription
				, Item.DoesStack ? NumberFormat : null
				, Item.IsRelicOrCharm ? "- " + RelicBonusFormat : null
				, Item.IsQuestItem ? this.ItemQuest : null
				, Item.GameDlcSuffix
			}.RemoveEmptyAndSanitize()
		.JoinWithoutStartingSpaces(" ");

	public readonly Item Item = itm;
	public string PrefixInfoDescription;
	public string[] PrefixAttributes;
	public DBRecordCollection PrefixInfoRecords;
	public string SuffixInfoDescription;
	public string[] SuffixAttributes;
	public DBRecordCollection SuffixInfoRecords;

	public RecordId BaseItemId;
	public string BaseItemRarity;

	private static readonly Regex s_baseItemInfoClassRegEx = BaseItemInfoClassRegex();
	private string _baseItemInfoClass;

	public string BaseItemInfoClass
	{
		get => _baseItemInfoClass;
		set => _baseItemInfoClass = s_baseItemInfoClassRegEx.Replace((value ?? string.Empty), string.Empty);// Clean everything except few things;
	}

	public string BaseItemInfoStyle;
	public string BaseItemInfoQuality;
	public string BaseItemInfoDescription;
	public string ItemSeed;
	public string ItemQuest;
	public string ItemOrigin;
	public string ItemThrown;
	public string NumberFormat;
	public string ItemWith;
	public string[] FlavorText = [];
	public string[] Requirements = [];
	public SortedList<string, Variable> RequirementVariables;
	public RequirementInfo RequirementInfo;
	public string[] BaseAttributes = [];
	public SetItemInfo ItemSet;
	public DBRecordCollection BaseItemInfoRecords;

	// Relic common
	public string AnimalPartComplete;
	public string AnimalPartCompleteBonus;
	public string AnimalPart;
	public string AnimalPartRatio;
	public string RelicComplete;
	public string RelicBonus;
	public string RelicShard;
	public string RelicRatio;
	public string RelicClass;
	public string RelicCompletionFormat;
	public string RelicPattern;
	public string RelicBonusPattern;
	public string RelicBonusFormat;
	public string RelicBonusFileName;
	public string RelicBonusTitle;
	public DBRecordCollection RelicInfoRecords;

	// RelicInfo 1 & 2
	public string ItemRelicBonus1Format;
	public string ItemRelicBonus2Format;
	public string RelicInfo1Description;
	public string RelicInfo2Description;
	public string[] Relic1Attributes = [];
	public string[] Relic2Attributes = [];
	public string[] RelicBonus1Attributes = [];
	public string[] RelicBonus2Attributes = [];
	public DBRecordCollection RelicBonus1InfoRecords;
	public DBRecordCollection RelicBonus2InfoRecords;
	public DBRecordCollection Relic2InfoRecords;

	/// <summary>
	/// Resolve first relic completion label
	/// </summary>
	public string? RelicInfo1CompletionResolved
	{
		get
		{
			if (Item.RelicInfo is null) 
				return null;

			if (Item.IsRelic1Charm)
			{
				if (Item.IsRelicBonus1Complete)
					return AnimalPartComplete;

				return string.Format(AnimalPartRatio,
 									 AnimalPart,
									 Item.Var1,
									 Item.RelicBonusInfo?.CompletedRelicLevel.ToString() ?? "??"
				);
			}

			if (Item.IsRelicBonus1Complete)
				return RelicComplete;

			return string.Format(RelicRatio,
								 RelicShard,
								 Item.Var1,
								 Item.RelicBonusInfo?.CompletedRelicLevel.ToString() ?? "??"
			);
		}
	}

	/// <summary>
	/// Resolve first relic completion bonus label
	/// </summary>
	public string? RelicInfo1CompletionBonusResolved
	{
		get
		{
			if (!Item.IsRelicBonus1Complete)
				return null;

			return Item.IsRelic1Charm ? AnimalPartCompleteBonus : RelicBonus;
		}
	}

	/// <summary>
	/// Resolve second relic completion label
	/// </summary>
	public string? RelicInfo2CompletionResolved
	{
		get
		{
			if (Item.Relic2Info is null) 
				return null;

			if (Item.IsRelic2Charm)
			{
				if (Item.IsRelicBonus2Complete)
					return AnimalPartComplete;

				return string.Format(AnimalPartRatio,
									 AnimalPart,
									 Item.Var2,
									 Item.RelicBonus2Info?.CompletedRelicLevel.ToString() ?? "??"
				);
			}

			if (Item.IsRelicBonus2Complete)
				return RelicComplete;

			return string.Format(RelicRatio,
				RelicShard,
				Item.Var2,
				Item.RelicBonus2Info?.CompletedRelicLevel.ToString() ?? "??"
			);
		}
	}

	/// <summary>
	/// Resolve second relic completion bonus label
	/// </summary>
	public string? RelicInfo2CompletionBonusResolved
	{
		get
		{
			if (!Item.IsRelicBonus2Complete)
				return null;

			return Item.IsRelic2Charm ? AnimalPartCompleteBonus: RelicBonus;
		}
	}

	public string ArtifactClass;
	public string ArtifactRecipe;
	public string ArtifactReagents;
	public string ArtifactBonus;
	public string ArtifactBonusFormat;

	public string FormulaeArtifactClass;
	public string FormulaeArtifactName;
	public string FormulaeFormat;
	public string[] FormulaeArtifactAttributes = [];
	public DBRecordCollection FormulaeArtifactRecords;
	string[]? _AttributesAll = null;

	/// <summary>
	/// Return the collection of all attributes without any color tags.
	/// <see cref="BaseAttributes"/>
	/// <see cref="FormulaeArtifactAttributes"/>
	/// <see cref="PrefixAttributes"/>
	/// <see cref="SuffixAttributes"/>
	/// <see cref="Relic1Attributes"/>
	/// <see cref="Relic2Attributes"/>
	/// <see cref="RelicBonus1Attributes"/>
	/// <see cref="RelicBonus2Attributes"/>
	/// </summary>
	public string[] AttributesAll
	{
		get
		{
			_AttributesAll ??= [.. new string[][] {
					BaseAttributes
					, PrefixAttributes
					, SuffixAttributes
					, Relic1Attributes
					, Relic2Attributes
					, RelicBonus1Attributes
					, RelicBonus2Attributes
					, FormulaeArtifactAttributes
				}.Where(static c => c?.Any() ?? false)
				.SelectMany(a => a)
				.Where(a => !(string.IsNullOrWhiteSpace(a) || a.IsColorTagOnly()))
				.Select(a => a.RemoveAllTQTags())];
			return _AttributesAll;
		}
	}

	/// <summary>
	/// Used to give attribute list factory some kind of global awareness during its process
	/// </summary>
	public readonly List<string> TmpAttrib = [];

	[GeneratedRegex(@"[^\w\s']", RegexOptions.Compiled)]
	internal static partial Regex BaseItemInfoClassRegex();
}
