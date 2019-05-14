using Grove.RoleplayingGameInterfaces;
using Grove.RPGCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFBattle
{
	public class EpisodeAstraiaCharacter:CharacterBase
	{
		private CharacterImage characterControl;
		protected int startingHealth = 0;
		protected int startingSP = 0;


		protected IList<ISkill> characterSkills = new List<ISkill>();
		public override IList<ISkill> Skills => characterSkills;

		protected IList<IAbility> characterAbilities = new List<IAbility>();
		public override IList<IAbility> Abilities => characterAbilities;
		protected IPlayerAgent controllingPlayerAgent;

		public EpisodeAstraiaCharacter(string name, int health, CharacterImage characterControl)
		{
			this.Name = name;
			this.Health = health;
			this.startingHealth = health;
			this.characterControl = characterControl;
		}
		public EpisodeAstraiaCharacter(string name, int health, CharacterImage characterControl,   IPlayerAgent playerAgent):this(name,health,characterControl)
		{
			this.controllingPlayerAgent = playerAgent;
		}
		public EpisodeAstraiaCharacter(string name, int health, int sp, CharacterImage characterControl, IPlayerAgent playerAgent) : this(name, health, characterControl, playerAgent)
		{
			this.SP = sp;
			this.startingSP = sp;
		}

		public EpisodeAstraiaCharacter(string name, int maxHealth, int maxSP, int baseAttack, int numDiceOnBaseAttack, IList<ISkill> skills, IList<IAbility> abilities, CharacterImage characterControl, IPlayerAgent playerAgent)
		{
			Name = name;
			this.characterControl = characterControl;
		}

		public override void ReceiveAttack(float damage)
		{
			characterControl.CharacterState = CharacterState.Defending;
			Thread.Sleep(500);
			base.ReceiveAttack(damage);
			if (Health <= 0)
			{
				characterControl.CharacterState = CharacterState.Dead;
			}
			else
			{
				characterControl.CharacterState = CharacterState.Idle;
			}
		}
		public override void PerformAttack(ICharacter target)
		{
			base.PerformAttack(target);
		}

		public override void PerformSkill(ICharacter target, ISkill skill)
		{
			SP -= skill.SPCost;
			skill.Attack(this, target);
		}

		public override void PerformAbility(ICharacter target, IAbility ability)
		{
			ability.Attack(this, target);
		}
		public override void MakeChoice()
		{
			base.MakeChoice();
			controllingPlayerAgent.MakeChoice(this);
		}

		public override void RestoreHealth()
		{
			this.Health = startingHealth;
			this.SP = startingSP;
		}

		public override void RestoreSP()
		{
			throw new NotImplementedException();
		}
	}

	public class EpisodeAstraiaAttack : NormalAttack
	{
		protected CharacterImage characterControl;
		protected int numDice = 2;
		protected int baseDamage = 25;
		public EpisodeAstraiaAttack(CharacterImage characterControl)
		{
			this.characterControl = characterControl;
		}

		public EpisodeAstraiaAttack(int numDice, int baseDamage,CharacterImage characterControl)
		{
			this.characterControl = characterControl;
			this.numDice = numDice;
			this.baseDamage = baseDamage;
		}

		public override void Attack(ICharacter attacker, ICharacter target)
		{
			characterControl.CharacterState = CharacterState.Attacking;
			Console.WriteLine(attacker.Name + " attacks " + target.Name);
			Thread.Sleep(500);
			float damage = GameConstants.CalculateDamage(numDice, baseDamage);
			target.ReceiveAttack(damage);
			characterControl.CharacterState = CharacterState.Idle;
		}
	}

	public class EpisodeAstraiaPlayerAttack : EpisodeAstraiaAttack
	{
		public EpisodeAstraiaPlayerAttack(CharacterImage characterControl):base(characterControl)
		{
			
		}
		public override void Attack(ICharacter attacker, ICharacter target)
		{
			base.Attack(attacker, target);
			GameConstants.Instance.IncrementAbilityPointPool(1);
		}
	}
}
