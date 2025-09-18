//-----------------------------------------------------------------------
// <copyright file="Info.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace TQVaultAE.Domain.Entities
{

	/// <summary>
	/// Holds information on magical prefixes and suffixes
	/// </summary>
	public class Info
	{
		private const StringComparison NoCase = StringComparison.OrdinalIgnoreCase;

		// tags
		// animalrelics - description relicBitmap shardBitmap Class itemClassification completedRelicLevel
		// equipment* - itemQualityTag itemStyleTag itemNameTag bitmap Class itemClassification
		// lootmagicalaffixes - lootRandomizerName itemClassification
		// miscellaneous\oneshot - description bitmap Class itemClassification
		// questitems - description bitmap Class itemClassification
		// relics - description relicBitmap shardBitmap Class itemClassification completedRelicLevel
		// dyes - description bitmap Class
		// sets - tagSetName

		/// <summary>
		/// database record
		/// </summary>
		private readonly DBRecordCollection _record;

		/// <summary>
		/// description variable
		/// </summary>
		private string _descriptionVar = string.Empty;

		/// <summary>
		/// item classification variable
		/// </summary>
		private string _itemClassificationVar = string.Empty;

		/// <summary>
		/// bitmap variable
		/// </summary>
		private string _bitmapVar = string.Empty;

		/// <summary>
		/// shard bitmap variable
		/// </summary>
		private string _shardBitmapVar = string.Empty;

		/// <summary>
		/// item class variable
		/// </summary>
		private string _itemClassVar = string.Empty;

		/// <summary>
		/// completed relic level variable
		/// </summary>
		private string _completedRelicLevelVar = string.Empty;

		/// <summary>
		/// item quality variable
		/// </summary>
		private string _qualityVar = string.Empty;

		/// <summary>
		/// item style variable
		/// </summary>
		private string _styleVar = string.Empty;

		/// <summary>
		/// itemscalepercent attribute
		/// </summary>
		private string _itemScalePercent = string.Empty;

		/// <summary>
		/// Initializes a new instance of the Info class.
		/// </summary>
		/// <param name="record">database record for which this info is for.</param>

		public Info(DBRecordCollection record)
		{
			_record = record;
			AssignVariableNames();
		}

		/// <summary>
		/// Gets the item ID
		/// </summary>
		public RecordId ItemId => _record.Id;

		/// <summary>
		/// Gets the item description tag
		/// </summary>
		public string DescriptionTag => GetString(_descriptionVar);

		/// <summary>
		/// Gets the item classification
		/// </summary>
		public string ItemClassification => GetString(_itemClassificationVar);

		/// <summary>
		/// Gets the item quality tag
		/// </summary>
		public string QualityTag => GetString(_qualityVar);

		/// <summary>
		/// Gets the item style tag
		/// </summary>
		public string StyleTag => GetString(_styleVar);

		/// <summary>
		/// Gets the item bitmap
		/// </summary>
		public RecordId Bitmap => GetString(_bitmapVar);

		/// <summary>
		/// Gets the item shard bitmap
		/// </summary>
		public RecordId ShardBitmap => GetString(_shardBitmapVar);

		/// <summary>
		/// Gets the item class
		/// </summary>
		public string ItemClass => GetString(_itemClassVar);

		/// <summary>
		/// Gets the relic level
		/// </summary>
		public int CompletedRelicLevel => GetInt32(_completedRelicLevelVar);

		/// <summary>
		/// Gets the item scale percentage
		/// </summary>
		public float ItemScalePercent => 1.0F + (GetSingle(_itemScalePercent) / 100);

		/// <summary>
		/// Gets a string from the record
		/// </summary>
		/// <param name="variable">variable which we are getting the string from</param>
		/// <returns>string from the variable.</returns>
		public string GetString(string variable) => _record.GetString(variable, 0);

		/// <summary>
		/// Gets an int from the record
		/// </summary>
		/// <param name="variable">variable which we are getting the integer from</param>
		/// <returns>integer value from the variable.</returns>
		public int GetInt32(string variable) => _record.GetInt32(variable, 0);

		/// <summary>
		/// Gets a float value
		/// </summary>
		/// <param name="variable">variable which we are getting the float from</param>
		/// <returns>float value from the variable</returns>
		public float GetSingle(string variable) => _record.GetSingle(variable, 0);

		// TODO use factory and or strategy pattern instead
		/// <summary>
		/// Find the type using the type from the database record instead of the path.
		/// </summary>
		/// <remarks>
		/// Changed by VillageIdiot <-- He indeed is an idiot, wtf is this method
		/// </remarks>
		private void AssignVariableNames()
		{
			string id = _record.RecordType;

			if (id.StartsWith("LOOTRANDOMIZER", NoCase))
			{
				_descriptionVar = "lootRandomizerName";
				_itemClassificationVar = "itemClassification";
			}
			else if (id.StartsWith(Item.ItemTypeRelic, NoCase) || id.StartsWith(Item.ItemTypeCharm, NoCase))
			{
				_descriptionVar = "description";
				_itemClassificationVar = "itemClassification";
				_bitmapVar = "relicBitmap";
				_shardBitmapVar = "shardBitmap";
				_itemClassVar = "Class";
				_completedRelicLevelVar = "completedRelicLevel";
				_styleVar = "itemText";
			}
			else if (id.StartsWith(Item.ItemTypeDye, NoCase))
			{
				_descriptionVar = "description";
				_bitmapVar = "bitmap";
				_itemClassVar = "Class";
			}
			else if (id.StartsWith("ONESHOT", NoCase) || id.StartsWith(Item.ItemTypeQuestItem, NoCase))
			{
				_descriptionVar = "description";
				_itemClassificationVar = "itemClassification";
				_bitmapVar = "bitmap";
				_itemClassVar = "Class";
				_styleVar = "itemText";
			}
			else if (id.StartsWith(Item.ItemTypeFormula, NoCase))
			{
				_descriptionVar = "description";
				_itemClassificationVar = "itemClassification";
				_bitmapVar = "artifactFormulaBitmapName";
				_itemClassVar = "Class"; // ItemArtifactFormula
			}
			else if (id.StartsWith(Item.ItemTypeArtifact, NoCase))
			{
				_descriptionVar = "description";
				_itemClassificationVar = "itemClassification";
				_bitmapVar = "artifactBitmap";
				_itemClassVar = "Class"; // ItemArtifact
			}
			else if (id.StartsWith(Item.ItemTypeEquipment, NoCase))
			{
				_descriptionVar = "description";
				_itemClassificationVar = "itemClassification";
				_bitmapVar = "bitmap";
				_itemClassVar = "Class"; // ItemEquipment
				_styleVar = "itemText";
			}
			else if (id.StartsWith("Skill_Mastery", NoCase))
			{
				_descriptionVar = "skillDisplayName";//skillBaseDescription
				_itemClassificationVar = "itemClassification";
				_bitmapVar = "skillUpBitmapName";
				_itemClassVar = "Class";
			}
			else if (id.Equals(Item.ItemTypeRangedOneHand, NoCase))
			{
				_descriptionVar = "itemNameTag";
				_itemClassificationVar = "itemClassification";
				_bitmapVar = "bitmap";
				_itemClassVar = "Class";
				_qualityVar = "itemQualityTag";
				_styleVar = "itemText";
			}
			else
			{
				_descriptionVar = "itemNameTag";
				_itemClassificationVar = "itemClassification";
				_bitmapVar = "bitmap";
				_itemClassVar = "Class";
				_qualityVar = "itemQualityTag";
				_styleVar = "itemStyleTag";
			}

			_itemScalePercent = "itemScalePercent";
		}
	}
}