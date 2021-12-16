using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom9_QuanLyNhaHang
{
    public partial class FormBaoCaoNhanVien : Form
    {
        public FormBaoCaoNhanVien()
        {
            InitializeComponent();
        }

        private void FormBaoCaoNhanVien_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            KetNoiDatabase db = new KetNoiDatabase();
            dt = db.LAYDULIEU("select * from TBL_NHANVIEN");
            BaoCaoNhanVien rp = new BaoCaoNhanVien();
            rp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rp;
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void btnxem_Click(object sender, EventArgs e)
        {
            if (txtma.Text == "")
            {
                MessageBox.Show("Ban Chua Nhap");
            }
            else
            {
                DataTable dt = new DataTable();
                KetNoiDatabase db = new KetNoiDatabase();
                dt = db.LAYDULIEU("select * from TBL_NHANVIEN where PK_iNhanVienID='" + txtma.Text + "' ");
                BaoCaoNhanVien rp = new BaoCaoNhanVien();
                rp.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rp;
            }
        }
    }
}
