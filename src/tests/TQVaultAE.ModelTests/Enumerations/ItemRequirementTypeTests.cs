using System.Reflection;
using Castle.Core.Internal;
using TQVaultAE.Model.Attributes;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class ItemRequirementTypeTests
    {
        [Test]
        public void Dexterity_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(ItemRequirementType).GetField(nameof(ItemRequirementType.Dexterity));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("dexterityRequirement"));
            }
        }

        [Test]
        public void Dexterity_HasCorrectIntegerValue()
        {
            int result = (int)ItemRequirementType.Dexterity;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void Intelligence_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(ItemRequirementType).GetField(nameof(ItemRequirementType.Intelligence));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("intelligenceRequirement"));
            }
        }

        [Test]
        public void Intelligence_HasCorrectIntegerValue()
        {
            int result = (int)ItemRequirementType.Intelligence;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Level_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(ItemRequirementType).GetField(nameof(ItemRequirementType.Level));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("levelRequirement"));
            }
        }

        [Test]
        public void Level_HasCorrectLocalizationTagAttribute()
        {
            FieldInfo? fieldInfo = typeof(ItemRequirementType).GetField(nameof(ItemRequirementType.Level));
            LocalizationTagAttribute? attribute = fieldInfo?.GetAttribute<LocalizationTagAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.LocalizationTag, Is.EqualTo("LevelRequirement"));
            }
        }

        [Test]
        public void Level_HasCorrectIntegerValue()
        {
            int result = (int)ItemRequirementType.Level;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Strength_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(ItemRequirementType).GetField(nameof(ItemRequirementType.Strength));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("strengthRequirement"));
            }
        }

        [Test]
        public void Strength_HasCorrectIntegerValue()
        {
            int result = (int)ItemRequirementType.Strength;

            Assert.That(result, Is.EqualTo(3));
        }
    }
}
