using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemulOsosPrezentareSiQuizuri
{
    public partial class FereastraInformare : Form
    {
        public FereastraInformare()
        {
            InitializeComponent();

            Icon = new Icon(@"../../../../Resurse/AppIcon.ico");
            InitPictureBoxes();
        }

        private void InitPictureBoxes()
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = Image.FromFile(@"../../../../Resurse/FotoMaterial1.jpg");
            pictureBox2.Image = Image.FromFile(@"../../../../Resurse/FotoMaterial2.jpg");
            pictureBox3.Image = Image.FromFile(@"../../../../Resurse/FotoMaterial3.jpg");
        }

        private void btnMaterial1_Click(object sender, EventArgs e)
        {
            MediaPlayer playerForm = new MediaPlayer(@"Resurse\Video1.mp4");
            playerForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MediaPlayer playerForm = new MediaPlayer(@"Resurse\Video2.mp4");
            playerForm.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MediaPlayer playerForm = new MediaPlayer(@"Resurse\Video3.mp4");
            playerForm.ShowDialog();
        }
    }
}
