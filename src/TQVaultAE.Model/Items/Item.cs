using System.ComponentModel.Design;
using System.Drawing;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents a Titan Quest item.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Gets or sets the position of the <see cref="Item"/> in its container.
        /// </summary>
        public Point Position { get; set; } = new Point(0, 0);

        /// <summary>
        /// Gets the path to the resource within the Titan Quest database.
        /// </summary>
        public string ResourcePath { get; init; } = string.Empty;

        /// <summary>
        /// Gets or sets the name of the <see cref="Item"/>.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the <see cref="Item"/>.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the seed of the <see cref="Item"/>.
        /// </summary>
        public int Seed { get; set; }

        /// <summary>
        /// Gets or sets the prefix of the <see cref="Item"/>.
        /// </summary>
        public Affix? Prefix { get; set; } = null;

        /// <summary>
        /// Gets or sets the suffix of the <see cref="Item"/>.
        /// </summary>
        public Affix? Suffix { get; set; } = null;

        /// <summary>
        /// Gets or sets the first relic of the <see cref="Item"/>.
        /// </summary>
        public RelicItem? RelicOne { get; set; }

        /// <summary>
        /// Gets or sets the second relic of the <see cref="Item"/>. 
        /// </summary>
        public RelicItem? RelicTwo { get; set; }

        // TODO Currently no clue what these are used for.
        // Seems to be the stack size of relics on an item.
        public int Var1 { get; set; }
        public int Var2 { get; set; }

        /// <summary>
        /// Gets or sets the requirements to equip the <see cref="Item"/>.
        /// </summary>
        public List<ItemRequirement> Requirements { get; set; } = [];

        /// <summary>
        /// Gets or sets the bitmap of the <see cref="Item"/> icon.
        /// </summary>
        public Bitmap Icon { get; set; } = null!;

        /// <summary>
        /// Gets the accent color of the <see cref="Item"/>.
        /// </summary>
        public Color AccentColor
        {
            get
            {
                Type type = GetType();
                Color color;

                if (Class == ItemClass.OneShot_Scroll)
                {
                    color = TitanQuestColors.Indigo;
                }
                else if (Class == ItemClass.OneShot_PotionHealth)
                {
                    color = TitanQuestColors.Red;
                }
                else if (Class == ItemClass.OneShot_PotionMana)
                {
                    color = TitanQuestColors.Blue;
                }
                else if (Classification == ItemClassification.Quest)
                {
                    return TitanQuestColors.Purple;
                }
                else if (type == typeof(WeaponItem)) // Or armor item. Not implemented yet
                {
                    // TODO Handle colors defined by amount of affixes, silver, yellow, green. Also handle monster rares (yellow green)
                    color = Classification switch
                    {
                        ItemClassification.Common => TitanQuestColors.Silver,
                        ItemClassification.Rare => TitanQuestColors.Yellow,
                        ItemClassification.Magical => TitanQuestColors.Green,
                        ItemClassification.Epic => TitanQuestColors.LightCyan,
                        ItemClassification.Legendary => TitanQuestColors.Purple,
                        _ => TitanQuestColors.Red,
                    };
                }
                else if (type == typeof(ArtifactItem) || Class == ItemClass.ItemArtifactFormula)
                {
                    color = TitanQuestColors.Turquoise;
                }
                else
                {
                    color = TitanQuestColors.Green;
                }

                return Color.FromArgb(0x20, color);
            }
        }

        /// <summary>
        /// Gets or sets the classification of the <see cref="Item"/>.
        /// </summary>
        public ItemClassification Classification { get; set; }

        /// <summary>
        /// Gets or sets the cost of the <see cref="Item"/>.
        /// </summary>
        public int Cost { get; set; }

        // TODO Figure out, what exactly this represents.
        /// <summary>
        /// Gets or sets the level of the <see cref="Item"/>.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// Gets or sets the MaxTransparency of the <see cref="Item"/>.
        /// </summary>
        public int MaxTransparency { get; set; }

        /// <summary>
        /// Gets or sets the properties of the <see cref="Item"/>.
        /// </summary>
        public List<ItemProperty> Properties { get; set; } = [];

        /// <summary>
        /// Gets or sets the scale of the <see cref="Item"/>.
        /// </summary>
        public float Scale { get; set; }

        // TODO Verify, if ALL items have them and move, if necessary:
        // TODO Also verify, if they are needed otherwise ignore them:
        public string TemplateName { get; set; } = string.Empty;

        /// <summary>
        /// Cell size.
        /// </summary>
        public Size Size { get; set; } = new Size(1, 1);

        public ItemClass Class { get; set; }
    }
}
