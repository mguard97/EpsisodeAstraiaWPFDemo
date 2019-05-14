using Grove.RoleplayingGameInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WPFBattle
{
	public class CombatThread
	{
		private Thread thread;
		private ICombat encounter;

		public CombatThread(ICombat encounter)
		{
			this.encounter = encounter;
		}
		public void Start()
		{
			thread = new System.Threading.Thread(() => //give this code
			{
				encounter.AutoBattle();
			});
			thread.Name = "GameThread";
			thread.Start();
		}

	}
}
