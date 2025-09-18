using TQVaultAE.Domain.Helpers;

namespace TQVaultAE.Domain.Results;

public class CsvRow(string vaultname, int bagid, ToFriendlyNameResult fnr, int rowIndex, char csvDelimiter)
{
	private readonly string _vaultname = vaultname;
	private readonly int _bagId = bagid;
	private readonly ToFriendlyNameResult _friendlyNameResult = fnr;
	private readonly int _rowIndex = rowIndex;
	private readonly char _csvDelimiter = csvDelimiter;

	public static string GetCSVHeader(char csvDelimiter)
	{
		return string.Join(csvDelimiter.ToString(),
			@"Row",
			@"Vault",
			@"BagId",
			@"PosX",
			@"PosY",
			@"ItemClass",
			@"ItemOrigin",
			@"ItemSeed",
			@"RequireLvl",
			@"RequireStr",
			@"RequireDex",
			@"RequireInt",
			@"BaseRarity",
			@"BaseStyle",
			@"BaseQuality",
			@"BaseId",
			@"BaseName",
			@"PrefixId",
			@"PrefixName",
			@"SuffixId",
			@"SuffixName",
			@"RelicId",
			@"RelicName",
			@"RelicBonusId",
			@"RelicVar",
			@"Relic2Id",
			@"Relic2Name",
			@"Relic2BonusId",
			@"Relic2Var"
		);
	}

	public override string ToString()
	{
		return string.Join(_csvDelimiter.ToString(),
			_rowIndex,
			_vaultname,
			_bagId,
			_friendlyNameResult.Item.Location.X,
			_friendlyNameResult.Item.Location.Y,
			_friendlyNameResult.Item.ItemClass,
			_friendlyNameResult.Item.GameDlcCode,
			_friendlyNameResult.Item.Seed,
			_friendlyNameResult.RequirementInfo.Lvl,
			_friendlyNameResult.RequirementInfo.Str,
			_friendlyNameResult.RequirementInfo.Dex,
			_friendlyNameResult.RequirementInfo.Int,
			_friendlyNameResult.BaseItemRarity,
			_friendlyNameResult.BaseItemInfoStyle,
			_friendlyNameResult.BaseItemInfoQuality,
			_friendlyNameResult.Item.BaseItemId?.Raw,
			_friendlyNameResult.BaseItemInfoDescription?.RemoveAllTQTags(),
			_friendlyNameResult.Item.PrefixId?.Raw,
			_friendlyNameResult.PrefixInfoDescription?.RemoveAllTQTags(),
			_friendlyNameResult.Item.SuffixId?.Raw,
			_friendlyNameResult.SuffixInfoDescription?.RemoveAllTQTags(),
			_friendlyNameResult.Item.RelicId?.Raw,
			_friendlyNameResult.RelicInfo1Description?.RemoveAllTQTags(),
			_friendlyNameResult.Item.RelicBonusId?.Raw,
			_friendlyNameResult.Item.Var1,
			_friendlyNameResult.Item.Relic2Id?.Raw,
			_friendlyNameResult.RelicInfo2Description?.RemoveAllTQTags(),
			_friendlyNameResult.Item.RelicBonus2Id?.Raw,
			_friendlyNameResult.Item.Var2
		);
	}
}