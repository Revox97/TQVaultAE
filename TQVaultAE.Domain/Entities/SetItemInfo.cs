using System.Collections.ObjectModel;

namespace TQVaultAE.Domain.Entities;

public class SetItemInfo(string itemSetName, string setName, Dictionary<string, Info> setMembers, DBRecordCollection setRecords)
{
	/// <summary>
	/// Id of the Set
	/// </summary>
	public readonly string ItemSetName = itemSetName;

	/// <summary>
	/// Records of this set
	/// </summary>
	public readonly DBRecordCollection SetRecords = setRecords;

	/// <summary>
	/// Id of the Set Name
	/// </summary>
	public readonly string SetName = setName;

	/// <summary>
	/// Id list of the set items
	/// </summary>
	public readonly ReadOnlyDictionary<string, Info> SetMembers = new(setMembers);

	/// <summary>
	/// Translations
	/// </summary>
	public readonly Dictionary<string, string> Translations = [];
}
