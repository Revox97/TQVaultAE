using Avalonia;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Chr;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.Players;

namespace TQVaultAE.Application.Factories
{
    internal sealed class PlayerFactory
    {
        // TODO Provide via app services.
        private readonly TitanQuestDatabaseService _databaseService = new();

        // TODO works, but files always seem to be structured in the same way, so parsing correct data should be possible
        internal Player CreateCharacterFromChrFile(ChrFile input)
        {
            Player result = new()
            {
                Id = input.Root.FindChild("uniqueId")?.AsGuid() ?? Guid.Empty,
                Version = input.Root.FindChild("playerVersion")?.AsInt32() ?? -1,
                Name = input.Root.FindElement("myPlayerName")?.AsString() ?? string.Empty,
                Class = input.Root.FindChild("playerCharacterClass")?.AsString() ?? string.Empty,
                ClassTag = input.Root.FindChild("playerClassTag")?.AsString() ?? string.Empty,
                Level = input.Root.FindChild("playerLevel")?.AsInt32() ?? -1,
                Experience = input.Root.FindElement("currentStats.experiencePoints")?.AsInt32() ?? -1,
                AvailableAttributePoints = input.Root.FindElement("modifierPoints")?.AsInt32() ?? -1,
                AvailableSkillPoints = input.Root.FindElement("skillPoints")?.AsInt32() ?? -1,
                Money = input.Root.FindElement("money")?.AsInt32() ?? -1,
                AltMoney = input.Root.FindElement("altMoney")?.AsInt32() ?? -1, // TODO: Is this the other currency from AE?
                Statistics = ReadStatistics(input.Root),
                Equipment = ReadEquipment(input.Root),
            };

            // Inventory
            int numberOfSacks = input.Root.FindElement("numberOfSacks")?.AsInt32() ?? -1;

            ChrBlock? sack1 = input.Root.FindElement("Block_0-6")!.FindChild("Block_1-0");
            result.Sacks.Add(ReadItemSack(1, sack1!));

            ChrBlock? sack2 = numberOfSacks > 1
                ? input.Root.FindElement("Block_0-6")!.FindChild("Block_1-1")
                : null;

            if (sack2 is not null)
                result.Sacks.Add(ReadItemSack(2, sack2));

            ChrBlock? sack3 = numberOfSacks > 2
                ? input.Root.FindElement("Block_0-6")!.FindChild("Block_1-2")
                : null;

            if (sack3 is not null)
                result.Sacks.Add(ReadItemSack(3, sack3));

            ChrBlock? sack4 = numberOfSacks > 3
                ? input.Root.FindElement("Block_0-6")!.FindChild("Block_1-3")
                : null;

            if (sack4 is not null)
                result.Sacks.Add(ReadItemSack(4, sack4));

            // TODO Read player stash
            return result;
        }

        private static Equipment ReadEquipment(ChrBlock root)
        {
            ChrBlock? equipmentBlock = root.FindChild("Block_0-8")
                ?? throw new KeyNotFoundException("Could not find player equipment block.");

            Equipment equipment = new()
            {
                Head = ReadEquipmentItem<ArmorItem>(equipmentBlock, 2),
                Amulet = ReadEquipmentItem<JewelryItem>(equipmentBlock, 4),
                Body = ReadEquipmentItem<ArmorItem>(equipmentBlock, 6),
                Legs = ReadEquipmentItem<ArmorItem>(equipmentBlock, 8),
                Arms = ReadEquipmentItem<ArmorItem>(equipmentBlock, 10),
                RingOne = ReadEquipmentItem<JewelryItem>(equipmentBlock, 12),
                RingTwo = ReadEquipmentItem<JewelryItem>(equipmentBlock, 14),
                Artifact = ReadEquipmentItem<ArtifactItem>(equipmentBlock, 18),
            };

            ChrBlock weaponSetOne = equipmentBlock.Children[16]!;
            equipment.PrimaryWeaponSetOne = ReadEquipmentItem<WeaponItem>(weaponSetOne, 1);
            equipment.SecundaryWeaponSetOne = ReadEquipmentItem<WeaponItem>(weaponSetOne, 3);

            ChrBlock weaponSetTwo = equipmentBlock.Children[17]!;
            equipment.PrimaryWeaponSetTwo = ReadEquipmentItem<WeaponItem>(weaponSetTwo, 1);
            equipment.SecundaryWeaponSetTwo = ReadEquipmentItem<WeaponItem>(weaponSetTwo, 3);

            return equipment;
        }

