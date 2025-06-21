using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.DataAccess;
using DevExpress.DashboardWin;
using DevExpress.XtraReports;
using System.Data.SqlClient;
using DevExpress.DashboardCommon.Native;
using DevExpress.DashboardCommon;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing.Printing;

namespace Aldep2
{
    public partial class Urun_Gonder : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public Urun_Gonder()
        {
            InitializeComponent();
            gridView1.RowClick += gridView1_RowClick;
        }

        private string connectionString = "Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True";

        private void Urun_Gonder_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                Yukle_Urunler();
                Yukle_Talepler();
                Yukle_Araclar();
                Yukle_Kategoriler();

                listBoxControl1.Items.Clear();
                comboBoxEdit1.Text = "-- Tümünü Göster --";
                gridView1.RowClick += gridView1_RowClick;
                gridView3.RowClick += gridView3_RowClick;
            }


        }
        public class SepetItem
        {
            public string UrunAdi { get; set; }
            public string UrunKategori { get; set; }
            public string CikisSehir { get; set; }
            public string TalepAdi { get; set; }
            public string VarisSehir { get; set; }
            public int TalepKod { get; set; }
            public decimal Miktar { get; set; }

            public override string ToString()
            {
                return $"{UrunAdi} / {Miktar} adet [{CikisSehir} → {VarisSehir}]";
            }
        }
        // Sepet listesi
        private List<SepetItem> sepetListesi = new List<SepetItem>();
        private decimal toplamDesi = 0;

        private void Guncelle_Sepet()
        {
            listBoxControl1.Items.Clear();
            foreach (var item in sepetListesi)
            {
                listBoxControl1.Items.Add(item);
            }
        }

        private void Yukle_Urunler()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM UrunKaydet";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl1.DataSource = dt;
            }
        }

        private void Yukle_Talepler()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM TalepF";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl2.DataSource = dt;
            }
        }

        private void Yukle_Araclar()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM araclar";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gridControl3.DataSource = dt;
            }
        }

        private void Yukle_Kategoriler()
        {
            comboBoxEdit1.Properties.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                SELECT UKategori FROM UrunKaydet WHERE UKategori IS NOT NULL
                UNION
                SELECT TKategori FROM TalepF WHERE TKategori IS NOT NULL";

                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    comboBoxEdit1.Properties.Items.Add(reader[0].ToString());
                }
            }
        }


        private string aktifAracPlaka = "";
        private void SepetiTemizle()
        {

            sepetListesi.Clear();
            listBoxControl1.Items.Clear();
            toplamDesi = 0;
            aktifAracPlaka = "";

            // Tüm gridleri yeniden yükle
            Yukle_Urunler();
            Yukle_Talepler();
            Yukle_Araclar();
        }



        private void gridView1_FocusedRowChanged(object sender, EventArgs e)
        {

        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                DataRow row = gridView1.GetDataRow(e.RowHandle);
                if (row != null && row["UKategori"] != DBNull.Value)
                {
                    string kategori = row["UKategori"].ToString();
                    comboBoxEdit1.Text = kategori;
                    comboBoxEdit1_SelectedIndexChanged(sender, e); // filtre uygula
                }
            }

        }

        private void gridView2_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void gridView3_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                DataRow row = gridView3.GetDataRow(e.RowHandle);
                if (row != null && row["aracplaka"] != DBNull.Value)
                {
                    string yeniPlaka = row["aracplaka"].ToString().Trim().ToUpper();
                    string mevcutPlaka = aktifAracPlaka.Trim().ToUpper();

                    if (!string.IsNullOrEmpty(mevcutPlaka) && yeniPlaka != mevcutPlaka && sepetListesi.Count > 0)
                    {
                        DialogResult sonuc = MessageBox.Show("Araç değiştiriliyor. Sepet temizlensin mi?", "Uyarı", MessageBoxButtons.YesNo);
                        if (sonuc == DialogResult.Yes)
                        {
                            SepetiTemizle();
                            aktifAracPlaka = yeniPlaka;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        aktifAracPlaka = yeniPlaka; // İlk atama veya aynı araç
                    }

                    textEdit2.Text = yeniPlaka;
                }
            }
        }


        private void simpleButton2_Click(object sender, EventArgs e) // Ekle butonu
        {

        }


        private void simpleButton1_Click(object sender, EventArgs e) // Gönder butonu
        {
            if (sepetListesi.Count == 0)
            {
                MessageBox.Show("Sepet boş, gönderilecek ürün yok!", "Uyarı");
                return;
            }

            string plaka = aktifAracPlaka;
            DateTime cikisZaman = DateTime.Now;

            // Sabit süre örneği: 5 saat (İleride Google API ile değiştirilebilir veya ücretsiz bir API ile)
            int seyahatSuresi = 5;
            DateTime tahminiVaris = cikisZaman.AddHours(seyahatSuresi);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    foreach (var item in sepetListesi)
                    {
                        // 1. Gönderilenler tablosuna kayıt
                        string insertQuery = @"INSERT INTO gonderilenler 
                (aracplaka, cikissehir, varissehir, cikiszaman, tahminivaris, urunadi, urunmiktar)
                VALUES (@plaka, @cikis, @varis, @cikisZaman, @tahminiVaris, @urunAdi, @miktar)";

                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn, trans);
                        insertCmd.Parameters.AddWithValue("@plaka", plaka);
                        insertCmd.Parameters.AddWithValue("@cikis", item.CikisSehir);
                        insertCmd.Parameters.AddWithValue("@varis", item.VarisSehir);
                        insertCmd.Parameters.AddWithValue("@cikisZaman", cikisZaman);
                        insertCmd.Parameters.AddWithValue("@tahminiVaris", tahminiVaris);
                        insertCmd.Parameters.AddWithValue("@urunAdi", item.UrunAdi);
                        insertCmd.Parameters.AddWithValue("@miktar", item.Miktar);
                        insertCmd.ExecuteNonQuery();

                        // 2. TalepF tablosunu güncelle
                        string updateTalep = "UPDATE TalepF SET KarsilananMiktar = ISNULL(KarsilananMiktar,0) + @ekle WHERE TKod = @tkod";
                        SqlCommand talepCmd = new SqlCommand(updateTalep, conn, trans);
                        talepCmd.Parameters.AddWithValue("@ekle", item.Miktar);
                        talepCmd.Parameters.AddWithValue("@tkod", item.TalepKod);
                        talepCmd.ExecuteNonQuery();
                    }

                    // 3. Araç desisini güncelle
                    string updateArac = "UPDATE araclar SET kalandesi = kalandesi - @azalt WHERE aracplaka = @plaka";
                    SqlCommand aracCmd = new SqlCommand(updateArac, conn, trans);
                    aracCmd.Parameters.AddWithValue("@azalt", toplamDesi);
                    aracCmd.Parameters.AddWithValue("@plaka", plaka);
                    aracCmd.ExecuteNonQuery();

                    // 4. Aracı yoldan kaldır: araclar tablosundan sil
                    string deleteArac = "DELETE FROM araclar WHERE aracplaka = @plaka";
                    SqlCommand deleteCmd = new SqlCommand(deleteArac, conn, trans);
                    deleteCmd.Parameters.AddWithValue("@plaka", plaka);
                    deleteCmd.ExecuteNonQuery();



                    trans.Commit();

                    // Bilgi mesajı
                    StringBuilder mesaj = new StringBuilder();
                    mesaj.AppendLine("📦 Gönderim Tamamlandı!");
                    mesaj.AppendLine($"🚚 Araç: {plaka}");
                    mesaj.AppendLine($"🏙️  Çıkış: {sepetListesi[0].CikisSehir}");
                    mesaj.AppendLine($"📍 Varış: {sepetListesi[0].VarisSehir}");
                    mesaj.AppendLine($"🕒 Süre: {seyahatSuresi} saat");
                    mesaj.AppendLine($"📅 Tahmini Varış: {tahminiVaris:dd.MM.yyyy HH:mm}");
                    mesaj.AppendLine($"📦 Toplam Yük: {toplamDesi} desi");
                    mesaj.AppendLine("\n📝 Gönderilen Ürünler:");
                    foreach (var item in sepetListesi)
                    {
                        mesaj.AppendLine($"• {item.UrunAdi} / {item.Miktar} adet → {item.VarisSehir} ({item.TalepAdi})");
                    }

                    MessageBox.Show(mesaj.ToString(), "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Sepeti temizle
                    SepetiTemizle();

                    // Tabloları yenile
                    Yukle_Urunler();
                    Yukle_Talepler();
                    Yukle_Araclar();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Hata oluştu: " + ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void dashboardDesigner1_Load_1(object sender, EventArgs e)
        {

        }

        private void gridControl1_Load(object sender, EventArgs e)
        {

        }

        private void gridControl2_Load(object sender, EventArgs e)
        {

        }

        private void gridControl3_Load(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedKategori = comboBoxEdit1.Text;

            if (string.IsNullOrEmpty(selectedKategori) || selectedKategori == "-- Tümünü Göster --")
            {
                if (gridControl1.DataSource is DataTable dt1)
                    dt1.DefaultView.RowFilter = "";
                if (gridControl2.DataSource is DataTable dt2)
                    dt2.DefaultView.RowFilter = "";
            }
            else
            {
                string escapedKategori = selectedKategori.Replace("'", "''");

                if (gridControl1.DataSource is DataTable dt1)
                    dt1.DefaultView.RowFilter = $"UKategori = '{escapedKategori}'";
                if (gridControl2.DataSource is DataTable dt2)
                    dt2.DefaultView.RowFilter = $"TKategori = '{escapedKategori}'";
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Ürün ve talep seçimlerini al
                var selectedUrun = gridView1.GetFocusedDataRow();
                var selectedTalep = gridView2.GetFocusedDataRow();

                if (selectedUrun == null || selectedTalep == null)
                {
                    MessageBox.Show("Lütfen hem ürün hem talep seçin!", "Uyarı");
                    return;
                }

                if (string.IsNullOrWhiteSpace(textEdit1.Text) || !decimal.TryParse(textEdit1.Text, out decimal miktar))
                {
                    MessageBox.Show("Geçerli bir miktar girin!", "Uyarı");
                    return;
                }

                if (string.IsNullOrWhiteSpace(textEdit2.Text))
                {
                    MessageBox.Show("Araç plakası girin!", "Uyarı");
                    return;
                }

                // Araç plakası doğrulama
                string plaka = textEdit2.Text.Trim().ToUpper();
                DataRow arac = null;

                if (gridControl3.DataSource is DataTable aracTablo)
                {
                    arac = aracTablo.AsEnumerable().FirstOrDefault(row =>
                        row["aracplaka"].ToString().Trim().ToUpper() == plaka);
                }

                if (arac == null)
                {
                    MessageBox.Show("Girilen plakaya ait araç bulunamadı!", "Uyarı");
                    return;
                }

                // Araç değişimi kontrolü (aktifAracPlaka değişkeni class içinde olmalı)
                string yeniPlaka = plaka;
                string mevcutPlaka = aktifAracPlaka.Trim().ToUpper();

                if (sepetListesi.Count > 0 && !string.IsNullOrEmpty(mevcutPlaka) && mevcutPlaka != yeniPlaka)
                {
                    DialogResult result = MessageBox.Show("Araç değiştiriliyor. Sepet temizlensin mi?", "Uyarı", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        sepetListesi.Clear();
                        listBoxControl1.Items.Clear();
                        toplamDesi = 0;
                        aktifAracPlaka = yeniPlaka;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    aktifAracPlaka = yeniPlaka; // İlk seçim veya aynı araç
                }

                // Verileri oku
                decimal kalanDesi = Convert.ToDecimal(arac["kalandesi"]);
                string varisSehir = selectedTalep["TSehir"].ToString();
                string cikisSehir = selectedUrun["Sehir"].ToString();
                decimal talepMiktar = Convert.ToDecimal(selectedTalep["TMiktar"]);
                decimal karsilananMiktar = selectedTalep["KarsilananMiktar"] == DBNull.Value ? 0 : Convert.ToDecimal(selectedTalep["KarsilananMiktar"]);

                // Şehir uyumu kontrolü
                if (sepetListesi.Count > 0)
                {
                    string sepetSehir = sepetListesi[0].VarisSehir;
                    if (varisSehir != sepetSehir)
                    {
                        MessageBox.Show("Aynı araç sadece tek bir şehre ürün götürebilir!", "Şehir Uyumsuzluğu");
                        return;
                    }
                }

                // Talep miktarı kontrolü
                decimal sepettekiAyniTalepMiktari = sepetListesi
                    .Where(x => x.TalepKod == Convert.ToInt32(selectedTalep["TKod"]))
                    .Sum(x => x.Miktar);

                decimal kalanTalep = talepMiktar - karsilananMiktar - sepettekiAyniTalepMiktari;
                if (miktar > kalanTalep)
                {
                    MessageBox.Show($"Talep miktarı aşıldı. Kalan: {kalanTalep}", "Talep Hatası");
                    return;
                }

                // Araç kapasite kontrolü
                if (toplamDesi + miktar > kalanDesi)
                {
                    MessageBox.Show($"Araç kapasitesi yetersiz! Kalan kapasite: {kalanDesi - toplamDesi}", "Kapasite Aşıldı");
                    return;
                }

                // Sepete ekle
                SepetItem item = new SepetItem
                {
                    UrunAdi = selectedUrun["UAdi"].ToString(),
                    UrunKategori = selectedUrun["UKategori"].ToString(),
                    CikisSehir = cikisSehir,
                    TalepAdi = selectedTalep["TAdi"].ToString(),
                    VarisSehir = varisSehir,
                    TalepKod = Convert.ToInt32(selectedTalep["TKod"]),
                    Miktar = miktar
                };

                sepetListesi.Add(item);
                toplamDesi += miktar;

                // Sepeti güncelle
                Guncelle_Sepet();
                textEdit1.Text = "";

                // Talep kalan miktarı sıfıra yaklaşırsa görünmesin
                if (gridControl2.DataSource is DataTable dtTalep)
                {
                    foreach (DataRow row in dtTalep.Rows)
                    {
                        if (row["TKod"].ToString() == selectedTalep["TKod"].ToString())
                        {
                            decimal tmiktar = Convert.ToDecimal(row["TMiktar"]);
                            decimal karsilanan = row["KarsilananMiktar"] == DBNull.Value ? 0 : Convert.ToDecimal(row["KarsilananMiktar"]);
                            decimal sepettekiler = sepetListesi
                                .Where(x => x.TalepKod == Convert.ToInt32(row["TKod"]))
                                .Sum(x => x.Miktar);

                            if ((tmiktar - karsilanan - sepettekiler) <= 0)
                                row.Delete();
                        }
                    }

                    dtTalep.AcceptChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "HATA");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string plaka = textEdit3.Text.Trim().ToUpper();
            string model = textEdit4.Text.Trim();
            string tur = comboBoxEdit2.Text;

            if (string.IsNullOrEmpty(plaka) || string.IsNullOrEmpty(tur) || string.IsNullOrEmpty(model))
            {
                MessageBox.Show("Lütfen plaka, araç türü ve modeli girin!", "Eksik Bilgi");
                return;
            }

            // Araç türüne göre desi belirle
            int desi = 0;
            switch (tur)
            {
                case "Binek Araç": desi = 500; break;
                case "Hafif Ticari": desi = 1000; break;
                case "Ticari Araç": desi = 1500; break;
                case "Kamyonet": desi = 2000; break;
                case "Kamyon": desi = 4000; break;
                case "Tır": desi = 10000; break;
                default:
                    MessageBox.Show("Geçerli bir araç türü seçin!", "Hata");
                    return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Aynı plakalı araç varsa ekleme (isteğe bağlı)
                string kontrolSorgu = "SELECT COUNT(*) FROM araclar WHERE aracplaka = @plaka";
                SqlCommand kontrolCmd = new SqlCommand(kontrolSorgu, conn);
                kontrolCmd.Parameters.AddWithValue("@plaka", plaka);
                int varMi = (int)kontrolCmd.ExecuteScalar();
                if (varMi > 0)
                {
                    MessageBox.Show("Bu plakaya sahip bir araç zaten kayıtlı!", "Uyarı");
                    return;
                }

                // Aracı ekle
                string insertSorgu = @"
        INSERT INTO araclar (aracplaka, aractur, aracmodel, aracdesi, kalandesi)
        VALUES (@plaka, @tur, @model, @desi, @desi)";
                SqlCommand insertCmd = new SqlCommand(insertSorgu, conn);
                insertCmd.Parameters.AddWithValue("@plaka", plaka);
                insertCmd.Parameters.AddWithValue("@tur", tur);
                insertCmd.Parameters.AddWithValue("@model", model);
                insertCmd.Parameters.AddWithValue("@desi", desi);
                insertCmd.ExecuteNonQuery();

                MessageBox.Show("Araç başarıyla oluşturuldu!", "Başarılı");

                // Temizlik
                textEdit3.Text = "";
                textEdit4.Text = "";
                comboBoxEdit2.SelectedIndex = -1;

                Yukle_Araclar(); // Grid güncelle
            }
        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (AracGonderVeKaydet(out string yazdirilacakMetin))
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += (s, ev) =>
                {
                    ev.Graphics.DrawString(yazdirilacakMetin, new Font("Arial", 10), Brushes.Black, 50, 50);
                };

                PrintDialog pd = new PrintDialog();
                pd.Document = printDoc;

                if (pd.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                }
            }
        }

        private bool AracGonderVeKaydet(out string yazdirilacakMetin)
        {
            yazdirilacakMetin = "";

            if (sepetListesi.Count == 0)
            {
                MessageBox.Show("Sepet boş, gönderilecek ürün yok!", "Uyarı");
                return false;
            }

            string plaka = aktifAracPlaka;
            DateTime cikisZaman = DateTime.Now;
            int seyahatSuresi = 5;
            DateTime tahminiVaris = cikisZaman.AddHours(seyahatSuresi);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();

                try
                {
                    foreach (var item in sepetListesi)
                    {
                        string insertQuery = @"INSERT INTO gonderilenler 
                    (aracplaka, cikissehir, varissehir, cikiszaman, tahminivaris, urunadi, urunmiktar)
                    VALUES (@plaka, @cikis, @varis, @cikisZaman, @tahminiVaris, @urunAdi, @miktar)";

                        SqlCommand insertCmd = new SqlCommand(insertQuery, conn, trans);
                        insertCmd.Parameters.AddWithValue("@plaka", plaka);
                        insertCmd.Parameters.AddWithValue("@cikis", item.CikisSehir);
                        insertCmd.Parameters.AddWithValue("@varis", item.VarisSehir);
                        insertCmd.Parameters.AddWithValue("@cikisZaman", cikisZaman);
                        insertCmd.Parameters.AddWithValue("@tahminiVaris", tahminiVaris);
                        insertCmd.Parameters.AddWithValue("@urunAdi", item.UrunAdi);
                        insertCmd.Parameters.AddWithValue("@miktar", item.Miktar);
                        insertCmd.ExecuteNonQuery();

                        string updateTalep = "UPDATE TalepF SET KarsilananMiktar = ISNULL(KarsilananMiktar,0) + @ekle WHERE TKod = @tkod";
                        SqlCommand talepCmd = new SqlCommand(updateTalep, conn, trans);
                        talepCmd.Parameters.AddWithValue("@ekle", item.Miktar);
                        talepCmd.Parameters.AddWithValue("@tkod", item.TalepKod);
                        talepCmd.ExecuteNonQuery();
                    }

                    string updateArac = "UPDATE araclar SET kalandesi = kalandesi - @azalt WHERE aracplaka = @plaka";
                    SqlCommand aracCmd = new SqlCommand(updateArac, conn, trans);
                    aracCmd.Parameters.AddWithValue("@azalt", toplamDesi);
                    aracCmd.Parameters.AddWithValue("@plaka", plaka);
                    aracCmd.ExecuteNonQuery();

                    string deleteArac = "DELETE FROM araclar WHERE aracplaka = @plaka";
                    SqlCommand deleteCmd = new SqlCommand(deleteArac, conn, trans);
                    deleteCmd.Parameters.AddWithValue("@plaka", plaka);
                    deleteCmd.ExecuteNonQuery();

                    trans.Commit();

                    // Yazdırılacak metni oluştur
                    StringBuilder yazici = new StringBuilder();
                    yazici.AppendLine($"Araç Plaka: {plaka}");
                    yazici.AppendLine($"Çıkış Şehir: {sepetListesi[0].CikisSehir}");
                    yazici.AppendLine($"Varış Şehir: {sepetListesi[0].VarisSehir}");
                    yazici.AppendLine($"Tahmini Varış: {tahminiVaris:dd.MM.yyyy HH:mm}");
                    yazici.AppendLine($"Toplam Yük: {toplamDesi} desi");
                    yazici.AppendLine();
                    yazici.AppendLine("Gönderilen Ürünler:");
                    foreach (var item in sepetListesi)
                    {
                        yazici.AppendLine($"- {item.UrunAdi} / {item.Miktar} adet → {item.VarisSehir} ({item.TalepAdi})");
                    }

                    yazdirilacakMetin = yazici.ToString();

                    SepetiTemizle();
                    Yukle_Urunler();
                    Yukle_Talepler();
                    Yukle_Araclar();

                    return true;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    MessageBox.Show("Gönderim sırasında hata oluştu: " + ex.Message, "HATA");
                    return false;
                }
            }
        }
    }
}