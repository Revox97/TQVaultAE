namespace TQVaultAE.Models.CharacterData
{
    /// <summary>
    /// Represents a Titan Quest ingame character.
    /// </summary>
	public class Character
	{
        /// <summary>
        /// Gets or sets the id of the <see cref="Character"/>.
        /// </summary>
		public int Id { get; set; }
		
        /// <summary>
        /// Gets or sets the name of the <see cref="Character"/>.
        /// </summary>
		public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the <see cref="Inventory"/> of the <see cref="Character"/>.
        /// </summary>
        public Inventory Inventory { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Statistics"/> of the <see cref="Character"/>.
        /// </summary>
		public Statistics Statistics { get; set; }
	}
}
