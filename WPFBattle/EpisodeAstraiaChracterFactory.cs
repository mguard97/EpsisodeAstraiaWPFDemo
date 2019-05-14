using Grove.RoleplayingGameInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBattle
{
	public class EpisodeAstraiaCharacterFactory
	{
		//singleton implementation
		private static EpisodeAstraiaCharacterFactory instance = new EpisodeAstraiaCharacterFactory();
		public static EpisodeAstraiaCharacterFactory Instance
		{
			get => instance;
		}

		private EpisodeAstraiaCharacterFactory()
		{
			PopulateCharacterInfo();
		}

		private IDictionary<string, CharacterDefinitionProperties> characterDefinitions;
		//Character Definitions include:
		// Name
		// Max Health
		// Max SP
		// Skills
		// Abilities
		// Base Attack
		// Number of Dice


		
		public void PopulateCharacterInfo()
		{
			characterDefinitions.Add("Astraia", new CharacterDefinitionProperties("Astraia",200,8,25,2,new List<Type>() { typeof(HammerBashSkill), typeof(BoomerangSkill) }, new List<Type>()));
			characterDefinitions.Add("Belles", new CharacterDefinitionProperties("Belles", 250, 8, 45, 2, new List<Type>() { typeof(HeadBashSkill), typeof(KnuckleCrunchSkill), typeof(AssaultSkill) }, new List<Type>()));
			characterDefinitions.Add("Talos", new CharacterDefinitionProperties("Talos", 210, 8, 35, 2, new List<Type>() { typeof(AbsorbSkill), typeof(ClayRushSkill), typeof(GiantsFistSkill) }, new List<Type>()));

			characterDefinitions.Add("Fire Squirrel A", new CharacterDefinitionProperties("Fire Squirrel A", 200, 8, 15, 2, new List<Type>(), new List<Type>()));
			characterDefinitions.Add("Fire Squirrel B", new CharacterDefinitionProperties("Fire Squirrel B", 400, 8, 20, 2, new List<Type>(), new List<Type>()));
			characterDefinitions.Add("Blixen", new CharacterDefinitionProperties("Blixen", 500, 8, 35, 1, new List<Type>(), new List<Type>()));
			characterDefinitions.Add("Scorpius", new CharacterDefinitionProperties("Scorpius", 800, 8, 45, 1, new List<Type>(), new List<Type>()));
		}

		public ICharacter InstantiateCharacter(string defID,CharacterImage characterControl, IPlayerAgent playerAgent)
		{
			Debug.Assert(characterDefinitions.ContainsKey(defID),"Character does not exist in definitions");
			CharacterDefinitionProperties charLookup = characterDefinitions[defID];
			IList<ISkill> skills = new List<ISkill>();
			IList<IAbility> abilities = new List<IAbility>();
			foreach(Type T in charLookup.skills)
			{
				skills.Add((ISkill)T.GetConstructor(new Type[] { typeof(CharacterImage) }).Invoke(new object[] { characterControl }));
			}
			foreach(Type T in charLookup.abilities)
			{
				abilities.Add((IAbility)T.GetConstructor(new Type[] { typeof(CharacterImage) }).Invoke(new object[] { characterControl }));
			}
			return InstantiateCharacterExplicit(charLookup.name, charLookup.maxHealth, charLookup.maxSP, charLookup.baseAttack, charLookup.numDiceOnBaseAttack, skills, abilities, characterControl, playerAgent);
		}

		public ICharacter InstantiateCharacterExplicit(string name, int maxHealth, int maxSP, int baseAttack, int numDiceOnBaseAttack, IList<ISkill> skills, IList<IAbility> abilities, CharacterImage characterControl, IPlayerAgent playerAgent)
		{
			return new EpisodeAstraiaCharacter(name, maxHealth, maxSP, baseAttack, numDiceOnBaseAttack, skills, abilities, characterControl, playerAgent);
		}

		private struct CharacterDefinitionProperties
		{
			public string name;
			public int maxHealth;
			public int maxSP;
			public int baseAttack;
			public int numDiceOnBaseAttack;
			public IList<Type> skills;
			public IList<Type> abilities;

			public CharacterDefinitionProperties(string name, int maxHealth, int maxSP, int baseAttack, int numDiceOnBaseAttack, IList<Type> skills, IList<Type> abilities)
			{
				this.name = name;
				this.maxHealth = maxHealth;
				this.maxSP = maxSP;
				this.baseAttack = baseAttack;
				this.numDiceOnBaseAttack = numDiceOnBaseAttack;
				this.skills = skills;
				this.abilities = abilities;
			}
		}
	}
}
