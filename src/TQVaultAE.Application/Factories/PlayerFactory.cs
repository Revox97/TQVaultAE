using System.Drawing;
using TQVaultAE.Model.Items;
using TQVaultAE.Model.Players;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.Application.Factories
{
    internal sealed class PlayerFactory
    {
        // TODO works, but files always seem to be structured in the same way, so parsing correct data should be possible
        internal static Player CreateCharacterFromChrFile(ChrFile input)
        {
            Player result = new()
            {
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
                Equipment = ReadEquipment(input.Root)
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

            // TODO Where is the storage area, etc. stored?
            return result;
        }

        // TODO implement
        private static Equipment ReadEquipment(ChrBlock root)
        {
            ChrBlock? equipmentBlock = root.FindChild("Block_0-8");
            // Equipment
            // head
            ReadItem(equipmentBlock!.FindChild("Block_1-0")!);

            return new();
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

            for (int i = 2; i < itemCount; i++)
            {
                ChrBlock itemBlock = sack.Children[i];
                items.Add(ReadItem(itemBlock));
            }

            return new Sack()
            {
                Number = number,
                Items = items,
            };
        }

        private static Item ReadItem(ChrBlock item)
        {
            int positionX = item.FindChild("pointX")?.AsInt32() ?? -1;
            int positionY = item.FindChild("pointX")?.AsInt32() ?? -1;
            Point position = new(positionX, positionY);

            Item result = new()
            {
                Position = position,
                Path = item.FindElement("baseName")?.AsString() ?? string.Empty,
                Seed = item.FindElement("seed")?.AsInt32() ?? -1,
                Var1 = item.FindElement("var1")?.AsInt32() ?? -1,
                Var2 = item.FindElement("var2")?.AsInt32() ?? -1
            };

            string? prefixName = item.FindElement("prefixName")?.AsString() ?? null;
            result.Prefix = prefixName is not null ? new Affix() { Path = prefixName } : null;

            string? suffixName = item.FindElement("suffixName")?.AsString() ?? null;
            result.Suffix = suffixName is not null ? new Affix() { Path = suffixName } : null;

            string relicName = item.FindElement("relicName")?.AsString() ?? string.Empty;
            string relicBonus = item.FindElement("relicBonus")?.AsString() ?? string.Empty;
            result.RelicOne = relicName is not null ? new RelicItem()
            {
                Path = relicName,
                Bonus = relicBonus
            } : null;

            string relicName2 = item.FindElement("relicName2")?.AsString() ?? string.Empty;
            string relicBonus2 = item.FindElement("relicBonus2")?.AsString() ?? string.Empty;
            result.RelicOne = relicName2 is not null ? new RelicItem()
            {
                Path = relicName2,
                Bonus = relicBonus2
            } : null;

            return result;
        }
    }
}
