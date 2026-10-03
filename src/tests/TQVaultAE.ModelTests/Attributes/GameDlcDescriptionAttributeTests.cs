using Castle.Core.Internal;
using TQVaultAE.Model.Attributes;

namespace TQVaultAE.ModelTests.Attributes
{
    [TestFixture]
    public class GameDlcDescriptionAttributeTests
    {
        [Test]
        public void Class_HasCorrectAttributeUsageAttribute()
        {
            Type type = typeof(GameDlcDescriptionAttribute);
            AttributeUsageAttribute? attribute = type.GetAttribute<AttributeUsageAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute.ValidOn, Is.EqualTo(AttributeTargets.Field));
                Assert.That(attribute.AllowMultiple, Is.False);
            }
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("expected")]
        [TestCase("ex pected")]
        [TestCase(" expected ")]
        public void Ctor_CodePropertyIsSetCorrectly(string? expected)
        {
            GameDlcDescriptionAttribute instance = new(expected!, string.Empty);

            string result = instance.Code;

            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("expected")]
        [TestCase("ex pected")]
        [TestCase(" expected ")]
        public void Ctor_TranslationTagPropertyIsSetCorrectly(string? expected)
        {
            GameDlcDescriptionAttribute instance = new(string.Empty, expected!);

            string result = instance.TranslationTag;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
