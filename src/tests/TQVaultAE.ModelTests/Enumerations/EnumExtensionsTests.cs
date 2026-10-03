using TQVaultAE.Model.Attributes;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        public void GetEnumValue_ValueIsNull_ThrowsArgumentNullException()
        {
            string? input = null;
            Assert.Throws<ArgumentNullException>(() => input!.GetEnumValue<TestEnum>());
        }

        [Test]
        public void GetEnumValue_ValueIsEmptyString_ThrowsArgumentException()
        {
            string input = string.Empty;
            Assert.Throws<ArgumentException>(() => input.GetEnumValue<TestEnum>());
        }

        [Test]
        [TestCase("Sample_Desc", TestEnum.Item1)]
        [TestCase("sample_Desc", TestEnum.Item1)]
        [TestCase("sample_desc", TestEnum.Item1)]
        [TestCase("Sample_desc", TestEnum.Item1)]
        public void GetEnumValue_EnumContainsFieldWithMatchingDescription_ReturnsCorrectField(string input, TestEnum expected)
        {
            TestEnum result = input.GetEnumValue<TestEnum>();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase("Item1", TestEnum.Item1)]
        [TestCase("item1", TestEnum.Item1)]
        [TestCase("Item2", TestEnum.Item2)]
        [TestCase("item2", TestEnum.Item2)]
        public void GetEnumValue_EnumContainsFieldWithMatchingName_ReturnsCorrectField(string input, TestEnum expected)
        {
            TestEnum result = input.GetEnumValue<TestEnum>();

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void GetEnumValue_EnumDoesNotContainMatchingNameOrDescription_ThrowsArgumentException()
        {
            string input = "invalid";

            Assert.Throws<ArgumentException>(() => input.GetEnumValue<TestEnum>());
        }

        [Test]
        public void GetEnumStringValue_FieldOfValueHasDescription_ReturnsDescription()
        {
            string result = TestEnum.Item1.GetEnumStringValue();

            Assert.That(result, Is.EqualTo("Sample_Desc"));
        }

        [Test]
        public void GetEnumStringValue_FieldOfValueHasNoDescription_ReturnsFieldName()
        {
            string result = TestEnum.Item2.GetEnumStringValue();

            Assert.That(result, Is.EqualTo("Item2"));
        }

        [Test]
        public void GetLocalizationTag_FieldOfValueHasLocalizationTag_ReturnsLocalizationTag()
        {
            string? result = TestEnum.Item1.GetLocalizationTag();

            Assert.That(result, Is.EqualTo("Sample_Loc"));
        }

        [Test]
        public void GetLocalizationTag_FieldOfValueHasNoLocalizationTag_ReturnsNull()
        {
            string? result = TestEnum.Item2.GetLocalizationTag();

            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetLocalizationTagOrEnumValue_FieldOfValueHasLocalizationTag_ReturnsLocalizationTag()
        {
            string? result = TestEnum.Item1.GetLocalizationTagOrEnumValue();

            Assert.That(result, Is.EqualTo("Sample_Loc"));
        }

        [Test]
        public void GetLocalizationTagOrEnumValue_FieldOfValueHasNoLocalizationTag_ReturnsEnumValue()
        {
            string? result = TestEnum.Item2.GetLocalizationTagOrEnumValue();

            Assert.That(result, Is.EqualTo("Item2"));
        }
    }

    public enum TestEnum
    {
        [System.ComponentModel.Description("Sample_Desc")]
        [LocalizationTag("Sample_Loc")]
        Item1,
        Item2
    }
}
