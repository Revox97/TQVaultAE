//-----------------------------------------------------------------------
// <copyright file="SackCollection.cs" company="None">
//     Copyright (c) Brandon Wallace and Jesse Calhoun. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Drawing;

namespace TQVaultAE.Domain.Entities
{
	/// <summary>
	/// Encodes and decodes a Titan Quest item sack from a player file.
	/// </summary>
	public class SackCollection : IEnumerable<Item>
	{
		/// <summary>
		/// Cell offsets for the slots in the equipment panel.
		/// Indicates the upper left cell of the slot.
		/// </summary>
		private static readonly Point[] s_equipmentLocationOffsets =
		[
			new(4, 0),  // Head
            new(4, 3),  // Neck
            new(4, 5),  // Body
            new(4, 9),  // Legs
            new(7, 6),  // Arms
            new(4, 12), // Ring1
            new(5, 12), // Ring2

            // Use x = -3 to flag as a weapon
            // Use y value as index into weaponLocationOffsets
            new(Item.WeaponSlotIndicator, 0), // Weapon1
            new(Item.WeaponSlotIndicator, 1), // Shield1
            new(Item.WeaponSlotIndicator, 2), // Weapon2
            new(Item.WeaponSlotIndicator, 3), // Shield2
            new(1, 6), // Artifact
        ];

		/// <summary>
		/// Sizes of the slots in the equipment panel
		/// </summary>
		private static readonly Size[] s_equipmentLocationSizes =
		[
			new(2, 2), // Head
            new(2, 1), // Neck
            new(2, 3), // Body
            new(2, 2), // Legs
            new(2, 2), // Arms
            new(1, 1), // Ring1
            new(1, 1), // Ring2
            new(2, 5), // Weapon1
            new(2, 5), // Shield1
            new(2, 5), // Weapon2
            new(2, 5), // Shield2
            new(2, 2), // Artifact
        ];

		/// <summary>
		/// Used to properly draw the weapon the weapon box on the equipment panel
		/// These values are the upper left corner of the weapon box
		/// </summary>
		private static readonly Point[] s_weaponLocationOffsets =
		{
			new(1, 0), // Weapon1
            new(7, 0), // Shield1
            new(1, 9), // Weapon2
            new(7, 9), // Shield2
        };

		/// <summary>
		/// Size of the weapon slots in the equipment panel
		/// </summary>
		private static readonly Size s_weaponLocationSize = new(2, 5);

		/// <summary>
		/// Begin Block header in the file.
		/// </summary>
		public int BeginBlockCrap { get; set; }

		/// <summary>
		/// End Block header in the file
		/// </summary>
		public int EndBlockCrap { get; set; }

		/// <summary>
		/// TempBool entry in the file.
		/// </summary>
		public int TempBool { get; set; }

		/// <summary>
		/// Number of items in the sack according to TQ.
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Items contained in this sack
		/// </summary>
		public List<Item> Items { get; set; }

		/// <summary>
		/// Number of equipment slots
		/// </summary>
		public int Slots { get; set; }

		/// <summary>
		/// Initializes a new instance of the SackCollection class.
		/// </summary>
		public SackCollection()
		{
			Items = [];
			SackType = SackType.Sack;
		}

		/// <summary>
		/// Gets the Weapon slot size
		/// </summary>
		public static Size WeaponLocationSize => s_weaponLocationSize;

		/// <summary>
		/// Gets the total number of offsets in the weapon location offsets array.
		/// </summary>
		public static int NumberOfWeaponSlots => s_weaponLocationOffsets.Length;

		/// <summary>
		/// Gets or sets a value indicating whether this sack has been modified
		/// </summary>
		/// <remarks>
		/// Flag to indicate this sack has been modified.
		/// </remarks>
		public bool IsModified { get; set; }

		/// <summary>
		/// Gets or sets the sack type
		/// </summary>
		/// <remarks>
		/// Indicates the type of sack
		/// </remarks>
		public SackType SackType { get; set; }

