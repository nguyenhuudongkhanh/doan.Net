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
    public partial class KhachHang : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtKhachHang;
        DataColumn[] keyKhachHang = new DataColumn[1];
        bool themMoi = false;

        public KhachHang()
        {
            InitializeComponent();
        }

        private void KhachHang_Load(object sender, EventArgs e)
        {
            
            string sql = "select* from TBL_KHACHHANG";
           dtKhachHang = db.LAYDULIEU(sql);

            keyKhachHang[0] = dtKhachHang.Columns[0];
            dtKhachHang.PrimaryKey = keyKhachHang;

            dtgvKhachHang.AutoGenerateColumns = false;
            dtgvKhachHang.DataSource = dtKhachHang;

            dtgvKhachHang.Columns[0].DataPropertyName = "PK_iKhachHangID";
            dtgvKhachHang.Columns[1].DataPropertyName = "sTenKhachHang";
            dtgvKhachHang.Columns[2].DataPropertyName = "sSoDienThoai";
            dtgvKhachHang.Columns[3].DataPropertyName = "sDiaChi";
            dtgvKhachHang.Columns[4].DataPropertyName = "iDiemSo";

            KhachHang_databiding();
        }
        void KhachHang_databiding()
        {
            txtMaKhachHang.DataBindings.Add("Text", dtKhachHang, "PK_iKhachHangID");
            txtTenKhachHang.DataBindings.Add("Text", dtKhachHang, "sTenKhachHang");
            txtSoDienThoai.DataBindings.Add("Text", dtKhachHang, "sSoDienThoai");
            txtQueQuan.DataBindings.Add("Text", dtKhachHang, "sDiaChi");
            txtDiemSo.DataBindings.Add("Text", dtKhachHang, "iDiemSo");
       
        }
        public void LamMoiDuLieu()
        {
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtSoDienThoai.Text = "";
            txtQueQuan.Text = "";
            txtDiemSo.Text = "";
          

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        //private void btnHuy_Click(object sender, EventArgs e)
        //{

        //}

        private void button4_Click(object sender, EventArgs e)
        {
            //LamMoiDuLieu();

            //txtMaKhachHang.Enabled = true;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.YesNoCancel);
            if (h == DialogResult.Yes)
                Application.Exit();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvKhachHang.DataSource = db.LAYDULIEU("select * from TBL_KHACHHANG where sTenKhachHang like N'%" + txtTimKiem.Text+ "%'");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            txtMaKhachHang.DataBindings.Clear();
            txtTenKhachHang.DataBindings.Clear();
            txtQueQuan.DataBindings.Clear();
            txtSoDienThoai.DataBindings.Clear();
            txtDiemSo.DataBindings.Clear();
            LamMoiDuLieu();

            txtTenKhachHang.Enabled = txtQueQuan.Enabled = txtSoDienThoai.Enabled = txtDiemSo.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvKhachHang.AllowUserToAddRows = false;//sua
            dtgvKhachHang.ReadOnly = false;
            tangId();
        }

        private void tangId()
        {
            int r = dtgvKhachHang.RowCount - 1;
            int soNV = int.Parse(dtgvKhachHang.Rows[r].Cells[0].Value.ToString()) + 1;
            txtMaKhachHang.Text = soNV.ToString();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtMaKhachHang.Enabled = txtTenKhachHang.Enabled = txtSoDienThoai.Enabled = txtQueQuan.Enabled = txtDiemSo.Enabled  = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvKhachHang.ReadOnly = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int so;
            errorProvider1.Clear();
            if (txtTenKhachHang.Text.Equals(""))
            {
                errorProvider1.SetError(txtTenKhachHang, "không được để rỗng");
                return;
            }

            errorProvider2.Clear();
            if (txtQueQuan.Text.Equals(""))
            {
                errorProvider2.SetError(txtQueQuan, "không được để rỗng");
                return;
            }
            errorProvider3.Clear();
            if (txtSoDienThoai.Text.Equals(""))
            {
                errorProvider3.SetError(txtSoDienThoai, "không được để rỗng");
                return;
            }
            errorProvider4.Clear();
            if (txtDiemSo.Text.Equals(""))
            {
                errorProvider4.SetError(txtDiemSo, "không được để rỗng");
                return;
            }
            if (!int.TryParse(txtSoDienThoai.Text, out so))
            {
                errorProvider3.SetError(txtSoDienThoai, "Bạn phải nhập số");
                txtSoDienThoai.Clear();
                return;
            }
            if (!int.TryParse(txtDiemSo.Text, out so))
            {
                errorProvider4.SetError(txtDiemSo, "Bạn phải nhập số");
                txtDiemSo.Clear();
                return;
            }
            if (themMoi)
            {
                if (txtMaKhachHang.Text != "" || txtTenKhachHang.Text != "" || txtSoDienThoai.Text != "" || txtQueQuan.Text != "" || txtDiemSo.Text != "")
                {
                    db.luuKhachHang(dtKhachHang,txtMaKhachHang.Text,txtTenKhachHang.Text,txtSoDienThoai.Text,txtQueQuan.Text,txtDiemSo.Text);
                }
                KhachHang_databiding();
            }
            else
            {
                dtgvKhachHang.Refresh();
            }
            txtMaKhachHang.Enabled = txtTenKhachHang.Enabled = txtSoDienThoai.Enabled = txtQueQuan.Enabled = txtDiemSo.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            dtgvKhachHang.AllowUserToAddRows = false;
            dtgvKhachHang.ReadOnly = true;  
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_iKhachHangID from TBL_KHACHHANG where PK_iKhachHangID='" + txtMaKhachHang.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaKhachHang(dtKhachHang, txtMaKhachHang.Text);
                }
                else
                    MessageBox.Show("Nhân Viên tồn tại không xóa được");
            }
        }

        private void dtgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_KHACHHANG", dtKhachHang);
                MessageBox.Show("Cập nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
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
            txtMaKhachHang.DataBindings.Clear();
            txtTenKhachHang.DataBindings.Clear();
            txtQueQuan.DataBindings.Clear();
            txtSoDienThoai.DataBindings.Clear();
            txtDiemSo.DataBindings.Clear();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = true;
            txtMaKhachHang.Enabled=txtTenKhachHang.Enabled = txtQueQuan.Enabled = txtSoDienThoai.Enabled = txtDiemSo.Enabled = false;
            KhachHang_databiding();
        }

        private void btnTangDan_Click(object sender, EventArgs e)
        {
            DataTable dtTang = new DataTable();
            dtTang = db.LAYDULIEU("SELECT * FROM TBL_KHACHHANG ORDER BY iDiemSo ASC");
            dtgvKhachHang.DataSource = dtTang;
        }

        private void btnGiam_Click(object sender, EventArgs e)
        {
            DataTable dtGiam = new DataTable();
            dtGiam = db.LAYDULIEU("SELECT * FROM TBL_KHACHHANG ORDER BY iDiemSo DESC");
            dtgvKhachHang.DataSource = dtGiam;
        }

        private void btnPrintf_Click(object sender, EventArgs e)
        {
           
        }

        private void grpTim_Enter(object sender, EventArgs e)
        {

        }
    }
}
