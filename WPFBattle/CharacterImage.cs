using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace WPFBattle
{
	public enum CharacterState
	{
		Attacking,
		Defending,
		Idle,
		Dead
	}
	public class CharacterImage: System.Windows.Controls.Image
	{
		private CharacterState characterState;
		public CharacterState CharacterState
		{
			get { return characterState; }
			set
			{
				characterState = value;
				this.Dispatcher.Invoke((Action)(() =>
				{
					UpdateImageSource();
				}));
			}

		}

		public ImageSource IdleImageSource { get; set; }
		public ImageSource AttackingImageSource { get; set; }
		public ImageSource TakeDamageImageSource { get; set; }
		public ImageSource DeadImageSource { get; set; }

		protected void UpdateImageSource()
		{
			switch (CharacterState)
			{
				case CharacterState.Attacking:
					this.Source = AttackingImageSource;
					break;
				case CharacterState.Defending:
					this.Source = TakeDamageImageSource;
					break;
				case CharacterState.Dead:
					this.Source = DeadImageSource;
					break;
				case CharacterState.Idle:
				default:
					this.Source = IdleImageSource;
					break;
			}
		}

		protected override void OnRender(DrawingContext dc)
		{
			UpdateImageSource();
			base.OnRender(dc);
		}
	}
}
