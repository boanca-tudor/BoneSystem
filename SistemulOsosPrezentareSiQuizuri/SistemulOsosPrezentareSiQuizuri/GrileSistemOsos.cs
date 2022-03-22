using System;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace SistemulOsosPrezentareSiQuizuri
{
    public partial class GrileSistemOsos : Form
    {
        private DatabaseOperator dbOperator;
        Random rng = null;

        bool[] intrebareSelectata;
        string raspunsCorect;
        int punctaj;
        int nrIntrebari;

        public GrileSistemOsos()
        {
            InitializeComponent();
            SetupWindow();
            SetupLabelFont();

            QuizVisible(false);
            ConfiguratorEnabled(true);
        }

        private void SetupLabelFont()
        {
            lblIntrebare.Font = new Font(lblIntrebare.Font.Name, 16F);
            lblIntrebare.Font = new Font(lblIntrebare.Font, lblIntrebare.Font.Style | FontStyle.Bold | FontStyle.Italic);
        }

        private void SetupWindow()
        {
            Icon = new Icon(@"../../../../Resurse/AppIcon.ico");

            dbOperator = new DatabaseOperator(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\IntrebariQuiz.mdf;Integrated Security=True");

            dbOperator.FillDatabase("../../../../Resurse/IntrebariSiRaspunsuri.txt");

            rng = new Random();
            intrebareSelectata = new bool[20];
        }

        private void QuizVisible(bool visible)
        {
            rbRasp1.Visible = visible;
            rbRasp2.Visible = visible;
            rbRasp3.Visible = visible;
            rbRasp4.Visible = visible;
            lblIntrebare.Visible = visible;
            btnFinal.Visible = visible;
        }

        private void ConfiguratorEnabled(bool enabled)
        {
            numIntrebari.Enabled = enabled;
            btnGenerate.Enabled = enabled;
        }

        private void ButtonsEnabled(bool enabled)
        {
            rbRasp1.Enabled = enabled;
            rbRasp2.Enabled = enabled;
            rbRasp3.Enabled = enabled;
            rbRasp4.Enabled = enabled;
            btnFinal.Enabled = enabled;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ConfiguratorEnabled(false);
            QuizVisible(true);

            StartQuiz((int)numIntrebari.Value);
        }

        private void StartQuiz(int numQuestions)
        {
            ResetQuestionArray();
            GenerateNewItem(GenerateID());
            punctaj = 0;
            nrIntrebari = 1;
        }

        private void GenerateNewItem(int id)
        {
            lblIntrebare.Text = dbOperator.GetValue("Intrebare", "ResurseGrile", id);
            rbRasp1.Text = dbOperator.GetValue("Raspuns1", "ResurseGrile", id);
            rbRasp2.Text = dbOperator.GetValue("Raspuns2", "ResurseGrile", id);
            rbRasp3.Text = dbOperator.GetValue("Raspuns3", "ResurseGrile", id);
            rbRasp4.Text = dbOperator.GetValue("Raspuns4", "ResurseGrile", id);
            raspunsCorect = dbOperator.GetValue("RaspunsCorect", "ResurseGrile", id);
        }

        private int GenerateID()
        {
            int num = rng.Next(1, 20);
            if (intrebareSelectata[num - 1])
            {
                while (intrebareSelectata[num - 1])
                {
                    num = rng.Next(1, 20);
                }
            }
            else intrebareSelectata[num - 1] = true;

            return num;
        }

        private void ResetQuestionArray()
        {
            for (int i = 0; i != intrebareSelectata.Length; ++i)
                intrebareSelectata[i] = false;
        }

        private void btnFinal_Click(object sender, EventArgs e)
        {
            var rb = FindCheckedButton();
            if (rb == null) MessageBox.Show("Alegeti un raspuns!");
            else
            {
                if (rb.Text == raspunsCorect)
                    punctaj++;
                ApplyAnswerColor();
                ButtonsEnabled(false);
                Thread.Sleep(2000);
                ResetState();
                nrIntrebari++;

                if (nrIntrebari <= (int)numIntrebari.Value) GenerateNewItem(GenerateID());
                else
                {
                    MessageBox.Show("Ati obtinut " + punctaj + " / " + numIntrebari.Value.ToString() + " puncte");
                    ConfiguratorEnabled(true);
                    QuizVisible(false);
                }
            }
        }

        private RadioButton FindCheckedButton()
        {
            foreach(var control in tableLayoutPanel1.Controls)
            {
                RadioButton rb = control as RadioButton;
                if (rb != null && rb.Checked)
                {
                    return rb;
                }
            }

            return null;
        }

        private void ApplyAnswerColor()
        {
            foreach(var control in tableLayoutPanel1.Controls)
            {
                RadioButton rb = control as RadioButton;
                if (rb != null)
                {
                    if (rb.Text == raspunsCorect) rb.BackColor = Color.Green;
                    else rb.BackColor = Color.Red;
                }
            }
        }

        private void ResetState()
        {
            foreach (var control in tableLayoutPanel1.Controls)
            {
                RadioButton rb = control as RadioButton;
                if (rb != null)
                {
                    rb.BackColor = default(Color);
                    rb.Checked = false;
                    rb.Enabled = true;
                }
            }
            btnFinal.Enabled = true;
        }

        private void GrileSistemOsos_Load(object sender, EventArgs e)
        {

        }
    }
}
