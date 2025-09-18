//-----------------------------------------------------------------------
// <copyright file="ArzFile.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.Domain.Entities
{

	/// <summary>
	/// Class for decoding Titan Quest ARZ files.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the ArzFile class.
	/// </remarks>
	/// <param name="fileName">name of the ARZ file.</param>
	public class ArzFile(string fileName)
	{
		/// <summary>
		/// Name of the ARZ file.
		/// </summary>
		public readonly string FileName = fileName;

		// TODO Find better name
		/// <summary>
		/// String table
		/// </summary>
		public string[] Strings = [];

		/// <summary>
		/// RecordInfo keyed by their ID
		/// </summary>
		public Dictionary<RecordId, RecordInfo> RecordInfo = [];

		/// <summary>
		/// Ordered keys for the recordInfo Dictionary
		/// </summary>
		public IEnumerable<RecordId> Keys => RecordInfo.Keys.OrderBy(v => v);

		/// <summary>
		/// Gets the number of DBRecords
		/// </summary>
		public int Count => RecordInfo?.Count ?? 0;
	}
}
