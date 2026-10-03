using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    internal class ArmorItemTypeTests
    {
        [Test]
        public void Arms_HasCorrectIntegerValue()
        {
            int result = (int)ArmorItemType.Arms;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Body_HasCorrectIntegerValue()
        {
            int result = (int)ArmorItemType.Body;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Head_HasCorrectIntegerValue()
        {
            int result = (int)ArmorItemType.Head;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Legs_HasCorrectIntegerValue()
        {
            int result = (int)ArmorItemType.Legs;

            Assert.That(result, Is.EqualTo(3));
        }
    }
}
