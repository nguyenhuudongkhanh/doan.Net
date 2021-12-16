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
    public partial class LoaiMonAn : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtLoaiMonAn;
        DataColumn[] keyLoaiMonAn = new DataColumn[1];
        bool themMoi = false;
        public LoaiMonAn()
        {
            InitializeComponent();


        }
        void LoaiMonAn_databiding()
        {
            txtMaLoai.DataBindings.Add("Text", dtLoaiMonAn, "PK_sLoaiMonAnID");
            txtTenLoai.DataBindings.Add("Text", dtLoaiMonAn, "sTenLoaiMonAn");
          
        }
        public void LamMoiDuLieu()
        {
            txtMaLoai.Text = "";
            txtTenLoai.Text = "";
          
        }
        private void LoaiMonAn_Load(object sender, EventArgs e)
        {
            string sql = "select* from TBL_LOAIMONAN";
            dtLoaiMonAn = db.LAYDULIEU(sql);

            keyLoaiMonAn[0] = dtLoaiMonAn.Columns[0];
            dtLoaiMonAn.PrimaryKey = keyLoaiMonAn;

            dtgvloaiMonAn.AutoGenerateColumns = false;
            dtgvloaiMonAn.DataSource = dtLoaiMonAn;

            dtgvloaiMonAn.Columns[0].DataPropertyName = "PK_sLoaiMonAnID";
            dtgvloaiMonAn.Columns[1].DataPropertyName = "sTenLoaiMonAn";

            LoaiMonAn_databiding();
            cbbTim.Items.Add("Món ăn chay");
            cbbTim.Items.Add("Món ăn mặn");
            cbbTim.Items.Add("Món ăn tráng miệng");
            cbbTim.Items.Add("Nước uống");
            cbbTim.SelectedIndex = 0;
            

           
        }

        private void btnlammoi_Click(object sender, EventArgs e)
        {
            LamMoiDuLieu();

            txtMaLoai.Enabled = true;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.YesNoCancel);
            if (h == DialogResult.Yes)
                Application.Exit();
        }

        

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            LamMoiDuLieu();
            txtMaLoai.DataBindings.Clear();
            txtTenLoai.DataBindings.Clear();

            txtMaLoai.Enabled = txtTenLoai.Enabled =  true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvloaiMonAn.AllowUserToAddRows = false;
            dtgvloaiMonAn.ReadOnly = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvloaiMonAn.DataSource = db.LAYDULIEU("select * from TBL_LOAIMONAN where sTenLoaiMonAn like N'%" + cbbTim.SelectedItem.ToString() + "%'");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtMaLoai.Enabled = txtTenLoai.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvloaiMonAn.ReadOnly = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtMaLoai.Text.Equals(""))
            {
                errorProvider1.SetError(txtMaLoai, "không được để rỗng");
                return;
            }

            errorProvider2.Clear();
            if (txtTenLoai.Text.Equals(""))
            {
                errorProvider2.SetError(txtTenLoai, "không được để rỗng");
                return;
            }
            if (themMoi)
            {
                if (txtMaLoai.Text != "" || txtTenLoai.Text != "")
                {
                    db.luuLoaiMonAn(dtLoaiMonAn, txtMaLoai.Text, txtTenLoai.Text);
                }
                LoaiMonAn_databiding();
            }
            else
            {
                dtgvloaiMonAn.Refresh();
            }
            txtMaLoai.Enabled = txtTenLoai.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            dtgvloaiMonAn.AllowUserToAddRows = false;
            dtgvloaiMonAn.ReadOnly = true;  
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_LOAIMONAN", dtLoaiMonAn);
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
                dtNV_tam = db.LAYDULIEU("select distinct PK_sLoaiMonAnID from TBL_LOAIMONAN where PK_sLoaiMonAnID='" + txtMaLoai.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaLoaiBanAn(dtLoaiMonAn, txtMaLoai.Text);
                }
                else
                    MessageBox.Show("Loại bàn đã tồn tại không xóa được");
            }
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
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
            txtMaLoai.DataBindings.Clear();
            txtTenLoai.DataBindings.Clear();

            txtMaLoai.Enabled = txtTenLoai.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            LoaiMonAn_databiding();
        }
    }
}
