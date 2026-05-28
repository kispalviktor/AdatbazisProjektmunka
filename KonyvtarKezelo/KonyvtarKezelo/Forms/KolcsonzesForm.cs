using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using KonyvtarKezelo.Database;

namespace KonyvtarKezelo
{
    public partial class KolcsonzesForm : Form
    {
        public KolcsonzesForm()
        {
            InitializeComponent();

            LoadKolcsonzesek();
            LoadPeldanyok();
        }

        private void LoadKolcsonzesek()
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"
                    SELECT *
                    FROM KOLCSONZES";

                MySqlDataAdapter adapter =
                    new MySqlDataAdapter(query, conn);

                DataTable table = new DataTable();

                adapter.Fill(table);

                dgvKolcsonzes.DataSource = table;
            }
        }

        private void LoadPeldanyok()
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT id FROM PELDANY";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cmbPeldany.Items.Add(reader["id"]);
                }
            }
        }

        private void btnKolcsonoz_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"
                    INSERT INTO KOLCSONZES
                    (peldany_id, olvaso_nev, kolcsonzes_datum)
                    VALUES
                    (@pid, @nev, NOW())";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue(
                    "@pid",
                    cmbPeldany.SelectedItem);

                cmd.Parameters.AddWithValue(
                    "@nev",
                    txtOlvaso.Text);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Kölcsönzés sikeres!");

                LoadKolcsonzesek();
            }
        }

        private void btnVisszahoz_Click(object sender, EventArgs e)
        {
            if (dgvKolcsonzes.SelectedRows.Count == 0)
                return;

            int id = Convert.ToInt32(
                dgvKolcsonzes.SelectedRows[0].Cells["id"].Value);

            using (MySqlConnection conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"
                    UPDATE KOLCSONZES
                    SET visszahozas_datum = NOW()
                    WHERE id=@id";

                MySqlCommand cmd =
                    new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Könyv visszahozva!");

                LoadKolcsonzesek();
            }
        }
    }
}
