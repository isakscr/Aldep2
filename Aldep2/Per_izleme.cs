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

namespace Aldep2
{
    public partial class Per_izleme : DevExpress.XtraEditors.XtraForm
    {
        public Per_izleme()
        {
            InitializeComponent();
        }

        private void dashboardBackstageViewControl1_Click(object sender, EventArgs e)
        {

        }

        private void Per_izleme_Load(object sender, EventArgs e)
        {
            string dashboardPath = @"C:\Users\isaks\OneDrive\Masaüstü\iş\TÜ YBS Tüm Notlar\YBS\YBS 4+2\PORJE II\Per_izlemeDashboard\perizleson.xml";

            // Dashboard'ı yükle
            dashboardDesigner1.LoadDashboard(dashboardPath);
        }
    }
}