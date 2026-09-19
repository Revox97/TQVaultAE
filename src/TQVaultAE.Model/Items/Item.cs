using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.Model.Items
{
    /// <summary>
    /// Represents a Titan Quest item.
    /// </summary>
    // TODO Make this abstract. Currently collides with ItemCreationStrategies
    // TODO Clean up
    public class Item : INotifyPropertyChanged
    {
        protected const string ClassSelectorRunItemName = "Run__ItemName";
        protected const string ClassSelectorRunItemDefault = "Run__ItemDefault";
        protected const string ClassSelectorRunItemSeparator = "ItemSeparator";

        // TODO figure out how to handle item ids the best way.
        public Guid Id { get; set; } = Guid.Empty;

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
        [NotMapped]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the <see cref="Item"/>.
        /// </summary>
        [NotMapped]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the seed of the <see cref="Item"/>.
        /// </summary>
        public int Seed { get; set; }

        // TODO Make this a guid? How should the id be defined?
        public string PrefixId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the prefix of the <see cref="Item"/>.
        /// </summary>
        public Affix? Prefix { get; set; } = null;

        // TODO Make this a guid? How should the id be defined?
        public string SuffixId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the suffix of the <see cref="Item"/>.
        /// </summary>
        public Affix? Suffix { get; set; } = null;

        // TODO Make this a guid? How should the id be defined?
        public Guid TalismanOneId { get; set; }

        /// <summary>
        /// Gets or sets the first relic of the <see cref="Item"/>.
        /// </summary>
        public TalismanItem? TalismanOne { get; set; }

        // TODO Make this a guid? How should the id be defined?
        public Guid TalismanTwoId { get; set; }

        /// <summary>
        /// Gets or sets the second relic of the <see cref="Item"/>. 
        /// </summary>
        public TalismanItem? TalismanTwo { get; set; }

        // TODO Currently no clue what these are used for.
        // Seems to be the stack size of relics on an item.
        public int Var1 { get; set; }

        public int Var2 { get; set; }

        [NotMapped]
        public GameDlc GameDlc { get; set; }

        /// <summary>
        /// Gets or sets the requirements to equip the <see cref="Item"/>.
        /// </summary>
        [NotMapped]
        public ObservableCollection<ItemRequirement> Requirements { get; set; } = [];

        /// <summary>
        /// Gets or sets the bitmap of the <see cref="Item"/> icon.
        /// </summary>
        [NotMapped]
        public virtual Bitmap Icon { get; set; } = null!;

        /// <summary>
        /// Gets the accent color of the <see cref="Item"/>.
        /// </summary>
        [NotMapped]
        public virtual Color AccentColor => new(0x10, Color.R, Color.G, Color.B);

        // Let each type handle its color, then make it abstract.
        [NotMapped]
        public virtual Color Color { get; }

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
        [NotMapped]
        public ObservableCollection<ItemProperty> Properties { get; set; } = [];

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

        public int StackCount
        {
            get;
            set
            {
                field = value;
                OnPropertyChanged();
            }
        } = 1;

        public virtual bool ShowStackCount => CanStack;

        public bool CanStack { get; set; } = false;

        [NotMapped]
        public virtual bool ShowIconAccent => true;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        // TODO Make abstract once possible
        public virtual TextBlock GetItemDescription()
        {
            return new TextBlock();
        }

        protected List<string> GetItemDescriptionProperties()
        {
            List<string> properties = [];

            foreach (ItemProperty property in Properties)
            {
                // TODO Implement
            }

            return properties;
        }

        protected List<string> GetItemDescriptionRequirements()
        {
            List<string> requirements = [];

            foreach (ItemRequirement requirement in Requirements)
            {
                // TODO Implement
            }

            return requirements;
        }
    }
}
