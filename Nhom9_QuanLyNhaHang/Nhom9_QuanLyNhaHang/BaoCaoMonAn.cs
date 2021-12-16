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
    public partial class BaoCaoMonAn : Form
    {
        public BaoCaoMonAn()
        {
            InitializeComponent();
        }
        public void loadbaocao()
        {
            DataTable dt = new DataTable();
            KetNoiDatabase db = new KetNoiDatabase();
            dt = db.LAYDULIEU("SELECT TBL_LOAIMONAN.PK_sLoaiMonAnID,TBL_LOAIMONAN.sTenLoaiMonAn, TBL_MONAN.PK_iMonAnID, TBL_MONAN.FK_sLoaiMonAnID, TBL_MONAN.sTenMonAn, TBL_MONAN.fGiaMonAn, TBL_MONAN.sHinhAnhMonAn,TBL_MONAN.bTrangThai FROM TBL_LOAIMONAN INNER JOIN TBL_MONAN ON TBL_LOAIMONAN.PK_sLoaiMonAnID = TBL_MONAN.FK_sLoaiMonAnID ");
            rpbaocaomonantheoloai rp = new rpbaocaomonantheoloai();
            rp.SetDataSource(dt);
            crystalReportViewer1.ReportSource = rp;
        }
        private void BaoCaoMonAn_Load(object sender, EventArgs e)
        {
            loadbaocao();
        }
    }
}
