using System.Text.Json.Serialization;
using Castle.Core.Internal;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class LanguageTests
    {
        [Test]
        public void Class_HasCorrectJsonConverterAttribute()
        {
            Type type = typeof(Language);
            JsonConverterAttribute? attribute = type.GetAttribute<JsonConverterAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute.ConverterType, Is.EqualTo(typeof(JsonStringEnumConverter)));
            }
        }

        [Test]
        public void Deutsch_HasCorrectIntegerValue()
        {
            int result = (int)Language.Deutsch;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void English_HasCorrectIntegerValue()
        {
            int result = (int)Language.English;

            Assert.That(result, Is.EqualTo(1));
        }
    }
}
