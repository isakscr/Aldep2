using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aldep2
{
    public partial class depolar : DevExpress.XtraEditors.XtraForm
    {
        public depolar()
        {
            InitializeComponent();
            Kayıtlıverileriyukle();
            Talepverileriyukle();
            comboBoxSehir.Properties.Items.Clear();
            comboBoxSehir.Properties.Items.Add("Tümü");

            // Veritabanından şehirleri yükle
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Sehir FROM UrunKaydet UNION SELECT DISTINCT TSehir FROM TalepF", connection);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxSehir.Properties.Items.Add(reader[0].ToString());
                }
            }

            comboBoxSehir.SelectedIndex = 0; // Tümü varsayılan
            comboBoxSehir_SelectedIndexChanged(null, null); // Başlangıçta filtre uygula
        }

        private string connectionString = "Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True";

        private void Kayıtlıverileriyukle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM UrunKaydet";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gridControl1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Talepverileriyukle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM TalepF";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    gridControl2.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri yükleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void depolar_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            string secilenSehir = comboBoxSehir.SelectedItem.ToString();
            string TsecilenSehir = comboBoxSehir.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // GridControl1 için filtreli veri
                string query1 = secilenSehir == "Tümü" ?
                    "SELECT * FROM UrunKaydet" :
                    "SELECT * FROM UrunKaydet WHERE Sehir = @sehir";

                SqlDataAdapter adapter1 = new SqlDataAdapter(query1, conn);
                if (secilenSehir != "Tümü")
                    adapter1.SelectCommand.Parameters.AddWithValue("@sehir", secilenSehir);

                DataTable dt1 = new DataTable();
                adapter1.Fill(dt1);
                gridControl1.DataSource = dt1;

                // GridControl2 için filtreli veri
                string query2 = TsecilenSehir == "Tümü" ?
                    "SELECT * FROM TalepF" :
                    "SELECT * FROM TalepF WHERE TSehir = @sehir";

                SqlDataAdapter adapter2 = new SqlDataAdapter(query2, conn);
                if (TsecilenSehir != "Tümü")
                    adapter2.SelectCommand.Parameters.AddWithValue("@sehir", TsecilenSehir);

                DataTable dt2 = new DataTable();
                adapter2.Fill(dt2);
                gridControl2.DataSource = dt2;

                // Mevcut ve talep toplamları
                string sehirFilter = secilenSehir == "Tümü" ? "" : "WHERE Sehir = @sehir";
                string TsehirFilter = TsecilenSehir == "Tümü" ? "" : "WHERE TSehir = @sehir";

                SqlCommand cmdMevcut = new SqlCommand($"SELECT ISNULL(SUM(UMiktar), 0) FROM UrunKaydet {sehirFilter}", conn);
                SqlCommand cmdTalep = new SqlCommand($"SELECT ISNULL(SUM(TMiktar), 0) FROM TalepF {TsehirFilter}", conn);

                if (secilenSehir != "Tümü")
                {
                    cmdMevcut.Parameters.AddWithValue("@sehir", secilenSehir);
                    cmdTalep.Parameters.AddWithValue("@sehir", TsecilenSehir);
                }

                int toplamMevcut = Convert.ToInt32(cmdMevcut.ExecuteScalar());
                int toplamTalep = Convert.ToInt32(cmdTalep.ExecuteScalar());

                labelTumMevcut.Text = toplamMevcut.ToString();
                labelTumTalep.Text = toplamTalep.ToString();
                labelTumDurum.Text = (toplamMevcut - toplamTalep).ToString();

                // Kategoriler
                string[] kategoriler = { "Yiyecek", "İçecek", "Giyim", "Barınma", "Medikal" };
                foreach (string kategori in kategoriler)
                {
                    SqlCommand cmdKatMevcut = new SqlCommand(
                        $"SELECT ISNULL(SUM(UMiktar), 0) FROM UrunKaydet WHERE UKategori = @kategori " +
                        (secilenSehir == "Tümü" ? "" : "AND Sehir = @sehir"), conn);

                    SqlCommand cmdKatTalep = new SqlCommand(
                        $"SELECT ISNULL(SUM(TMiktar), 0) FROM TalepF WHERE TKategori = @kategori " +
                        (TsecilenSehir == "Tümü" ? "" : "AND TSehir = @sehir"), conn);

                    cmdKatMevcut.Parameters.AddWithValue("@kategori", kategori);
                    cmdKatTalep.Parameters.AddWithValue("@kategori", kategori);

                    if (secilenSehir != "Tümü")
                    {
                        cmdKatMevcut.Parameters.AddWithValue("@sehir", secilenSehir);
                        cmdKatTalep.Parameters.AddWithValue("@sehir", TsecilenSehir);
                    }

                    int katMevcut = Convert.ToInt32(cmdKatMevcut.ExecuteScalar());
                    int katTalep = Convert.ToInt32(cmdKatTalep.ExecuteScalar());
                    int katDurum = katMevcut - katTalep;

                    switch (kategori)
                    {
                        case "Yiyecek":
                            labelYiyecekMevcut.Text = katMevcut.ToString();
                            labelYiyecekTalep.Text = katTalep.ToString();
                            labelYiyecekDurum.Text = katDurum.ToString();
                            break;
                        case "İçecek":
                            labelIcecekMevcut.Text = katMevcut.ToString();
                            labelIcecekTalep.Text = katTalep.ToString();
                            labelIcecekDurum.Text = katDurum.ToString();
                            break;
                        case "Giyim":
                            labelGiyimMevcut.Text = katMevcut.ToString();
                            labelGiyimTalep.Text = katTalep.ToString();
                            labelGiyimDurum.Text = katDurum.ToString();
                            break;
                        case "Barınma":
                            labelBarinmaMevcut.Text = katMevcut.ToString();
                            labelBarinmaTalep.Text = katTalep.ToString();
                            labelBarinmaDurum.Text = katDurum.ToString();
                            break;
                        case "Medikal":
                            labelMedikalMevcut.Text = katMevcut.ToString();
                            labelMedikalTalep.Text = katTalep.ToString();
                            labelMedikalDurum.Text = katDurum.ToString();
                            break;
                    }
                }

                conn.Close();
            }
            // GridControl1 için filtre
            DataTable urunDt = (DataTable)gridControl1.DataSource;
            if (urunDt != null)
            {
                DataView view = urunDt.DefaultView;
                if (secilenSehir == "Tümü")
                    view.RowFilter = "";
                else
                    view.RowFilter = $"Sehir = '{secilenSehir.Replace("'", "''")}'";
            }

            // GridControl2 için filtre
            DataTable talepDt = (DataTable)gridControl2.DataSource;
            if (talepDt != null)
            {
                DataView view = talepDt.DefaultView;
                if (TsecilenSehir == "Tümü")
                    view.RowFilter = "";
                else
                    view.RowFilter = $"TSehir = '{TsecilenSehir.Replace("'", "''")}'";
            }
        }
    }
}
        
