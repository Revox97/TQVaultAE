//using EnumsNET;
namespace TQVaultAE.Domain.Entities;

/// <summary>
/// Gear classification
/// </summary>
[Flags]
public enum GearType
{
	Undefined = 0,
	[GearTypeDescription(Item.ItemTypeHead, "head")]
	Head = 1 << 0,
	[GearTypeDescription(Item.ItemTypeUpperBody, "upperBody")]
	Torso = 1 << 1,
	[GearTypeDescription(Item.ItemTypeForeArm, "forearm")]
	Arm = 1 << 2,
	[GearTypeDescription(Item.ItemTypeLowerBody, "lowerBody")]
	Leg = 1 << 3,
	[GearTypeDescription(Item.ItemTypeRing, "ring")]
	Ring = 1 << 4,
	[GearTypeDescription(Item.ItemTypeAmulet, "amulet")]
	Amulet = 1 << 5,
	[GearTypeDescription(Item.ItemTypeArtifact, "")]
	Artifact = 1 << 6,
	[GearTypeDescription(Item.ItemTypeSpear, "spear")]
	Spear = 1 << 7,
	[GearTypeDescription(Item.ItemTypeStaff, "staff")]
	Staff = 1 << 8,
	[GearTypeDescription(Item.ItemTypeRangedOneHand, "bow")]
	Thrown = 1 << 9,
	[GearTypeDescription(Item.ItemTypeBow, "bow")]
	Bow = 1 << 10,
	[GearTypeDescription(Item.ItemTypeSword, "sword")]
	Sword = 1 << 11,
	[GearTypeDescription(Item.ItemTypeMace, "mace")]
	Mace = 1 << 12,
	[GearTypeDescription(Item.ItemTypeAxe, "axe")]
	Axe = 1 << 13,
	[GearTypeDescription(Item.ItemTypeShield, "shield")]
	Shield = 1 << 14,
	//Unique = 1 << 28,
	MonsterInfrequent = 1 << 29,
	ForMage = 1 << 30,
	ForMelee = 1 << 31,
	Jewellery = Ring | Amulet,
	AllArmor = Head | Torso | Arm | Leg,
	AllWeapons = Spear | Staff | Thrown | Bow | Sword | Mace | Axe,
	AllWearable = AllWeapons | AllArmor | Jewellery | Shield,
}

public static class GearTypeExtension
{
	private static readonly Dictionary<GearType, GearTypeDescriptionAttribute> s_gearTypeMap = [];

	public static Dictionary<GearType, GearTypeDescriptionAttribute> GearTypeMap
	{
		get
		{
			if (s_gearTypeMap.Count == 0)
			{
				// TODO get enum value correctly
				//s_gearTypeMap = Enums.GetMembers<GearType>().Select(m => (m.Value, Attrib: m.Attributes.Get<GearTypeDescriptionAttribute>()))
				//	.Where(m => m.Attrib is not null)
				//	.ToDictionary(m => m.Value, m => m.Attrib);
			}

			return s_gearTypeMap;
		}
	}

	/// <summary>
	/// Return ItemClass constant for <paramref name="type"/> like <see cref="Item.ItemTypeAmulet"/> or <see cref="string.Empty"/> if none
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public static string GetItemClass(this GearType type)
	{
		return GearTypeMap.TryGetValue(type, out GearTypeDescriptionAttribute? attribute) 
			? attribute.ICLASS 
			: string.Empty;
	}

	/// <summary>
	/// Return "requirement equation prefix" related to <paramref name="type"/> for use in the requirements equation or <see cref="string.Empty"/> if none
	/// </summary>
	/// <param name="type"></param>
	/// <returns></returns>
	public static string GetRequirementEquationPrefix(this GearType type)
	{
		return GearTypeMap.TryGetValue(type, out GearTypeDescriptionAttribute? attribute)
			? attribute.RequirementEquationPrefix
			: string.Empty;
	}
}

/// <summary>
/// Allowed <see cref="GearType"/> for this element
/// </summary>
[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
public class GearTypeAttribute(GearType type) : Attribute
{
	public GearType Type { get; } = type;
}

/// <summary>
/// Allowed <see cref="GearType"/> for this element
/// </summary>
[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class GearTypeDescriptionAttribute(string ICLASS_CONST, string requirementEquationPrefix) : Attribute
{
	/// <summary>
	/// ICLASS constant for this <see cref="GearType"/> like <see cref="Item.ItemTypeAmulet"/>
	/// </summary>
	public readonly string ICLASS = ICLASS_CONST;
	/// <summary>
	/// Requirement equation prefix for use in the requirements equation see <see cref="IItemProvider.GetRequirementEquationPrefix"/>.
	/// </summary>
	public readonly string RequirementEquationPrefix = requirementEquationPrefix;
}
