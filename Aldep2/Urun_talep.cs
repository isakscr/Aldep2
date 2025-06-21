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
    public partial class Urun_talep : DevExpress.XtraEditors.XtraForm
    {
        public Urun_talep()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True");



        private void Urun_talep_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sorgu = @"INSERT INTO TalepF (TAdi, TKategori, TMiktarTur, TMiktar, TSehir, TAciklama, TalepTarih, KarsilananMiktar)
                            VALUES  (@TAdi, @TKategori, @TMiktarTur, @TMiktar, @TSehir, @TAciklama, @TalepTarih, @KarsilananMiktar)";

            SqlCommand komut = new SqlCommand(sorgu, baglanti);

            komut.Parameters.AddWithValue("@TAdi", textEdit1.Text);
            komut.Parameters.AddWithValue("@TKategori", comboBoxEdit1.Text);
            komut.Parameters.AddWithValue("@TMiktarTur", comboBoxEdit2.Text);
            komut.Parameters.AddWithValue("@TMiktar", Convert.ToInt16(textEdit2.Text));
            komut.Parameters.AddWithValue("@TSehir", comboBoxEdit3.Text);
            komut.Parameters.AddWithValue("@TAciklama", textEdit4.Text);
            komut.Parameters.AddWithValue("@TalepTarih", DateTime.Now);
            komut.Parameters.AddWithValue("@KarsilananMiktar", 0);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Talep başarıyla eklendi.");

        }
    }
}