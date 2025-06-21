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
    public partial class Kayıt_Ol : DevExpress.XtraEditors.XtraForm
    {
        public Kayıt_Ol()
        {
            InitializeComponent();
        }

        private string connectionString = "Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True";

     
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //string email = txtEposta.Email;
            //object objEmail = txtEposta.EditValue;
            // Şifre kontrolü
            if (txtSifre.Text != txtSifreOnay.Text)
            {
                XtraMessageBox.Show("Şifreler aynı değil!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Verileri alma
            string tcno = txtTCNo.Text.Trim();
            string adsoyad = txtAdSoyad.Text.Trim();
            string kullaniciadi = txtKullaniciAdi.Text.Trim();
            string eposta = txtEposta.Text.Trim();
            string sifre = txtSifre.Text.Trim();
            string yetkiTalep = ımgYetkiTalep.SelectedItem?.ToString();

            // Veritabanına kaydetme işlemi

            {
                string query = "INSERT INTO kullanicigiris (TCNO, adsoyad, kullaniciadi, eposta, sifre, yetkitalep, yetki) " +
                               "VALUES (@TCNO, @adsoyad, @kullaniciadi, @eposta, @sifre, @yetkitalep, 3)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TCNO", tcno);
                    cmd.Parameters.AddWithValue("@adsoyad", adsoyad);
                    cmd.Parameters.AddWithValue("@kullaniciadi", kullaniciadi);
                    cmd.Parameters.AddWithValue("@eposta", eposta);
                    cmd.Parameters.AddWithValue("@sifre", sifre);
                    cmd.Parameters.AddWithValue("@yetkitalep", yetkiTalep);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        XtraMessageBox.Show("Kayıt başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }



                    catch (Exception ex)
                    {
                        XtraMessageBox.Show("Hata: " + ex.Message, "Veritabanı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    
                }
            }
        }
    }
}
