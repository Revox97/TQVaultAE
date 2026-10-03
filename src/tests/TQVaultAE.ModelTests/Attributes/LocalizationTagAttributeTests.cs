using Castle.Core.Internal;
using TQVaultAE.Model.Attributes;

namespace TQVaultAE.ModelTests.Attributes
{
    [TestFixture]
    public class LocalizationTagAttributeTests
    {
        [Test]
        public void Class_HasCorrectAttributeUsageAttribute()
        {
            Type type = typeof(LocalizationTagAttribute);
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
        public void Ctor_LocalizationTagPropertyIsSetCorrectly(string? expected)
        {
            LocalizationTagAttribute instance = new(expected!);

            string result = instance.LocalizationTag;

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}
