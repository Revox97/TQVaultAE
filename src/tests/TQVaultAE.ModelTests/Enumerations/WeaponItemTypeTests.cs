using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class WeaponItemTypeTests
    {
        [Test]
        public void Axe_HasCorrectIntegerValue()
        {
            int result = (int)WeaponItemType.Axe;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Bow_HasCorrectIntegerValue()
        {
            int result = (int)WeaponItemType.Bow;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Mace_HasCorrectIntegerValue()
        {
            int result = (int)WeaponItemType.Mace;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void RangedOneHand_HasCorrectIntegerValue()
        {
            int result = (int)WeaponItemType.RangedOneHand;

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Shield_HasCorrectIntegerValue()
        {
            int result = (int)WeaponItemType.Shield;

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Spear_HasCorrectIntegerValue()
        {
            int result = (int)WeaponItemType.Spear;

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Staff_HasCorrectIntegerValue()
        {
            int result = (int)WeaponItemType.Staff;

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Sword_HasCorrectIntegerValue()
        {
            int result = (int)WeaponItemType.Sword;

            Assert.That(result, Is.EqualTo(7));
        }
    }
}
