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
    public partial class Taikhoan : Form
    {
        DataTable tblDn;

        public Taikhoan()
        {
            InitializeComponent();
        }

        private void grid_TK_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo chỉ lấy dữ liệu khi hàng được chọn
            {
                DataGridViewRow row = grid_TK.Rows[e.RowIndex];

                // Gán dữ liệu từ các cột của hàng được chọn vào các TextBox tương ứng
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
            }
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * From DANGNHAP";
            tblDn = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            grid_TK.DataSource = tblDn; //Nguồn dữ liệu            
            grid_TK.Columns[0].HeaderText = "Mã nhân viên";
            grid_TK.Columns[1].HeaderText = "Tên đăng nhập";
            grid_TK.Columns[2].HeaderText = "Mật khẩu";
            
            grid_TK.Columns[0].Width = 100;
            grid_TK.Columns[1].Width = 300;
            grid_TK.Columns[2].Width = 100;
            


            grid_TK.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void Taikhoan_Load(object sender, EventArgs e)
        {
            btnXoa.Enabled = true;
            LoadDataGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("proc_XoaDANGNHAP", Functions.ConAdmin);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@TenDangNhap", txtTenDangNhap.Text.Trim());
                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetValues();
            btnXoa.Enabled = true;
           
        }

        private void ResetValues()
        {
            
            txtTenDangNhap.Text = "";
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TaoTK frm = new TaoTK();
            frm.Show();
            this.Hide();
        }
    }
}
