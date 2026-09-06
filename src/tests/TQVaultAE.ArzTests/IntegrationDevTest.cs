using NUnit.Framework;
using TQVaultAE.Arz;
using TQVaultAE.Arz.Model;
using TQVaultAE.IO;

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
    }
}