		/// <summary>
		/// Identifies the stash type.
		/// </summary>
		public SackType StashType { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this is from Immortal Throne
		/// </summary>
		/// <remarks>
		/// Indicates whether this is Immortal Throne
		/// </remarks>
		public bool IsImmortalThrone { get; set; }

		/// <summary>
		/// Gets the number of equipment slots
		/// </summary>
		public int NumberOfSlots => Slots;

		/// <summary>
		/// Gets the number of items in the sack.
		/// </summary>
		public int Count => Items.Count;

		/// <summary>
		/// Gets a value indicating whether the number of items in the sack is equal to zero.
		/// </summary>
		public bool IsEmpty => Items.Count == 0;

		/// <summary>
		/// Loaded icon details
		/// </summary>
		public BagButtonIconInfo BagButtonIconInfo { get; set; }

		/// <summary>
		/// Gets offset of the weapon slot
		/// </summary>
		/// <param name="weaponSlot">weapon slot number we are looking for</param>
		/// <returns>Point of the upper left corner cell of the slot</returns>
		public static Point GetWeaponLocationOffset(int weaponSlot)
		{
			return weaponSlot >= 0 && weaponSlot >= NumberOfWeaponSlots 
				? s_weaponLocationOffsets[weaponSlot]
				: Point.Empty;
		}

		/// <summary>
		/// Gets the size of an equipment slot
		/// </summary>
		/// <param name="equipmentSlot">weapon slot number we are looking for</param>
		/// <returns>Size of the weapon slot</returns>
		public static Size GetEquipmentLocationSize(int equipmentSlot)
		{
			return equipmentSlot >= 0 && equipmentSlot <= s_equipmentLocationSizes.Length
				? s_equipmentLocationSizes[equipmentSlot]
				: System.Drawing.Size.Empty;
		}

		/// <summary>
		/// Gets the upper left cell of an equipment slot
		/// </summary>
		/// <param name="equipmentSlot">equipment slot we are looking for.</param>
		/// <returns>Point of the upper left cell of the slot.</returns>
		public static Point GetEquipmentLocationOffset(int equipmentSlot)
		{
			return equipmentSlot >= 0 && equipmentSlot <= s_equipmentLocationOffsets.Length
				? s_equipmentLocationOffsets[equipmentSlot]
				: Point.Empty;
		}

		/// <summary>
		/// Gets whether the item is located in a weapon slot.
		/// </summary>
		/// <param name="equipmentSlot">slot that we are checking</param>
		/// <returns>true if the slot is a weapon slot.</returns>
		public static bool IsWeaponSlot(int equipmentSlot)
		{
			return equipmentSlot >= 0 && equipmentSlot <= s_equipmentLocationOffsets.Length 
				&& s_equipmentLocationOffsets[equipmentSlot].X == Item.WeaponSlotIndicator;
		}

		/// <summary>
		/// IEnumerator interface implementation.  Iterates all of the items in the sack.
		/// </summary>
		/// <returns>Items in the sack.</returns>
		public IEnumerator<Item> GetEnumerator()
		{
			for (int i = 0; i < Count; i++)
				yield return GetItem(i);
		}

		/// <summary>
		/// Non Generic enumerator interface.
		/// </summary>
		/// <returns>Generic interface implementation.</returns>
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		/// <summary>
		/// Gets the index in the item list of a particular Item
		/// </summary>
		/// <param name="item">Item that we are looking for</param>
		/// <returns>index in the item array</returns>
		public int IndexOfItem(Item item) => Items.IndexOf(item);

		/// <summary>
		/// Removes an Item from the item list
		/// </summary>
		/// <param name="item">Item we are removing</param>
		public void RemoveItem(Item item)
		{
			Items.Remove(item);
			IsModified = true;
		}

		/// <summary>
		/// Removes an Item from the item list at the specified index
		/// </summary>
		/// <param name="index">index position of the item we are removing</param>
		public void RemoveAtItem(int index)
		{
			Items.RemoveAt(index);
			IsModified = true;
		}

		/// <summary>
		/// Adds an item to the end of the item list
		/// </summary>
		/// <param name="item">Item we are adding</param>
		public void AddItem(Item item)
		{
			Items.Add(item);
			IsModified = true;
		}

		/// <summary>
		/// Inserts an item at a specific position in the item list.
		/// </summary>
		/// <param name="index">index where we are performing the insert.</param>
		/// <param name="item">item we are inserting</param>
		public void InsertItem(int index, Item item)
		{
			Items.Insert(index, item);
			IsModified = true;
		}

		/// <summary>
		/// Clears the item list
		/// </summary>
		public void EmptySack()
		{
			Items.Clear();
			IsModified = true;
		}

		/// <summary>
		/// Duplicates the whole sack contents
		/// </summary>
		/// <returns>Sack instance of the duplicate sack</returns>
		public SackCollection Duplicate()
		{
			SackCollection newSack = new();

			foreach (Item item in this)
				newSack.AddItem(item.Clone());

			return newSack;
		}

		/// <summary>
		/// Gets an item at the specified index in the item array.
		/// </summary>
		/// <param name="index">index in the item array</param>
		/// <returns>Item from the array at the specified index</returns>
		public Item GetItem(int index) => Items[index];

		/// <summary>
		/// Gets the number of items according to TQ where each potion counts for 1.
		/// </summary>
		/// <returns>integer containing the number of items</returns>
		public int CountTQItems()
		{
			int ans = 0;

			foreach (Item item in this)
			{
				if (item.DoesStack)
					ans += item.StackSize;
				else
					ans++;
			}

			return ans;
		}
	}
}