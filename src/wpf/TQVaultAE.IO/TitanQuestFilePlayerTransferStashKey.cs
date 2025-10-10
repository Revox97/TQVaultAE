using System.ComponentModel;

namespace TQVaultAE.IO
{
    internal enum TitanQuestFilePlayerTransferStashKey
    {
        // [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
		CRC,

        //[TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("stashVersion")]
		StashVersion,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.String1252)]
        [Description("fName")]
		FName,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("sackWidth")]
		SackWidth,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("sackHeight")]
		SackHeight,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("numItems")]
		NumItems,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("begin_block")]
		Begin_block,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("end_block")]
		End_block,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("stackCount")]
		StackCount,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.String1252)]
        [Description("baseName")]
		BaseName,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.String1252)]
        [Description("prefixName")]
		PrefixName,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.String1252)]
        [Description("suffixName")]
		SuffixName,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.String1252)]
        [Description("relicName")]
		RelicName,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.String1252)] // TODO TBD
        [Description("relicBonus")]
		RelicBonus,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("seed")]
		Seed,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("var1")]
		Var1,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("xOffset")]
		XOffset,

		// [TQFileDataType(TQVersion.TQ_All, TQFileDataType.Int)]
        [Description("yOffset")]
		YOffset,

		// [TQFileDataType(TQVersion.TQAE, TQFileDataType.String1252)]
        [Description("relicName2")]
		RelicName2,

		// [TQFileDataType(TQVersion.TQAE, TQFileDataType.String1252)]
        [Description("relicBonus2")]
		RelicBonus2,

		// [TQFileDataType(TQVersion.TQAE, TQFileDataType.Int)]
        [Description("var2")]
		Var2,
    }
}
