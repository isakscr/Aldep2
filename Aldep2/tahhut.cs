using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Aldep2
{
    public partial class tahhut : DevExpress.XtraEditors.XtraForm
    {
        public tahhut()
        {
            InitializeComponent();

        }

        private string connectionString = "Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True";

        private void vgetir()
        {

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tahhutler", connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void tahhut_Load(object sender, EventArgs e)
        {
            vgetir();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Tahhutler 
                            (Kurum, UrunAdi, UKategori, UrunMiktarTur, UrunMiktar, USehir, aciklama) 
                            VALUES 
                            (@Kurum, @UrunAdi,@UKategori, @UrunMiktarTur, @UrunMiktar, @USehir, @aciklama)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Parametreleri ekleyerek SQL Injection'a karşı koruma
                        command.Parameters.AddWithValue("@Kurum", textEdit1.Text.Trim());
                        command.Parameters.AddWithValue("@UrunAdi", textEdit2.Text.Trim());
                        command.Parameters.AddWithValue("@UKategori", comboBoxEdit1.Text.Trim());
                        command.Parameters.AddWithValue("@UrunMiktarTur", comboBoxEdit2.Text.Trim());
                        command.Parameters.AddWithValue("@UrunMiktar", textEdit3.Text.Trim());
                        command.Parameters.AddWithValue("@USehir", comboBoxEdit3.Text.Trim());
                        command.Parameters.AddWithValue("@aciklama", textEdit4.Text.Trim());

                        connection.Open();
                        int result = command.ExecuteNonQuery();

                        if (result > 0)
                        {
                            XtraMessageBox.Show("Taahhüt başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Formu temizle
                            textEdit1.Text = "";
                            textEdit2.Text = "";
                            comboBoxEdit1.Text = "";
                            comboBoxEdit2.Text = "";
                            textEdit3.Text = "";
                            comboBoxEdit3.Text = "";
                            textEdit4.Text = "";
                        }
                        else
                        {
                            XtraMessageBox.Show("Taahhüt eklenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            vgetir();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                // Seçili satır kontrolü
                if (gridView1.SelectedRowsCount == 0)
                {
                    XtraMessageBox.Show("Lütfen bir ürün seçin!", "Uyarı");
                    return;
                }

                // Seçili satırın ID'sini al
                int seciliId = Convert.ToInt32(gridView1.GetFocusedRowCellValue("TId"));

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Transaction başlat
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Ürünü UrunKaydet'e ekle
                            string insertQuery = @"INSERT INTO UrunKaydet 
                                        (UAdi, UKategori, UMiktarTuru, UMiktar, Sehir, Aciklama, UTarih) 
                                        VALUES 
                                        (@UAdi, @UKategori, @UMiktarTuru, @UMiktar, @Sehir, @Aciklama, @UTarih)";

                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction))
                            {
                                DataRowView selectedRow = gridView1.GetFocusedRow() as DataRowView;

                                insertCmd.Parameters.AddWithValue("@UAdi", selectedRow["UrunAdi"]);
                                insertCmd.Parameters.AddWithValue("@UKategori", selectedRow["UKategori"]);
                                insertCmd.Parameters.AddWithValue("@UMiktarTuru", selectedRow["UrunMiktarTur"]);
                                insertCmd.Parameters.AddWithValue("@UMiktar", selectedRow["UrunMiktar"]);
                                insertCmd.Parameters.AddWithValue("@Sehir", selectedRow["USehir"]);
                                insertCmd.Parameters.AddWithValue("@Aciklama", selectedRow["aciklama"]);
                                insertCmd.Parameters.AddWithValue("@UTarih", DateTime.Now);

                                int insertResult = insertCmd.ExecuteNonQuery();

                                if (insertResult <= 0)
                                {
                                    transaction.Rollback();
                                    XtraMessageBox.Show("Ürün kaydedilemedi!", "Hata");
                                    return;
                                }
                            }

                            // 2. Ürünü Tahhutler'den sil
                            string deleteQuery = "DELETE FROM Tahhutler WHERE TId = @TId";
                            using (SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection, transaction))
                            {
                                deleteCmd.Parameters.AddWithValue("@TId", seciliId);
                                int deleteResult = deleteCmd.ExecuteNonQuery();

                                if (deleteResult <= 0)
                                {
                                    transaction.Rollback();
                                    XtraMessageBox.Show("Ürün silinemedi!", "Hata");
                                    return;
                                }
                            }

                            // İşlemleri onayla
                            transaction.Commit();
                            XtraMessageBox.Show("Ürün başarıyla işlendi!");
                            vgetir(); // Grid'i yenile
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            XtraMessageBox.Show("Hata: " + ex.Message, "Hata");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata: " + ex.Message, "Hata");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tek transaction içinde iki işlem
                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // 1. Tüm verileri UrunKaydet'e ekle
                            string insertQuery = @"INSERT INTO UrunKaydet 
                                        (UAdi, UKategori, UMiktarTuru, UMiktar, Sehir, Aciklama, UTarih)
                                        SELECT 
                                        UrunAdi, UKategori, UrunMiktarTur, UrunMiktar, USehir, aciklama, GETDATE()
                                        FROM Tahhutler";

                            SqlCommand insertCmd = new SqlCommand(insertQuery, connection, transaction);
                            int insertedCount = insertCmd.ExecuteNonQuery();

                            // 2. Tüm verileri Tahhutler'den sil
                            string deleteQuery = "DELETE FROM Tahhutler";
                            SqlCommand deleteCmd = new SqlCommand(deleteQuery, connection, transaction);
                            int deletedCount = deleteCmd.ExecuteNonQuery();

                            transaction.Commit();

                            XtraMessageBox.Show($"{insertedCount} ürün başarıyla taşındı ve silindi!", "Başarılı");
                            vgetir(); // Grid'i yenile
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Hata: " + ex.Message, "Hata");
            }
        }
    }
}