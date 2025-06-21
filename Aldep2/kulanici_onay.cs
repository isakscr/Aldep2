using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace Aldep2
{
    public partial class kulanici_onay : DevExpress.XtraEditors.XtraForm
    {
        public kulanici_onay()
        {
            InitializeComponent();
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
            this.Shown += Kullanici_Onay_Show;
        }

        private string connectionString = "Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True";

        

        private void Kullanici_Onay_Show(object sender, EventArgs e)
        {
            verileriyukle();
        }

        private void verileriyukle()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT TCNO, adsoyad, kullaniciadi, eposta, sifre, yetkitalep, yetki FROM kullanicigiris";
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;

            if (view != null && e.FocusedRowHandle >= 0)
            {
                DataRow row = view.GetDataRow(e.FocusedRowHandle);
                if (row != null)
                {
                    txtTCKimlik.Text = row["TCNO"].ToString();
                    txtAdSoyad.Text = row["adsoyad"].ToString();
                    txtKullaniciAdi.Text = row["kullaniciadi"].ToString();
                    txtEPosta.Text = row["eposta"].ToString();
                    txtSifre.Text = row["sifre"].ToString();
                    cmbYetkiTalep.Text = row["yetkitalep"].ToString();

                    int yetkiValue = Convert.ToInt32(row["yetki"]);
                    switch (yetkiValue)
                    {
                        case 1: cmbYetkiDurum.Text = "Onayla"; break;
                        case 2: cmbYetkiDurum.Text = "Reddet"; break;
                        case 3: cmbYetkiDurum.Text = "Beklet"; break;
                        default: cmbYetkiDurum.Text = ""; break;
                    }
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTCKimlik.Text))
            {
                MessageBox.Show("Lütfen bir kullanıcı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = @"UPDATE kullanicigiris 
                                        SET adsoyad = @adsoyad, 
                                            kullaniciadi = @kullaniciadi, 
                                            eposta = @eposta, 
                                            sifre = @sifre, 
                                            yetkitalep = @yetkitalep 
                                        WHERE TCNO = @tcno";

                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@adsoyad", txtAdSoyad.Text);
                    command.Parameters.AddWithValue("@kullaniciadi", txtKullaniciAdi.Text);
                    command.Parameters.AddWithValue("@eposta", txtEPosta.Text);
                    command.Parameters.AddWithValue("@sifre", txtSifre.Text);
                    command.Parameters.AddWithValue("@yetkitalep", cmbYetkiTalep.Text);
                    command.Parameters.AddWithValue("@tcno", txtTCKimlik.Text);

                    int affectedRows = command.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Kullanıcı bilgileri başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        verileriyukle();
                    }
                    else
                    {
                        MessageBox.Show("Güncellenecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTCKimlik.Text))
            {
                MessageBox.Show("Lütfen bir kullanıcı seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int yetkiValue = cmbYetkiDurum.Text switch
                {
                    "Onayla" => 1,
                    "Reddet" => 2,
                    "Beklet" => 3,
                    _ => 0
                };

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string updateQuery = "UPDATE kullanicigiris SET yetki = @yetki WHERE TCNO = @tcno";

                    SqlCommand command = new SqlCommand(updateQuery, connection);
                    command.Parameters.AddWithValue("@yetki", yetkiValue);
                    command.Parameters.AddWithValue("@tcno", txtTCKimlik.Text);

                    int affectedRows = command.ExecuteNonQuery();
                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Kullanıcı yetki durumu başarıyla güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        verileriyukle();
                    }
                    else
                    {
                        MessageBox.Show("Güncellenecek kayıt bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Yetki güncelleme hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void labelControl7_Click(object sender, EventArgs e)
        {

        }
    }
}