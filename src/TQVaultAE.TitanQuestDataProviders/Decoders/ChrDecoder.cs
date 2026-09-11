using System.Text;
using TQVaultAE.FileFormats.Chr;

namespace TQVaultAE.TitanQuestDataProviders.Decoders
{
    // TODO works, but files always seem to be structured in the same way, so parsing correct BlockNames should be possible
    #region CHRDOCU
    // CHR FILE STRUCTURE
    // headerVersion - Int32
    // Followed by data blocks
    // usually label size (Int32) followed by the label (string)
    // Then a primitive type
    // A label can mark the start of a complex block (begin_block)
    // complex blocks can contain other blocks and/or primitives
    // complex blocks end on end_block label

    // File Structure:
    // headerVersion: Int32
    // playerCharacterClass: String
    // uniqueId: Guid
    // streamData: Stream
    // playerClassTag: String
    // playerLevel: Int32
    // playerVersion: Int32
    // BLOCK0 #PlayerMetadata
    // BLOCK1 #Skills
    // BLOCK2 #??UIState??
    // BLOCK3 #PlayerLevel
    // BLOCK4 #Temp (not sure what it is used for)
    // BLOCK5 #PlayerStatistics
    // controllerStreamed: bool
    // BLOCK6 #PlayerInventory
    // BLOCK7 #??Unknown??
    // BLOCK8 #PlayerEquipment
    // BLOCK9 #Skillbar
    // Description: Unknown
    #endregion CHRDOCU

    internal class ChrDecoder
    {
        private const string Label_BeginBlock = "begin_block";
        private const string Label_EndBlock = "end_block";

        internal static async Task<ChrFile> DecodeAsync(FileStream stream, string path)
        {
            using BinaryReader reader = new(stream, Encoding.UTF8);
            ChrBlock root = ParseCharacterFile(stream, reader);

            return new ChrFile(path, root);
        }
        public static ChrBlock ParseCharacterFile(FileStream stream, BinaryReader reader)
        {
            ChrBlock root = new("Root");

            int blockCount = 0;
            while (stream.Position < stream.Length)
                ParseNextToken(reader, root, ref blockCount);

            return root;
        }

        private static void ParseNextToken(BinaryReader reader, ChrBlock currentBlock, ref int blockCount, int level = 0)
        {
            if (reader.BaseStream.Position >= reader.BaseStream.Length)
                return;

            int labelLength = reader.ReadInt32();

            if (labelLength <= 0 || labelLength > 512) // Fallback in case of corrupted or invalid data
                return;

            byte[] labelBytes = reader.ReadBytes(labelLength);
            string label = Encoding.UTF8.GetString(labelBytes);

            if (label == Label_BeginBlock)
            {
                // Seems to be always the same
                int blockId = reader.ReadInt32();
                //var subBlock = new ChrBlock($"Block_{blockId}");
                var subBlock = new ChrBlock($"Block_{level}-{blockCount++}");

                currentBlock.Children.Add(subBlock);

                int subBlockCount = 0;
                while (true)
                {
                    long currentPos = reader.BaseStream.Position;
                    int nextLength = reader.ReadInt32();
                    byte[] nextLabelBytes = reader.ReadBytes(nextLength);
                    string nextLabel = Encoding.UTF8.GetString(nextLabelBytes);

                    if (nextLabel == Label_EndBlock)
                    {
                        List<byte> trailingBytes = [];

                        while (true)
                        {
                            trailingBytes.Add(reader.ReadByte());

                            if (trailingBytes.Count >= 4
                                && trailingBytes[trailingBytes.Count - 1] == 0x00
                                && trailingBytes[trailingBytes.Count - 2] == 0x00
                                && trailingBytes[trailingBytes.Count - 3] == 0x00
                                && trailingBytes[trailingBytes.Count - 4] != 0x00)
                            {
                                reader.BaseStream.Position = reader.BaseStream.Position - 4;
                                break;
                            }
                        }

                        break;
                    }

                    reader.BaseStream.Position = currentPos;
                    ParseNextToken(reader, subBlock, ref subBlockCount, level + 1);
                }
            }
            else
            {
                byte[] dataValue = ReadPrimitiveValue(reader, label, out Type type);
                currentBlock.Children.Add(new ChrBlock(label, dataValue, type));
            }
        }

        private static byte[] ReadPrimitiveValue(BinaryReader reader, string label, out Type type)
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

        private static byte[] ReadInteger(BinaryReader reader)
        {
            return reader.ReadBytes(4);
        }

        private static byte[] ReadString(BinaryReader reader)
        {
            int stringLength = reader.ReadInt32();
            return reader.ReadBytes(stringLength);
        }

        private static byte[] ReadExtendedString(BinaryReader reader)
        {
            int stringLength = reader.ReadInt32() * 2; // 2 bytes per char
            List<byte> result = [];

            for (int i = 0; i < stringLength / 2; i++)
            {
                result.Add(reader.ReadByte());
                reader.BaseStream.Position += 1;
            }

            return [.. result];
        }

        private static byte[] ReadGuid(BinaryReader reader)
        {
            List<byte> entry = [];

            while (true)
            {
                entry.Add(reader.ReadByte());

                if (entry.Count > 4 && entry[entry.Count - 1] == 0x00 && entry[entry.Count - 2] == 0x00 && entry[entry.Count - 3] == 0x00 && entry[entry.Count - 4] != 0x00)
                {
                    entry = entry[..(entry.Count - 4)];
                    reader.BaseStream.Position = reader.BaseStream.Position - 4;
                    break;
                }
            }

            return [.. entry];
        }
    }
}
