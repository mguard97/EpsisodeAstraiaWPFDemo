using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBattle
{
	class TextBoxStreamWriter : System.IO.TextWriter
	{
		System.Windows.Controls.TextBox output;

		public TextBoxStreamWriter(System.Windows.Controls.TextBox textBox)
		{
			this.output = textBox;
		}
		public override Encoding Encoding
		{
			get { return System.Text.Encoding.UTF8; }
		}

		public override void Write(char value)
		{
			base.Write(value);
			output.Dispatcher.BeginInvoke(new Action(() =>
			{
				output.AppendText(value.ToString());
			})
			); // When character data is written, append it to the text box.
		}

	}
}
