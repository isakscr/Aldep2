using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Navigation;

namespace Aldep2
{
    public partial class Form1 : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public Form1()
        {
            InitializeComponent();


        }


        private string connectionString = "Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True";



        private void simpleButton1_Click(object sender, EventArgs e)
        {

            // Giriş butonuna tıklandığında çalışacak kod
            string kullaniciAdi = textEdit1.Text.Trim();
            string sifre = textEdit2.Text.Trim();

            if (string.IsNullOrEmpty(kullaniciAdi) || string.IsNullOrEmpty(sifre))
            {
                XtraMessageBox.Show("Kullanıcı adı ve şifre boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kullanıcı doğrulama ve yetki kontrolü
            KullaniciGirisKontrol(kullaniciAdi, sifre);

        }
        private void KullaniciGirisKontrol(string kullaniciAdi, string sifre)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kullanıcı adı ve şifre kontrolü için SQL sorgusu
                    string query = "SELECT * FROM dbo.kullanicigiris WHERE kullaniciadi = @kullaniciadi AND sifre = @sifre";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@kullaniciadi", kullaniciAdi);
                    command.Parameters.AddWithValue("@sifre", sifre);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);


                    if (dataTable.Rows.Count > 0)
                    {
                        // Kullanıcı bulundu, yetki durumunu kontrol et
                        DataRow row = dataTable.Rows[0];
                        string yetkiTalep = row["yetkitalep"].ToString().Trim().ToLower();
                        int yetkiSeviyesi = Convert.ToInt32(row["yetki"]);


                        // YetkiTalep durumuna göre işlem yap (yönetici, personel, misafir)
                        switch (yetkiTalep.ToLower())
                        {
                            case "yönetici":
                                KontrolEtVeMenuAc(yetkiSeviyesi, "yönetici", kullaniciAdi);
                                break;

                            case "personel":
                                KontrolEtVeMenuAc(yetkiSeviyesi, "personel", kullaniciAdi);

                                break;

                            case "misafir":
                                KontrolEtVeMenuAc(yetkiSeviyesi, "misafir", kullaniciAdi);
                                break;

                            //default:
                            //    XtraMessageBox.Show("Tanımlanamayan yetki türü. Lütfen sistem yöneticisi ile iletişime geçin.",
                            //        "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    break;
                            default:
                                XtraMessageBox.Show($"Tanımlanamayan yetki türü: '{yetkiTalep}'", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Giriş Başarısız",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Bağlantı hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void KontrolEtVeMenuAc(int yetkiSeviyesi, string yetkiTuru, string kullaniciAdi)
        {
            // Yetki seviyesine göre işlem yap
            if (yetkiSeviyesi == 1)
            {
                // menu formunu oluştur
                menu menuForm = new menu();
                // Kullanıcı bilgilerini menu formuna aktaracak public property'ler tanımlanmalı

                menuForm.KullaniciAdi = kullaniciAdi;
                menuForm.YetkiTuru = yetkiTuru;
                menuForm.YetkiSeviyesi = yetkiSeviyesi;

                this.Hide();
                menuForm.FormClosed += (s, args) => this.Show();
                menuForm.Show();
            }
            else if (yetkiSeviyesi == 2)
            {
                // Reddedildi
                XtraMessageBox.Show($"{yetkiTuru} yetki talebiniz reddedilmiştir. Lütfen sistem yöneticisi ile iletişime geçin.",
                    "Erişim Reddedildi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else // yetkiSeviyesi == 3 veya diğer durumlar
            {
                // Onay bekliyor
                XtraMessageBox.Show($"{yetkiTuru} yetki talebiniz onay bekliyor. Lütfen daha sonra tekrar deneyiniz.",
                    "Onay Bekliyor", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void accordionControlElement1_Click_1(object sender, EventArgs e)
        {
            Kayıt_Ol kayitFormu = new Kayıt_Ol();
            kayitFormu.Show();
        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}

