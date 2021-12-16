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
    public partial class ChucVu : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtChucVu;
        DataColumn[] keyChucVu = new DataColumn[1];
        bool themMoi = false;

        public ChucVu()
        {
            InitializeComponent();
        }
        void chucVu_databiding()
        {
            txtMaChucVu.DataBindings.Add("Text", dtChucVu, "PK_sChucVuID");
            txtTenChucVu.DataBindings.Add("Text", dtChucVu, "sTenChucVu");
         

        }
        private void ChucVu_Load(object sender, EventArgs e)
        {
            string sql = "select* from TBL_CHUCVU";
            dtChucVu = db.LAYDULIEU(sql);

            keyChucVu[0] = dtChucVu.Columns[0];
            dtChucVu.PrimaryKey = keyChucVu;

            dtgvChucVu.AutoGenerateColumns = false;
            dtgvChucVu.DataSource = dtChucVu;

            dtgvChucVu.Columns[0].DataPropertyName = "PK_sChucVuID";
            dtgvChucVu.Columns[1].DataPropertyName = "sTenChucVu";

            chucVu_databiding();

            cbbTim.Items.Add("Thu Ngân");
            cbbTim.Items.Add("Tạp Vụ");
            cbbTim.Items.Add("Đầu Bếp");
            cbbTim.SelectedIndex = 0;

        }
        public void LamMoiDuLieu()
        {
            txtMaChucVu.Text = "";
            txtTenChucVu.Text = "";
        }
        private void button4_Click(object sender, EventArgs e)
        {
            //LamMoiDuLieu();

            //txtMaChucVu.Enabled = true;
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            DialogResult h = MessageBox.Show("Bạn có chắc muốn thoát không?", "Error", MessageBoxButtons.YesNoCancel);
            if (h == DialogResult.Yes)
                Application.Exit();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            themMoi = true;
            LamMoiDuLieu();
            txtMaChucVu.DataBindings.Clear();
            txtTenChucVu.DataBindings.Clear();

            txtMaChucVu.Enabled = txtTenChucVu.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvChucVu.AllowUserToAddRows = false;
            dtgvChucVu.ReadOnly = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtMaChucVu.Enabled = txtTenChucVu.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvChucVu.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_sChucVuID from TBL_CHUCVU where PK_sChucVuID='" + txtMaChucVu.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaChucVu(dtChucVu, txtMaChucVu.Text);
                }
                else
                    MessageBox.Show("Chức vụ đã tồn tại không xóa được");
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtMaChucVu.Text.Equals(""))
            {
                errorProvider1.SetError(txtMaChucVu, "không được để rỗng");
                return;
            }

            errorProvider2.Clear();
            if (txtTenChucVu.Text.Equals(""))
            {
                errorProvider2.SetError(txtTenChucVu, "không được để rỗng");
                return;
            }
            if (themMoi)
            {
                if (txtMaChucVu.Text != "" || txtTenChucVu.Text != "")
                {
                    db.luuChucVu(dtChucVu, txtMaChucVu.Text, txtTenChucVu.Text);
                }
                chucVu_databiding();
            }
            else
            {
                dtgvChucVu.Refresh();
            }
            txtMaChucVu.Enabled = txtTenChucVu.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            dtgvChucVu.AllowUserToAddRows = false;
            dtgvChucVu.ReadOnly = true; 
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_CHUCVU", dtChucVu);
                MessageBox.Show("Cập nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvChucVu.DataSource = db.LAYDULIEU("select * from TBL_CHUCVU where sTenChucVu like N'%" + cbbTim.SelectedItem.ToString() + "%'");
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
            txtMaChucVu.DataBindings.Clear();
            txtTenChucVu.DataBindings.Clear();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = true;
            txtMaChucVu.Enabled = txtTenChucVu.Enabled = false;
            chucVu_databiding();
        }
    }
}
