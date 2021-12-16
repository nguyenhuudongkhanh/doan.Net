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
    public partial class NhanVien : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtNhanVien,dtChucVu;
        DataColumn[] keyNhanVien = new DataColumn[1];
        bool themMoi = false;
        public NhanVien()
        {
            InitializeComponent();
        }
        void NhanVien_databiding()
        {
            txtMaNhanVien.DataBindings.Add("Text", dtNhanVien, "PK_iNhanVienID");
            cbbTenChucVu.DataBindings.Add("SelectedValue", dtNhanVien , "FK_sChucVuID");
            txtTenNhanVien.DataBindings.Add("Text", dtNhanVien, "sTenNhanVien");
            txtSoDienThoai.DataBindings.Add("Text", dtNhanVien, "sSoDienThoai");
            txtEmail.DataBindings.Add("Text", dtNhanVien, "sEmail");
            txtDiaChi.DataBindings.Add("Text", dtNhanVien, "sDiaChi");
            txtTenDangNhap.DataBindings.Add("Text", dtNhanVien, "sTenDangNhap");
            txtMatKhau.DataBindings.Add("Text", dtNhanVien, "sMatKhau");
            cbbGioiTinh.DataBindings.Add("Text", dtNhanVien, "sGioiTinh");
        }
        public void LamMoiDuLieu()
        {
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            cbbTenChucVu.Text = "";
            cbbGioiTinh.Text = "";
            txtDiaChi.Text = "";
            txtSoDienThoai.Text = "";
           
            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";
            txtEmail.Text = "";
            cbbTrangThai.Text = "";

        }
        public void DuLieuComboBox()
        {
           
            cbbGioiTinh.Items.Add("Nam");
            cbbGioiTinh.Items.Add("Nữ");
            cbbGioiTinh.SelectedIndex = 0;
            cbbTrangThai.Items.Add("Còn Sử Dụng");
            cbbTrangThai.Items.Add("Hết Hạn");
            cbbTrangThai.SelectedIndex = 0;
           
        }
        private void nhanvien1_Load(object sender, EventArgs e)
        {
            DuLieuComboBox();
            dtNhanVien = db.LAYDULIEU("Select PK_iNhanVienID,FK_sChucVuID,sTenNhanVien,sTenDangNhap,sMatKhau,sSoDienThoai,sEmail,sDiaChi,sGioiTinh,bTrangThaiTaiKhoan from TBL_NHANVIEN");
            dtChucVu = db.LAYDULIEU("select * from TBL_CHUCVU");

            keyNhanVien[0] = dtNhanVien.Columns[0];
            dtNhanVien.PrimaryKey = keyNhanVien;

            cbbTenChucVu.DataSource = dtChucVu;
            cbbTenChucVu.DisplayMember = "sTenChucVu";
            cbbTenChucVu.ValueMember = "PK_sChucVuID";

            dtgvNhanVien.AutoGenerateColumns = false;
            dtgvNhanVien.DataSource = dtNhanVien;
            dtgvNhanVien.Columns[0].DataPropertyName = "PK_iNhanVienID";
            DataGridViewComboBoxColumn cboCv = (DataGridViewComboBoxColumn)dtgvNhanVien.Columns[1];
            cboCv.DataSource = dtChucVu;
            cboCv.DisplayMember = "sTenChucVu";
            cboCv.ValueMember = "PK_sChucVuID";
            cboCv.DataPropertyName = "FK_sChucVuID";
            dtgvNhanVien.Columns[2].DataPropertyName = "sTenNhanVien";
            dtgvNhanVien.Columns[3].DataPropertyName = "sTenDangNhap";
            dtgvNhanVien.Columns[4].DataPropertyName = "sMatKhau";
            dtgvNhanVien.Columns[5].DataPropertyName = "sSoDienThoai";
            dtgvNhanVien.Columns[6].DataPropertyName = "sEmail";
            dtgvNhanVien.Columns[7].DataPropertyName = "sDiaChi";
            dtgvNhanVien.Columns[8].DataPropertyName = "sGioiTinh";
            dtgvNhanVien.Columns[9].DataPropertyName = "bTrangThaiTaiKhoan";

           

            NhanVien_databiding();
        }
        
        private void btnTim_Click(object sender, EventArgs e)
        {
            load();
           
        }
        public void load()
        {
            dtgvNhanVien.DataSource = db.LAYDULIEU("select * from TBL_NHANVIEN where sTenNhanVien like '%" + txttimkiem.Text + "%'");
        }

        private void cbbTim_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
           
            LamMoiDuLieu();
            
            txtMaNhanVien.Enabled = true;
            grpTim.Text = "Tìm kiếm theo tên nhân viên";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Thông Báo", MessageBoxButtons.YesNoCancel);
            if (h == DialogResult.Yes)
                Application.Exit();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void dtgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnQuayLai_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            LamMoiDuLieu();
            txtMaNhanVien.DataBindings.Clear();
            cbbTenChucVu.DataBindings.Clear();
            txtTenNhanVien.DataBindings.Clear();
            txtSoDienThoai.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            cbbGioiTinh.DataBindings.Clear();

            txtTenDangNhap.DataBindings.Clear();
            txtMatKhau.DataBindings.Clear();
            cbbTrangThai.DataBindings.Clear();

            cbbTenChucVu.Enabled = txtTenNhanVien.Enabled = txtSoDienThoai.Enabled = txtEmail.Enabled = txtDiaChi.Enabled = cbbGioiTinh.Enabled = txtTenDangNhap.Enabled = txtMatKhau.Enabled = cbbTrangThai.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvNhanVien.AllowUserToAddRows = false;//sua
            dtgvNhanVien.ReadOnly = false;

            int r = dtgvNhanVien.RowCount - 1;
            int soNV = int.Parse(dtgvNhanVien.Rows[r].Cells[0].Value.ToString()) + 1;
            txtMaNhanVien.Text = soNV.ToString();
           

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtMaNhanVien.Enabled = cbbTenChucVu.Enabled = txtTenNhanVien.Enabled = txtSoDienThoai.Enabled = txtEmail.Enabled = txtDiaChi.Enabled = cbbGioiTinh.Enabled  = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvNhanVien.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_iNhanVienID from TBL_NHANVIEN where PK_iNhanVienID='" + txtMaNhanVien.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaNhanVien(dtNhanVien, txtMaNhanVien.Text);
                }
                else
                    MessageBox.Show("Nhân Viên tồn tại không xóa được");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int so;
            errorProvider1.Clear();
            if (txtTenNhanVien.Text.Equals(""))
            {
                errorProvider1.SetError(txtTenNhanVien, "không được để rỗng");
                return;
            }

            errorProvider2.Clear();
            if (txtDiaChi.Text.Equals(""))
            {
                errorProvider2.SetError(txtDiaChi, "không được để rỗng");
                return;
            }
            errorProvider3.Clear();
            if (txtEmail.Text.Equals(""))
            {
                errorProvider3.SetError(txtEmail, "không được để rỗng");
                return;
            }
            errorProvider4.Clear();
            if (txtSoDienThoai.Text.Equals(""))
            {
                errorProvider4.SetError(txtSoDienThoai, "không được để rỗng");
                return;
            }
            if (!int.TryParse(txtSoDienThoai.Text, out so))
            {
                errorProvider4.SetError(txtSoDienThoai, "Bạn phải nhập số");
                txtSoDienThoai.Clear();
                return;
            }
            errorProvider5.Clear();
            if (txtTenDangNhap.Text.Equals(""))
            {
                errorProvider5.SetError(txtTenDangNhap, "không được để rỗng");
                return;
            }
            errorProvider6.Clear();
            if (txtMatKhau.Text.Equals(""))
            {
                errorProvider6.SetError(txtMatKhau, "không được để rỗng");
                return;
            }
            if (themMoi)
            {
                if (txtMaNhanVien.Text != "" || txtTenNhanVien.Text != "" || txtSoDienThoai.Text != "" || txtEmail.Text != "" || txtDiaChi.Text != "" || txtMatKhau.Text != "" || txtTenDangNhap.Text!="" ||txtMatKhau.Text!="" || cbbTrangThai.Text!="")
                {
                    db.luuNhanVien(dtNhanVien, txtMaNhanVien.Text, cbbTenChucVu.SelectedValue.ToString(), txtTenNhanVien.Text, txtTenDangNhap.Text, txtMatKhau.Text, txtSoDienThoai.Text, txtEmail.Text, txtDiaChi.Text,cbbGioiTinh.SelectedItem.ToString(),cbbTrangThai.SelectedItem.ToString());
                }
                NhanVien_databiding();
            }
            else
            {
                dtgvNhanVien.Refresh();
            }
            txtMaNhanVien.Enabled = cbbTenChucVu.Enabled = txtTenNhanVien.Enabled = txtSoDienThoai.Enabled = txtEmail.Enabled = txtDiaChi.Enabled = cbbGioiTinh.Enabled = txtTenDangNhap.Enabled = txtMatKhau.Enabled = cbbTrangThai.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            dtgvNhanVien.AllowUserToAddRows = false;
            dtgvNhanVien.ReadOnly = true;  
        }

        private void txtQueQuan_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtgvNhanVien_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnHuyNV_Click(object sender, EventArgs e)
        {
            
        }

        private void btnHuy1_Click(object sender, EventArgs e)
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
            txtMaNhanVien.DataBindings.Clear();
            cbbTenChucVu.DataBindings.Clear();
            txtTenNhanVien.DataBindings.Clear();
            txtSoDienThoai.DataBindings.Clear();
            txtEmail.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            cbbGioiTinh.DataBindings.Clear();
            txtTenDangNhap.DataBindings.Clear();
            txtMatKhau.DataBindings.Clear();
            cbbTrangThai.DataBindings.Clear();

            cbbTenChucVu.Enabled = txtTenNhanVien.Enabled = txtSoDienThoai.Enabled = txtEmail.Enabled = txtDiaChi.Enabled = cbbGioiTinh.Enabled = txtTenDangNhap.Enabled = txtMatKhau.Enabled = cbbTrangThai.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new FormBaoCaoNhanVien().Show();
        }

        private void BtnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_NHANVIEN", dtNhanVien);
                MessageBox.Show("Cập nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
