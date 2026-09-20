using System.Text.RegularExpressions;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items.ItemProperties;

namespace TQVaultAE.Application.Factories
{
    internal partial class ItemPropertiesFactory
    {
        internal List<ItemProperty> CreateProperties(ArzRecord itemRecord)
        {
            List<ArzRecordProperty> validProperties = [.. itemRecord.Properties.Where(x => x.IsValueRelevant && !x.Name.EndsWith("Tag") &&
            (
                   x.Name.StartsWith("offensive")
                || x.Name.StartsWith("defensive")
                || x.Name.StartsWith("retaliation")
                || x.Name.StartsWith("skill")
                || x.Name.StartsWith("character")
            ))];

            // TODO Handle global chances
            List<ItemProperty> itemProperties = [];

            foreach (ArzRecordProperty property in validProperties)
            {
                // No relevant property
                if (property.Name == "characterBaseAttackSpeed")
                    continue;

                if (property.Name == "offensiveGlobalChance")
                {
                    CreateGlobalItemProperty(ref itemProperties, property);
                    continue;
                }

                Regex propertyRegex = PropertyNameRegex();
                Match match = propertyRegex.Match(property.Name);

                string propertyName = match.Groups[1].Value;
                string type = match.Groups[2].Value;
                string variableName = match.Groups[3].Value;

                switch (type)
                {
                    case "character":
                        ProcessCharacterProperty(ref itemProperties, property, propertyName);
                        break;
                    case "defensive":
                        ProcessDefensiveProperty(ref itemProperties, property, propertyName);
                        break;
                    case "defensiveSlow":
                        ProcessDefensiveSlowProperty(ref itemProperties, property, propertyName);
                        break;
                    case "skill":
                        ProcessSkillProperty(ref itemProperties, property, propertyName);
                        break;
                    case "offensive":
                        ProcessOffensiveProperty(ref itemProperties, property, propertyName, variableName, validProperties);
                        break;
                    case "offensiveSlow":
                        ProcessOffensiveSlowProperty(ref itemProperties, property, propertyName, variableName, validProperties);
                        break;
                    case "retaliation":
                        ProcessRetaliationProperty(ref itemProperties, property, propertyName, variableName);
                        break;
                    case "retaliationSlow":
                        ProcessRetaliationSlowProperty(ref itemProperties, property, propertyName, variableName);
                        break;
                    default:
                        string bReak = "";
                        break;
                }
            }

            return itemProperties;
        }

        private static void ProcessRetaliationSlowProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property, string propertyName, string variableName)
        {
            RetaliationSlowItemProperty? retaliationSlowProperty = (RetaliationSlowItemProperty?)itemProperties.SingleOrDefault(x => x.Name == propertyName);

            if (retaliationSlowProperty is null)
            {
                retaliationSlowProperty = new()
                {
                    Name = propertyName
                };

                itemProperties.Add(retaliationSlowProperty);
            }

            switch (variableName)
            {
                case "Chance":
                    retaliationSlowProperty.Chance = property.Get<float>(0);
                    break;
                case "Global":
                    retaliationSlowProperty.IsGlobal = property.Get<bool>(0);
                    break;
                case "DurationMax":
                    retaliationSlowProperty.DurationMax = property.Get<float>(0);
                    break;
                case "DurationMin":
                    retaliationSlowProperty.DurationMin = property.Get<float>(0);
                    break;
                case "Max":
                    retaliationSlowProperty.Max = property.Get<float>(0);
                    break;
                case "Min":
                    retaliationSlowProperty.Min = property.Get<float>(0);
                    break;
                default:
                    string breakS = "";
                    break;
            }
        }

        private static void ProcessDefensiveSlowProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property, string propertyName)
        {
            itemProperties.Add(new DefensiveSlowItemProperty()
            {
                Name = propertyName,
                Value = property.Get<float>(0),
            });
        }

        private static void ProcessOffensiveSlowProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property, string propertyName, string variableName, List<ArzRecordProperty> propertyRecords)
        {
            OffensiveSlowItemProperty? offensiveSlowProperty = (OffensiveSlowItemProperty?)itemProperties
                .SingleOrDefault(x => x.Name == propertyName);

            offensiveSlowProperty ??= (OffensiveSlowItemProperty?)((GlobalItemProperty?)itemProperties
                .SingleOrDefault(x => x.Name == "offensiveGlobalChance"))?.Children
                .SingleOrDefault(x => x.Name == propertyName) ?? null;

            if (offensiveSlowProperty is null)
            {
                offensiveSlowProperty = new()
                {
                    Name = propertyName,
                };

                itemProperties.Add(offensiveSlowProperty);
            }

            switch (variableName)
            {
                case "Chance":
                    offensiveSlowProperty.Chance = property.Get<float>(0);
                    break;
                case "DurationMin":
                    offensiveSlowProperty.DurationMin = property.Get<float>(0);
                    break;
                case "DurationMax":
                    offensiveSlowProperty.DurationMax = property.Get<float>(0);
                    break;
                case "Global":
                    offensiveSlowProperty.IsGlobal = property.Get<bool>(0);

                    if (offensiveSlowProperty.IsGlobal)
                        AddItemPropertyToGlobalItemProperty(ref itemProperties, offensiveSlowProperty, property, propertyName, propertyRecords);

                    break;
                case "Max":
                    offensiveSlowProperty.Max = property.Get<float>(0);
                    break;
                case "Min":
                    offensiveSlowProperty.Min = property.Get<float>(0);
                    break;
                case "Modifier":
                    offensiveSlowProperty.Modifier = property.Get<float>(0);
                    break;
                default:
                    string breakS = "";
                    break;
            }
        }

        private static void CreateGlobalItemProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property)
        {
            if (itemProperties.Any(x => x.Name == property.Name))
                return;

            itemProperties.Add(new GlobalItemProperty()
            {
                Name = property.Name,
                Value = property.Get<float>(0),
            });
        }

        private static void ProcessRetaliationProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property, string propertyName, string variableName)
        {
            RetaliationItemProperty? retaliationProperty = (RetaliationItemProperty?)itemProperties.SingleOrDefault(x => x.Name == propertyName);

            if (retaliationProperty is null)
            {
                retaliationProperty = new()
                {
                    Name = propertyName
                };

                itemProperties.Add(retaliationProperty);
            }

            switch (variableName)
            {
                case "Chance":
                    retaliationProperty.Chance = property.Get<float>(0);
                    break;
                case "DurationMax":
                    retaliationProperty.DurationMax = property.Get<float>(0);
                    break;
                case "DurationMin":
                    retaliationProperty.DurationMin = property.Get<float>(0);
                    break;
                case "Global":
                    retaliationProperty.IsGlobal = property.Get<bool>(0);
                    break;
                case "Max":
                    retaliationProperty.Max = property.Get<float>(0);
                    break;
                case "Min":
                    retaliationProperty.Min = property.Get<float>(0);
                    break;
                default:
                    string breakS = "";
                    break;
            }
        }

        private static void ProcessOffensiveProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property, string propertyName, string variableName, List<ArzRecordProperty> propertyRecords)
        {
            OffensiveItemProperty? offensiveProperty = (OffensiveItemProperty?)itemProperties.SingleOrDefault(x => x.Name == propertyName);

            offensiveProperty ??= (OffensiveItemProperty?)((GlobalItemProperty?)itemProperties
                .SingleOrDefault(x => x.Name == "offensiveGlobalChance"))?.Children
                .SingleOrDefault(x => x.Name == propertyName) ?? null;

            if (offensiveProperty is null)
            {
                offensiveProperty = (OffensiveItemProperty?)((GlobalItemProperty?)itemProperties.SingleOrDefault(x => x.Name == "offensiveGlobalChance"))?
                    .Children.SingleOrDefault(x => x.Name == propertyName) ?? null;

                offensiveProperty = new()
                {
                    Name = propertyName,
                };

                itemProperties.Add(offensiveProperty);
            }

            switch (variableName)
            {
                case "Chance":
                    offensiveProperty.Chance = property.Get<float>(0);
                    break;
                case "DamageRatio":
                    offensiveProperty.DamageRatio = property.Get<float>(0);
                    break;
                case "DurationMin":
                    offensiveProperty.DurationMin = property.Get<float>(0);
                    break;
                case "DurationMax":
                    offensiveProperty.DurationMax = property.Get<float>(0);
                    break;
                case "DrainMax":
                    offensiveProperty.DrainMax = property.Get<float>(0);
                    break;
                case "DrainMin":
                    offensiveProperty.DrainMin = property.Get<float>(0);
                    break;
                case "Global":
                    offensiveProperty.IsGlobal = property.Get<bool>(0);

                    if (offensiveProperty.IsGlobal)
                        AddItemPropertyToGlobalItemProperty(ref itemProperties, offensiveProperty, property, propertyName, propertyRecords);

                    break;
                case "Max":
                    offensiveProperty.Max = property.Get<float>(0);
                    break;
                case "Min":
                    offensiveProperty.Min = property.Get<float>(0);
                    break;
                case "Modifier":
                    offensiveProperty.Modifier = property.Get<float>(0);
                    break;
                default:
                    string breakS = "";
                    break;
            }
        }

        private static void AddItemPropertyToGlobalItemProperty(ref List<ItemProperty> itemProperties, ItemProperty itemProperty, ArzRecordProperty property, string propertyName, List<ArzRecordProperty> propertyRecords)
        {

            if (!itemProperties.Any(x => x.GetType() == typeof(GlobalItemProperty) && x.Name == "offensiveGlobalChance"))
            {
                ArzRecordProperty? globalPropertyRecord = propertyRecords.FirstOrDefault(x => x.Name == "offensiveGlobalChance");

                if (globalPropertyRecord is null)
                    return;

                CreateGlobalItemProperty(ref itemProperties, globalPropertyRecord);
            }

            GlobalItemProperty? globalItemProperty = (GlobalItemProperty?)itemProperties.FirstOrDefault(x => x.Name == "offensiveGlobalChance");

            if (globalItemProperty is null)
                return;

            itemProperties.Remove(itemProperty);
            globalItemProperty.Children.Add(itemProperty);
        }

        private static void ProcessCharacterProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property, string fullPropertyName)
        {
            itemProperties.Add(new CharacterItemProperty()
            {
                Name = fullPropertyName,
                Value = property.Get<float>(0)
            });
        }

        private static void ProcessDefensiveProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property, string fullPropertyName)
        {
            itemProperties.Add(new DefensiveItemProperty()
            {
                Name = fullPropertyName,
                Value = property.Get<float>(0)
            });
        }

        private static void ProcessSkillProperty(ref List<ItemProperty> itemProperties, ArzRecordProperty property, string fullPropertyName)
        {
            itemProperties.Add(new SkillItemProperty()
            {
                Name = fullPropertyName,
                Value = property.Get<float>(0)
            });
        }

        [GeneratedRegex(@"^((character|defensiveSlow|defensive|offensiveSlow|offensive|retaliationSlow|retaliation|skill)[A-Za-z]+?)(Chance|DamageRatio|DurationMin|DurationMax|DrainMax|DrainMin|Global|Min|Max|Modifier|XOR)?$")]
        internal static partial Regex PropertyNameRegex();
    }
}
