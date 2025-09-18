//-----------------------------------------------------------------------
// <copyright file="Item.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Drawing;
using TQVaultAE.Domain.Results;
using static TQVaultAE.Domain.Entities.RelicAndCharmExtension;
//using TQVaultAE.Domain.Results;

namespace TQVaultAE.Domain.Entities;

/// <summary>
/// Class for holding item information
/// </summary>
public class Item
{
	private const StringComparison NoCase = StringComparison.OrdinalIgnoreCase;

	/// <summary>
	/// Default value for empty var2.
	/// </summary>
	public const int Var2Default = 2035248;

	/// <summary>
	/// Random number used as a seed for new items
	/// </summary>
	private static readonly Random s_random = new();

	/// <summary>
	/// Binary marker for the beginning of a block
	/// </summary>
	public int BeginBlockCrap1 { get; set; }

	/// <summary>
	/// Binary marker for the end of a block
	/// </summary>
	public int EndBlockCrap1 {get; set; }

	/// <summary>
	/// A different binary marker for the beginning of a block
	/// </summary>
	public int BeginBlockCrap2 {get; set; }

	/// <summary>
	/// A different binary marker for the end of a block
	/// </summary>
	public int EndBlockCrap2 {get; set; }

	public bool Atlantis { get; set; } = false;

	/// <summary>
	/// Prefix database record ID
	/// </summary>
	public RecordId PrefixId {get; set; }

	/// <summary>
	/// Suffix database record ID
	/// </summary>
	public RecordId SuffixId {get; set; }

	/// <summary>
	/// Relic database record ID
	/// </summary>
	public RecordId RelicId {get; set; }

	/// <summary>
	/// Relic database record ID
	/// </summary>
	public RecordId Relic2Id {get; set; }

	/// <summary>
	/// Info structure for the base item
	/// </summary>
	public Info BaseItemInfo {get; set; }

	/// <summary>
	/// Info structure for the item's prefix
	/// </summary>
	public Info PrefixInfo {get; set; }

	/// <summary>
	/// Info structure for the item's suffix
	/// </summary>
	public Info SuffixInfo {get; set; }

	/// <summary>
	/// Used for level calculation
	/// </summary>
	public int AttributeCount {get; set; }

	/// <summary>
	/// Used so that attributes are not counted multiple times
	/// </summary>
	public bool IsAttributeCounted {get; set; }

	/// <summary>
	/// Used for itemScalePercent calculation
	/// </summary>
	public float ItemScalePercent {get; set; }

	/// <summary>
	/// Initializes a new instance of the Item class.
	/// </summary>
	public Item()
	{
		// Added by VillageIdiot <-- Oh no not this guy again. Dude this is what git history is for lol
		// Used for bare item attributes in properties display in this order:
		// baseinfo, artifactCompletionBonus, prefixinfo, suffixinfo, relicinfo, relicCompletionBonus
		ItemScalePercent = 1.00F;
		StackSize = 1;
	}

	public bool HasPrefix => !RecordId.IsNullOrEmpty(PrefixId);

	public bool HasSuffix => !RecordId.IsNullOrEmpty(SuffixId);

	/// <summary>
	/// Gets the value indicating whether the item allows 2 relic socketing
	/// </summary>
	public bool AcceptExtraRelic => HasSuffix && SuffixId.Normalized.EndsWith("RARE_EXTRARELIC_01.DBR"); // TODO magic string

	/// <summary>
	/// Tell if the item is modified
	/// </summary>
	public bool IsModified { get; set; }

	/// <summary>
	/// Gets the weapon slot indicator value.
	/// This is a special value in the coordinates that signals an item is in a weapon slot.
	/// </summary>
	public const int WeaponSlotIndicator = -3;

	/// <summary>
	/// Gets the base item id
	/// </summary>
	public RecordId BaseItemId { get; set; }

	/// <summary>
	/// Gets or sets the relic bonus id
	/// </summary>
	public RecordId RelicBonusId { get; set; }

	/// <summary>
	/// Gets or sets the relic bonus2 id
	/// </summary>
	public RecordId RelicBonus2Id { get; set; }

	/// <summary>
	/// Gets or sets the item seed
	/// </summary>
	public int Seed { get; set; }

	/// <summary>
	/// Gets the number of relics
	/// </summary>
	private int _var1; // What a nice variable name dude

