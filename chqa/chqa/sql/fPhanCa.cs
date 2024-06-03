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

namespace sql
{
    public partial class fPhanCa : Form
    {
        DataTable tblpc;

        public fPhanCa()
        {
            InitializeComponent();

        }
        private void fPhanCa_Load(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * From v_BangPhanCa";
            tblpc = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dvgPhanCa.DataSource = tblpc; //Nguồn dữ liệu            
            dvgPhanCa.Columns[0].HeaderText = "Mã ca";
            dvgPhanCa.Columns[1].HeaderText = "Mã nhân vien";
            dvgPhanCa.Columns[2].HeaderText = "Họ nhân viên";
            dvgPhanCa.Columns[3].HeaderText = "Tên nhân viên";
            dvgPhanCa.Columns[4].HeaderText = "Ngày làm";
            dvgPhanCa.Columns[0].Width = 100;
            dvgPhanCa.Columns[1].Width = 300;
            dvgPhanCa.Columns[2].Width = 100;
            dvgPhanCa.Columns[3].Width = 100;
            dvgPhanCa.Columns[4].Width = 100;
            dvgPhanCa.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dvgPhanCa.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaCa.Enabled = true;
            txtMaCa.Focus();
        }
        private void ResetValues()
        {
            txtMaCa.Text = "";
            dtpNgayLam.Text = "";
            txtBatDau.Text = "";
            txtKetThuc.Text = "";
            txtMaNV.Text = "";

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaCa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã ca", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaCa.Focus();
                return;
            }

            if (txtBatDau.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập giá sản phẩm
            {
                MessageBox.Show("Bạn phải mã nhân viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtBatDau.Focus();
                return;
            }

            if (txtKetThuc.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập tình trạng sản phẩm
            {
                MessageBox.Show("Bạn phải nhập họ nhân viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtKetThuc.Focus();
                return;
            }
            if (txtMaNV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNV.Focus();
                return;
            }



            try
            {
                

                SqlCommand cmd = new SqlCommand("proc_AddPhanCa", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.Add("@maca", SqlDbType.NChar, 10).Value = txtMaCa.Text;
                cmd.Parameters.Add("@manv", SqlDbType.NChar, 10).Value = txtMaNV.Text;
                cmd.Parameters.Add("@ngaylam", SqlDbType.NChar, 10).Value = dtpNgayLam.Text;


                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Thêm ca thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }



        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                

                SqlCommand cmd = new SqlCommand("proc_XoaPhanCa", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@maca", txtMaCa.Text.Trim());
                cmd.Parameters.AddWithValue("@manv", txtMaNV.Text.Trim());
                cmd.Parameters.AddWithValue("@ngaylam", dtpNgayLam.Value);


                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Xóa ca thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;

        }

        private void dvgPhanCa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaCa.Focus();
                return;
            }

            if (e.RowIndex >= 0) // Đảm bảo chỉ lấy dữ liệu khi hàng được chọn
            {
                DataGridViewRow row = dvgPhanCa.Rows[e.RowIndex];

                // Gán dữ liệu từ các cột của hàng được chọn vào các TextBox tương ứng
                txtMaCa.Text = row.Cells["MaCa"].Value.ToString();
                txtMaNV.Text = row.Cells["MaNV"].Value.ToString();
                txtBatDau.Text = row.Cells["HoNV"].Value.ToString();
                txtKetThuc.Text = row.Cells["TenNV"].Value.ToString();
                dtpNgayLam.Text = row.Cells["NgayLam"].Value.ToString();

            }

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }




    }
}
