//-----------------------------------------------------------------------
// <copyright file="ArcFile.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.Domain.Entities
{

	/// <summary>
	/// Reads and decodes a Titan Quest ARC file.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the ArcFile class.
	/// </remarks>
	/// <param name="fileName">File Name of the ARC file to be read.</param>
	public class ArcFile(string fileName)
	{
		/// <summary>
		/// Signifies that the file has been read into memory.
		/// </summary>
		public bool FileHasBeenRead;

		/// <summary>
		/// Dictionary of the directory entries.
		/// </summary>
		public Dictionary<RecordId, ArcDirEntry> DirectoryEntries = [];

		/// <summary>
		/// Ordered keys for the directoryEntries dictionary.
		/// </summary>
		public IEnumerable<RecordId> Keys => DirectoryEntries.Keys.OrderBy(v => v);

		/// <summary>
		/// Gets the ARC file name.
		/// </summary>
		public string FileName { get; private set; } = fileName;

		/// <summary>
		/// Gets the number of Directory entries
		/// </summary>
		public int Count => DirectoryEntries.Count;
	}
}