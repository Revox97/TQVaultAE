using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    internal class ArtifactClassificationTests
    {
        [Test]
        public void Lesser_HasCorrectIntegerValue()
        {
            int result = (int)ArtifactClassification.Lesser;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Greater_HasCorrectIntegerValue()
        {
            int result = (int)ArtifactClassification.Greater;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Divine_HasCorrectIntegerValue()
        {
            int result = (int)ArtifactClassification.Divine;

            Assert.That(result, Is.EqualTo(2));
        }
    }
}