	/// <summary>
	/// Last <see cref="ToFriendlyNameResult"/> queried for this item.
	/// </summary>
	public ToFriendlyNameResult CurrentFriendlyNameResult { get; set; }

	public int Var1 // Come on at least try please
	{
		get
		{
			// The "Power of Nerthus" relic is a special quest-reward relic with only 1 shard (it is always complete). 
			// Since its database var1 value is 0, we hard-set it to 1.
			return (IsRelicOrCharm && _var1 == 0) // TODO Cool additional magic numbers
				? 1
				: _var1;
		}
		set => _var1 = value;
	}

	/// <summary>
	/// Gets the number of relics for the second relic slot
	/// </summary>
	public int Var2 { get; set; } // Seriously?!?!?

	/// <summary>
	/// Gets or sets the stack size.
	/// Used for stackable items like potions.
	/// </summary>
	public int StackSize { get; set; }

	/// <summary>
	/// Gets or sets the item's upper left corner in cell coordinates.
	/// </summary>
	public Point Location { get; set; }

	/// <summary>
	/// Gets the relic info
	/// </summary>
	public Info RelicInfo { get; set; }

	/// <summary>
	/// Gets or sets the relic bonus info
	/// </summary>
	public Info RelicBonusInfo { get; set; }

	/// <summary>
	/// Gets the second relic info
	/// </summary>
	public Info Relic2Info { get; set; }

	/// <summary>
	/// Gets or sets the second relic bonus info
	/// </summary>
	public Info RelicBonus2Info { get; set; }

	/// <summary>
	/// Raw image data from game resource file
	/// </summary>
	public byte[] TexImage { get; set; }

	/// <summary>
	/// ResourceId related to <see cref="TexImage"/>
	/// </summary>
	public RecordId TexImageResourceId { get; set; }

	/// <summary>
	/// Gets the item's size in cells.
	/// </summary>
	public Size Size { get; set; }

	/// <summary>
	/// Gets the item's right cell location
	/// </summary>
	public int Right => Location.X + Size.Width;

	/// <summary>
	/// Gets the item's bottom cell location.
	/// </summary>
	public int Bottom => Location.Y + Size.Height;


	/// <summary>
	/// Gets or sets the item container type
	/// </summary>
	public SackType ContainerType { get; set; }

	/// <summary>
	/// Gets a value indicating whether the item is in an equipment weapon slot.
	/// </summary>
	public bool IsInWeaponSlot => Location.X == WeaponSlotIndicator;

	/// <summary>
	/// Gets a value indicating whether or not the item comes from Immortal Throne expansion pack.
	/// </summary>
	public bool IsImmortalThrone
		=> (BaseItemId.Dlc == GameDlc.ImmortalThrone
		|| (PrefixId?.Dlc.Equals(GameDlc.ImmortalThrone) ?? false)
		|| (SuffixId?.Dlc.Equals(GameDlc.ImmortalThrone) ?? false)
		) && !IsRagnarok;

	/// <summary>
	/// Gets a value indicating whether or not the item comes from Ragnarok DLC.
	/// </summary>
	public bool IsRagnarok
		=> (BaseItemId.Dlc == GameDlc.Ragnarok
		|| (PrefixId?.Dlc.Equals(GameDlc.Ragnarok) ?? false)
		|| (SuffixId?.Dlc.Equals(GameDlc.Ragnarok) ?? false)
		) && !IsAtlantis;

	/// <summary>
	/// Gets a value indicating whether or not the item comes from Atlantis DLC.
	/// </summary>
	public bool IsAtlantis
		=> (BaseItemId.Dlc == GameDlc.Atlantis
		|| (PrefixId?.Dlc.Equals(GameDlc.Atlantis) ?? false)
		|| (SuffixId?.Dlc.Equals(GameDlc.Atlantis) ?? false)
		) && !IsEmbers;

	/// <summary>
	/// Gets a value indicating whether or not the item comes from Eternal Embers DLC.
	/// </summary>
	public bool IsEmbers
		=> BaseItemId.Dlc == GameDlc.EternalEmbers
		|| (PrefixId?.Dlc.Equals(GameDlc.EternalEmbers) ?? false)
		|| (SuffixId?.Dlc.Equals(GameDlc.EternalEmbers) ?? false);


