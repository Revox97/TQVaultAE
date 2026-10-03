using Moq;
using TQVaultAE.Localisation;
using TQVaultAE.Model.Items.ItemProperties;

namespace TQVaultAE.ModelTests.Items.ItemProperties
{
    [TestFixture]
    public class GlobalItemPropertyTests
    {
        private Mock<IGameLocalizationService> _gameLocalizationService;
        private GlobalItemProperty _instance;

        [SetUp]
        public void SetUp()
        {
            _gameLocalizationService = new Mock<IGameLocalizationService>();
            _instance = new(_gameLocalizationService.Object);
        }

        [Test]
        public void Ctor_SubPropertiesDefaultsToEmptyList()
        {
            List<ItemProperty> result = _instance.SubProperties;

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Ctor_TypeIsSetToGlobal()
        {
            ItemPropertyType result = _instance.Type;

            Assert.That(result, Is.EqualTo(ItemPropertyType.Global));
        }

        [Test]
        public void Ctor_NameDefaultsToEmptyString()
        {
            string result = _instance.Name;

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Ctor_ChanceDefaultsToZero()
        {
            float result = _instance.Chance;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void ToString_HasNoChildProperties_ReturnsCorrectValue()
        {
            string expected = "4% Chance of";
            _instance.Name = "GlobalItemProperty";
            _instance.Chance = 3.5f;
            _gameLocalizationService.Setup(x => x.GetLocalizedValueByTagAsync("GlobalItemProperty")).ReturnsAsync("{%.0f0}% Chance of");

            string result = _instance.ToString();

            Assert.That(result, Is.EqualTo(expected));
        }

        // TODO Continue here
    }
}
