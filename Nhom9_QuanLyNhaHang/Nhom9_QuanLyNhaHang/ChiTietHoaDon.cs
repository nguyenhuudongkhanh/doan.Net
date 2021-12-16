using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom9_QuanLyNhaHang
{
    public partial class ChiTietHoaDon : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtCTHoaDon, dtHoaDon, dtMoAn;
        DataColumn[] keyCTHoaDon = new DataColumn[1];
        bool themMoi = false;

        public ChiTietHoaDon()
        {
            InitializeComponent();
        }

        private void ChiTietHoaDon_Load(object sender, EventArgs e)
        {
            dtCTHoaDon = db.LAYDULIEU("Select*from TBL_CHITIETHOADON");
          
            dtMoAn = db.LAYDULIEU("Select * from TBL_MONAN");
            keyCTHoaDon[0] = dtCTHoaDon.Columns[0];
            dtCTHoaDon.PrimaryKey = keyCTHoaDon;

            cboTenMonAn.DataSource = dtMoAn;
            cboTenMonAn.DisplayMember = "sTenMonAn";
            cboTenMonAn.ValueMember = "PK_iMonAnID";

            dtgvCTHD.AutoGenerateColumns = false;
            dtgvCTHD.DataSource = dtCTHoaDon;

            dtgvCTHD.Columns[0].DataPropertyName = "PK_iChiTietHoaDonID";
            dtgvCTHD.Columns[1].DataPropertyName = "FK_iHoaDonID";
            DataGridViewComboBoxColumn cmbo1 = (DataGridViewComboBoxColumn)dtgvCTHD.Columns[2];
            cmbo1.DataSource = dtMoAn;
            cmbo1.DisplayMember = "sTenMonAn";
            cmbo1.ValueMember = "PK_iMonAnID";
            cmbo1.DataPropertyName = "FK_iMonAn";
            dtgvCTHD.Columns[3].DataPropertyName = "iSoLuong";
            

            CTHD_databiding();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LamMoiDuLieu();

            txtMaCTHD.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.YesNoCancel);
            if (h == DialogResult.Yes)
                Application.Exit();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Bạn chắc chắn hủy không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes)
            {

                LamMoiDuLieu();
            }
            else
            {
                return;
            }
            txtMaCTHD.DataBindings.Clear();
            txtMaHoaDon.DataBindings.Clear();
            cboTenMonAn.DataBindings.Clear();
            txtSoLuong.DataBindings.Clear();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = true;
            txtMaHoaDon.Enabled = cboTenMonAn.Enabled = txtSoLuong.Enabled = false;
            CTHD_databiding();
        }

        public void LamMoiDuLieu()
        {
            txtMaCTHD.Text = "";
            txtMaHoaDon.Text = "";
            cboTenMonAn.Text = "";
            txtSoLuong.Text = "";
          


        }
        void CTHD_databiding()
        {
            txtMaCTHD.DataBindings.Add("Text", dtCTHoaDon, "PK_iChiTietHoaDonID");
            txtMaHoaDon.DataBindings.Add("Text", dtCTHoaDon, "FK_iHoaDonID");
            cboTenMonAn.DataBindings.Add("SelectedValue", dtCTHoaDon, "FK_iMonAn");
            txtSoLuong.DataBindings.Add("Text", dtCTHoaDon, "iSoLuong");

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            LamMoiDuLieu();
            txtMaCTHD.DataBindings.Clear();
            txtMaHoaDon.DataBindings.Clear();
            cboTenMonAn.DataBindings.Clear();
            txtSoLuong.DataBindings.Clear();

            txtMaHoaDon.Enabled = cboTenMonAn.Enabled = txtSoLuong.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvCTHD.AllowUserToAddRows = false;
            dtgvCTHD.ReadOnly = false;
            tangId();
        }

        void tangId()
        {
            int r = dtgvCTHD.RowCount - 1;
            int soNV = int.Parse(dtgvCTHD.Rows[r].Cells[0].Value.ToString()) + 1;
            txtMaCTHD.Text = soNV.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtMaHoaDon.Enabled = cboTenMonAn.Enabled = txtSoLuong.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int so;
            errorProvider1.Clear();
            if (txtMaHoaDon.Text.Equals(""))
            {
                errorProvider1.SetError(txtMaHoaDon, "không được để rỗng");
                return;
            }

            errorProvider2.Clear();
            if (txtSoLuong.Text.Equals(""))
            {
                errorProvider2.SetError(txtSoLuong, "không để được rỗng");
                return;
            }

            if (!int.TryParse(txtSoLuong.Text, out so))
            {
                errorProvider1.SetError(txtSoLuong, "Bạn phải nhập số");
                return;
            }
            if (themMoi)
            {
                if (txtMaCTHD.Text != "" || txtMaHoaDon.Text != "" || cboTenMonAn.Text != "" || txtSoLuong.Text != "")
                {
                    int sl = int.Parse(txtSoLuong.Text);
                   db.luuCTHD(dtCTHoaDon, txtMaCTHD.Text, txtMaHoaDon.Text, cboTenMonAn.SelectedValue.ToString(), sl);

                }
                CTHD_databiding();
            }
            else
            {
                dtgvCTHD.Refresh();
            }
            txtMaHoaDon.Enabled = cboTenMonAn.Enabled = txtSoLuong.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            dtgvCTHD.AllowUserToAddRows = false;
            dtgvCTHD.ReadOnly = true;  

        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_CHITIETHOADON", dtHoaDon);
                MessageBox.Show("Cập nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_iChiTietHoaDonID from TBL_CHITIETHOADON where PK_iChiTietHoaDonID='" + txtMaHoaDon.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaCTHD(dtCTHoaDon, txtMaCTHD.Text);
                }
                else
                    MessageBox.Show("Nhân Viên tồn tại không xóa được");
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvCTHD.DataSource = db.LAYDULIEU("select * from TBL_CHITIETHOADON where FK_iHoaDonID like N'%" + txtTimKiem.Text + "%'");
        }

    }
}
