using System.Reflection;
using Castle.Core.Internal;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class OneShotBonusTypeTests
    {
        [Test]
        public void AttributePoints_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(OneShotBonusType).GetField(nameof(OneShotBonusType.AttributePoints));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("bonusAttributePoints"));
            }
        }

        [Test]
        public void AttributePoints_HasCorrectIntegerValue()
        {
            int result = (int)OneShotBonusType.AttributePoints;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void ExperiencePoints_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(OneShotBonusType).GetField(nameof(OneShotBonusType.ExperiencePoints));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("bonusExperiencePoints"));
            }
        }

        [Test]
        public void ExperiencePoints_HasCorrectIntegerValue()
        {
            int result = (int)OneShotBonusType.ExperiencePoints;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void GoldPoints_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(OneShotBonusType).GetField(nameof(OneShotBonusType.GoldPoints));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("bonusGoldPoints"));
            }
        }

        [Test]
        public void GoldPoints_HasCorrectIntegerValue()
        {
            int result = (int)OneShotBonusType.GoldPoints;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void LifePercent_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(OneShotBonusType).GetField(nameof(OneShotBonusType.LifePercent));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("bonusLifePercent"));
            }
        }

        [Test]
        public void LifePercent_HasCorrectIntegerValue()
        {
            int result = (int)OneShotBonusType.LifePercent;

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void LifePoints_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(OneShotBonusType).GetField(nameof(OneShotBonusType.LifePoints));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("bonusLifePoints"));
            }
        }

        [Test]
        public void LifePoints_HasCorrectIntegerValue()
        {
            int result = (int)OneShotBonusType.LifePoints;

            Assert.That(result, Is.EqualTo(4));
        }

        [Test]
        public void ManaPercent_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(OneShotBonusType).GetField(nameof(OneShotBonusType.ManaPercent));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("bonusManaPercent"));
            }
        }

        [Test]
        public void ManaPercent_HasCorrectIntegerValue()
        {
            int result = (int)OneShotBonusType.ManaPercent;

            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void ManaPoints_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(OneShotBonusType).GetField(nameof(OneShotBonusType.ManaPoints));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("bonusManaPoints"));
            }
        }

        [Test]
        public void ManaPoints_HasCorrectIntegerValue()
        {
            int result = (int)OneShotBonusType.ManaPoints;

            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void SkillPoints_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(OneShotBonusType).GetField(nameof(OneShotBonusType.SkillPoints));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("bonusSkillPoints"));
            }
        }

        [Test]
        public void SkillPoints_HasCorrectIntegerValue()
        {
            int result = (int)OneShotBonusType.SkillPoints;

            Assert.That(result, Is.EqualTo(7));
        }
    }
}
