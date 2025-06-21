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
    public partial class Per_Duzenle : DevExpress.XtraEditors.XtraForm
    {
        public Per_Duzenle()
        {
            InitializeComponent();
            gridView1.FocusedRowChanged += gridView1_FocusedRowChanged;
        }

        SqlConnection baglanti = new SqlConnection("Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True");

        private void Per_Duzenle_Load(object sender, EventArgs e)
        {
            PersonelListele();
        }
        
        void PersonelListele()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM personel", baglanti);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE personel SET PerAdSoyad=@ad, PerDTarih=@tarih, Telefon=@tel, Adres=@adres WHERE PerTC=@tc", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);
            komut.Parameters.AddWithValue("@ad", textEdit1.Text);
            komut.Parameters.AddWithValue("@tarih", dateEdit1.DateTime);
            komut.Parameters.AddWithValue("@tel", txtTelefon.Text);
            komut.Parameters.AddWithValue("@adres", memoEdit1.Text);
           

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();

            XtraMessageBox.Show("Personel bilgileri güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PersonelListele();
        }
        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView1.GetFocusedRowCellValue("PerTC") != null)
            {
                txtTc.Text = gridView1.GetFocusedRowCellValue("PerTC").ToString();
                textEdit1.Text = gridView1.GetFocusedRowCellValue("PerAdSoyad").ToString();
                dateEdit1.DateTime = Convert.ToDateTime(gridView1.GetFocusedRowCellValue("PerDTarih"));
                txtTelefon.Text = gridView1.GetFocusedRowCellValue("Telefon").ToString();
                memoEdit1.Text = gridView1.GetFocusedRowCellValue("Adres").ToString();
                txtTelsizKod.Text = gridView1.GetFocusedRowCellValue("TelsizKod").ToString();
                textEdit2.Text = gridView1.GetFocusedRowCellValue("GrupNu").ToString();
                textEdit3.Text = gridView1.GetFocusedRowCellValue("BagliKurum").ToString();
                cmbGorevAlani.Text = gridView1.GetFocusedRowCellValue("GorevAlani").ToString();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE personel SET  TelsizKod=@tkod, GrupNu=@grup, BagliKurum=@kurum, GorevAlani=@gorev WHERE PerTC=@tc", baglanti);
            komut.Parameters.AddWithValue("@tc", txtTc.Text);

            komut.Parameters.AddWithValue("@tkod", txtTelsizKod.Text);
            komut.Parameters.AddWithValue("@grup", textEdit2.Text);
            komut.Parameters.AddWithValue("@kurum", textEdit3.Text);
            komut.Parameters.AddWithValue("@gorev", cmbGorevAlani.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            XtraMessageBox.Show("Personel Görevlendirme güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            PersonelListele();
        }
    }
}