	/// <summary>
	/// Gets a value indicating whether the item is a scroll.
	/// </summary>
	public bool IsScroll
	{
		get
		{
			return BaseItemInfo is not null
				? BaseItemInfo.ItemClass.Equals(ItemTypeScroll, NoCase)
				: (IsImmortalThrone || IsRagnarok || IsAtlantis || IsEmbers) && BaseItemId.IsScroll;
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item is a parchment.
	/// </summary>
	public bool IsParchment
	{
		get
		{
			return (IsImmortalThrone || IsRagnarok || IsAtlantis || IsEmbers) && BaseItemId.IsParchment;
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item is a formulae.
	/// </summary>
	public bool IsFormulae
	{
		get
		{
			return BaseItemInfo is not null
				? BaseItemInfo.ItemClass.Equals(ItemTypeFormula, NoCase)
				: (IsImmortalThrone || IsRagnarok || IsAtlantis || IsEmbers) && BaseItemId.IsFormulae;
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item is an artifact.
	/// </summary>
	public bool IsArtifact
	{
		get
		{
			return BaseItemInfo is not null
				? BaseItemInfo.ItemClass.Equals(ItemTypeArtifact, NoCase)
				: (IsImmortalThrone || IsRagnarok || IsAtlantis || IsEmbers) && BaseItemId.IsArtifact;
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item is a shield.
	/// </summary>
	public bool IsShield => BaseItemInfo is not null && BaseItemInfo.ItemClass.Equals(ItemTypeShield, NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is armor.
	/// </summary>
	public bool IsArmor => BaseItemInfo is not null && BaseItemInfo.ItemClass.StartsWith("ARMORPROTECTIVE", NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is a helm.
	/// </summary>
	public bool IsHelm => BaseItemInfo is not null && BaseItemInfo.ItemClass.Equals(ItemTypeHead, NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is a bracer.
	/// </summary>
	public bool IsBracer => BaseItemInfo is not null && BaseItemInfo.ItemClass.Equals(ItemTypeForeArm, NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is torso armor.
	/// </summary>
	public bool IsTorsoArmor => BaseItemInfo is not null && BaseItemInfo.ItemClass.Equals(ItemTypeUpperBody, NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is a greave.
	/// </summary>
	public bool IsGreave => BaseItemInfo is not null && BaseItemInfo.ItemClass.Equals(ItemTypeLowerBody, NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is Jewellery.
	/// </summary>
	public bool IsJewellery => IsRing || IsAmulet;

	/// <summary>
	/// Gets a value indicating whether the item is a ring.
	/// </summary>
	public bool IsRing => BaseItemInfo is not null && BaseItemInfo.ItemClass.Equals(ItemTypeRing, NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is an amulet.
	/// </summary>
	public bool IsAmulet => BaseItemInfo is not null && BaseItemInfo.ItemClass.Equals(ItemTypeAmulet, NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is a weapon.
	/// </summary>
	public bool IsWeapon => BaseItemInfo is not null && !IsShield && BaseItemInfo.ItemClass.StartsWith("WEAPON", NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is a thrown weapon.
	/// </summary>
	public bool IsThrownWeapon => BaseItemInfo is not null && BaseItemInfo.ItemClass.Equals(ItemTypeRangedOneHand, NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is a two handed weapon.
	/// </summary>
	public bool Is2HWeapon => BaseItemInfo is not null && 
		(BaseItemInfo.ItemClass.Equals(ItemTypeBow, NoCase) || BaseItemInfo.ItemClass.Equals(ItemTypeStaff, NoCase));

	/// <summary>
	/// Gets a value indicating whether the item is a weapon or shield.
	/// </summary>
	public bool IsWeaponShield => BaseItemInfo is not null && BaseItemInfo.ItemClass.StartsWith("WEAPON", NoCase);

	/// <summary>
	/// Gets a value indicating whether the item is a quest item.
	/// </summary>
	public bool IsQuestItem
	{
		get
		{
			return (BaseItemInfo is not null 
				&& (BaseItemInfo.ItemClassification.Equals("QUEST", NoCase) || BaseItemInfo.ItemClass.Equals(ItemTypeQuestItem, NoCase))
				)|| BaseItemId.IsQuestItem;
		}
	}

	/// <summary>
	/// Get a value indicating gear level from <see cref="Rarity.Broken"/> to <see cref="Rarity.Legendary"/>
	/// </summary>
	public Rarity Rarity
	{
		get
		{
			return ItemStyle switch
			{
				ItemStyle.Broken => Rarity.Broken,
				ItemStyle.Mundane => Rarity.Mundane,
				ItemStyle.Common => Rarity.Common,
				ItemStyle.Rare => Rarity.Rare,
				ItemStyle.Epic => Rarity.Epic,
				ItemStyle.Legendary => Rarity.Legendary,
				_ => Rarity.NoGear,
			};
		}
	}

	// TODO Should be an enum anyways -> adjust to get rid of strings
	private const string GearClassificationBroken = "BROKEN";
	private const string GearClassificationRare = "RARE";
	private const string GearClassificationEpic = "EPIC";
	private const string GearClassificationLegendary = "LEGENDARY";

	/// <summary>
	/// Gets the item style enumeration
	/// </summary>
	public ItemStyle ItemStyle
	{
		get
		{
			if (PrefixInfo?.ItemClassification.Equals(GearClassificationBroken, NoCase) ?? false)
				return ItemStyle.Broken;

			if (IsArtifact)
				return ItemStyle.Artifact;

			if (IsFormulae)
				return ItemStyle.Formulae;

			if (IsScroll)
				return ItemStyle.Scroll;

			if (IsParchment)
				return ItemStyle.Parchment;

			if (IsRelicOrCharm)
				return ItemStyle.Relic;

			if (IsPotion)
				return ItemStyle.Potion;

			if (IsQuestItem)
				return ItemStyle.Quest;

			if (BaseItemInfo is not null)
			{
				// TODO Magic strings again fucker
				if (BaseItemInfo.ItemClassification.Equals(GearClassificationEpic, NoCase))
					return ItemStyle.Epic;

				if (BaseItemInfo.ItemClassification.Equals(GearClassificationLegendary, NoCase))
					return ItemStyle.Legendary;

				if (BaseItemInfo.ItemClassification.Equals(GearClassificationRare, NoCase))
					return ItemStyle.Rare;
			}

			// At this point baseItem indicates Common.  Let's check affixes
			if ((SuffixInfo?.ItemClassification.Equals(GearClassificationRare, NoCase) ?? false)
				|| (PrefixInfo?.ItemClassification.Equals(GearClassificationRare, NoCase) ?? false)) 
				return ItemStyle.Rare;

			// Not rare.  If we have a suffix or prefix, then call it common, else mundane
			return SuffixInfo is not null || PrefixInfo is not null 
				? ItemStyle.Common 
				: ItemStyle.Mundane;
		}
	}

	/// <summary>
	/// Gets the item class
	/// </summary>
	public string ItemClass => BaseItemInfo is not null ? BaseItemInfo.ItemClass : string.Empty;

	/// <summary>
	/// Gets a value indicating whether or not the item will stack.
	/// </summary>
	public bool DoesStack => IsPotion || IsScroll;

	/// <summary>
	/// Gets a value indicating whether the item has a number attached.
	/// </summary>
	public bool HasNumber => DoesStack || (IsRelicOrCharm && !IsRelicComplete);

	/// <summary>
	/// Gets or sets the number attached to the item.
	/// </summary>
	public int Number
	{
		get
		{
			if (DoesStack)
				return StackSize;

			if (IsRelicOrCharm)
				return Math.Max(Var1, 1);

			return 0;
		}
		set
		{
			// Added by VillageIdiot <- Dude you dont have to mention that you added crap every time
			if (DoesStack)
				StackSize = value;

			else if (IsRelicOrCharm)
			{
				// Limit value to complete Relic level
				Var1 = (value >= BaseItemInfo.CompletedRelicLevel) ? BaseItemInfo.CompletedRelicLevel : value;
			}
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item has an embedded charm in first slot.
	/// </summary>
	public bool HasCharmSlot1 => !IsRelicOrCharm && !RecordId.IsNullOrEmpty(RelicId) && RelicId.IsCharm;

	/// <summary>
	/// Gets a value indicating whether the item has an embedded charm in second slot.
	/// </summary>
	public bool HasCharmSlot2 => !IsRelicOrCharm && !RecordId.IsNullOrEmpty(Relic2Id) && Relic2Id.IsCharm;

	/// <summary>
	/// Gets a value indicating whether the item has an embedded relic only in first slot.
	/// </summary>
	public bool HasRelicSlot1 => !IsRelicOrCharm && !RecordId.IsNullOrEmpty(RelicId) && RelicId.IsRelic;

	/// <summary>
	/// Gets a value indicating whether the item has an embedded relic only in second slot.
	/// </summary>
	public bool HasRelicSlot2 => !IsRelicOrCharm && !RecordId.IsNullOrEmpty(Relic2Id) && Relic2Id.IsRelic;

	/// <summary>
	/// Gets a value indicating whether the item has an embedded relic or a charm.
	/// </summary>
	public bool HasRelicOrCharmSlot1 => HasCharmSlot1 || HasRelicSlot1;

	/// <summary>
	/// Gets a value indicating whether the item has a second embedded relic or a charm.
	/// </summary>
	public bool HasRelicOrCharmSlot2 => HasCharmSlot2 || HasRelicSlot2;

	/// <summary>
	/// Indicate that the item has an embedded relic or a charm.
	/// </summary>
	public bool HasRelicOrCharm => HasRelicOrCharmSlot1 || HasRelicOrCharmSlot2;

	/// <summary>
	/// Indicate that the item has an embedded relic in slot 1 or 2.
	/// </summary>
	public bool HasRelic => HasRelicSlot1 || HasRelicSlot2;

	/// <summary>
	/// Indicate that the item has an embedded charm in slot 1 or 2.
	/// </summary>
	public bool HasCharm => HasCharmSlot1 || HasCharmSlot2;

	/// <summary>
	/// Gets a value indicating whether the item is a potion.
	/// </summary>
	public bool IsPotion
	{
		get
		{
			if (BaseItemInfo is not null)
				return BaseItemInfo.ItemClass.Equals(ItemTypePotionHealth, NoCase)
					|| BaseItemInfo.ItemClass.Equals(ItemTypePotionEnergy, NoCase)
					|| BaseItemInfo.ItemClass.Equals(ItemTypeScrollEternal, NoCase); //AMS: New EE Potions (Mystical Potions)

			return BaseItemId.IsPotion;
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item is a charm and only a charm.
	/// </summary>
	public bool IsCharmOnly
	{
		get
		{
			return BaseItemInfo is not null 
				? BaseItemInfo.ItemClass.Equals(ItemTypeCharm, NoCase) 
				: BaseItemId.IsCharm;
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item is a relic and only a relic.
	/// </summary>
	public bool IsRelicOnly
	{
		get
		{
			return BaseItemInfo is not null 
				? BaseItemInfo.ItemClass.Equals(ItemTypeRelic, NoCase) 
				: BaseItemId.IsRelic;
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item first relic is a charm and only a charm.
	/// </summary>
	public bool IsRelic1Charm
	{
		get
		{
			return RelicInfo is not null && RelicInfo.ItemClass.Equals(ItemTypeCharm, NoCase);
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item second relic is a charm and only a charm.
	/// </summary>
	public bool IsRelic2Charm
	{
		get
		{
			return Relic2Info is not null && Relic2Info.ItemClass.Equals(ItemTypeCharm, NoCase);
		}
	}

	/// <summary>
	/// Gets a value indicating whether the item is a relic or a charm.
	/// </summary>
	public bool IsRelicOrCharm
	{
		get
		{
			if (BaseItemInfo is not null)
				return BaseItemInfo.ItemClass.Equals(ItemTypeRelic, NoCase)
					|| BaseItemInfo.ItemClass.Equals(ItemTypeCharm, NoCase);

			return BaseItemId.IsRelic || BaseItemId.IsCharm;
		}
	}

	/// <summary>
	/// Indicate if this item is a completed relic.
	/// </summary>
	public bool IsRelicComplete => BaseItemInfo is not null && Var1 >= BaseItemInfo.CompletedRelicLevel;

	/// <summary>
	/// Indicate if the first relic completion bonus apply.
	/// </summary>
	public bool IsRelicBonus1Complete => RelicBonusInfo is not null && Var1 >= RelicBonusInfo.CompletedRelicLevel;

	/// <summary>
	/// Indicate if the second relic completion bonus apply.
	/// </summary>
	public bool IsRelicBonus2Complete => RelicBonus2Info is not null && Var2 >= RelicBonus2Info.CompletedRelicLevel;

	public const string ItemTypeHead = "ArmorProtective_Head";
	public const string ItemTypeForeArm = "ArmorProtective_Forearm";
	public const string ItemTypeUpperBody = "ArmorProtective_UpperBody";
	public const string ItemTypeLowerBody = "ArmorProtective_LowerBody";
	public const string ItemTypeAxe = "WeaponMelee_Axe";
	public const string ItemTypeMace = "WeaponMelee_Mace";
	public const string ItemTypeSword = "WeaponMelee_Sword";
	public const string ItemTypeShield = "WeaponArmor_Shield";
	public const string ItemTypeRangedOneHand = "WeaponHunting_RangedOneHand";
	public const string ItemTypeBow = "WeaponHunting_Bow";
	public const string ItemTypeSpear = "WeaponHunting_Spear";
	public const string ItemTypeStaff = "WeaponMagical_Staff";
	public const string ItemTypeAmulet = "ArmorJewelry_Amulet";
	public const string ItemTypeRing = "ArmorJewelry_Ring";
	public const string ItemTypeArtifact = "ItemArtifact";
	public const string ItemTypeFormula = "ItemArtifactFormula";
	public const string ItemTypeRelic = "ItemRelic";
	public const string ItemTypeCharm = "ItemCharm";
	public const string ItemTypeScroll = "OneShot_Scroll";
	public const string ItemTypePotionEnergy = "OneShot_PotionMana";
	public const string ItemTypePotionHealth = "OneShot_PotionHealth";
	public const string ItemTypeScrollEternal = "OneShot_Scroll_Eternal";
	public const string ItemTypeQuestItem = "QuestItem";
	public const string ItemTypeEquipment = "ItemEquipment";
	public const string ItemTypeDye = "OneShot_Dye";

	internal static ReadOnlyCollection<ItemClassMapItem<string>> ItemClassToTagNameMap = new([
		new (ItemTypeQuestItem, "tagQuestItem"),
		new (ItemTypePotionHealth, "tagHUDHealthPotion"),
		new (ItemTypePotionEnergy, "tagHUDEnergyPotion"),
		new (ItemTypeScrollEternal, "x4tag_PotionReward"), // Translate into: Mystical Potion
		new (ItemTypeAmulet, "tagItemAmulet") ,
		new (ItemTypeRing, "tagItemRing") ,
		new (ItemTypeCharm, "tagItemCharm") ,
		new (ItemTypeRelic, "tagRelic"),
		new (ItemTypeFormula, "xtagEnchant02"),
		new (ItemTypeArtifact, "tagArtifact"),
		new (ItemTypeScroll, "xtagLogScroll"),
		new (ItemTypeLowerBody, "tagCR_Leg"),
		new (ItemTypeForeArm, "tagCR_Arm"),
		new (ItemTypeHead, "tagCR_Head"),
		new (ItemTypeUpperBody, "tagCR_Torso"),
		new (ItemTypeShield, "tagItemWarShield"),
		new (ItemTypeAxe, "tagItemAxe"),
		new (ItemTypeMace, "tagItemMace"),
		new (ItemTypeSword, "tagItemWarBlade"),//tagSword
		new (ItemTypeBow, "tagItemWarBow"),
		new (ItemTypeRangedOneHand, "x2tagThrownWeapon"),
		new (ItemTypeStaff, "tagItemBattleStaff"),// xtagLogStaff
		new (ItemTypeSpear,"tagItemLance"),
		new (ItemTypeEquipment,"xtagArtifactReagentTypeEquipment"),
		new (ItemTypeDye,"tagDye"),
	]); 

	/// <summary>
	/// Get the tagName assigned to the <see cref="ItemClass"/>
	/// </summary>
	public string ItemClassTagName => GetClassTagName(BaseItemInfo.ItemClass);

	/// <summary>
	/// Get the tagName assigned to the <see cref="ItemClass"/>
	/// </summary>
	public static string GetClassTagName(string itemClass) => ItemClassToTagNameMap.Where(i => i.ItemClass.Equals(itemClass, NoCase))
																				   .Select(i => i.Value)
																				   .FirstOrDefault() ?? itemClass;

	/// <summary>
	/// Gets the item group.
	/// Used for grouping during autosort.
	/// </summary>
	public int ItemGroup
	{
		get
		{
			if (BaseItemInfo == null)
				return 0;

			string itemClass = BaseItemInfo.ItemClass;

			return ItemClassToTagNameMap.Select((val, index) => new { val, index })
				.Where(m => m.val.ItemClass.Equals(itemClass, NoCase))
				.Select(m => m.index)
				.FirstOrDefault();
		}
	}

	/// <summary>
	/// Generates a new seed that can be used on a new item
	/// </summary>
	/// <returns>New seed from 0 to 0x7FFF</returns>
	public static int GenerateSeed()
		=> s_random.Next(0x00007fff); // The seed values in the player files seem to be limitted to 0x00007fff or less.

	/// <summary>
	/// Creates an empty item
	/// </summary>
	/// <returns>Empty Item structure</returns>
	public Item MakeEmptyItem() => 
		new()
		{
			BeginBlockCrap1 = BeginBlockCrap1,
			EndBlockCrap1 = EndBlockCrap1,
			BeginBlockCrap2 = BeginBlockCrap2,
			EndBlockCrap2 = EndBlockCrap2,
			BaseItemId = RecordId.Empty,
			PrefixId = RecordId.Empty,
			SuffixId = RecordId.Empty,
			RelicId = RecordId.Empty,
			Relic2Id = RecordId.Empty,
			RelicBonusId = RecordId.Empty,
			RelicBonus2Id = RecordId.Empty,
			Seed = GenerateSeed(),
			Var1 = _var1,
			Var2 = Var2Default,
			Location = new(-1, -1)
		};

	/// <summary>
	/// Makes a new item based on the passed item id string
	/// </summary>
	/// <param name="baseItemId">base item id of the new item</param>
	/// <returns>Empty Item structure based on the passed item string</returns>
	public Item MakeEmptyCopy(RecordId baseItemId)
	{
		ArgumentException.ThrowIfNullOrEmpty(baseItemId, nameof(baseItemId));

		Item newItem = MakeEmptyItem();
		newItem.BaseItemId = baseItemId;
		return newItem;
	}

	/// <summary>
	/// Makes a duplicate of the item
	/// VillageIdiot - Added option to keep item seed.
	/// </summary>
	/// <param name="keepItemSeed">flag on whether we are keeping our item seed or creating a new one.</param>
	/// <returns>New Duplicated item</returns>
	public Item Duplicate(bool keepItemSeed)
	{
		var newItem = (Item)MemberwiseClone();

		if (!keepItemSeed)
			newItem.Seed = GenerateSeed();

		newItem.Location = new(-1, -1);
		newItem.IsModified = true;
		return newItem;
	}

	/// <summary>
	/// Clones the item
	/// </summary>
	/// <returns>A new clone of the item</returns>
	public Item Clone()
	{
		var newItem = (Item)MemberwiseClone();
		newItem.IsModified = true;
		return newItem;
	}

	/// <summary>
	/// Pops all but one item from the stack.
	/// </summary>
	/// <returns>Returns popped items as a new Item stack</returns>
	public Item? PopAllButOneItem()
	{
		if (!DoesStack || StackSize < 2)
			return null;

		// make a complete copy then change a few things
		var newItem = (Item)MemberwiseClone();

		newItem.StackSize = StackSize - 1;
		newItem.Var1 = 0;
		newItem.Seed = GenerateSeed();
		newItem.Location = new(-1, -1);
		newItem.IsModified = true;

		IsModified = true;
		StackSize = 1;

		return newItem;
	}

	/// <summary>
	/// <see cref="GearType"/> of this Item.
	/// </summary>
	/// <remarks>return <see cref="GearType.Undefined"/> if not a piece of gear</remarks>
	public GearType GearType
		=> GearTypeExtension.GearTypeMap
			.Where(m => m.Value.ICLASS.Equals(ItemClass, NoCase))
			.Select(m => m.Key).FirstOrDefault();

	/// <summary>
	/// Tells if <paramref name="relicBaseItemId"/> is allowed.
	/// </summary>
	/// <param name="relicBaseItemId"></param>
	/// <returns></returns>
	public bool IsRelicAllowed(RecordId relicBaseItemId) => IsRelicAllowed(this, relicBaseItemId);

	/// <summary>
	/// Tells if <paramref name="relicItem"/> is allowed.
	/// </summary>
	/// <param name="relicItem"></param>
	/// <returns></returns>
	public bool IsRelicAllowed(Item relicItem) => IsRelicAllowed(this, relicItem);

	/// <summary>
	/// Tells if <paramref name="relicItem"/> is allowed on <paramref name="item"/>.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="relicItem"></param>
	/// <returns></returns>
	public static bool IsRelicAllowed(Item item, Item relicItem)
	{
		if (!relicItem.IsRelicOrCharm) 
			throw new ArgumentException("Must be a relic or a charm!", nameof(relicItem));

		GearType itemGearType = item.GearType;

		// Is it allowed ?
		return itemGearType != GearType.Undefined 
			&& TryGetAllowedGearTypes(relicItem, out GearType relicAllowedGearTypes) 
			&& relicAllowedGearTypes.HasFlag(itemGearType);
	}

	/// <summary>
	/// Tells if <paramref name="relicBaseItemId"/> is allowed on <paramref name="item"/>.
	/// </summary>
	/// <param name="item"></param>
	/// <param name="relicBaseItemId"></param>
	/// <returns></returns>
	public static bool IsRelicAllowed(Item item, RecordId relicBaseItemId)
	{
		GearType itemGearType = item.GearType;

		if (itemGearType == GearType.Undefined) return false;

		// Is it allowed ?
		return itemGearType != GearType.Undefined 
			&& TryGetAllowedGearTypes(relicBaseItemId, out GearType relicAllowedGearTypes) 
			&& relicAllowedGearTypes.HasFlag(itemGearType);
	}

	/// <summary>
	/// Try to get <paramref name="relicItem"/> allowed <see cref="GearType"/>
	/// </summary>
	/// <param name="relicItem"></param>
	/// <param name="types"></param>
	/// <returns></returns>
	public static bool TryGetAllowedGearTypes(Item relicItem, out GearType types)
	{
		types = GearType.Undefined;
		return relicItem.IsRelicOrCharm && TryGetAllowedGearTypes(relicItem.BaseItemId, out types);
	}

	/// <summary>
	/// Try to get allowed <see cref="GearType"/> for that <paramref name="relicBaseItemId"/>
	/// </summary>
	/// <param name="relicBaseItemId"></param>
	/// <param name="types"></param>
	/// <returns></returns>
	public static bool TryGetAllowedGearTypes(RecordId relicBaseItemId, out GearType types)
	{
		types = GearType.Undefined;

		if (RecordId.IsNullOrEmpty(relicBaseItemId)) 
			return false;

		RelicAndCharmMapItem? map = RelicAndCharmMap.FirstOrDefault(m => m.RecordId == relicBaseItemId);

		if (map.Value == RelicAndCharm.Unknown) 
			return false;

		types = map.Types;
		return true;
	}

	public GameDlc GameDlc
	{
		get
		{
			if (IsEmbers) return GameDlc.EternalEmbers;
			else if (IsAtlantis) return GameDlc.Atlantis;
			else if (IsRagnarok) return GameDlc.Ragnarok;
			else if (IsImmortalThrone) return GameDlc.ImmortalThrone;
			else return GameDlc.TitanQuest;
		}
	}

	public string GameDlcCode => GameDlc.GetCode();

	public string GameDlcSuffix
	{
		get
		{
			string code = GameDlc.GetSuffix();

			return code != string.Empty
				? ItemStyle.Quest.TQColor().ColorTag() + code
				: code;
		}
	}

	/// <summary>
	/// Extract color from <paramref name="tqText"/> and fallback to default item color if none.
	/// </summary>
	/// <param name="tqText">text containing the color tag</param>
	/// <returns><see cref="System.Drawing.Color"/> of the embedded color code</returns>
	public Color ExtractTextColorOrItemColor(string tqText)
	{
		if (string.IsNullOrWhiteSpace(tqText))
		{
			// Use the standard color code for the item
			return ItemStyle.Color();
		}

		// Look for a formatting tag in the beginning of the string
		TQColor? colorCode = TQColorHelper.GetColorFromTaggedString(tqText);

		// We didn't find a code so use the standard color code for the item
		if (colorCode is null)
			return ItemStyle.Color();

		// We found something so lets try to find the code
		return colorCode.Value.Color();
	}
}
