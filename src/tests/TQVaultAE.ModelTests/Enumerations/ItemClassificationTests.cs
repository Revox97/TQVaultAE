using System.Text.Json.Serialization;
using Castle.Core.Internal;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class ItemClassificationTests
    {
        [Test]
        public void Class_HasCorrectJsonConverterAttribute()
        {
            Type type = typeof(ItemClassification);
            JsonConverterAttribute? attribute = type.GetAttribute<JsonConverterAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute.ConverterType, Is.EqualTo(typeof(JsonStringEnumConverter)));
            }
        }

        [Test]
        public void Broken_HasCorrectIntegerValue()
        {
            int result = (int)ItemClassification.Broken;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Common_HasCorrectIntegerValue()
        {
            int result = (int)ItemClassification.Common;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Rare_HasCorrectIntegerValue()
        {
            int result = (int)ItemClassification.Rare;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Epic_HasCorrectIntegerValue()
        {
            int result = (int)ItemClassification.Epic;

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Legendary_HasCorrectIntegerValue()
        {
            int result = (int)ItemClassification.Legendary;

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void Magical_HasCorrectIntegerValue()
        {
            int result = (int)ItemClassification.Magical;

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Quest_HasCorrectIntegerValue()
        {
            int result = (int)ItemClassification.Quest;

            Assert.That(result, Is.EqualTo(6));
        }
    }
}
