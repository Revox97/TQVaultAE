using System.Diagnostics;
using TQVaultAE.Application.Factories.ItemCreationStrategies;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;

namespace TQVaultAE.Application.Factories
{
    /// <summary>
    /// Represents a factory, that creates an  <see cref="Item"/>.
    /// </summary>
    public class ItemFactory
    {
        public static async Task<Item> GetCompleteItemAsync(Item item)
        {
            try
            {
                ArzRecord itemRecord = await new TitanQuestDatabaseService().GetRecordByPathAsync(item.ResourcePath);
                return await CreateItemByClassAsync(item, itemRecord).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Getting item meta data failed: {ex.Message}");
                return item;
            }
        }

        private static async Task<Item> CreateItemByClassAsync(Item item, ArzRecord itemRecord)
        {
            ItemClass? itemClass = itemRecord["Class"]?.Get<ItemClass>(0);
            item.Class = itemClass ?? default;

            if (itemClass is null)
                return item;

            ItemCreationStrategy itemCreationStrategy = itemClass switch
            {
                ItemClass.WeaponMelee_Sword
                    or ItemClass.WeaponHunting_RangedOneHand
                    or ItemClass.WeaponMelee_Mace
                    or ItemClass.WeaponMelee_Axe
                    or ItemClass.WeaponHunting_Spear
                    or ItemClass.WeaponHunting_Bow
                    or ItemClass.WeaponArmor_Shield
                    or ItemClass.WeaponMagical_Staff
                    => new WeaponItemCreationStrategy(),
                ItemClass.ArmorProtective_Head
                    or ItemClass.ArmorProtective_LowerBody
                    or ItemClass.ArmorProtective_Forearm
                    or ItemClass.ArmorProtective_UpperBody
                    => new ArmorItemCreationStrategy(),
                ItemClass.ItemArtifact => new ArtifactItemCreationStrategy(),
                ItemClass.OneShot_PotionHealth
                    or ItemClass.OneShot_PotionMana
                    or ItemClass.OneShot_Dye
                    or ItemClass.OneShot_Scroll
                    or ItemClass.OneShot_Scroll_Eternal
                    => new OneShotItemCreationStrategy(),
                ItemClass.ItemCharm or ItemClass.ItemRelic => new TalismanItemCreationStrategy(),
                ItemClass.ItemArtifactFormula => new ArtifactFormularItemCreationStrategy(),
                ItemClass.ArmorJewelry_Amulet or ItemClass.ArmorJewelry_Ring => new JewelryItemCreationStrategy(),
                ItemClass.QuestItem => new QuestItemCreationStrategy(),
                ItemClass.ItemEquipment => new ItemEquipmentItemCreationStrategy(),
                _ => new DefaultItemCreationStrategy()
            };

            return await itemCreationStrategy.CreateAsync(item, itemRecord).ConfigureAwait(false);
        }
    }
}
