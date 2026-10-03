using System.Reflection;
using Castle.Core.Internal;
using TQVaultAE.Model.Attributes;
using TQVaultAE.Model.Enumerations;

namespace TQVaultAE.ModelTests.Enumerations
{
    [TestFixture]
    public class GameDlcTests
    {
        [Test]
        public void TitanQuest_HasCorrectGameDlcDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(GameDlc).GetField(nameof(GameDlc.TitanQuest));
            GameDlcDescriptionAttribute? attribute = fieldInfo?.GetAttribute<GameDlcDescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Code, Is.EqualTo("TQ"));
                Assert.That(attribute!.TranslationTag, Is.EqualTo("tagBackground01"));
            }
        }

        [Test]
        public void TitanQuest_HasCorrectIntegerValue()
        {
            int result = (int)GameDlc.TitanQuest;

            Assert.That(result, Is.Zero);
        }

        [Test]
        public void ImmortalThrone_HasCorrectGameDlcDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(GameDlc).GetField(nameof(GameDlc.ImmortalThrone));
            GameDlcDescriptionAttribute? attribute = fieldInfo?.GetAttribute<GameDlcDescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Code, Is.EqualTo("IT"));
                Assert.That(attribute!.TranslationTag, Is.EqualTo("tagBackground02"));
            }
        }

        [Test]
        public void ImmortalThrone_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(GameDlc).GetField(nameof(GameDlc.ImmortalThrone));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("Immortal Throne"));
            }
        }

        [Test]
        public void ImmortalThrone_HasCorrectIntegerValue()
        {
            int result = (int)GameDlc.ImmortalThrone;

            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void Ragnarok_HasCorrectGameDlcDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(GameDlc).GetField(nameof(GameDlc.Ragnarok));
            GameDlcDescriptionAttribute? attribute = fieldInfo?.GetAttribute<GameDlcDescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Code, Is.EqualTo("RAG"));
                Assert.That(attribute!.TranslationTag, Is.EqualTo("tagBackground03"));
            }
        }

        [Test]
        public void Ragnarok_HasCorrectIntegerValue()
        {
            int result = (int)GameDlc.Ragnarok;

            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Atlantis_HasCorrectGameDlcDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(GameDlc).GetField(nameof(GameDlc.Atlantis));
            GameDlcDescriptionAttribute? attribute = fieldInfo?.GetAttribute<GameDlcDescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Code, Is.EqualTo("ATL"));
                Assert.That(attribute!.TranslationTag, Is.EqualTo("tagBackground04"));
            }
        }

        [Test]
        public void Atlantis_HasCorrectIntegerValue()
        {
            int result = (int)GameDlc.Atlantis;

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void EternalEmbers_HasCorrectGameDlcDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(GameDlc).GetField(nameof(GameDlc.EternalEmbers));
            GameDlcDescriptionAttribute? attribute = fieldInfo?.GetAttribute<GameDlcDescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Code, Is.EqualTo("EEM"));
                Assert.That(attribute!.TranslationTag, Is.EqualTo("x4tagBackground05"));
            }
        }

        [Test]
        public void EternalEmbers_HasCorrectDescriptionAttribute()
        {
            FieldInfo? fieldInfo = typeof(GameDlc).GetField(nameof(GameDlc.EternalEmbers));
            System.ComponentModel.DescriptionAttribute? attribute = fieldInfo?.GetAttribute<System.ComponentModel.DescriptionAttribute>();

            using (Assert.EnterMultipleScope())
            {
                Assert.That(attribute, Is.Not.Null);
                Assert.That(attribute!.Description, Is.EqualTo("Eternal Embers"));
            }
        }

        [Test]
        public void EternalEmbers_HasCorrectIntegerValue()
        {
            int result = (int)GameDlc.EternalEmbers;

            Assert.That(result, Is.EqualTo(4));
        }
    }
}
