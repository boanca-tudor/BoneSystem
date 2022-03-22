using System;
using System.Drawing;
using System.Windows.Forms;

namespace SistemulOsosPrezentareSiQuizuri
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            pictureBox1.Image = Image.FromFile(@"../../../../Resurse/scheletul.jpg");
            Icon = new Icon(@"../../../../Resurse/AppIcon.ico");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GrileSistemOsos formaGrile = new GrileSistemOsos();
            formaGrile.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FereastraInformare formaInformare = new FereastraInformare();
            formaInformare.Show();
        }
    }
}