        private static T? ReadEquipmentItem<T>(ChrBlock equipmentBlock, int itemIndex) where T : Item
        {
            bool isItemAttached = equipmentBlock!.Children[itemIndex + 1]?.AsBool() ?? false;

            if (isItemAttached)
            {
                Item item = ReadBaseItem(equipmentBlock!.Children[itemIndex]);
                return ReadCompleteItem(item) as T;
            }

            return null;
        }

        private static PlayerStatistics ReadStatistics(ChrBlock root)
        {
            int playTimeInSeconds = root.FindElement("playTimeInSeconds")?.AsInt32() ?? -1;
            TimeSpan playTime = TimeSpan.FromSeconds(playTimeInSeconds);

            return new PlayerStatistics()
            {
                PlayTime = playTime,
                Deaths = root.FindElement("numberOfDeaths")?.AsInt32() ?? -1,
                Kills = root.FindElement("numberOfKills")?.AsInt32() ?? -1,
                ExperienceFromKills = root.FindElement("experienceFromKills")?.AsInt32() ?? -1,
                HealthPotionsUsed = root.FindElement("healthPotionsUsed")?.AsInt32() ?? -1,
                EnergyPotionsUsed = root.FindElement("manaPotionsUsed")?.AsInt32() ?? -1,
                MaxLevel = root.FindElement("maxLevel")?.AsInt32() ?? -1,
                HitsReceived = root.FindElement("numHitsReceived")?.AsInt32() ?? -1,
                HitsInflicted = root.FindElement("numHitsInflicted")?.AsInt32() ?? -1,
                CriticalHitsInflicted = root.FindElement("criticalHitsInflicted")?.AsInt32() ?? -1,
                CriticalHitsReceived = root.FindElement("criticalHitsReceived")?.AsInt32() ?? -1,
                GreatestDamageInflicted = root.FindElement("greatesDatamageInflicted")?.AsInt32() ?? -1,
            };
        }

        private static Sack ReadItemSack(int number, ChrBlock sack)
        {
            int itemCount = sack.FindChild("size")?.AsInt32() ?? -1;
            List<Item> items = [];

            for (int i = 0; i < itemCount; i++)
            {
                int itemPosition = i + 2;
                ChrBlock itemBlock = sack.Children[itemPosition];

                Item item = ReadBaseItem(itemBlock);

                if (item.Position.X == -1 && item.Position.Y == -1)
                {
                    // Additional stacks are stored in sequence for one shot items
                    items[items.Count - 1].StackCount++;
                    continue;
                }

                item = ReadCompleteItem(item);
                items.Add(item);
            }

            return new Sack()
            {
                Number = number,
                Items = items,
            };
        }

        private static Item ReadBaseItem(ChrBlock item)
        {
            int positionX = item.FindChild("pointX")?.AsInt32() ?? -1;
            int positionY = item.FindChild("pointY")?.AsInt32() ?? -1;
            Point position = new(positionX, positionY);

            return new Item()
            {
                Position = position,
                ResourcePath = item.FindElement("baseName")?.AsString() ?? string.Empty,
                Seed = item.FindElement("seed")?.AsInt32() ?? -1,
                Prefix = ReadAffix(item, "prefixName"),
                Suffix = ReadAffix(item, "suffixName"),
                Var1 = item.FindElement("var1")?.AsInt32() ?? -1,
                Var2 = item.FindElement("var2")?.AsInt32() ?? -1
            };
        }

        private static Affix? ReadAffix(ChrBlock item, string key)
        {
            ChrBlock? affixBlock = item.FindElement(key);

            if (affixBlock is null || affixBlock.RawData.Length == 0)
                return null;

            string result = System.Text.Encoding.UTF8.GetString(affixBlock.RawData);

            return new Affix()
            {
                Path = result
            };
        }

        private static Item ReadCompleteItem(Item item) => ItemFactory.GetCompleteItemAsync(item).Result;
    }
}
