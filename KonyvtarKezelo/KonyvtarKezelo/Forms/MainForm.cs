using System;
using System.Windows.Forms;

namespace KonyvtarKezelo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnKonyvek_Click(object sender, EventArgs e)
        {
            KonyvekForm form = new KonyvekForm();
            form.ShowDialog();
        }

        private void btnKolcsonzes_Click(object sender, EventArgs e)
        {
            KolcsonzesForm form = new KolcsonzesForm();
            form.ShowDialog();
        }

        private void btnKilepes_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(545, 398);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}