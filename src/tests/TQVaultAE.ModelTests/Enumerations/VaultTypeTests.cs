using System.Text.Json.Serialization;
using Castle.Core.Internal;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class VaultTypeTests
    {
        [Test]
        public void Class_HasCorrectJsonConverterAttribute()
        {
            Type type = typeof(VaultType);
            JsonConverterAttribute? attribute = type.GetAttribute<JsonConverterAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute.ConverterType, Is.EqualTo(typeof(JsonStringEnumConverter)));
            }
        }

        [Test]
        public void Items_HasCorrectIntegerValue()
        {
            int result = (int)VaultType.Items;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Sets_HasCorrectIntegerValue()
        {
            int result = (int)VaultType.Sets;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Crafting_HasCorrectIntegerValue()
        {
            int result = (int)VaultType.Crafting;

            Assert.That(result, Is.EqualTo(2));
        }
    }
}
