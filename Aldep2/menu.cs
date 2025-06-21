using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
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
using System.Windows.Controls.Ribbon;
using System.Windows.Forms;

namespace Aldep2
{
    public partial class menu : DevExpress.XtraBars.Ribbon.RibbonForm
    {

        // Public property'ler - Form1'den erişilebilir
        public string KullaniciAdi { get; set; }
        public int YetkiSeviyesi { get; set; }
        public string YetkiTuru { get; set; }

        private BarStaticItem barKullaniciAdi;

        public menu()
        {
            InitializeComponent();
            this.Load += menu_Load;

        }

        private void menu_Load(object sender, EventArgs e)
        {

            if (!this.DesignMode)
            {
                YetkileriYapilandir();
                KullaniciBilgileriniGoster();
            }

        }
        private void KullaniciBilgileriniGoster()
        {
            barStaticItem6.Caption = GetirAdSoyad(KullaniciAdi);

        }
        private string GetirAdSoyad(string kullaniciAdi)
        {
            string connectionString = "Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    string query = "SELECT adsoyad FROM kullanicigiris WHERE kullaniciadi = @kullaniciadi";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kullaniciadi", kullaniciAdi);

                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    return result != null ? result.ToString() : "Bilinmeyen Kullanıcı";
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show("Kullanıcı adı alınırken hata: " + ex.Message);
                    return "Bilinmeyen Kullanıcı";
                }
            }
        }


        private void YetkileriYapilandir()
        {
            try
            {
                // Yetki türüne göre ribbon sayfalarını yapılandır
                switch (YetkiTuru.ToLower())
                {
                    case "yönetici":
                        // Yönetici için tüm sayfalar görünür
                        break;

                    case "personel":
                        ribbonPageGroup4.Enabled = false;
                        barButtonItem10.Enabled = false;
                        barButtonItem11.Enabled = false;
                        break;

                    case "misafir":
                        ribbonPageGroup2.Enabled = false;
                        ribbonPageGroup3.Enabled = false;
                        ribbonPageGroup4.Enabled = false;
                        barButtonItem10.Enabled = false;
                        barButtonItem11.Enabled = false;


                        break;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Form yapılandırılırken hata oluştu. Tekrar giriş yapmayı deneyiniz: {ex.Message}",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
            Personel_kayıt Pkayit = this.MdiChildren.OfType<Personel_kayıt>().FirstOrDefault();

            if (Pkayit == null)
            {
                // Form açılmamışsa, yeni formu aç
                Pkayit = new Personel_kayıt();
                Pkayit.MdiParent = this;
                Pkayit.Show();
            }
            else
            {
                // Form zaten açıksa, ona odaklan
                Pkayit.Focus();
            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Urun_Kaydet ukaydet = this.MdiChildren.OfType<Urun_Kaydet>().FirstOrDefault();

            if (ukaydet == null)
            {
                // Form açılmamışsa, yeni formu aç
                ukaydet = new Urun_Kaydet();
                ukaydet.MdiParent = this;
                ukaydet.Show();
            }
            else
            {
                // Form zaten açıksa, ona odaklan
                ukaydet.Focus();
            }

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            Urun_talep utalep = this.MdiChildren.OfType<Urun_talep>().FirstOrDefault();

            if (utalep == null)
            {
                // Form açılmamışsa, yeni formu aç
                utalep = new Urun_talep();
                utalep.MdiParent = this;
                utalep.Show();
            }
            else
            {
                // Form zaten açıksa, ona odaklan
                utalep.Focus();
            }

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {

            Urun_Gonder ugonder = this.MdiChildren.OfType<Urun_Gonder>().FirstOrDefault();

            if (ugonder == null)
            {
                //Form açılmamışsa, yeni formu aç
                ugonder = new Urun_Gonder();
                ugonder.MdiParent = this;
                ugonder.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                ugonder.Focus();
            }

        }

        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {

            Kayıt_Ol kayitform = this.MdiChildren.OfType<Kayıt_Ol>().FirstOrDefault();

            if (kayitform == null)
            {
                //Form açılmamışsa, yeni formu aç
                kayitform = new Kayıt_Ol();
                kayitform.MdiParent = this;
                kayitform.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                kayitform.Focus();
            }
            //Kayıt_Ol kayitFormu = new Kayıt_Ol();
            //kayitFormu.Show();
        }

        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            kulanici_onay konay = this.MdiChildren.OfType<kulanici_onay>().FirstOrDefault();

            if (konay == null)
            {
                //Form açılmamışsa, yeni formu aç
                konay = new kulanici_onay();
                konay.MdiParent = this;
                konay.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                konay.Focus();
            }
        }

        private void menu_Load_1(object sender, EventArgs e)
        {

        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
            Per_Duzenle pduzenle = this.MdiChildren.OfType<Per_Duzenle>().FirstOrDefault();

            if (pduzenle == null)
            {
                //Form açılmamışsa, yeni formu aç
                pduzenle = new Per_Duzenle();
                pduzenle.MdiParent = this;
                pduzenle.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                pduzenle.Focus();
            }
        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Per_izleme pizle = this.MdiChildren.OfType<Per_izleme>().FirstOrDefault();

            if (pizle == null)
            {
                //Form açılmamışsa, yeni formu aç
                pizle = new Per_izleme();
                pizle.MdiParent = this;
                pizle.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                pizle.Focus();
            }
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
            depolar depo = this.MdiChildren.OfType<depolar>().FirstOrDefault();

            if (depo == null)
            {
                //Form açılmamışsa, yeni formu aç
                depo = new depolar();
                depo.MdiParent = this;
                depo.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                depo.Focus();
            }
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            Dokuman dkm = this.MdiChildren.OfType<Dokuman>().FirstOrDefault();

            if (dkm == null)
            {
                //Form açılmamışsa, yeni formu aç
                dkm = new Dokuman();
                dkm.MdiParent = this;
                dkm.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                dkm.Focus();
            }
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            tahhut tht = this.MdiChildren.OfType<tahhut >().FirstOrDefault();

            if (tht == null)
            {
                //Form açılmamışsa, yeni formu aç
                tht = new tahhut ();
                tht.MdiParent = this;
                tht.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                tht.Focus();
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            takip takip = this.MdiChildren.OfType<takip>().FirstOrDefault();

            if (takip == null)
            {
                //Form açılmamışsa, yeni formu aç
                takip = new takip();
                takip.MdiParent = this;
                takip.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                takip.Focus();
            }
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            istatistik ists = this.MdiChildren.OfType<istatistik>().FirstOrDefault();

            if (ists == null)
            {
                //Form açılmamışsa, yeni formu aç
                ists = new istatistik();
                ists.MdiParent = this;
                ists.Show();
            }
            else
            {
                //Form zaten açıksa, ona odaklan
                ists.Focus();
            }
        }
    }
}
