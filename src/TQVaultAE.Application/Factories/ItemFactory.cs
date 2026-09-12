using System.Diagnostics;
using System.Runtime.Versioning;
using TQVaultAE.Application.Factories.ItemCreationStrategies;
using TQVaultAE.Application.Services;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.Model.Enumerations;
using TQVaultAE.Model.Items;
using TQVaultAE.TitanQuestDataProviders.Database;

namespace TQVaultAE.Application.Factories
{
    /// <summary>
    /// Represents a factory, that creates an  <see cref="Item"/>.
    /// </summary>
    public class ItemFactory
    {

        // TODO Make dynamic, hardcoded for testing purposes.
        private readonly string _dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TQVaultTestData", "database.arz");
        private static ArzFile? s_database;

        public async Task<Item> GetCompleteItemAsync(Item item)
        {
            try
            {
                string itemDbPath = item.ResourcePath;

                s_database ??= await new ArzProvider().ReadAsync(_dbPath).ConfigureAwait(false);

                ArzRecord itemRecord = s_database.GetRecordByPath(itemDbPath);
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
                    or ItemClass.WeaponHunting_Spear
                    or ItemClass.WeaponArmor_Shield
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
                ItemClass.ItemCharm => new CharmItemCreationStrategy(),
                ItemClass.ItemArtifactFormula => new ArtifactFormularItemCreationStrategy(),
                ItemClass.ArmorJewelry_Amulet or ItemClass.ArmorJewelry_Ring => new ArmorJewelryItemCreationStrategy(),
                ItemClass.QuestItem => new QuestItemCreationStrategy(),
                ItemClass.ItemEquipment => new ItemEquipmentItemCreationStrategy(),
                _ => new DefaultItemCreationStrategy()
            };

            return await itemCreationStrategy.CreateAsync(item, itemRecord).ConfigureAwait(false);
        }
    }
}
