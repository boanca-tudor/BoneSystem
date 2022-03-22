using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace SistemulOsosPrezentareSiQuizuri
{
    public partial class MediaPlayer : Form
    {
        public MediaPlayer()
        {
            InitializeComponent();
        }

        public MediaPlayer(string fileName)
        {
            InitializeComponent();
            Icon = new Icon(@"../../../../Resurse/AppIcon.ico");

            string dirPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName;
            var filePath = Path.Combine(dirPath, fileName);

            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.URL = filePath;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            axWindowsMediaPlayer1.close();
            base.OnFormClosing(e);
        }
    }
}
