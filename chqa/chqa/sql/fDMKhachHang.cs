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
    public partial class fDMKhachHang : Form
    {
        DataTable tblKH; //Bảng khách hàng
        public fDMKhachHang()
        {
            InitializeComponent();
        }

        
        private void fDMKhachHang_Load(object sender, EventArgs e)
        {
            //txtMaKhach.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * from KHACH_HANG";
            tblKH = Functions.GetDataToTable(sql); //Lấy dữ liệu từ bảng
            dgvKhachHang.DataSource = tblKH; //Hiển thị vào dataGridView
            dgvKhachHang.Columns[0].HeaderText = "Mã khách";
            dgvKhachHang.Columns[1].HeaderText = "Tên khách";
            dgvKhachHang.Columns[2].HeaderText = "Điện thoại";
            dgvKhachHang.Columns[3].HeaderText = "Điểm TL";
            dgvKhachHang.Columns[0].Width = 100;
            dgvKhachHang.Columns[1].Width = 150;
            dgvKhachHang.Columns[2].Width = 150;
            dgvKhachHang.Columns[3].Width = 150;
            dgvKhachHang.AllowUserToAddRows = false;
            dgvKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvKhachHang_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaKhach.Focus();
                return;
            }
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaKhach.Text = dgvKhachHang.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKhach.Text = dgvKhachHang.CurrentRow.Cells["TenKH"].Value.ToString();
            txtDiemTL.Text = dgvKhachHang.CurrentRow.Cells["DiemTL"].Value.ToString();
            txtSDT.Text = dgvKhachHang.CurrentRow.Cells["SDT"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaKhach.Enabled = true;
            txtMaKhach.Focus();
        }
        private void ResetValues()
        {
            txtMaKhach.Text = "";
            txtTenKhach.Text = "";
            txtDiemTL.Text = "";
            txtSDT.Text = "";
        }

        
        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaKH = txtMaKhach.Text;
            string TenKH = txtTenKhach.Text;
            string SDT = txtSDT.Text;

            if (string.IsNullOrEmpty(MaKH))
            {
                MessageBox.Show("Please enter a valid value for 'MaKH'.");
                return;
            }

            try
            {

                SqlCommand cmd = new SqlCommand("UpdateKhachHang", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MaKH", SqlDbType.NChar).Value = MaKH;
                cmd.Parameters.Add("@TenKH", SqlDbType.NVarChar).Value = TenKH;
                cmd.Parameters.Add("@SDT", SqlDbType.NChar).Value = SDT;

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Cập nhật thành công!", "Update Customer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng với MaKH này.", "Update Customer", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            btnBoQua.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            //ResetValues();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            //txtMaKhach.Enabled = false;
            LoadDataGridView();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblKH.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaKhach.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá bản ghi này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                sql = "DELETE KHACH_HANG WHERE MaKH=N'" + txtMaKhach.Text + "'";
                Functions.RunSQL(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtTimKiem_Click(object sender, EventArgs e)
        {
            string sdt = txtSDT.Text.Trim();


            try
            {
                SqlCommand cmd = new SqlCommand("SELECT dbo.SearchTenKHBySDT(@SDT)", Functions.Con);
                cmd.Parameters.AddWithValue("@SDT", sdt);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    string tenKH = result.ToString();
                    // Hiển thị TenKH trong TextBox
                    txtTenKhach.Text = tenKH;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng với số điện thoại này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {



            try
            {


                SqlCommand cmd = new SqlCommand("InsertNewKhachHang", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@MaKH", txtMaKhach.Text.Trim());
                cmd.Parameters.AddWithValue("@TenKH", txtTenKhach.Text.Trim());
                cmd.Parameters.AddWithValue("@SDT", txtSDT.Text.Trim());
                cmd.Parameters.AddWithValue("@DiemTL", txtDiemTL.Text.Trim());





                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
    }
}
