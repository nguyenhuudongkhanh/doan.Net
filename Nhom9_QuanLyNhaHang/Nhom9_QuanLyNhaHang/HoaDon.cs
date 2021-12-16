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
    public partial class HoaDon : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtHoaDon, dtKhachHang, dtNhanVien,dtBanAn;
        DataColumn[] keyHoaDon = new DataColumn[1];
        bool themMoi = false;


        public void LamMoiDuLieu()
        {
            txtMaHoaDon.Text = "";
            cbbTenKhachHang.Text = "";
            cbbBanAn.Text = "";
            cbbTenNhanVien.Text = "";
            dtNgayLap.Text = "";
         


        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            LamMoiDuLieu();

            txtMaHoaDon.Enabled = true;
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
            //txtMaHoaDon.Enabled = true;
            txtMaHoaDon.DataBindings.Clear();
            cbbTenKhachHang.DataBindings.Clear();
            cbbBanAn.DataBindings.Clear();
            cbbTenNhanVien.DataBindings.Clear();
            dtNgayLap.DataBindings.Clear();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled =btnLuu.Enabled= true;
            cbbTenKhachHang.Enabled = cbbBanAn.Enabled = cbbTenNhanVien.Enabled = dtNgayLap.Enabled = false;
            HoaDon_databiding();
        }

        public HoaDon()
        {
            InitializeComponent();
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            dtHoaDon = db.LAYDULIEU("Select*from TBL_HOADON");
            dtKhachHang = db.LAYDULIEU("Select * from TBL_KHACHHANG");
            dtNhanVien = db.LAYDULIEU("Select * from TBL_NHANVIEN");
            dtBanAn = db.LAYDULIEU("Select * from TBL_BANAN");
            keyHoaDon[0] = dtHoaDon.Columns[0];
            dtHoaDon.PrimaryKey = keyHoaDon;

            cbbTenKhachHang.DataSource = dtKhachHang;
            cbbTenKhachHang.DisplayMember = "sTenKhachHang";
            cbbTenKhachHang.ValueMember = "PK_iKhachHangID";


            cbbBanAn.DataSource = dtBanAn;
            cbbBanAn.DisplayMember = "sTenBanAn";
            cbbBanAn.ValueMember = "PK_iBanAnID";

            cbbTenNhanVien.DataSource = dtNhanVien;
            cbbTenNhanVien.DisplayMember = "sTenNhanVien";
            cbbTenNhanVien.ValueMember = "PK_iNhanVienID";

            dtgvHoaDon.AutoGenerateColumns = false;
            dtgvHoaDon.DataSource = dtHoaDon;

            dtgvHoaDon.Columns[0].DataPropertyName = "PK_iHoaDonID";
            DataGridViewComboBoxColumn cmbo = (DataGridViewComboBoxColumn)dtgvHoaDon.Columns[1];
            DataGridViewComboBoxColumn cmbo1 = (DataGridViewComboBoxColumn)dtgvHoaDon.Columns[2];
            DataGridViewComboBoxColumn cmbo2 = (DataGridViewComboBoxColumn)dtgvHoaDon.Columns[3];
            dtgvHoaDon.Columns[4].DataPropertyName = "dNgayLapHoaDon";
  
        


            cmbo.DataSource = dtKhachHang;
            cmbo.DisplayMember = "sTenKhachHang";
            cmbo.ValueMember = "PK_iKhachHangID";


            cmbo2.DataSource = dtBanAn;
            cmbo2.DisplayMember = "sTenBanAn";
            cmbo2.ValueMember = "PK_iBanAnID";

            cmbo1.DataSource = dtNhanVien;
            cmbo1.DisplayMember = "sTenNhanVien";
            cmbo1.ValueMember = "PK_iNhanVienID";

            cmbo.DataPropertyName = "FK_iKhachHangID";
            cmbo2.DataPropertyName = "FK_iBanAnID";
            cmbo1.DataPropertyName = "FK_iNhanVienID";


            cbbTim.Items.Add("2021");
            cbbTim.Items.Add("2020");
            cbbTim.Items.Add("2019");

            HoaDon_databiding();
        }
        void HoaDon_databiding()
        {
            txtMaHoaDon.DataBindings.Add("Text", dtHoaDon, "PK_iHoaDonID");
            cbbTenKhachHang.DataBindings.Add("SelectedValue", dtHoaDon, "FK_iKhachHangID");
            cbbBanAn.DataBindings.Add("SelectedValue", dtHoaDon, "FK_iBanAnID");
            cbbTenNhanVien.DataBindings.Add("SelectedValue", dtHoaDon, "FK_iNhanVienID");
            dtNgayLap.DataBindings.Add("Text", dtHoaDon, "dNgayLapHoaDon");
          


        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            LamMoiDuLieu();
            txtMaHoaDon.DataBindings.Clear();
            cbbTenKhachHang.DataBindings.Clear();
            cbbBanAn.DataBindings.Clear();
            cbbTenNhanVien.DataBindings.Clear();
            dtNgayLap.DataBindings.Clear();

            cbbTenKhachHang.Enabled = cbbBanAn.Enabled = cbbTenNhanVien.Enabled = dtNgayLap.Enabled =  true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvHoaDon.AllowUserToAddRows = false;
            dtgvHoaDon.ReadOnly = false;
            tangId();
            
        }

        void tangId()
        {
            int r = dtgvHoaDon.RowCount - 1;
            int soNV = int.Parse(dtgvHoaDon.Rows[r].Cells[0].Value.ToString()) + 1;
            txtMaHoaDon.Text = soNV.ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (themMoi)
            {
                if (txtMaHoaDon.Text != "" || cbbTenKhachHang.Text != "" || cbbTenNhanVien.Text != "" || cbbBanAn.Text != "" || dtNgayLap.Text != "")
                {
                    db.luuHoaDon(dtHoaDon, txtMaHoaDon.Text, cbbTenKhachHang.SelectedValue.ToString(), cbbTenNhanVien.SelectedValue.ToString(), cbbBanAn.SelectedValue.ToString(), dtNgayLap.Text);
                    
                }
                HoaDon_databiding(); 
            }
            else
            {
                dtgvHoaDon.Refresh();
            }
            cbbTenKhachHang.Enabled = cbbBanAn.Enabled = cbbTenNhanVien.Enabled = dtNgayLap.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            dtgvHoaDon.AllowUserToAddRows = false;
            dtgvHoaDon.ReadOnly = true;  
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            cbbTenKhachHang.Enabled = cbbBanAn.Enabled = cbbTenNhanVien.Enabled = dtNgayLap.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvHoaDon.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_iHoaDonID from TBL_HOADON where PK_iHoaDonID='" + txtMaHoaDon.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaHoaDon(dtHoaDon, txtMaHoaDon.Text);
                }
                else
                    MessageBox.Show("Hoá Đơn tồn tại không xóa được");
            }
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_HOADON", dtHoaDon);
                MessageBox.Show("Cập nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvHoaDon.DataSource = db.LAYDULIEU("select * from TBL_HOADON where YEAR(dNgayLapHoaDon)='"+cbbTim.SelectedItem.ToString()+"'");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new formbaocaotheodoanhthu().Show();
        }

        private void cbbBanAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dtloc = new DataTable();
            //dtloc = db.LAYDULIEU("select PK_iHoaDonID,FK_iKhachHangID,FK_iNhanVienID,FK_iBanAnID,dNgayLapHoaDon from TBL_HOADON, TBL_BANAN where TBL_HOADON.FK_iBanAnID=TBL_BANAN.PK_iBanAnID and sTenBanAn='" + cbbBanAn.SelectedItem.ToString() + "'");
            //dtgvHoaDon.DataSource = dtloc;

        }

        private void btnTangDan_Click(object sender, EventArgs e)
        {
            DataTable dtTang = new DataTable();
            dtTang = db.LAYDULIEU("SELECT * FROM TBL_HOADON ORDER BY PK_iHoaDonID ASC");
            dtgvHoaDon.DataSource = dtTang;
        }

        private void btnGiam_Click(object sender, EventArgs e)
        {
            DataTable dtGiam = new DataTable();
            dtGiam = db.LAYDULIEU("SELECT * FROM TBL_HOADON ORDER BY PK_iHoaDonID DESC");
            dtgvHoaDon.DataSource = dtGiam;
        }
    }
}
