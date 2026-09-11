using NUnit.Framework;
using TQVaultAE.FileFormats.Arc;
using TQVaultAE.FileFormats.Arz;
using TQVaultAE.FileFormats.Chr;
using TQVaultAE.TitanQuestDataProviders.Database;
using TQVaultAE.TitanQuestDataProviders.SaveGame;

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
            string path = "C:\\Users\\Leo\\Documents\\TQVaultTestData\\Text_DE.arc";
            ArcFile result = await new ArcProvider().ReadAsync(path);

            Assert.IsTrue(true);
        }

        [Test]
        public async Task TestChrReader()
        {
            string path = "C:\\Users\\Leo\\Documents\\TQVaultTestData\\Player.chr";
            ChrFile result = await new ChrProvider().ReadAsync(path);

            Assert.IsTrue(true);
        }
    }
}
