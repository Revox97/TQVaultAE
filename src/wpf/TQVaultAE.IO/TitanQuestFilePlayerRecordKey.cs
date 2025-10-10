using System.ComponentModel;

namespace TQVaultAE.IO
{
    internal enum TitanQuestFilePlayerRecordKey
    {
        //[TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("headerVersion")]
        HeaderVersion,

        //[TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("playerCharacterClass")]
        PlayerCharacterClass,

        // [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.ByteArray16)]
        [Description("uniqueId")]
        UniqueId,

        // [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.ByteArrayVar)]
        [Description("streamData")]
		StreamData,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("playerClassTag")]
		PlayerClassTag,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("playerLevel")]
		PlayerLevel,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("playerVersion")]
		PlayerVersion,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("begin_block")]
		BeginBlock,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.StringUTF16)]
        [Description("myPlayerName")]
		MyPlayerName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("isInMainQuest")]
		IsInMainQuest,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("disableAutoPopV2")]
		DisableAutoPopV2,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("numTutorialPagesV2")]
		NumTutorialPagesV2,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("currentPageV2")]
		CurrentPageV2,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("versionCheckTeleportInfo")]
		VersionCheckTeleportInfo,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("teleportUIDsSize")]
		TeleportUIDsSize,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.ByteArray16)]
        [Description("teleportUID")]
		TeleportUID,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("versionCheckMovementInfo")]
		VersionCheckMovementInfo,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("markerUIDsSize")]
		MarkerUIDsSize,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.ByteArray16)]
        [Description("markerUID")]
		MarkerUID,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("versionCheckRespawnInfo")]
		VersionCheckRespawnInfo,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("respawnUIDsSize")]
		RespawnUIDsSize,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.ByteArray16)]
        [Description("respawnUID")]
		RespawnUID,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("versionRespawnPoint")]
		VersionRespawnPoint,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.ByteArray16)]
		[Description("strategicMovementRespawnPoint[i]")]
		StrategicMovementRespawnPoint,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("money")]
		Money,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("compassState")]
		CompassState,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillWindowShowHelp")]
		SkillWindowShowHelp,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("alternateConfig")]
		AlternateConfig,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("alternateConfigEnabled")]
		AlternateConfigEnabled,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("playerTexture")]
		PlayerTexture,

		// [TitanQuestFileDataType(TitanQuestVersion.ImmortalThrone | TitanQuestVersion.AnniversaryEdition, TitanQuestFileDataType.Int)]
        [Description("itemsFoundOverLifetimeUniqueTotal")]
		ItemsFoundOverLifetimeUniqueTotal,

		// [TitanQuestFileDataType(TitanQuestVersion.ImmortalThrone | TitanQuestVersion.AnniversaryEdition, TitanQuestFileDataType.Int)]
        [Description("itemsFoundOverLifetimeRandomizedTotal")]
		ItemsFoundOverLifetimeRandomizedTotal,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Float)]
        [Description("temp")]
		Temp,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("hasBeenInGame")]
		HasBeenInGame,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("end_block")]
		EndBlock,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("max")]
		Max,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("skillName")]
		SkillName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillLevel")]
		SkillLevel,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillEnabled")]
		SkillEnabled,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillSubLevel")]
		SkillSubLevel,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillActive")]
		SkillActive,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillTransition")]
		SkillTransition,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("masteriesAllowed")]
		MasteriesAllowed,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillReclamationPointsUsed")]
		SkillReclamationPointsUsed,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("equipmentSelection")]
		EquipmentSelection,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillWindowSelection")]
		SkillWindowSelection,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillSettingValid")]
		SkillSettingValid,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("primarySkill1")]
		PrimarySkill1,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("secondarySkill1")]
		SecondarySkill1,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillActive1")]
		SkillActive1,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("primarySkill2")]
		PrimarySkill2,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("secondarySkill2")]
		SecondarySkill2,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillActive2")]
		SkillActive2,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("primarySkill3")]
		PrimarySkill3,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("secondarySkill3")]
		SecondarySkill3,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillActive3")]
		SkillActive3,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("primarySkill4")]
		PrimarySkill4,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("secondarySkill4")]
		SecondarySkill4,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillActive4")]
		SkillActive4,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("primarySkill5")]
		PrimarySkill5,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("secondarySkill5")]
		SecondarySkill5,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillActive5")]
		SkillActive5,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
		[Description("currentStats.charLevel")]
		CurrentStats_charLevel,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
		[Description("currentStats.experiencePoints")]
		CurrentStats_experiencePoints,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("modifierPoints")]
		ModifierPoints,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("skillPoints")]
		SkillPoints,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("playTimeInSeconds")]
		PlayTimeInSeconds,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("numberOfDeaths")]
		NumberOfDeaths,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("numberOfKills")]
		NumberOfKills,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("experienceFromKills")]
		ExperienceFromKills,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("healthPotionsUsed")]
		HealthPotionsUsed,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("manaPotionsUsed")]
		ManaPotionsUsed,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("maxLevel")]
		MaxLevel,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("numHitsReceived")]
		NumHitsReceived,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("numHitsInflicted")]
		NumHitsInflicted,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("greatestDamageInflicted")]
		GreatestDamageInflicted,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.StringUTF16)]
		[Description("(*greatestMonsterKilledName)[i]")]
		GreatestMonsterKilledName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
		[Description("(*greatestMonsterKilledLevel)[i]")]
		GreatestMonsterKilledLevel,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
		[Description("(*greatestMonsterKilledLifeAndMana)[i]")]
		GreatestMonsterKilledLifeAndMana,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("criticalHitsInflicted")]
		CriticalHitsInflicted,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("criticalHitsReceived")]
		CriticalHitsReceived,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("controllerStreamed")]
		ControllerStreamed,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("itemPositionsSavedAsGridCoords")]
		ItemPositionsSavedAsGridCoords,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("numberOfSacks")]
		NumberOfSacks,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("currentlyFocusedSackNumber")]
		CurrentlyFocusedSackNumber,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("currentlySelectedSackNumber")]
		CurrentlySelectedSackNumber,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("tempBool")]
		TempBool,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("size")]
		Size,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("baseName")]
		BaseName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("prefixName")]
		PrefixName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("suffixName")]
		SuffixName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("relicName")]
		RelicName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("relicBonus")]
		RelicBonus,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("seed")]
		Seed,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("var1")]
		Var1,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.String1252)]
        [Description("relicName2")]
		RelicName2,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.String1252)]
        [Description("relicBonus2")]
		RelicBonus2,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.Int)]
        [Description("var2")]
		Var2,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("pointX")]
		PointX,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("pointY")]
		PointY,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("useAlternate")]
		UseAlternate,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("equipmentCtrlIOStreamVersion")]
		EquipmentCtrlIOStreamVersion,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("itemAttached")]
		ItemAttached,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("alternate")]
		Alternate,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("storedType")]
		StoredType,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.Int)]
        [Description("isItemSkill")]
		IsItemSkill,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("itemName")]
		ItemName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.ByteArrayVar)]
        [Description("description")]
		Description,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuest, TitanQuestFileDataType.Int)]
        [Description("storedDefaultType")]
		StoredDefaultType,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("scrollName")]
		ScrollName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("bitmapUpName")]
		BitmapUpName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.String1252)]
        [Description("bitmapDownName")]
		BitmapDownName,

		// [TitanQuestFileDataType(TitanQuestVersion.All, TitanQuestFileDataType.StringUTF16)]
        [Description("defaultText")]
		DefaultText,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.Int)]
        [Description("altMoney")]
		AltMoney,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.Int)]
        [Description("hasSkillServices")]
		HasSkillServices,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.Int)]
        [Description("version")]
		Version,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.Int)]
        [Description("boostedCharacterForX4")]
		BoostedCharacterForX4,
		
		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.Int)]
		[Description("tartarusDefeatedCount[i]")]
		TartarusDefeatedCount,

		// [TitanQuestFileDataType(TitanQuestVersion.TitanQuestAE, TitanQuestFileDataType.String1252)]
        [Description("buffSkillName")]
		BuffSkillName,
    }
}
