using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicExample
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		SoundPlayer _snd;
		private void button1_Click(object sender, EventArgs e)
		{
			Stream str = Properties.Resources.imperial_march1;
			_snd = new SoundPlayer(str);
			_snd.Play();
		}
	}
}
