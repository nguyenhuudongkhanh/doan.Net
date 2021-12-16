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
    public partial class FormBaoCaoTheoHTTT : Form
    {
        public FormBaoCaoTheoHTTT()
        {
            InitializeComponent();
        }
        public void loadbaocao()
        {
            DataTable dt = new DataTable();
            KetNoiDatabase db = new KetNoiDatabase();
            dt = db.LAYDULIEU("SELECT TBL_HINHTHUCTHANHTOAN.PK_sHinhThucThanhToanID, TBL_HINHTHUCTHANHTOAN.sTenHinhThucThanhToan, TBL_HINHTHUCTHANHTOAN.bTrangThai, TBL_THANHTOAN.FK_iHoaDonID, TBL_THANHTOAN.fTongTien, TBL_THANHTOAN.dNgayThanhToan, TBL_THANHTOAN.PK_iThanhToanID FROM TBL_HINHTHUCTHANHTOAN INNER JOIN TBL_THANHTOAN ON TBL_HINHTHUCTHANHTOAN.PK_sHinhThucThanhToanID = TBL_THANHTOAN.FK_sHinhThucThanhToanID and PK_sHinhThucThanhToanID='" + truyendulieu.maloaithanhtoan + "'");
            rpBaoCaoTheoHTTT rp = new rpBaoCaoTheoHTTT();
            rp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rp;

        }
        private void FormBaoCaoTheoHTTT_Load(object sender, EventArgs e)
        {
            loadbaocao();
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
                dt = db.LAYDULIEU("SELECT TBL_HINHTHUCTHANHTOAN.PK_sHinhThucThanhToanID, TBL_HINHTHUCTHANHTOAN.sTenHinhThucThanhToan, TBL_HINHTHUCTHANHTOAN.bTrangThai, TBL_THANHTOAN.FK_iHoaDonID, TBL_THANHTOAN.fTongTien, TBL_THANHTOAN.dNgayThanhToan, TBL_THANHTOAN.PK_iThanhToanID FROM TBL_HINHTHUCTHANHTOAN INNER JOIN TBL_THANHTOAN ON TBL_HINHTHUCTHANHTOAN.PK_sHinhThucThanhToanID = TBL_THANHTOAN.FK_sHinhThucThanhToanID where dNgayThanhToan='" + txtma.Text + "' ");
                rpBaoCaoTheoHTTT rp = new rpBaoCaoTheoHTTT();
                rp.SetDataSource(dt);
                crystalReportViewer1.ReportSource = rp;
            }
        }
    }
}
