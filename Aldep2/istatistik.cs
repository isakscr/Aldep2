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
    public partial class istatistik : DevExpress.XtraEditors.XtraForm
    {
        public istatistik()
        {
            InitializeComponent();
        }

        private void istatistik_Load(object sender, EventArgs e)
        {
            string dashboardPath = @"C:\Users\isaks\OneDrive\Masaüstü\iş\TÜ YBS Tüm Notlar\YBS\YBS 4+2\PORJE II\İstatistik_dashboard\istatistik deneme.xml";
           
            // Dashboard'ı yükle
            dashboardDesigner1.LoadDashboard(dashboardPath);
        }
    }
}