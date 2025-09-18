//-----------------------------------------------------------------------
// <copyright file="Stash.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.Domain.Entities
{
	/// <summary>
	/// Class for handling the stash file
	/// </summary>
	public class Stash
	{
		// TODO Oh no here we go again
		/// <summary>
		/// Raised exception at loading time.
		/// </summary>
		public ArgumentException ArgumentException;

		/// <summary>
		/// return result of <see cref="StashProvider.LoadFile"/> from loading time.
		/// </summary>
		public bool? StashFound { get; set; }

		/// <summary>
		/// Player name associated with this stash file.
		/// </summary>
		private string _playerName;

		/// <summary>
		/// Raw file data
		/// </summary>
		public byte[] RawData { get; set; }

		/// <summary>
		/// Binary marker for begin block
		/// </summary>
		public int BeginBlockCrap { get; set; }

		/// <summary>
		/// The number of sacks in this stash file
		/// </summary>
		public int numberOfSacks { get; set; }

		/// <summary>
		/// Stash file version
		/// </summary>
		public int StashVersion { get; set; }

		/// <summary>
		/// Raw data holding the name.
		/// Changed to raw data to support extended characters
		/// </summary>
		public byte[] Name { get; set; }

		/// <summary>
		/// Gets the current sack instance
		/// </summary>
		public SackCollection Sack { get; set; }

		/// <summary>
		/// Initializes a new instance of the Stash class.
		/// </summary>
		/// <param name="playerName">Name of the player</param>
		/// <param name="stashFile">name of the stash file</param>
		public Stash(string playerName, string stashFile)
		{
			StashFile = stashFile;
			PlayerName = playerName;
			IsImmortalThrone = true;
			numberOfSacks = 2;
		}

		/// <summary>
		/// Creates an empty sack
		/// </summary>
		public void CreateEmptySack()
		{
			Sack = new SackCollection
			{
				IsModified = false
			};
		}

		/// <summary>
		/// Gets or sets a value indicating whether this is from Immortal Throne
		/// </summary>
		/// <remarks>
		/// This really should always be true since stashes are not supported without Immortal Throne.
		/// </remarks>
		public bool IsImmortalThrone { get; set; }

		/// <summary>
		/// Gets a value indicating whether this file has been modified
		/// </summary>
		public bool IsModified => Sack?.IsModified ?? false;

		/// <summary>
		/// Adjust internal status when the collection is saved
		/// </summary>
		public void Saved()
		{
			if (Sack is not null)
				Sack.IsModified = false;
		}

		// TODO Make the next two one object
		/// <summary>
		/// Gets the height of the stash sack
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// Gets the width of the stash sack
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// Gets the player name associated with this stash
		/// </summary>
		public string PlayerName
		{
			get => IsImmortalThrone ? string.Concat(_playerName, " - Immortal Throne") : _playerName;
			private set => _playerName = value;
		}

		/// <summary>
		/// Gets the stash file name
		/// </summary>
		public string StashFile { get; private set; }

		/// <summary>
		/// Gets the number of sack contained in this stash
		/// </summary>
		public int NumberOfSacks => Sack == null ? 0 : NumberOfSacks;
	}
}