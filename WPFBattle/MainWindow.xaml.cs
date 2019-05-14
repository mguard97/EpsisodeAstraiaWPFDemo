using Grove.RoleplayingGameInterfaces;
using Grove.RPGCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFBattle
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//TextBoxStreamWriter consoleWriter;
		CombatThread combatThread;
		static ICombat encounter;
		public MainWindow()
		{
			InitializeComponent();
			
			//consoleWriter = new TextBoxStreamWriter(output);
			//Console.SetOut(consoleWriter);

			IList<ICharacter> playerParty = new List<ICharacter>()
			{
				new BellesCharacter("Belles", 250, 8, imgPlayer1,  GameConstants.Instance.PlayerController),
				new AstraiaCharacter("Astraia", 190,8, imgPlayer2,   GameConstants.Instance.PlayerController),
				new TalosCharacter("Talos", 210, 8, imgPlayer3,   GameConstants.Instance.PlayerController)
			};

			IList<ICharacter> enemyParty = new List<ICharacter>()
			{
				new EpisodeAstraiaEnemyCharacter("Fire Squirrel A", 200, imgEnemy1, "FSA", 2,15,  GameConstants.Instance.GameMasterController),
				new EpisodeAstraiaEnemyCharacter("Fire Squirrel B", 400, imgEnemy2, "FSB", 2,20,  GameConstants.Instance.GameMasterController),
				new EpisodeAstraiaEnemyCharacter("Scorpius",800,imgEnemy3, "SCP", 1, 45,  GameConstants.Instance.GameMasterController),
				new EpisodeAstraiaEnemyCharacter("Blixen", 500, imgEnemy4, "BLX", 1, 35,  GameConstants.Instance.GameMasterController)
			};
			encounter = new Combat(playerParty, enemyParty, "Players", "Enemies");
			GameConstants.Instance.Encounter = encounter;
			combatThread = new CombatThread(encounter);
			combatThread.Start();

		}

		
	}
}
