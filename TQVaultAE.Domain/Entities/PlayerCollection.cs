//-----------------------------------------------------------------------
// <copyright file="PlayerCollection.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;

namespace TQVaultAE.Domain.Entities
{
	/// <summary>
	/// Loads, decodes, encodes and saves a Titan Quest player file.
	/// </summary>
	public class PlayerCollection : IEnumerable<SackCollection>
	{
		/// <summary>
		/// Tell if the Vault succesfully load
		/// </summary>
		public bool VaultLoaded { get; set; }

		/// <summary>
		/// Raised exception at loading time.
		/// </summary>
		public ArgumentException ArgumentException; // TODO <-- WTF is this can this be removed? Why is the exceptin so odly specific :D

		/// <summary>
		/// String holding the player name
		/// </summary>
		private string _playerName;

		/// <summary>
		/// Byte array holding the raw data from the file.
		/// </summary>
		public byte[] RawData { get; set; }

		/// <summary>
		/// Number of sacks that this file holds
		/// </summary>
		private int _numberOfSacks;

		/// <summary>
		/// Holds the currently focused sack
		/// </summary>
		/// <remarks>used to preseve right vault selected tab (Type = Vault only)</remarks>
		public int CurrentlyFocusedSackNumber { get; set; }

		/// <summary>
		/// Holds the currently selected sack
		/// </summary>
		/// <remarks>used to preseve left vault selected tab (Type = Vault only)</remarks>
		public int CurrentlySelectedSackNumber {get; set; }

		/// <summary>
		/// Holds the equipmentCtrlIOStreamVersion tag in the file.
		/// </summary>
		public int EquipmentCtrlIOStreamVersion {get; set; }

		/// <summary>
		/// Array of the sacks
		/// </summary>
		public SackCollection[] Sacks { get; set; } = [];

		/// <summary>
		/// Holds the currently disabled tooltip bagId.
		/// </summary>
		public List<int> DisabledTooltipBagId { get; set; } = [];

		/// <summary>
		/// Initializes a new instance of the PlayerCollection class.
		/// </summary>
		/// <param name="playerName">Name of the player</param>
		/// <param name="playerFile">filename of the player file</param>
		public PlayerCollection(string playerName, string playerFile)
		{
			PlayerFile = playerFile;
			PlayerName = playerName;
		}

		public bool IsPlayer => PlayerFile.EndsWith("player.chr", StringComparison.InvariantCultureIgnoreCase);

