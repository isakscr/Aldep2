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
    public partial class Personel_kayıt : DevExpress.XtraEditors.XtraForm
    {
        public Personel_kayıt()
        {
            InitializeComponent();
        }

        private string connectionString = "Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True";


        private void tablePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textEdit7_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti = new SqlConnection(connectionString);


            if (string.IsNullOrWhiteSpace(txtAdSoyad.Text) ||
                string.IsNullOrWhiteSpace(txtTc.Text) ||
                string.IsNullOrWhiteSpace(txtTelefon.Text) ||
                string.IsNullOrWhiteSpace(cmbGorevAlani.Text) ||
                
                dateEdit1.EditValue == null)
            {
                MessageBox.Show("Lütfen gerekli alanları doldurun:\n- Ad Soyad\n- TC\n- Doğum Tarihi\n- Görev Alanı\n- Telefon\n",
                                "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (baglanti.State == System.Data.ConnectionState.Closed)
                    baglanti.Open();

                SqlCommand komut = new SqlCommand("INSERT INTO personel (PerTC, GrupNu, PerAdSoyad, PerDTarih, GorevAlani, Adres, Telefon, TelsizKod, BagliKurum) " +
                                                  "VALUES (@PerTC, @GrupNu, @PerAdSoyad, @PerDTarih, @GorevAlani, @Adres, @Telefon, @TelsizKod, @BagliKurum)", baglanti);

                komut.Parameters.AddWithValue("@PerTC", txtTc.Text);
                komut.Parameters.AddWithValue("@GrupNu", Convert.ToInt32(txtGrupNo.Text));
                komut.Parameters.AddWithValue("@PerAdSoyad", txtAdSoyad.Text);
                komut.Parameters.AddWithValue("@PerDTarih", dateEdit1.DateTime);
                komut.Parameters.AddWithValue("@GorevAlani", cmbGorevAlani.Text);
                komut.Parameters.AddWithValue("@Adres", memoEdit1.Text);
                komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
                komut.Parameters.AddWithValue("@TelsizKod", txtTelsizKod.Text);
                komut.Parameters.AddWithValue("@BagliKurum", textEdit3.Text);

                komut.ExecuteNonQuery();
                MessageBox.Show("Personel başarıyla kaydedildi!", "Kayıt", MessageBoxButtons.OK, MessageBoxIcon.Information);

                baglanti.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
    
