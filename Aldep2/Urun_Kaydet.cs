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
    public partial class Urun_Kaydet : DevExpress.XtraEditors.XtraForm
    {
        public Urun_Kaydet()
        {
            InitializeComponent();

            // DateEdit kontrollerini nullable yapılandır
            ConfigureDateEdits();
        }

        private void ConfigureDateEdits()
        {
            // DateEdit'lerin boş değer alabilmesini sağla
            dateEdit1.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            dateEdit2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;

            // Başlangıçta null değer ata
            dateEdit1.EditValue = null;
            dateEdit2.EditValue = null;

            // Null değer gösterim formatını ayarla
            dateEdit1.Properties.NullText = "";
            dateEdit2.Properties.NullText = "";
        }

        // SQL bağlantı cümlesi
        SqlConnection baglanti = new SqlConnection("Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True");

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                // Bağlantıyı aç
                baglanti.Open();

                // SQL komutu
                SqlCommand komut = new SqlCommand("INSERT INTO UrunKaydet (UAdi, UKategori, UMiktarTuru, UMiktar, UTarih, USonTarih, Sehir, Aciklama) VALUES (@U1, @U2, @U3, @U4, @U5, @U6, @U7, @U8)", baglanti);

                // Parametreleri ekle
                komut.Parameters.AddWithValue("@U1", textEdit1.Text);
                komut.Parameters.AddWithValue("@U2", comboBoxEdit1.Text);
                komut.Parameters.AddWithValue("@U3", comboBoxEdit2.Text);
                komut.Parameters.AddWithValue("@U4", textEdit2.Text);

                // Tarih parametrelerini null kontrollü ekle
                if (dateEdit1.EditValue != null && dateEdit1.DateTime != DateTime.MinValue)
                {
                    komut.Parameters.AddWithValue("@U5", dateEdit1.DateTime);
                }
                else
                {
                    komut.Parameters.AddWithValue("@U5", DBNull.Value);
                }

                if (dateEdit2.EditValue != null && dateEdit2.DateTime != DateTime.MinValue)
                {
                    komut.Parameters.AddWithValue("@U6", dateEdit2.DateTime);
                }
                else
                {
                    komut.Parameters.AddWithValue("@U6", DBNull.Value);
                }

                komut.Parameters.AddWithValue("@U7", comboBoxEdit3.Text);
                komut.Parameters.AddWithValue("@U8", textEdit4.Text);

                // Komutu çalıştır
                komut.ExecuteNonQuery();

                // Bilgilendirme
                MessageBox.Show("Ürün başarıyla kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Temizleme
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Bağlantıyı kapat
                if (baglanti.State == System.Data.ConnectionState.Open)
                    baglanti.Close();
            }
        }

        private void ClearForm()
        {
            textEdit1.Text = "";
            comboBoxEdit1.SelectedIndex = -1;
            comboBoxEdit2.SelectedIndex = -1;
            textEdit2.Text = "";
            dateEdit1.EditValue = null;
            dateEdit2.EditValue = null;
            comboBoxEdit3.SelectedIndex = -1;
            textEdit4.Text = "";
        }
    }
}