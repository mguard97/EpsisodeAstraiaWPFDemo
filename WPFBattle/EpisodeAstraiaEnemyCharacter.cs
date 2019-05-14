using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grove.RoleplayingGameInterfaces;

namespace WPFBattle
{
	public class EpisodeAstraiaEnemyCharacter : EpisodeAstraiaCharacter
	{
		public EpisodeAstraiaEnemyCharacter(string name, int health, CharacterImage characterControl,   IPlayerAgent playerAgent) : base(name, health, characterControl,  playerAgent)
		{
		}

		public EpisodeAstraiaEnemyCharacter(string name, int health, CharacterImage characterControl, string className, int numDice, int baseAttack) : base(name, health, characterControl)
		{
			this.CharacterClass = className;
			this.attackBehavior = new EpisodeAstraiaEnemyAttack(characterControl, numDice, baseAttack);
		}

		public EpisodeAstraiaEnemyCharacter(string name, int health, CharacterImage characterControl, string className, int numDice, int baseAttack,  IPlayerAgent gmController) : this(name, health, characterControl, className, numDice, baseAttack)
		{
			this.controllingPlayerAgent = gmController;
		}
	}

	public class EpisodeAstraiaEnemyAttack : EpisodeAstraiaAttack
	{
		public EpisodeAstraiaEnemyAttack(CharacterImage characterControl) : base(characterControl)
		{
		}
		public EpisodeAstraiaEnemyAttack(CharacterImage characterControl, int numDice, int baseAttack) : this(characterControl)
		{
			this.numDice = numDice;
			this.baseDamage = baseAttack;
		}
		public override void Attack(ICharacter attacker, ICharacter target)
		{
			base.Attack(attacker, target);
		}
		
	}
}
