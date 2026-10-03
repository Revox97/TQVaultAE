using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class JewelryItemTypeTests
    {
        [Test]
        public void Amulet_HasCorrectIntegerValue()
        {
            int result = (int)JewelryItemType.Amulet;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Ring_HasCorrectIntegerValue()
        {
            int result = (int)JewelryItemType.Ring;

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
