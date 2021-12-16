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
    public partial class FormBaoCaoHoaDon : Form
    {
        public FormBaoCaoHoaDon()
        {
            InitializeComponent();
        }
        public void loadbaocao()
        {
            DataTable dt = new DataTable();
            KetNoiDatabase db = new KetNoiDatabase();
            dt = db.LAYDULIEU("SELECT TBL_HOADON.PK_iHoaDonID,TBL_HOADON.FK_iKhachHangID, TBL_HOADON.FK_iNhanVienID, TBL_HOADON.FK_iBanAnID, TBL_HOADON.dNgayLapHoaDon, TBL_CHITIETHOADON.FK_iMonAn, TBL_CHITIETHOADON.iSoLuong, TBL_THANHTOAN.FK_sHinhThucThanhToanID, TBL_THANHTOAN.fTongTien, TBL_THANHTOAN.PK_iThanhToanID FROM TBL_CHITIETHOADON INNER JOIN  TBL_HOADON ON  TBL_CHITIETHOADON.FK_iHoaDonID = TBL_HOADON.PK_iHoaDonID INNER JOIN TBL_THANHTOAN ON TBL_HOADON.PK_iHoaDonID = TBL_THANHTOAN.FK_iHoaDonID and PK_iHoaDonID='" + truyendulieu.maloaihoadon + "'");
            rpBaoCaoHoaDon rp = new rpBaoCaoHoaDon();
            rp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rp;

        }
        private void FormBaoCaoHoaDon_Load(object sender, EventArgs e)
        {
            loadbaocao();
        }
    }
}
