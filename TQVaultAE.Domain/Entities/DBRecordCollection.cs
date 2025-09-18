//-----------------------------------------------------------------------
// <copyright file="DBRecordCollection.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections;
using System.Globalization;

namespace TQVaultAE.Domain.Entities
{
	/// <summary>
	/// Class for encapsulating a record in the database.
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the DBRecordCollection class.
	/// </remarks>
	/// <param name="id">string: ID for this record.</param>
	/// <param name="recordType">string: type for this record</param>
	public class DBRecordCollection(RecordId id, string recordType) : IEnumerable<Variable>
	{
		/// <summary>
		/// Dictionary which holds all of the variables.
		/// </summary>
		private readonly Dictionary<string, Variable> _variables = [];

		/// <summary>
		/// Gets the ID for this record.
		/// </summary>
		public RecordId Id { get; private set; } = id;

		/// <summary>
		/// Gets the RecordType
		/// </summary>
		public string RecordType { get; private set; } = recordType;

		/// <summary>
		/// Gets a Variable from the hashtable.
		/// </summary>
		/// <param name="variableName">Name of the variable we are looking up.</param>
		/// <returns>Returns a Variable from the hashtable.</returns>
		public Variable? this[string variableName]
		{
			get => _variables.TryGetValue(variableName.ToUpperInvariant(), out var val) ? val : null;
		}

		/// <summary>
		/// Enumerates all of the variables in this DBrecord
		/// </summary>
		/// <returns>Each Variable in the record.</returns>
		public IEnumerator<Variable> GetEnumerator()
		{
			foreach (KeyValuePair<string, Variable> kvp in _variables)
				yield return kvp.Value;
		}

		/// <summary>
		/// Non Generic enumerable interface.
		/// </summary>
		/// <returns>Generic interface implementation.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Adds a variable to the hashtable.
		/// </summary>
		/// <param name="variable">Variable that we are adding.</param>
		public void Set(Variable variable) => _variables.Add(variable.Name.ToUpperInvariant(), variable);

		/// <summary>
		/// Returns a short descriptive string in the format of:
		/// recordID,recordType,numVariables
		/// </summary>
		/// <returns>string: recordID,recordType,numVariables</returns>
		public string ToShortString() => string.Format(CultureInfo.CurrentCulture, "{0},{1},{2}", Id, RecordType, _variables.Count);

		/// <summary>
		/// Gets the integer value for the variable, or 0 if the variable does not exist.
		/// throws exception of the variable is not integer type
		/// </summary>
		/// <param name="variableName">Name of the variable we are looking up.</param>
		/// <param name="index">Offset of the value in the array since a variable can have multiple values.</param>
		/// <returns>Returns the integer value for the variable, or 0 if the variable does not exist.</returns>
		public int GetInt32(string variableName, int index)
			=> _variables.TryGetValue(variableName.ToUpperInvariant(), out var val) ? val.GetInt32(index) : 0;

		/// <summary>
		/// Gets the float value for the variable, or 0 if the variable does not exist.
		/// throws exception of the variable is not float type
		/// </summary>
		/// <param name="variableName">Name of the variable we are looking up.</param>
		/// <param name="index">Offset of the value in the array since a variable can have multiple values.</param>
		/// <returns>Returns the float value for the variable, or 0 if the variable does not exist.</returns>
		public float GetSingle(string variableName, int index)
			=> _variables.TryGetValue(variableName.ToUpperInvariant(), out var val) ? val.GetSingle(index) : 0.0F;

		/// <summary>
		/// Gets the string value for the variable, or empty string if the variable does not exist.
		/// </summary>
		/// <param name="variableName">Name of the variable we are looking up.</param>
		/// <param name="index">Offset of the value in the array since a variable can have multiple values.</param>
		/// <returns>Returns the string value for the variable, or empty string if the variable does not exist.</returns>
		public string GetString(string variableName, int index)
		{
			return _variables.TryGetValue(variableName.ToUpperInvariant(), out Variable? val) && val.GetString(index) is string result
				? result 
				: string.Empty;
		}

		/// <summary>
		/// Gets all of the string values for a particular variable entry
		/// since some values can have multiple entries.
		/// </summary>
		/// <param name="variableName">Name of the variable.</param>
		/// <returns>Returns a string array of the string values.</returns>
		public string[] GetAllStrings(string variableName)
		{
			if (_variables.TryGetValue(variableName.ToUpperInvariant(), out Variable? variable))
			{
				string[] ansArray = new string[variable.NumberOfValues];

				for (int i = 0; i < variable.NumberOfValues; ++i)
					ansArray[i] = variable.GetString(i);

				return ansArray;
			}

			return [];
		}


		private KeyValuePair<string, Variable>[] _relevantVars = [];

		/// <summary>
		/// Get the relevant vars, meaning there is something to display.
		/// </summary>
		public KeyValuePair<string, Variable>[] RelevantVars
		{
			get
			{
				_relevantVars ??= _variables?.Where(v => v.Value.IsValueRelevant).ToArray() ?? [];
				return _relevantVars;
			}
		}
	}
}