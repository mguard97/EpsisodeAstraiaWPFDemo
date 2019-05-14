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

	public class SkillBase : EpisodeAstraiaAttack, ISkill
	{
		protected string skillName = "Basic Skill";
		public string Name => skillName;
		protected int spRequirement= 5;

		public SkillBase(string skillName, int spRequirement, CharacterImage characterControl):base(characterControl)
		{
			this.skillName = skillName;
			this.spRequirement = spRequirement;
		}

		public int SPCost
		{
			get{ return spRequirement; }
		}

		public override void Attack(ICharacter attacker, ICharacter target)
		{
			characterControl.CharacterState = CharacterState.Attacking;
			Thread.Sleep(500);
			Console.WriteLine(attacker.Name + " performs " + skillName + " on " + target.Name);
			// skills effect goes here
			characterControl.CharacterState = CharacterState.Idle;

		}

		public override string ToString()
		{
			return String.Format("{0} - {1} AP",skillName,spRequirement);
		}
	}

}
