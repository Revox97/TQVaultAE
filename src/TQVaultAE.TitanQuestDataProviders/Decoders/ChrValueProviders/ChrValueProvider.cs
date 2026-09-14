namespace TQVaultAE.TitanQuestDataProviders.Decoders.ChrValueProviders
{
    internal class ChrValueProvider : ChrLikeValueProvider
    {
        internal override byte[] ReadPrimitiveValue(BinaryReader reader, string label, out Type type)
        {
            byte[] result;

            // Titan Quest stores certain data type after fixed strings.
            switch (label)
            {
                case "uniqueId":
                case "teleportUID":
                case "markerUID":
                case "respawnUID":
                case "strategicMovementRespawnPoint[i]":
                    result = ReadGuid(reader);
                    type = typeof(Guid);
                    break;

                case "myPlayerName":
                case "(*greatestMonsterKilledName)[i]":
                case "defaultText":
                    result = ReadExtendedString(reader);
                    type = typeof(string);
                    break;

                case "prefixName":
                case "suffixName":
                case "relicName":
                case "relicName2":
                case "relicBonus":
                case "relicBonus2":
                case "baseName":
                case "charName":
                case "playerCharacterClass":
                case "playerClassTag":
                case "streamData":
                case "playerTexture":
                case "skillName":
                case "itemName":
                case "description":
                case "buffSkillName":
                case "scrollName":
                case "bitmapUpName":
                case "bitmapDownName":
                    result = ReadString(reader);
                    type = typeof(string);
                    break;

                case "headerVersion":
                case "playerLevel":
                case "playerVersion":
                case "altMoney":
                case "money":
                case "numTutorialPagesV2":
                case "currentPageV2":
                case "teleportUIDsSize":
                case "markerUIDsSize":
                case "respawnUIDsSize":
                case "versionRespawnPoint":
                case "compassState":
                case "itemsFoundOverLifetimeUniqueTotal":
                case "itemsFoundOverLifetimeRandomizedTotal":
                case "temp":
                case "tartarusDefeatedCount[i]":
                case "max":
                case "skillLevel":
                case "skillSubLevel":
                case "skillTransition":
                case "masteriesAllowed":
                case "skillReclamationPointsUsed":
                case "version":
                case "size":
                case "equipmentSelection":
                case "skillWindowSelection":
                case "primarySkill1":
                case "primarySkill2":
                case "primarySkill3":
                case "primarySkill4":
                case "primarySkill5":
                case "secondarySkill1":
                case "secondarySkill2":
                case "secondarySkill3":
                case "secondarySkill4":
                case "secondarySkill5":
                case "currentStats.charLevel":
                case "currentStats.experiencePoints":
                case "modifierPoints":
                case "skillPoints":
                case "playTimeInSeconds":
                case "numberOfDeaths":
                case "numberOfKills":
                case "experienceFromKills":
                case "healthPotionsUsed":
                case "manaPotionsUsed":
                case "maxLevel":
                case "numHitsReceived":
                case "numHitsInflicted":
                case "greatestDamageInflicted":
                case "(*greatestMonsterKilledLevel)[i]":
                case "(*greatestMonsterKilledLifeAndMana)[i]":
                case "criticalHitsInflicted":
                case "criticalHitsReceived":
                case "numberOfSacks":
                case "currentlyFocusedSackNumber":
                case "currentlySelectedSackNumber":
                case "var1":
                case "var2":
                case "seed":
                case "pointX":
                case "pointY":
                case "equipmentCtrlIOStreamVersion":
                case "storedType":
                    result = ReadInteger(reader);
                    type = typeof(int);
                    break;
                case "isInMainQuest":
                case "disableAutoPopV2":
                case "versionCheckTeleportInfo":
                case "versionCheckMovementInfo":
                case "versionCheckRespawnInfo":
                case "skillWindowShowHelp":
                case "alternateConfig":
                case "alternateConfigEnabled":
                case "hasBeenInGame":
                case "boostedCharacterForX4":
                case "skillEnabled":
                case "skillActive":
                case "hasSkillServices":
                case "skillSettingValid":
                case "skillActive1":
                case "skillActive2":
                case "skillActive3":
                case "skillActive4":
                case "skillActive5":
                case "controllerStreamed":
                case "itemPositionsSavedAsGridCoords":
                case "tempBool":
                case "useAlternate":
                case "itemAttached":
                case "alternate":
                case "isItemSkill":
                    result = ReadInteger(reader);
                    type = typeof(bool);
                    break;
                default:
                    result = ReadInteger(reader);
                    type = typeof(int);
                    break;
            }

            return result;
        }
    }
}
