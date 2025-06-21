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
    public partial class takip : DevExpress.XtraEditors.XtraForm
    {
        public takip()
        {
           
            InitializeComponent();
         
         
        }

        private void takip_Load(object sender, EventArgs e)
        {
            Yukle_Takip();
            gridView1.FocusedRowChanged += GridView1_FocusedRowChanged;
          
        }

        private SqlConnection connection = new SqlConnection("Data Source=isaK-PC;Initial Catalog=aldep;Integrated Security=True;TrustServerCertificate=True");
        private DataTable dataTable = new DataTable();
        private string currentPlaka = "";

        private void Yukle_Takip()
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM gonderilenler", connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dataTable.Clear();
                da.Fill(dataTable);
                gridControl1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Veri yüklenirken hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

        }

        private void GridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                DataRow row = gridView1.GetDataRow(e.FocusedRowHandle);
                if (row != null)
                {
                    currentPlaka = row["aracplaka"].ToString();
                    labelControl2.Text = currentPlaka;
                    textEdit1.Text = row["cikissehir"].ToString().Trim();
                    textEdit2.Text = row["varissehir"].ToString().Trim();
                    labelControl9.Text = Convert.ToDateTime(row["cikiszaman"]).ToString("g");
                    labelControl10.Text = row["tahminivaris"] != DBNull.Value ?
                                          Convert.ToDateTime(row["tahminivaris"]).ToString("g") : "Belirtilmemiş";
                    textEdit3.Text = row["urunadi"].ToString().Trim();
                    textEdit4.Text = row["urunmiktar"].ToString();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentPlaka))
            {
                XtraMessageBox.Show("Lütfen bir kayıt seçiniz.");
                return;
            }

            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE dbo.gonderilenler SET " +
                    "cikissehir = @cikissehir, " +
                    "varissehir = @varissehir, " +
                    "tahminivaris = @tahminivaris, " +
                    "urunadi = @urunadi, " +
                    "urunmiktar = @urunmiktar " +
                    "WHERE gracplaka = @gracplaka", connection);

                cmd.Parameters.AddWithValue("@gracplaka", currentPlaka);
                cmd.Parameters.AddWithValue("@cikissehir", textEdit1.Text);
                cmd.Parameters.AddWithValue("@varissehir", textEdit2.Text);

                // Tahmini varış null olabilir
                if (string.IsNullOrWhiteSpace(labelControl10.Text) || labelControl10.Text == "Belirtilmemiş")
                    cmd.Parameters.AddWithValue("@tahminivaris", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@tahminivaris", Convert.ToDateTime(labelControl10.Text));

                cmd.Parameters.AddWithValue("@urunadi", textEdit3.Text);
                cmd.Parameters.AddWithValue("@urunmiktar", Convert.ToInt16(textEdit4.Text));

                int affectedRows = cmd.ExecuteNonQuery();
                if (affectedRows > 0)
                {
                    XtraMessageBox.Show("Kayıt başarıyla güncellendi.");
                    Yukle_Takip(); // Grid'i yenile
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show("Güncelleme sırasında hata oluştu: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}