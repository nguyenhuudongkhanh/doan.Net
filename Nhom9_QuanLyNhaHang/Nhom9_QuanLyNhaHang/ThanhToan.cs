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
    public partial class ThanhToan : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtThanhToan,dtHTThanhToan;
        DataColumn[] keythanhToan = new DataColumn[1];
        bool themMoi = false;

        public ThanhToan()
        {
            InitializeComponent();
        }

        private void ThanhToan_Load(object sender, EventArgs e)
        {
            //dtThanhToan = db.LAYDULIEU("Select * from TBL_THANHTOAN");
            dtThanhToan = db.LAYDULIEU("select PK_iThanhToanID,TBL_THANHTOAN.FK_iHoaDonID,FK_sHinhThucThanhToanID,sum(iSoLuong*fGiaMonAn) as 'fTongTien',dNgayThanhToan from TBL_CHITIETHOADON,TBL_MONAN, TBL_THANHTOAN where TBL_CHITIETHOADON.FK_iMonAn=TBL_MONAN.PK_iMonAnID and TBL_THANHTOAN.FK_iHoaDonID=TBL_CHITIETHOADON.FK_iHoaDonID group by PK_iThanhToanID,TBL_THANHTOAN.FK_iHoaDonID,FK_sHinhThucThanhToanID,dNgayThanhToan");
            dtHTThanhToan = db.LAYDULIEU("Select * from TBL_HINHTHUCTHANHTOAN");
            keythanhToan[0] = dtThanhToan.Columns[0];
            dtThanhToan.PrimaryKey = keythanhToan;

            dtgvthanhtoan.AutoGenerateColumns = false;
            dtgvthanhtoan.DataSource = dtThanhToan;

            dtgvthanhtoan.Columns[0].DataPropertyName = "PK_iThanhToanID";
            dtgvthanhtoan.Columns[1].DataPropertyName = "FK_iHoaDonID";
            dtgvthanhtoan.Columns[2].DataPropertyName = "FK_sHinhThucThanhToanID";
            dtgvthanhtoan.Columns[3].DataPropertyName = "fTongTien";
            dtgvthanhtoan.Columns[4].DataPropertyName = "dNgayThanhToan";
            ThanhToan_databiding();

            cbbTim.Items.Add("2021");
            cbbTim.Items.Add("2020");
            cbbTim.Items.Add("2019");
        }
        public void LamMoiDuLieu()
        {
            txtMaHoaDon.Text = "";
            txtMaThanhToan.Text = "";
            txtHinhThuc.Text = "";
            txtTongTien.Text = "";
            ngayTT.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            LamMoiDuLieu();

            txtMaThanhToan.Enabled = true;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.YesNoCancel);
            if (h == DialogResult.Yes)
                Application.Exit();

        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show("Hủy sẽ làm mất dữ liệu bạn đang nhập. Bạn chắc chắn hủy không ? ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (DialogResult == DialogResult.Yes)
            {

                LamMoiDuLieu();
            }
            else
            {
                return;
            }
            txtMaThanhToan.DataBindings.Clear();
            txtMaHoaDon.DataBindings.Clear();
            txtHinhThuc.DataBindings.Clear();
            txtTongTien.DataBindings.Clear();
            ngayTT.DataBindings.Clear();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = true;

            txtMaHoaDon.Enabled = txtTongTien.Enabled=txtHinhThuc.Enabled = ngayTT.Enabled = false;
            ThanhToan_databiding();

        }

        void ThanhToan_databiding()
        {
            txtMaThanhToan.DataBindings.Add("Text", dtThanhToan, "PK_iThanhToanID");
            txtMaHoaDon.DataBindings.Add("Text", dtThanhToan, "FK_iHoaDonID");
            txtHinhThuc.DataBindings.Add("Text", dtThanhToan, "FK_sHinhThucThanhToanID");
            txtTongTien.DataBindings.Add("Text", dtThanhToan, "fTongTien");
            ngayTT.DataBindings.Add("Text", dtThanhToan, "dNgayThanhToan");

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            LamMoiDuLieu();
            txtMaThanhToan.DataBindings.Clear();
            txtMaHoaDon.DataBindings.Clear();
            txtHinhThuc.DataBindings.Clear();
            txtTongTien.DataBindings.Clear();
            ngayTT.DataBindings.Clear();

            txtMaHoaDon.Enabled = txtHinhThuc.Enabled = ngayTT.Enabled = true;
            txtTongTien.Text = "0";
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvthanhtoan.AllowUserToAddRows = false;
            dtgvthanhtoan.ReadOnly = false;
            tangId();
        }

        void tangId()
        {
            int r = dtgvthanhtoan.RowCount - 1;
            int soNV = int.Parse(dtgvthanhtoan.Rows[r].Cells[0].Value.ToString()) + 1;
            txtMaThanhToan.Text = soNV.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtMaHoaDon.Enabled = txtHinhThuc.Enabled= txtTongTien.Enabled = ngayTT.Enabled = true;
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
            if (txtTongTien.Text.Equals(""))
            {
                errorProvider2.SetError(txtTongTien, "không được để rỗng");
                return;
            }

            if (!int.TryParse(txtMaHoaDon.Text, out so))
            {
                errorProvider1.SetError(txtMaHoaDon, "Bạn phải nhập số");
                txtMaHoaDon.Clear();
                return;
            }
            if (!int.TryParse(txtTongTien.Text, out so))
            {
                errorProvider2.SetError(txtTongTien, "Bạn phải nhập số");
                txtTongTien.Clear();
                return;
            }
            if (themMoi)
            {
                if (txtMaThanhToan.Text != "" || txtHinhThuc.Text!="" || txtMaHoaDon.Text != "" || txtTongTien.Text != "" || ngayTT.Text != "")
                {
                    int tongTien = int.Parse(txtTongTien.Text);
                    db.luuThanhToan(dtThanhToan,txtMaThanhToan.Text,txtMaHoaDon.Text,txtHinhThuc.Text,tongTien,ngayTT.Text);
                    db.CAPNHATDULIEU("select * from TBL_THANHTOAN", dtThanhToan);

                    DataTable dtUpdate = new DataTable();
                    dtUpdate = db.LAYDULIEU("select PK_iThanhToanID,TBL_THANHTOAN.FK_iHoaDonID,FK_sHinhThucThanhToanID,sum(iSoLuong*fGiaMonAn) as 'fTongTien',dNgayThanhToan from TBL_CHITIETHOADON,TBL_MONAN, TBL_THANHTOAN where TBL_CHITIETHOADON.FK_iMonAn=TBL_MONAN.PK_iMonAnID and TBL_THANHTOAN.FK_iHoaDonID=TBL_CHITIETHOADON.FK_iHoaDonID group by PK_iThanhToanID,TBL_THANHTOAN.FK_iHoaDonID,FK_sHinhThucThanhToanID,dNgayThanhToan");
                    dtgvthanhtoan.DataSource = dtUpdate;
                }
                ThanhToan_databiding();
            }
            else
            {
                dtgvthanhtoan.Refresh();
            }
            txtMaHoaDon.Enabled = txtMaThanhToan.Enabled = txtTongTien.Enabled =ngayTT.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            dtgvthanhtoan.AllowUserToAddRows = false;
            dtgvthanhtoan.ReadOnly = true;  
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtTH_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtTH_tam = db.LAYDULIEU("select distinct PK_iThanhToanID from TBL_THANHTOAN where PK_iThanhToanID='" + txtMaThanhToan.Text + "'");
                if (dtTH_tam.Rows.Count == 0)
                {
                    db.xoaThanhToan(dtThanhToan, txtMaThanhToan.Text);
                }
                else
                    MessageBox.Show("Thanh toán tồn tại không xóa được");
            }
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_THANHTOAN", dtThanhToan);
                MessageBox.Show("Cập Nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvthanhtoan.DataSource = db.LAYDULIEU("select * from TBL_THANHTOAN where YEAR(dNgayThanhToan)='" + cbbTim.SelectedItem.ToString() + "'");
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new formchonhttt().Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
