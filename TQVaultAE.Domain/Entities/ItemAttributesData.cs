//-----------------------------------------------------------------------
// <copyright file="ItemAttributesData.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.Domain.Entities
{
	/// <summary>
	/// Used to hold the data for the item attributes
	/// </summary>
	/// <remarks>
	/// Initializes a new instance of the ItemAttributesData class.
	/// </remarks>
	/// <param name="effectType">effect type enumeration</param>
	/// <param name="attribute">attribute string</param>
	/// <param name="effect">effect string</param>
	/// <param name="variable">variable string</param>
	/// <param name="subOrder">attribute suborder</param>
	public class ItemAttributesData(ItemAttributesEffectType effectType, string attribute, string effect, string variable, int subOrder)
	{

		/// <summary>
		/// Gets or sets the Effect Type
		/// </summary>
		public ItemAttributesEffectType EffectType { get; set; } = effectType;

		/// <summary>
		/// Gets or sets the effect name
		/// </summary>
		public string Effect { get; set; } = effect;

		/// <summary>
		/// Gets or sets the variable string
		/// </summary>
		public string Variable { get; set; } = variable;

		/// <summary>
		/// Gets or sets the full attribute string
		/// </summary>
		public string FullAttribute { get; set; } = attribute;

		/// <summary>
		/// Gets or sets the suborder
		/// </summary>
		public int SubOrder { get; set; } = subOrder;
	}
}