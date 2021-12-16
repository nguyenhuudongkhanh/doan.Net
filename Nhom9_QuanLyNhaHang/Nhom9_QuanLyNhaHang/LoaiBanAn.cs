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
    public partial class LoaiBanAn : Form
    {
        KetNoiDatabase db = new KetNoiDatabase();
        DataTable dtLoaiBanAn;
        DataColumn[] keyLoaiBanAn = new DataColumn[1];
        bool themMoi = false;
        public LoaiBanAn()
        {
            InitializeComponent();
        }
        void LoaiBan_databiding()
        {
            txtMaLoai.DataBindings.Add("Text", dtLoaiBanAn, "PK_sLoaiBanAnID");
            txtLoai.DataBindings.Add("Text", dtLoaiBanAn, "sTenLoaiBanAn");
            txtPhuThu.DataBindings.Add("Text", dtLoaiBanAn, "fPhuThu");
          
        }
        public void LamMoiDuLieu()
        {
            txtMaLoai.Text = "";
            txtLoai.Text = "";
            txtPhuThu.Text = "";
           


        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoaiBanAn_Load(object sender, EventArgs e)
        {
            string sql = "select* from TBL_LOAIBANAN";
            dtLoaiBanAn = db.LAYDULIEU(sql);

            keyLoaiBanAn[0] = dtLoaiBanAn.Columns[0];
            dtLoaiBanAn.PrimaryKey = keyLoaiBanAn;

            //cbbMaLoai.DataSource = dtLoaiBanAn;
            //cbbMaLoai.DisplayMember = "sTenLoaiBanAn";
            //cbbMaLoai.ValueMember = "PK_sLoaiBanAnID";

            dtgvloaibanan.AutoGenerateColumns = false;
            dtgvloaibanan.DataSource = dtLoaiBanAn;

            dtgvloaibanan.Columns[0].DataPropertyName = "PK_sLoaiBanAnID";
            dtgvloaibanan.Columns[1].DataPropertyName = "sTenLoaiBanAn";
            dtgvloaibanan.Columns[2].DataPropertyName = "fPhuThu";

            LoaiBan_databiding();
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
            txtLoai.DataBindings.Clear();
            txtPhuThu.DataBindings.Clear();

            txtMaLoai.Enabled = txtLoai.Enabled = txtPhuThu.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvloaibanan.AllowUserToAddRows = false;//sua
            dtgvloaibanan.ReadOnly = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int so;
            errorProvider3.Clear();
            if (txtMaLoai.Text.Equals(""))
            {
                errorProvider3.SetError(txtMaLoai, "không được để rỗng");
                return;
            }

            errorProvider1.Clear();
            if (txtLoai.Text.Equals(""))
            {
                errorProvider1.SetError(txtLoai, "không được để rỗng");
                return;
            }

            errorProvider2.Clear();
            if (txtPhuThu.Text.Equals(""))
            {
                errorProvider2.SetError(txtPhuThu, "không được để rỗng");
                return;
            }

            if (!int.TryParse(txtPhuThu.Text, out so))
            {
                errorProvider2.SetError(txtPhuThu, "Bạn phải nhập số");
                txtPhuThu.Clear();
                return;
            }

            

            if (themMoi)
            {
                if (txtMaLoai.Text != "" || txtLoai.Text != "" || txtPhuThu.Text != "")
                {
                    db.luuLoaiBanAn(dtLoaiBanAn, txtMaLoai.Text,txtLoai.Text,txtPhuThu.Text);
                }
                LoaiBan_databiding();
            }
            else
            {
                dtgvloaibanan.Refresh();
            }
            txtMaLoai.Enabled = txtLoai.Enabled = txtPhuThu.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = false;
            dtgvloaibanan.AllowUserToAddRows = false;
            dtgvloaibanan.ReadOnly = true;  
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            themMoi = false;
            txtMaLoai.Enabled = txtLoai.Enabled = txtPhuThu.Enabled = true;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = false;
            btnLuu.Enabled = true;

            dtgvloaibanan.ReadOnly = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DataTable dtNV_tam = null;
            if (MessageBox.Show("Bạn có chắc không", "Cảnh báo xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                dtNV_tam = db.LAYDULIEU("select distinct PK_sLoaiBanAnID from TBL_LOAIBANAN where PK_sLoaiBanAnID='" + txtMaLoai.Text + "'");
                if (dtNV_tam.Rows.Count == 0)
                {
                    db.xoaLoaiBanAn(dtLoaiBanAn, txtMaLoai.Text);
                }
                else
                    MessageBox.Show("Loại bàn đã tồn tại không xóa được");
            }
        }

        private void btnCapNhap_Click(object sender, EventArgs e)
        {
            try
            {
                db.CAPNHATDULIEU("select * from TBL_LOAIBANAN", dtLoaiBanAn);
                MessageBox.Show("Cập nhập thành công", "Thông Báo");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
            txtMaLoai.DataBindings.Clear();
            txtLoai.DataBindings.Clear();
            txtPhuThu.DataBindings.Clear();

            txtMaLoai.Enabled = txtLoai.Enabled = txtPhuThu.Enabled = false;
            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            LoaiBan_databiding();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            dtgvloaibanan.DataSource = db.LAYDULIEU("select * from TBL_LOAIBANAN where sTenLoaiBanAn like N'%" + txtTimKiem.Text + "%'");
        }
    }
}
