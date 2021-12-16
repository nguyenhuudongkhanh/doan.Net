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
    public partial class BanAn : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtBanAn, dtLoaiBanAn;
        DataColumn[] keyBanAn = new DataColumn[1];
        bool themMoi = false;
        public BanAn()
        {
            InitializeComponent();
        }

        private void BanAn_Load(object sender, EventArgs e)
        {
            DuLieuComboBox();
            dtBanAn = db.LAYDULIEU("Select * from TBL_BANAN");
            dtLoaiBanAn = db.LAYDULIEU("Select * from TBL_LOAIBANAN");
            keyBanAn[0] = dtBanAn.Columns[0];
            dtBanAn.PrimaryKey = keyBanAn;

            cbbLoaiBanAn.DataSource = dtLoaiBanAn;
            cbbLoaiBanAn.DisplayMember = "sTenLoaiBanAn";
            cbbLoaiBanAn.ValueMember = "PK_sLoaiBanAnID";


            dtgvbanan.AutoGenerateColumns = false;
            dtgvbanan.DataSource = dtBanAn;
            dtgvbanan.Columns[0].DataPropertyName = "PK_iBanAnID";
            DataGridViewComboBoxColumn cmbo = (DataGridViewComboBoxColumn)dtgvbanan.Columns[1];
            cmbo.DataSource = dtLoaiBanAn;
            cmbo.DisplayMember = "sTenLoaiBanAn";
            cmbo.ValueMember = "PK_sLoaiBanAnID";
            cmbo.DataPropertyName = "FK_sLoaiBanAnID";
            dtgvbanan.Columns[2].DataPropertyName = "sTenBanAn";
            dtgvbanan.Columns[3].DataPropertyName = "bTrangThaiBanAn";

            BanAn_databiding();
            timKiem();

        }

        private void tangId()
        {
            int r = dtgvbanan.RowCount - 1;
            int soNV = int.Parse(dtgvbanan.Rows[r].Cells[0].Value.ToString()) + 1;
            txtMaBanAn.Text = soNV.ToString();
        }
        public void DuLieuComboBox()
        {
            cbbTrangThai.Items.Add("Sử Dụng");
            cbbTrangThai.Items.Add("Còn Trống");
            cbbTrangThai.SelectedIndex = 0;

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            LamMoiDuLieu();

            txtMaBanAn.Enabled = true;

        }

        private void btnthoat_Click(object sender, EventArgs e)
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
            txtMaBanAn.DataBindings.Clear();
            txtTenBanAn.DataBindings.Clear();
            cbbTrangThai.DataBindings.Clear();
            cbbLoaiBanAn.DataBindings.Clear();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = btnLuu.Enabled = true;
            txtTenBanAn.Enabled = cbbTrangThai.Enabled = cbbLoaiBanAn.Enabled = false;
            BanAn_databiding();
        }

        void BanAn_databiding()
        {
            txtMaBanAn.DataBindings.Add("Text", dtBanAn, "PK_iBanAnID");
            txtTenBanAn.DataBindings.Add("Text", dtBanAn, "sTenBanAn");
            cbbTrangThai.DataBindings.Add("Text", dtBanAn, "bTrangThaiBanAn");
            cbbLoaiBanAn.DataBindings.Add("SelectedValue", dtBanAn, "FK_sLoaiBanAnID");


        }
        public void LamMoiDuLieu()
        {
            txtMaBanAn.Text = "";
            //cbbLoaiBanAn.Text = "";
            txtTenBanAn.Text = "";

        }

        void timKiem()
        {
            cbbTimKiem.Items.Add("VIP-1");
            cbbTimKiem.Items.Add("TAMTRUNG");
            cbbTimKiem.Items.Add("BTHUONG");
            cbbTimKiem.SelectedIndex = 0;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvbanan.DataSource = db.LAYDULIEU("select * from TBL_BANAN where sTenBanAn like N'%" + cbbTimKiem.SelectedItem.ToString() + "%'");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            LamMoiDuLieu();
            themMoi = true;
            txtMaBanAn.DataBindings.Clear();
            txtTenBanAn.DataBindings.Clear();
            cbbTrangThai.DataBindings.Clear();
            cbbLoaiBanAn.DataBindings.Clear();

            txtTenBanAn.Enabled = cbbTrangThai.Enabled = cbbLoaiBanAn.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvbanan.AllowUserToAddRows = false;
            dtgvbanan.ReadOnly = false;
            tangId();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtTenBanAn.Enabled = cbbTrangThai.Enabled = cbbLoaiBanAn.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvbanan.ReadOnly = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            errorProvider1.Clear();
            if (txtTenBanAn.Text.Equals(""))
            {
                errorProvider1.SetError(txtTenBanAn, "không được để rỗng");
                return;
            }
            if (themMoi)
            {
                if (txtMaBanAn.Text != "" || txtTenBanAn.Text != "" || cbbLoaiBanAn.Text != "" || cbbTrangThai.Text != "")
                {
                    db.luuBanAn(dtBanAn, txtMaBanAn.Text, cbbLoaiBanAn.SelectedValue.ToString(), txtTenBanAn.Text, cbbTrangThai.SelectedItem.ToString());

                }
                BanAn_databiding();
            }
            else
            {
                dtgvbanan.Refresh();
            }
            txtTenBanAn.Enabled = cbbTrangThai.Enabled = cbbLoaiBanAn.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            dtgvbanan.AllowUserToAddRows = false;
            dtgvbanan.ReadOnly = true; 
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_iBanAnID from TBL_BANAN where PK_iBanAnID='" + txtMaBanAn.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaBanAn(dtBanAn, txtMaBanAn.Text);
                }
                else
                    MessageBox.Show("Loại bàn đã tồn tại không xóa được");
            }
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_BANAN", dtBanAn);
                MessageBox.Show("Cập nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
