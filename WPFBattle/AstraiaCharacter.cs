using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grove.RoleplayingGameInterfaces;
using Grove.RPGCore;

namespace WPFBattle
{
	public class AstraiaCharacter:EpisodeAstraiaCharacter
	{
		public AstraiaCharacter(string name, int health, int sp, CharacterImage characterControl, IPlayerAgent playerController) : base(name, health, sp, characterControl, playerController)
		{
			this.attackBehavior = new AstraiaAttack(characterControl);
			this.CharacterClass = "Astraia";
			this.characterSkills = new List<ISkill>()
			{
				new HammerBashSkill(characterControl),
				new BoomerangSkill(characterControl)
			};
		}
	}

	public class AstraiaAttack : EpisodeAstraiaPlayerAttack
	{
		public AstraiaAttack(CharacterImage characterControl) : base(characterControl)
		{
			numDice = 2;
			baseDamage = 25;
		}
	}

	public class HammerBashSkill : SkillBase
	{
		public HammerBashSkill(CharacterImage characterControl) : base("Hammer Bash", 3, characterControl)
		{
			
		}

		public override void Attack(ICharacter attacker, ICharacter target)
		{
			base.Attack(attacker, target);
			float damage = 40 * GameConstants.DiceRoll(6);
			target.ReceiveAttack(damage);
			Console.WriteLine(skillName + " does " + damage + " damage to " + target.Name);
		}
	}

	public class BoomerangSkill : SkillBase
	{
		public BoomerangSkill(CharacterImage characterControl): base("Boomerang",5, characterControl)
		{

		}
		public override void Attack(ICharacter attacker, ICharacter target)
		{
			base.Attack(attacker, target);
			float damage = 15 * GameConstants.DiceRoll(6);
			target.ReceiveAttack(damage);
			Console.WriteLine(skillName + " does " + damage + " damage to " + target.Name);
			damage = 15 * GameConstants.DiceRoll(6);
			target.ReceiveAttack(damage);
			Console.WriteLine(skillName + " does " + damage + " damage to " + target.Name);
		}
	}

}
