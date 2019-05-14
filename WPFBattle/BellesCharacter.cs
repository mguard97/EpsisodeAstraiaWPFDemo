using Grove.RoleplayingGameInterfaces;
using Grove.RPGCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBattle
{
	public class BellesCharacter : EpisodeAstraiaCharacter
	{

		public BellesCharacter(string name, int health, int sp, CharacterImage characterControl,  IPlayerAgent playerController) : base(name, health, sp, characterControl, playerController)
		{
			this.attackBehavior = new BellesAttack(characterControl);
			this.CharacterClass = "Belles";
			this.characterSkills = new List<ISkill>()
			{
				new HeadBashSkill(characterControl),
				new KnuckleCrunchSkill(characterControl),
				new AssaultSkill(characterControl)
			};
		}
	}
	public class BellesAttack : EpisodeAstraiaPlayerAttack
	{
		public BellesAttack(CharacterImage characterControl) : base(characterControl)
		{
			numDice = 2;
			baseDamage = 45;
		}
	}

	public class HeadBashSkill : SkillBase
	{
		public HeadBashSkill(CharacterImage characterControl) : base("Head Bash", 3, characterControl)
		{
		}

		public override void Attack(ICharacter attacker, ICharacter target)
		{
			base.Attack(attacker, target);
			float damage = 60 + GameConstants.DiceRoll(6, 3);
			target.ReceiveAttack(damage);
			Console.WriteLine(skillName + " does " + damage + " damage to " + target);
		}
	}

	public class KnuckleCrunchSkill : SkillBase
	{
		public KnuckleCrunchSkill( CharacterImage characterControl) : base("Knuckle Crunch", 8, characterControl)
		{
		}

		public override void Attack(ICharacter attacker, ICharacter target)
		{
			base.Attack(attacker, target);
			float damage = 55 * GameConstants.DiceRoll(6, 2);
			target.ReceiveAttack(damage);
			Console.WriteLine("Knuckle Crunch does " + damage +" damage to " + target.Name);

		}
	}

	public class AssaultSkill: SkillBase
	{
		public AssaultSkill(CharacterImage characterControl) : base("Assault", 5, characterControl)
		{

		}

		public override void Attack(ICharacter attacker, ICharacter target)
		{
			base.Attack(attacker, target);
			float recoilDamage = 0;
			for(int i = 0; i < 3; i++)
			{
				float damage = 15 * GameConstants.DiceRoll(6, 2);
				target.ReceiveAttack(damage);
				//damage actaully received or delivered?
				recoilDamage += damage * 0.5f;
			}
			attacker.ReceiveAttack(recoilDamage);
		}
	}
}
