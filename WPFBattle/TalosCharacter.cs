using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grove.RoleplayingGameInterfaces;

namespace WPFBattle
{
	public class TalosCharacter : EpisodeAstraiaCharacter
	{
		public TalosCharacter(string name, int health, int sp, CharacterImage characterControl, IPlayerAgent playerController) : base(name, health,sp ,characterControl, playerController)
		{

			this.attackBehavior = new TalosAttack(characterControl);
			this.CharacterClass = "Talos";
			this.characterSkills = new List<ISkill>()
			{
				new AbsorbSkill(characterControl),
				new ClayRushSkill(characterControl),
				new GiantsFistSkill(characterControl)
			};
		}
	}

	public class TalosAttack : EpisodeAstraiaPlayerAttack
	{
		public TalosAttack(CharacterImage characterControl) : base(characterControl)
		{
			numDice = 2;
			baseDamage = 35;
		}
	}

	public class AbsorbSkill : SkillBase
	{
		public AbsorbSkill(CharacterImage characterControl) : base("Absorb", 5, characterControl)
		{
		}
	}
	public class ClayRushSkill : SkillBase
	{
		public ClayRushSkill(CharacterImage characterControl) : base("Clay Rush", 6, characterControl)
		{
		}
	}
	public class GiantsFistSkill : SkillBase
	{
		public GiantsFistSkill(CharacterImage characterControl):base("Giant's Fist", 7, characterControl)
		{

		}

	}

}
