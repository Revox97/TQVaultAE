using System.Text.RegularExpressions;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Items.ItemProperties;

namespace TQVaultAE.Application.Factories
{
    public partial class ItemPropertiesFactory
    {
        public static List<ItemProperty> Create(ArzRecord itemRecord)
        {
            List<ArzRecordProperty> validProperties = [.. itemRecord.Properties.Where(x => x.IsValueRelevant && !x.Name.EndsWith("Tag") &&
            (
                   x.Name.StartsWith("offensive")
                || x.Name.StartsWith("defensive")
                || x.Name.StartsWith("retaliation")
                || x.Name.StartsWith("skill")
                || x.Name.StartsWith("character")
            ))];

            Dictionary<string, ItemProperty> itemPropertyMap = [];
            Regex itemPropertyRegex = PropertyNameRegex();

            foreach (ArzRecordProperty property in validProperties)
            {
                try
                {
                    if (property.Name == "characterBaseAttackSpeed")
                        continue;

                    Match result = itemPropertyRegex.Match(property.Name);
                    string propertyName = result.Groups[1].Value;
                    string type = result.Groups[2].Value;
                    string variable = result.Groups[3].Value;

                    ItemProperty? currentProperty = itemPropertyMap.FirstOrDefault(x => x.Key == propertyName).Value;

                    if (currentProperty is null)
                    {
                        currentProperty = propertyName.EndsWith("Global")
                            ? new GlobalItemProperty() { Name = propertyName }
                            : new NormalItemProperty() { Name = propertyName };

                        itemPropertyMap.Add(propertyName, currentProperty);
                    }

                    if (variable == "Chance")
                    {
                        currentProperty.Chance = property.Get<float>(0);
                        continue;
                    }

                    if (currentProperty is not NormalItemProperty normalProperty)
                        continue;

                    if (string.IsNullOrEmpty(variable))
                    {
                        normalProperty.Min = property.Get<float>(0);
                        continue;
                    }

                    switch (variable)
                    {
                        case "Global":
                            normalProperty.IsGlobal = true;
                            break;
                        case "Min":
                            normalProperty.Min = property.Get<float>(0);
                            break;
                        case "Max":
                            normalProperty.Max = property.Get<float>(0);
                            break;
                        case "DurationMin":
                            normalProperty.Duration = property.Get<float>(0);
                            break;
                        case "DurationModifier":
                            normalProperty.DurationModifier = property.Get<float>(0);
                            break;
                        case "XOR":
                            normalProperty.XOR = true;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {

                }
            }

            return AddPropertiesToGlobalProperties([.. itemPropertyMap.Values]);
        }

        private static List<ItemProperty> AddPropertiesToGlobalProperties(List<ItemProperty> properties)
        {
            // TODO Can there be multiple? If so needs special handling.
            GlobalItemProperty? globalProperty = (GlobalItemProperty?)properties.SingleOrDefault(x => x.GetType() == typeof(GlobalItemProperty));

            if (globalProperty is null)
                return properties;

            List<NormalItemProperty> normalProperties = [.. properties.Where(x => x.GetType() == typeof(NormalItemProperty)).Cast<NormalItemProperty>()];
            foreach (NormalItemProperty normalProperty in normalProperties)
            {
                if (normalProperty.IsGlobal)
                {
                    globalProperty.SubProperties.Add(normalProperty);
                    properties.Remove(normalProperty);
                }
            }

            return properties;
        }

        [GeneratedRegex(@"^((character|defensiveSlow|defensive|offensiveSlow|offensive|retaliationSlow|retaliation|skill)[A-Za-z]+?)(Chance|DamageRatio|DurationMax|DurationMin|DurationModifier|DrainMax|DrainMin|Global|Min|Max|XOR)?$")]
        internal static partial Regex PropertyNameRegex();
    }
}
