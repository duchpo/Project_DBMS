using sql.Class;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sql
{
    public partial class fPhieuChi : Form
    {
        DataTable tblpc;

        public fPhieuChi()
        {
            InitializeComponent();
        }

        private void fPhieuChi_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnXuat.Enabled = true;
            LoadDataGridView();

        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * From DonNhapHangView";
            tblpc = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dataGridView1.DataSource = tblpc; //Nguồn dữ liệu            
            dataGridView1.Columns[0].HeaderText = "Mã đơn nhập hàng";
            dataGridView1.Columns[1].HeaderText = "Ngày nhập hàng";
            dataGridView1.Columns[2].HeaderText = "Mã ncc";
            dataGridView1.Columns[3].HeaderText = "Tình Trạng";
            dataGridView1.Columns[4].HeaderText = "Mã phiếu chi";
            dataGridView1.Columns[5].HeaderText = "Ngày xuất phiếu";
            dataGridView1.Columns[6].HeaderText = "Đơn giá";
            dataGridView1.Columns[7].HeaderText = "Số lượng";
            dataGridView1.Columns[8].HeaderText = "Đơn vị";
            dataGridView1.Columns[9].HeaderText = "Số tiền chi";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 300;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
            dataGridView1.Columns[6].Width = 100;
            dataGridView1.Columns[7].Width = 100;
            dataGridView1.Columns[8].Width = 100;
            dataGridView1.Columns[9].Width = 100;


            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {

            btnXuat.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMadonNH.Enabled = true;
            txtMadonNH.Focus();
        }

        private void ResetValues()
        {
            txtMadonNH.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
            txtDV.Text = "";
            txtNCC.Text = "";
            txtPC.Text = "";
        }

        private void btnXuat_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMadonNH.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhập hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMadonNH.Focus();
                return;
            }

            if (txtDonGia.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập giá sản phẩm
            {
                MessageBox.Show("Bạn phải nhập đơn giá ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGia.Focus();
                return;
            }

            if (txtSoLuong.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập tình trạng sản phẩm
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Focus();
                return;
            }
            if (txtDV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập đơn vị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDV.Focus();
                return;
            }

            if (txtNCC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNCC.Focus();
                return;
            }

            if (txtPC.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập phiếu chi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPC.Focus();
                return;
            }

            try
            {

                SqlCommand cmd = new SqlCommand("proc_InsertDonNhapHangAndPhieuChiAndChiTietNhapHang", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;
                float TongTien = float.Parse(txtDonGia.Text) * float.Parse(txtSoLuong.Text);

                // Thêm các tham số vào Sqlcmd
                cmd.Parameters.AddWithValue("@MaDonNH", txtMadonNH.Text);
                cmd.Parameters.AddWithValue("@NgayNH", DateTime.Today);
                cmd.Parameters.AddWithValue("@TriGiaDonNH", TongTien);
                cmd.Parameters.AddWithValue("@MaNCC", txtNCC.Text);
                cmd.Parameters.AddWithValue("@TinhTrang", "Đang giao");
                // Add parameters for the PhieuChi table
                cmd.Parameters.AddWithValue("@MaPhieuChi", txtPC.Text);
                cmd.Parameters.AddWithValue("@NgayXuatPhieu", DateTime.Today);
                cmd.Parameters.AddWithValue("@SoTienChi", TongTien);
                // Add parameters for the ChiTietNhapHang table
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text);
                cmd.Parameters.AddWithValue("@DonGia", float.Parse(txtDonGia.Text));
                cmd.Parameters.AddWithValue("@SL", int.Parse(txtSoLuong.Text));
                cmd.Parameters.AddWithValue("@DonVi", txtDV.Text);
                cmd.Parameters.AddWithValue("@TongTien", TongTien);

                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Thêm phiếu chi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetValues();
            btnThem.Enabled = true;
            btnXuat.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
  

                SqlCommand cmd = new SqlCommand("proc_XoaDonNhapHang", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@MaDonNH", txtMadonNH.Text.Trim());
                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Xóa đơn nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;

            btnXuat.Enabled = false;

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMadonNH.Focus();
                return;
            }

            if (e.RowIndex >= 0) // Đảm bảo chỉ lấy dữ liệu khi hàng được chọn
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Gán dữ liệu từ các cột của hàng được chọn vào các TextBox tương ứng
                txtMadonNH.Text = row.Cells["MaDonNH"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                txtSoLuong.Text = row.Cells["SL"].Value.ToString();
                txtDV.Text = row.Cells["DonVi"].Value.ToString();
                txtNCC.Text = row.Cells["MaNCC"].Value.ToString();
                txtPC.Text = row.Cells["MaPhieuChi"].Value.ToString();

            }
        }
    }
}