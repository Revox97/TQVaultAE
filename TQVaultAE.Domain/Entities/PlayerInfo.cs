using System.IO;

namespace TQVaultAE.Domain.Entities
{
	/// <summary>
	/// Character data is saved here after reading through player.chr file
	/// </summary>
	public class PlayerInfo
	{
		private const StringComparison NoCase = StringComparison.OrdinalIgnoreCase;

		public bool MustResetMasteries => MasteriesAllowedOldValue.HasValue && MasteriesAllowed < MasteriesAllowedOldValue || MasteriesResetRequired;

		public bool MasteriesResetRequired { get; set; }

		/// <summary>
		/// Skills removed during <see cref="ResetMasteries"/>
		/// </summary>
		private readonly List<SkillRecord> _skillRecordListRemoved = [];

		/// <summary>
		/// List of Skills embded in the save file
		/// </summary>
		public readonly List<SkillRecord> SkillRecordList = [];

		// TODO fix enum stuff
		public bool MasteryDefensiveEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Defensive.AsString(EnumFormat.Description), NoCase));
		public bool MasteryStormEnabled => true; // SkillRecordList.Any(s => s.skillName.Equals(Masteries.Storm.AsString(EnumFormat.Description), NoCase));
		public bool MasteryEarthEnabled => true; // SkillRecordList.Any(s => s.skillName.Equals(Masteries.Earth.AsString(EnumFormat.Description), NoCase));
		public bool MasteryNatureEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Nature.AsString(EnumFormat.Description), NoCase));
		public bool MasteryDreamEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Dream.AsString(EnumFormat.Description), NoCase));
		public bool MasteryRuneEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Rune.AsString(EnumFormat.Description), NoCase));
		public bool MasteryWarfareEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Warfare.AsString(EnumFormat.Description), NoCase));
		public bool MasteryHuntingEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Hunting.AsString(EnumFormat.Description), NoCase));
		public bool MasteryStealthEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Stealth.AsString(EnumFormat.Description), NoCase));
		public bool MasterySpiritEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Spirit.AsString(EnumFormat.Description), NoCase));
		public bool MasteryNeidanEnabled => true; //  SkillRecordList.Any(s => s.skillName.Equals(Masteries.Neidan.AsString(EnumFormat.Description), NoCase));

		public string[] ActiveMasteriesRecordNames
		{
			get
			{
				string[] ret = [];

				if (ActiveMasteries.HasValue)
				{
					var actives = ActiveMasteries.Value;
					// TODO find new way
					//ret = Enums.GetValues<Masteries>().Where(v => actives.HasFlag(v)).Select(s => s.AsString(EnumFormat.Description)).ToArray();
				}

				return ret;
			}
		}

		public Masteries? ActiveMasteries
		{
			get
			{
				Masteries? val = null;
				int m = 0;
				m = MasteryDefensiveEnabled ? m | (int)Masteries.Defensive : m;
				m = MasteryStormEnabled ? m | (int)Masteries.Storm : m;
				m = MasteryEarthEnabled ? m | (int)Masteries.Earth : m;
				m = MasteryNatureEnabled ? m | (int)Masteries.Nature : m;
				m = MasteryDreamEnabled ? m | (int)Masteries.Dream : m;
				m = MasteryRuneEnabled ? m | (int)Masteries.Rune : m;
				m = MasteryWarfareEnabled ? m | (int)Masteries.Warfare : m;
				m = MasteryHuntingEnabled ? m | (int)Masteries.Hunting : m;
				m = MasteryStealthEnabled ? m | (int)Masteries.Stealth : m;
				m = MasterySpiritEnabled ? m | (int)Masteries.Spirit : m;
				m = MasteryNeidanEnabled ? m | (int)Masteries.Neidan : m;

				return m > 0 ? (Masteries)m : val;
			}
		}

		/// <summary>
		/// Gets or sets the PlayerStatBonus which contains the cululative stat bonus for the character's skills.
		/// </summary>
		public PlayerStatBonus SkillBonus { get; set; }

		/// <summary>
		/// Gets or sets the PlayerStatBonus which contains the cululative stat bonus for the character's equipped gear.
		/// </summary>
		public PlayerStatBonus GearBonus { get; set; }

		/// <summary>
		/// Released skill points from masteries reset
		/// </summary>
		public int ReleasedSkillPoints => _skillRecordListRemoved?.Sum(s => s.SkillLevel) ?? 0;

		/// <summary>
		/// Reset masteries if any
		/// </summary>
		/// <returns></returns>
		public bool ResetMasteries()
		{
			bool isActive = ActiveMasteries.HasValue;

			if (isActive)
			{
				foreach (string? mastery in ActiveMasteriesRecordNames)
				{
					string? recBase = Path.GetDirectoryName(mastery);

					if (recBase is null)
						continue;

					// Remove all skills having the same base skill line (ex :  storm, earth, etc...)
					_skillRecordListRemoved.AddRange(SkillRecordList.Where(sk => sk.SkillName.StartsWith(recBase, NoCase)));
				}

				SkillRecordList.RemoveAll(_skillRecordListRemoved.Contains);
				Modified = true;
			}

			return isActive;
		}

		/// <summary>
		/// Character current Level
		/// </summary>
		public int CurrentLevel { get; set; }

		/// <summary>
		/// Characters current XP
		/// </summary>
		public int CurrentXP { get; set; }

		/// <summary>
		/// Available Skill points
		/// </summary>
		public int SkillPoints { get; set; }

		/// <summary>
		/// Available Attribute points
		/// </summary>
		public int AttributesPoints { get; set; }

		/// <summary>
		/// Base Strength
		/// </summary>
		public int BaseStrength { get; set; }

		/// <summary>
		/// Character Strength with included bonuses and modifiers from gear and skills.
		/// </summary>
		public int CalculatedStrength => Convert.ToInt32(((float)((BaseStrength + (SkillBonus?.StrengthBonus ?? 0) + (GearBonus?.StrengthBonus ?? 0))) *
					(100.0f + ((SkillBonus?.StrengthModifier ?? 0) + (GearBonus?.StrengthModifier ?? 0)))) / 100.0f);

		/// <summary>
		/// Base Dexterity
		/// </summary>
		public int BaseDexterity { get; set; }

		/// <summary>
		/// Character Dexterity with included bonuses and modifiers from gear and skills.
		/// </summary>
		public int CalculatedDexterity => Convert.ToInt32(((float)((BaseDexterity + (SkillBonus?.DexterityBonus ?? 0) + (GearBonus?.DexterityBonus ?? 0))) *
					(100.0f + ((SkillBonus?.DexterityModifier ?? 0) + (GearBonus?.DexterityModifier ?? 0)))) / 100.0f);

		/// <summary>
		/// Base Intelligence
		/// </summary>
		public int BaseIntelligence { get; set; }

		/// <summary>
		/// Character Intelligence with included bonuses and modifiers from gear and skills.
		/// </summary>
		public int CalculatedIntelligence => Convert.ToInt32(((float)((BaseIntelligence + (SkillBonus?.IntelligenceBonus ?? 0) + (GearBonus?.IntelligenceBonus ?? 0))) *
					(100.0f + ((SkillBonus?.IntelligenceModifier ?? 0) + (GearBonus?.IntelligenceModifier ?? 0)))) / 100.0f);

		/// <summary>
		/// Base Health
		/// </summary>
		public int BaseHealth { get; set; }

		/// <summary>
		/// Character Health with included bonuses and modifiers from gear and skills.
		/// </summary>
		public int CalculatedHealth => Convert.ToInt32(((float)((BaseHealth + (SkillBonus?.HealthBonus ?? 0) + (GearBonus?.HealthBonus ?? 0))) *
					(100.0f + ((SkillBonus?.HealthModifier ?? 0) + (GearBonus?.HealthModifier ?? 0)))) / 100.0f);

		/// <summary>
		/// Base Mana
		/// </summary>
		public int BaseEnergy { get; set; }

		/// <summary>
		/// Character Mana with included bonuses and modifiers from gear and skills.
		/// </summary>
		public int CalculatedMana => Convert.ToInt32(((float)((BaseEnergy + (SkillBonus?.EnergyBonus ?? 0) + (GearBonus?.EnergyBonus ?? 0))) *
					(100.0f + ((SkillBonus?.ManaModifier ?? 0) + (GearBonus?.ManaModifier ?? 0)))) / 100.0f);

		/// <summary>
		/// Return skills having the same skill line base.
		/// </summary>
		/// <param name="dbr"></param>
		/// <returns></returns>
		public IEnumerable<SkillRecord> GetSkillsByBaseRecordName(RecordId dbr)
		{
			string? baseRec = Path.GetDirectoryName(dbr.Normalized);

			foreach (SkillRecord sk in SkillRecordList)
			{
				if (baseRec is not null && sk.SkillName.StartsWith(baseRec, NoCase))
					yield return sk;
			}
		}

		/// <summary>
		/// Total time played in seconds
		/// </summary>
		public int PlayTimeInSeconds { get; set; }


		/// <summary>
		/// Number of death
		/// </summary>
		public int NumberOfDeaths { get; set; }


		/// <summary>
		/// Number of Kills
		/// </summary>
		public int NumberOfKills { get; set; }

		/// <summary>
		/// Experience gained from kills
		/// </summary>
		public int ExperienceFromKills { get; set; }

		/// <summary>
		/// Health Postions Used
		/// </summary>
		public int HealthPotionsUsed { get; set; }

		/// <summary>
		/// Mana potions used
		/// </summary>
		public int ManaPotionsUsed { get; set; }

		public int MaxLevel { get; set; }


		/// <summary>
		/// Number of times hit
		/// </summary>
		public int NumHitsReceived { get; set; }

		/// <summary>
		/// Number of Hits Inflicted
		/// </summary>
		public int NumHitsInflicted { get; set; }

		/// <summary>
		/// Greatest Damage
		/// </summary>
		public int GreatestDamageInflicted { get; set; }

		/// <summary>
		/// Greatest Monster killed
		/// </summary>
		public string GreatestMonster { get; set; }

		/// <summary>
		/// Critical Hits Inflicted
		/// </summary>
		public int CriticalHitsInflicted { get; set; }

		/// <summary>
		/// Critical Hits Recieved
		/// </summary>
		public int CriticalHitsReceived { get; set; }

		/// <summary>
		/// Character Class
		/// </summary>
		public string Class { get; set; }

		/// <summary>
		/// Character difficulty unlocked
		/// </summary>
		public int DifficultyUnlocked { get; set; }

		/// <summary>
		/// Has used player in game one or more times
		/// </summary>
		public int HasBeenInGame { get; set; }

		/// <summary>
		/// set to true if player information is edited
		/// </summary>
		public bool Modified { get; set; }

		/// <summary>
		/// Players Money
		/// </summary>
		public int Money { get; set; }

		private int _masteriesAllowed = 0;
		/// <summary>
		/// Nbr of masteries unlocked
		/// </summary>
		public int MasteriesAllowed
		{
			get => _masteriesAllowed;
			set
			{
				if (!MasteriesAllowedOldValue.HasValue) 
					MasteriesAllowedOldValue = value;

				_masteriesAllowed = value;
			}
		}

		/// <summary>
		/// First set value of <see cref="MasteriesAllowed"/> 
		/// </summary>
		public int? MasteriesAllowedOldValue { get; private set; }

		/// <summary>
		/// TQ, TQIT, TQITAE
		/// </summary>
		public PlayerFileHeaderVersion HeaderVersion { get; set; }

		/// <summary>
		/// Class name
		/// </summary>
		public string PlayerCharacterClass { get; set; }

		public int GreatestMonsterKilledLevel { get; set; }

		public int GreatestMonsterKilledLifeAndMana { get; set; }
	}
}
