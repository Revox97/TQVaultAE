//-----------------------------------------------------------------------
// <copyright file="Result.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using TQVaultAE.Domain.Entities;
using TQVaultAE.Domain.Results;

namespace TQVaultAE.Domain.Search
{
	/// <summary>
	/// Class for an individual result in the results list.
	/// </summary>
	/// <remarks>
	/// Advanced Ctrs
	/// </remarks>
	/// <param name="container"></param>
	/// <param name="containerName"></param>
	/// <param name="sackNumber"></param>
	/// <param name="sackType"></param>
	/// <param name="fnames"></param>
	public class Result(string container, string containerName, int sackNumber, SackType sackType, Lazy<ToFriendlyNameResult> fnames)
	{
		private readonly Lazy<ToFriendlyNameResult> _friendlyNamesLazyLoader = fnames ?? throw new ArgumentNullException(nameof(fnames));

		public readonly string Container = container ?? throw new ArgumentNullException(nameof(container));
		public readonly string ContainerName = containerName ?? throw new ArgumentNullException(nameof(containerName));
		public readonly int SackNumber = sackNumber;
		public readonly SackType SackType = sackType;

		public ToFriendlyNameResult FriendlyNames { get; private set; }

		public string ItemName { get; private set; }

		public ItemStyle ItemStyle { get; private set; }

		public TQColor TQColor { get; private set; }

		public int RequiredLevel { get; private set; }

		public string IdString => string.Join("|", [
			Container
			, ContainerName
			, SackNumber.ToString()
			, SackType.ToString()
			, FriendlyNames.FullNameBagTooltip
		]);

		public void LazyLoad()
		{
			FriendlyNames = _friendlyNamesLazyLoader.Value;
			ItemName = FriendlyNames.FullNameClean;
			ItemStyle = FriendlyNames.Item.ItemStyle;
			TQColor = FriendlyNames.Item.ItemStyle.TQColor();
			RequiredLevel = GetRequirement(FriendlyNames.RequirementVariables.Values, "levelRequirement");
		}

		private static int GetRequirement(IList<Variable> variables, string key)
		{
			return variables
				.Where(v => string.Equals(v.Name, key, StringComparison.InvariantCultureIgnoreCase) && v.DataType == VariableDataType.Integer && v.NumberOfValues > 0)
				.Select(v => v.GetInt32(0))
				.DefaultIfEmpty(0)
				.Max();
		}
	}
}