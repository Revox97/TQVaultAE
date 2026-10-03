using System.Text.Json.Serialization;
using Castle.Core.Internal;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class ItemClassTests
    {
        [Test]
        public void Class_HasCorrectJsonConverterAttribute()
        {
            Type type = typeof(ItemClass);
            JsonConverterAttribute? attribute = type.GetAttribute<JsonConverterAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute.ConverterType, Is.EqualTo(typeof(JsonStringEnumConverter)));
            }
        }

        [Test]
        public void ArmorJewelry_Amulet_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ArmorJewelry_Amulet;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void ArmorJewelry_Ring_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ArmorJewelry_Ring;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void ArmorProtective_Forearm_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ArmorProtective_Forearm;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void ArmorProtective_Head_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ArmorProtective_Head;

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void ArmorProtective_LowerBody_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ArmorProtective_LowerBody;

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void ArmorProtective_UpperBody_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ArmorProtective_UpperBody;

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void ItemArtifact_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ItemArtifact;

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void ItemArtifactFormula_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ItemArtifactFormula;

            Assert.That(result, Is.EqualTo(7));
        }

        [Test]
        public void ItemCharm_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ItemCharm;

            Assert.That(result, Is.EqualTo(8));
        }

        [Test]
        public void ItemRelic_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ItemRelic;

            Assert.That(result, Is.EqualTo(9));
        }

        [Test]
        public void ItemEquipment_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.ItemEquipment;

            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void LootRandomizer_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.LootRandomizer;

            Assert.That(result, Is.EqualTo(11));
        }

        [Test]
        public void OneShot_Dye_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.OneShot_Dye;

            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void OneShot_PotionHealth_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.OneShot_PotionHealth;

            Assert.That(result, Is.EqualTo(13));
        }

        [Test]
        public void OneShot_PotionMana_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.OneShot_PotionMana;

            Assert.That(result, Is.EqualTo(14));
        }

        [Test]
        public void OneShot_Scroll_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.OneShot_Scroll;

            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void OneShot_Scroll_Eternal_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.OneShot_Scroll_Eternal;

            Assert.That(result, Is.EqualTo(16));
        }

        [Test]
        public void Quest_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.Quest;

            Assert.That(result, Is.EqualTo(17));
        }

        [Test]
        public void QuestItem_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.QuestItem;

            Assert.That(result, Is.EqualTo(18));
        }

        [Test]
        public void WeaponArmor_Shield_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.WeaponArmor_Shield;

            Assert.That(result, Is.EqualTo(19));
        }

        [Test]
        public void WeaponHunting_Bow_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.WeaponHunting_Bow;

            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void WeaponHunting_RangedOneHand_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.WeaponHunting_RangedOneHand;

            Assert.That(result, Is.EqualTo(21));
        }

        [Test]
        public void WeaponHunting_Spear_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.WeaponHunting_Spear;

            Assert.That(result, Is.EqualTo(22));
        }

        [Test]
        public void WeaponMagical_Staff_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.WeaponMagical_Staff;

            Assert.That(result, Is.EqualTo(23));
        }

        [Test]
        public void WeaponMelee_Axe_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.WeaponMelee_Axe;

            Assert.That(result, Is.EqualTo(24));
        }

        [Test]
        public void WeaponMelee_Mace_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.WeaponMelee_Mace;

            Assert.That(result, Is.EqualTo(25));
        }

        [Test]
        public void WeaponMelee_Sword_HasCorrectIntegerValue()
        {
            int result = (int)ItemClass.WeaponMelee_Sword;

            Assert.That(result, Is.EqualTo(26));
        }
    }
}
