using System.Reflection;
using Castle.Core.Internal;
using TQVaultAE.Model.Attributes;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class AttackSpeedTests
    {
        [Test]
        public void NotSet_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.NotSet));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("CharacterAttackSpeedNotSet"));
            }
        }

        [Test]
        public void NotSet_HasCorrectLocalizationTagAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.NotSet));
            LocalizationTagAttribute? attribute = fieldInfo?.GetAttribute<LocalizationTagAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.LocalizationTag, Is.EqualTo("CharacterAttackSpeedNotSet"));
            }
        }

        [Test]
        public void NotSet_HasCorrectIntegerValue()
        {
            int result = (int)AttackSpeed.NotSet;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void VerySlow_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.VerySlow));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("CharacterAttackSpeedVerySlow"));
            }
        }

        [Test]
        public void VerySlow_HasCorrectLocalizationTagAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.VerySlow));
            LocalizationTagAttribute? attribute = fieldInfo?.GetAttribute<LocalizationTagAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.LocalizationTag, Is.EqualTo("CharacterAttackSpeedVerySlow"));
            }
        }

        [Test]
        public void VerySlow_HasCorrectIntegerValue()
        {
            int result = (int)AttackSpeed.VerySlow;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Slow_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.Slow));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("CharacterAttackSpeedSlow"));
            }
        }

        [Test]
        public void Slow_HasCorrectLocalizationTagAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.Slow));
            LocalizationTagAttribute? attribute = fieldInfo?.GetAttribute<LocalizationTagAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.LocalizationTag, Is.EqualTo("CharacterAttackSpeedSlow"));
            }
        }

        [Test]
        public void Slow_HasCorrectIntegerValue()
        {
            int result = (int)AttackSpeed.Slow;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Average_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.Average));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("CharacterAttackSpeedAverage"));
            }
        }

        [Test]
        public void Average_HasCorrectLocalizationTagAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.Average));
            LocalizationTagAttribute? attribute = fieldInfo?.GetAttribute<LocalizationTagAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.LocalizationTag, Is.EqualTo("CharacterAttackSpeedAverage"));
            }
        }

        [Test]
        public void Average_HasCorrectIntegerValue()
        {
            int result = (int)AttackSpeed.Average;

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Fast_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.Fast));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("CharacterAttackSpeedFast"));
            }
        }

        [Test]
        public void Fast_HasCorrectLocalizationTagAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.Fast));
            LocalizationTagAttribute? attribute = fieldInfo?.GetAttribute<LocalizationTagAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.LocalizationTag, Is.EqualTo("CharacterAttackSpeedFast"));
            }
        }

        [Test]
        public void Fast_HasCorrectIntegerValue()
        {
            int result = (int)AttackSpeed.Fast;

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void VeryFast_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.VeryFast));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("CharacterAttackSpeedVeryFast"));
            }
        }

        [Test]
        public void VeryFast_HasCorrectLocalizationTagAttribute()
        {
            FieldInfo? fieldInfo = typeof(AttackSpeed).GetField(nameof(AttackSpeed.VeryFast));
            LocalizationTagAttribute? attribute = fieldInfo?.GetAttribute<LocalizationTagAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.LocalizationTag, Is.EqualTo("CharacterAttackSpeedVeryFast"));
            }
        }

        [Test]
        public void VeryFast_HasCorrectIntegerValue()
        {
            int result = (int)AttackSpeed.VeryFast;

            Assert.That(result, Is.EqualTo(5));
        }
    }
}
