using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using KonyvtarKezelo.Database;

namespace KonyvtarKezelo
{
    public partial class KonyvekForm : Form
    {
        public KonyvekForm()
        {
            InitializeComponent();
            LoadBooks();
        }

        private void LoadBooks()
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT KONYV.id, KONYV.cim, KONYV.mufaj,
                           SZERZO.nev AS szerzo
                    FROM KONYV
                    INNER JOIN SZERZO
                    ON KONYV.szerzo_id = SZERZO.id";

                MySqlDataAdapter adapter =
                    new MySqlDataAdapter(query, conn);

                DataTable table = new DataTable();

                adapter.Fill(table);

                dgvKonyvek.DataSource = table;
            }
        }

        private void btnHozzaad_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO KONYV
                    (szerzo_id, cim, mufaj)
                    VALUES
                    (1, @cim, @mufaj)";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@cim", txtCim.Text);
                cmd.Parameters.AddWithValue("@mufaj", txtMufaj.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Könyv hozzáadva!");

                LoadBooks();
            }
        }

        private void btnFrissit_Click(object sender, EventArgs e)
        {
            LoadBooks();
        }

        private void btnTorol_Click(object sender, EventArgs e)
        {
            if (dgvKonyvek.SelectedRows.Count == 0)
                return;

            int id = Convert.ToInt32(
                dgvKonyvek.SelectedRows[0].Cells["id"].Value);

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string query = "DELETE FROM KONYV WHERE id=@id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Törölve!");

                LoadBooks();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // KonyvekForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "KonyvekForm";
            this.Load += new System.EventHandler(this.KonyvekForm_Load);
            this.ResumeLayout(false);

        }

        private void KonyvekForm_Load(object sender, EventArgs e)
        {

        }
    }
}