		/// <summary>
		/// Gets or sets a value indicating whether this file is a vault
		/// </summary>
		public bool IsVault { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this file is an immortal throne file
		/// </summary>
		public bool IsImmortalThrone { get; set; }

		/// <summary>
		/// Gets the equipment sack for this file.
		/// </summary>
		public SackCollection EquipmentSack { get; set; }

		/// <summary>
		/// Holds playerInfo
		/// </summary>
		public PlayerInfo PlayerInfo { get; set; }

		/// <summary>
		/// Gets the player file name
		/// </summary>
		public string PlayerFile { get; set; }

		/// <summary>
		/// Gets a value indicating whether this file has been modified.
		/// </summary>
		public bool IsModified
		{
			get
			{
				// look through each sack and see if the sack has been modified
				if (Sacks is not null)
				{
					foreach (SackCollection sack in Sacks)
					{
						if (sack.IsModified)
							return true;
					}
				}

				return (EquipmentSack is not null && EquipmentSack.IsModified)
					|| (PlayerInfo is not null && PlayerInfo.Modified);
			}
		}

		/// <summary>
		/// Adjust internal status when the collection is saved
		/// </summary>
		public void Saved()
		{
			if (Sacks is not null)
			{
				foreach (SackCollection sack in Sacks)
					sack.IsModified = false;
			}

			if (EquipmentSack is not null && EquipmentSack.IsModified)
				EquipmentSack.IsModified = false;

			if (PlayerInfo is not null && PlayerInfo.Modified)
				PlayerInfo.Modified = false;
		}

		/// <summary>
		/// Gets the player name
		/// </summary>
		public string PlayerName
		{
			get => (!IsVault && IsImmortalThrone) ? string.Concat(_playerName, " - Immortal Throne") : _playerName;
			private set => _playerName = value;
		}

		/// <summary>
		/// Gets the number of sacks in this file
		/// </summary>
		public int NumberOfSacks
		{
			get => Sacks == null ? 0 : Sacks.Length;
		}

		/// <summary>
		/// Enumerator block to iterate all of the sacks in the Player
		/// </summary>
		/// <returns>Each Sack in the sack array.</returns>
		public IEnumerator<SackCollection> GetEnumerator()
		{
			if (Sacks == null)
				yield break;

			foreach (SackCollection sack in Sacks)
				yield return sack;
		}

		/// <summary>
		/// Non Generic enumerator interface.
		/// </summary>
		/// <returns>Generic interface implementation.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Creates empty sacks within the file.
		/// </summary>
		/// <param name="numberOfSacks">Number of sacks to create</param>
		public void CreateEmptySacks(int numberOfSacks)
		{
			Sacks = new SackCollection[numberOfSacks];
			_numberOfSacks = numberOfSacks;

			for (int i = 0; i < numberOfSacks; ++i)
			{
				Sacks[i] = new SackCollection
				{
					IsModified = false
				};
			}
		}


		/// <summary>
		/// Gets a sack from the instance
		/// </summary>
		/// <param name="sackNumber">Number of the sack we are retrieving</param>
		/// <returns>Sack instace for the corresponding sack number</returns>
		public SackCollection GetSack(int sackNumber) => (Sacks is null || Sacks.Length <= sackNumber) ? null : Sacks[sackNumber];

		/// <summary>
		/// Moves a sack within the instance.  Used for renumbering the sacks.
		/// </summary>
		/// <param name="source">source sack number</param>
		/// <param name="destination">destination sack number</param>
		/// <returns>true if successful</returns>
		public bool MoveSack(int source, int destination)
		{
			// Do a little bit of error handling
			if (Sacks is null
				|| destination < 0 || destination > Sacks.Length
				|| source < 0 || source > Sacks.Length || source == destination)
			{
				return false;
			}

			// Copy the whole array first.
			List<SackCollection> tmp = [ .. Sacks ];

			// Now we can shuffle things around
			tmp.RemoveAt(source);
			tmp.Insert(destination, Sacks[source]);
			Sacks[source].IsModified = true;
			Sacks[destination].IsModified = true;

			tmp.CopyTo(Sacks);
			return true;
		}

		/// <summary>
		/// Copies a sack within the instance
		/// </summary>
		/// <param name="source">source sack number</param>
		/// <param name="destination">desintation sack number</param>
		/// <returns>true if successful</returns>
		public bool CopySack(int source, int destination)
		{
			// Do a little bit of error handling
			if (Sacks == null
				|| destination < 0 || destination > Sacks.Length
				|| source < 0 || source > Sacks.Length || source == destination)
			{
				return false;
			}

			SackCollection newSack = Sacks[source].Duplicate();

			if (newSack is not null)
			{
				Sacks[destination] = newSack;
				return true;
			}

			return false;
		}

		/// <summary>
		/// Sets all of the bonuses from the equipped gear to 0.
		/// </summary>
		public void ClearPlayerGearBonuses()
		{
			if (PlayerInfo.GearBonus is null)
				return;

			PlayerInfo.GearBonus.StrengthBonus = 0;
			PlayerInfo.GearBonus.StrengthModifier = 0;

			PlayerInfo.GearBonus.DexterityBonus = 0;
			PlayerInfo.GearBonus.DexterityModifier = 0;

			PlayerInfo.GearBonus.IntelligenceBonus = 0;
			PlayerInfo.GearBonus.IntelligenceModifier = 0;

			PlayerInfo.GearBonus.HealthBonus = 0;
			PlayerInfo.GearBonus.HealthModifier = 0;

			PlayerInfo.GearBonus.EnergyBonus = 0;
			PlayerInfo.GearBonus.ManaModifier = 0;
		}

		/// <summary>
		/// Sets all of the bonuses from the player sklills to 0.
		/// </summary>
		public void ClearPlayerSkillBonuses()
		{
			if (PlayerInfo.SkillBonus is null)
				return;

			PlayerInfo.SkillBonus.StrengthBonus = 0;
			PlayerInfo.SkillBonus.StrengthModifier = 0;

			PlayerInfo.SkillBonus.DexterityBonus = 0;
			PlayerInfo.SkillBonus.DexterityModifier = 0;

			PlayerInfo.SkillBonus.IntelligenceBonus = 0;
			PlayerInfo.SkillBonus.IntelligenceModifier = 0;

			PlayerInfo.SkillBonus.HealthBonus = 0;
			PlayerInfo.SkillBonus.HealthModifier = 0;

			PlayerInfo.SkillBonus.EnergyBonus = 0;
			PlayerInfo.SkillBonus.ManaModifier = 0;
		}

		/// <summary>
		/// Updates GearBonus values based on the passed SortedList of bonus values.
		/// </summary>
		/// <param name="statBonusVariables">SortedList containing a list of the bonuses and values to be added.</param>
		public void UpdatePlayerGearBonuses(SortedList<string, int> statBonusVariables)
		{
			if (IsVault || PlayerInfo is null) 
				return;

			if (PlayerInfo.GearBonus is null)
				PlayerInfo.GearBonus = new PlayerStatBonus();

			// TODO use constants
			if (statBonusVariables.ContainsKey("CHARACTERSTRENGTH"))
				PlayerInfo.GearBonus.StrengthBonus += statBonusVariables["CHARACTERSTRENGTH"];
			if (statBonusVariables.ContainsKey("CHARACTERSTRENGTHMODIFIER"))
				PlayerInfo.GearBonus.StrengthModifier += statBonusVariables["CHARACTERSTRENGTHMODIFIER"];

			if (statBonusVariables.ContainsKey("CHARACTERDEXTERITY"))
				PlayerInfo.GearBonus.DexterityBonus += statBonusVariables["CHARACTERDEXTERITY"];
			if (statBonusVariables.ContainsKey("CHARACTERDEXTERITYMODIFIER"))
				PlayerInfo.GearBonus.DexterityModifier += statBonusVariables["CHARACTERDEXTERITYMODIFIER"];

			if (statBonusVariables.ContainsKey("CHARACTERINTELLIGENCE"))
				PlayerInfo.GearBonus.IntelligenceBonus += statBonusVariables["CHARACTERINTELLIGENCE"];
			if (statBonusVariables.ContainsKey("CHARACTERINTELLIGENCEMODIFIER"))
				PlayerInfo.GearBonus.IntelligenceModifier += statBonusVariables["CHARACTERINTELLIGENCEMODIFIER"];

			if (statBonusVariables.ContainsKey("CHARACTERLIFE"))
				PlayerInfo.GearBonus.HealthBonus += statBonusVariables["CHARACTERLIFE"];
			if (statBonusVariables.ContainsKey("CHARACTERLIFEMODIFIER"))
				PlayerInfo.GearBonus.HealthModifier += statBonusVariables["CHARACTERLIFEMODIFIER"];

			if (statBonusVariables.ContainsKey("CHARACTERMANA"))
				PlayerInfo.GearBonus.EnergyBonus += statBonusVariables["CHARACTERMANA"];
			if (statBonusVariables.ContainsKey("CHARACTERMANAMODIFIER"))
				PlayerInfo.GearBonus.ManaModifier += statBonusVariables["CHARACTERMANAMODIFIER"];
		}

		/// <summary>
		/// Updates SkillBonus values based on the passed SortedList of bonus values.
		/// </summary>
		/// <param name="statBonusVariables">SortedList containing a list of the bonuses and values to be added.</param>
		public void UpdatePlayerSkillBonuses(SortedList<string, int> skillStatBonusVariables)
		{
			if (IsVault || PlayerInfo is null) 
				return;

			if (PlayerInfo.SkillBonus is null)
				PlayerInfo.SkillBonus = new PlayerStatBonus();

			// TODO Use constants
			if (skillStatBonusVariables.ContainsKey("CHARACTERSTRENGTH"))
				PlayerInfo.SkillBonus.StrengthBonus += skillStatBonusVariables["CHARACTERSTRENGTH"];
			if (skillStatBonusVariables.ContainsKey("CHARACTERSTRENGTHMODIFIER"))
				PlayerInfo.SkillBonus.StrengthModifier += skillStatBonusVariables["CHARACTERSTRENGTHMODIFIER"];

			if (skillStatBonusVariables.ContainsKey("CHARACTERDEXTERITY"))
				PlayerInfo.SkillBonus.DexterityBonus += skillStatBonusVariables["CHARACTERDEXTERITY"];
			if (skillStatBonusVariables.ContainsKey("CHARACTERDEXTERITYMODIFIER"))
				PlayerInfo.SkillBonus.DexterityModifier += skillStatBonusVariables["CHARACTERDEXTERITYMODIFIER"];

			if (skillStatBonusVariables.ContainsKey("CHARACTERINTELLIGENCE"))
				PlayerInfo.SkillBonus.IntelligenceBonus += skillStatBonusVariables["CHARACTERINTELLIGENCE"];
			if (skillStatBonusVariables.ContainsKey("CHARACTERINTELLIGENCEMODIFIER"))
				PlayerInfo.SkillBonus.IntelligenceModifier += skillStatBonusVariables["CHARACTERINTELLIGENCEMODIFIER"];

			if (skillStatBonusVariables.ContainsKey("CHARACTERLIFE"))
				PlayerInfo.SkillBonus.HealthBonus += skillStatBonusVariables["CHARACTERLIFE"];
			if (skillStatBonusVariables.ContainsKey("CHARACTERLIFEMODIFIER"))
				PlayerInfo.SkillBonus.HealthModifier += skillStatBonusVariables["CHARACTERLIFEMODIFIER"];

			if (skillStatBonusVariables.ContainsKey("CHARACTERMANA"))
				PlayerInfo.SkillBonus.EnergyBonus += skillStatBonusVariables["CHARACTERMANA"];
			if (skillStatBonusVariables.ContainsKey("CHARACTERMANAMODIFIER"))
				PlayerInfo.SkillBonus.ManaModifier += skillStatBonusVariables["CHARACTERMANAMODIFIER"];
		}


		public bool IsPlayerMeetRequierements(SortedList<string, Variable> requirementVariables)
		{
			if (IsVault || PlayerInfo is null) 
				return true;

			int LevelRequirement = 0;
			if (requirementVariables.ContainsKey(Variable.KeyLevelRequirement))
				LevelRequirement = requirementVariables[Variable.KeyLevelRequirement].GetInt32();

			int Strength = 0;
			if (requirementVariables.ContainsKey(Variable.KeyStrength))
				Strength = requirementVariables[Variable.KeyStrength].GetInt32();

			int Dexterity = 0;
			if (requirementVariables.ContainsKey(Variable.KeyDexterity))
				Dexterity = requirementVariables[Variable.KeyDexterity].GetInt32();

			int Intelligence = 0;
			if (requirementVariables.ContainsKey(Variable.KeyIntelligence))
				Intelligence = requirementVariables[Variable.KeyIntelligence].GetInt32();

			return LevelRequirement <= PlayerInfo.CurrentLevel
				&& Strength <= PlayerInfo.CalculatedStrength
				&& Dexterity <= PlayerInfo.CalculatedDexterity
				&& Intelligence <= PlayerInfo.CalculatedIntelligence;
		}
	}
}