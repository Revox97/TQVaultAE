using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class TalismanTypeTests
    {
        [Test]
        public void Charm_HasCorrectIntegerValue()
        {
            int result = (int)TalismanType.Charm;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Relic_HasCorrectIntegerValue()
        {
            int result = (int)TalismanType.Relic;

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
