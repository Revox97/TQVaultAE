using NUnit.Framework;
using TQVaultAE.TitanQuestDataProviders.Database;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.ArzTests
{
    [TestFixture]
    public class IntegrationDevTest
    {
        [Test]
        public async Task TestArzReader()
        {
            string path = "C:\\Users\\Leo\\Documents\\TQVaultTestData\\database.arz";
            ArzFile result = await new ArzProvider().ReadAsync(path);

            Assert.IsTrue(true);
        }

        [Test]
        public async Task TestArcReader()
        {
            string path = "C:\\Users\\Leo\\Documents\\TQVaultTestData\\Items.arc";
            ArcFile result = await new ArcProvider().ReadAsync(path);

            Assert.IsTrue(true);
        }
    }
}
