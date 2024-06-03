using sql.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sql
{
    public partial class Luong : Form
    {
        public Luong()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnTinh_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn tháng và năm chưa
            if (string.IsNullOrEmpty(txtThang.Text) || string.IsNullOrEmpty(txtNam.Text))
            {
                MessageBox.Show("Vui lòng nhập tháng và năm để tính lương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int thang, nam;
            // Kiểm tra xem dữ liệu nhập vào có đúng định dạng số hay không
            if (!int.TryParse(txtThang.Text, out thang) || !int.TryParse(txtNam.Text, out nam))
            {
                MessageBox.Show("Tháng và năm phải là số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dữ liệu lương từ hàm func_tinhLuongNV trong CSDL
            string sql = "SELECT * FROM func_tinhLuongCung(" + thang + ", " + nam + ")";
            DataTable result = Functions.GetDataToTable(sql);

            // Hiển thị dữ liệu lương trên DataGridView hoặc control tương tự
            dgvLuongNhanVien.DataSource = result;

            // Thông báo khi hoàn tất tính toán
            MessageBox.Show("Đã tính toán lương cứng cho tháng " + thang + "/" + nam, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnTinhLuongCa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem người dùng đã chọn tháng và năm chưa
            if (string.IsNullOrEmpty(txtThang.Text) || string.IsNullOrEmpty(txtNam.Text))
            {
                MessageBox.Show("Vui lòng nhập tháng và năm để tính lương!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int thang, nam;
            // Kiểm tra xem dữ liệu nhập vào có đúng định dạng số hay không
            if (!int.TryParse(txtThang.Text, out thang) || !int.TryParse(txtNam.Text, out nam))
            {
                MessageBox.Show("Tháng và năm phải là số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dữ liệu lương từ hàm func_tinhLuongNV trong CSDL
            string sql = "SELECT * FROM func_tinhLuongCa(" + thang + ", " + nam + ")";
            DataTable result = Functions.GetDataToTable(sql);

            // Hiển thị dữ liệu lương trên DataGridView hoặc control tương tự
            dgvLuongNhanVien.DataSource = result;

            // Thông báo khi hoàn tất tính toán
            MessageBox.Show("Đã tính toán lương theo ca cho tháng " + thang + "/" + nam, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Luong_Load(object sender, EventArgs e)
        {

        }
    }
}
