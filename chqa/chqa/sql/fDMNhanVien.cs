using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using sql.Class;
using Microsoft.Office.Interop.Excel;

namespace sql
{
    public partial class fDMNhanVien : Form
    {
        private System.Data.DataTable tblNV; //Lưu dữ liệu bảng nhân viên
        public fDMNhanVien()
        {
            InitializeComponent();
        }

        private void fDMNhanVien_Load(object sender, EventArgs e)
        {
            //txtMaNhanVien.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * From v_LoadEmployee";
            tblNV = Functions.GetDataToTable(sql); //lấy dữ liệu
            dgvNhanVien.DataSource = tblNV;
            dgvNhanVien.Columns[0].HeaderText = "Mã nhân viên";
            dgvNhanVien.Columns[1].HeaderText = "Họ nhân viên";
            dgvNhanVien.Columns[2].HeaderText = "Tên nhân viên";
            dgvNhanVien.Columns[3].HeaderText = "Ngày sinh";
            dgvNhanVien.Columns[4].HeaderText = "Giới tính";
            dgvNhanVien.Columns[5].HeaderText = "Địa chỉ";
            dgvNhanVien.Columns[6].HeaderText = "Số điện thoại";
            dgvNhanVien.Columns[7].HeaderText = "Tên công việc";
            dgvNhanVien.Columns[8].HeaderText = "Tên công việc";
            dgvNhanVien.Columns[9].HeaderText = "Số ca";
            dgvNhanVien.Columns[10].HeaderText = "Thưởng";
            dgvNhanVien.Columns[11].HeaderText = "Ngày tuyển dụng";
            dgvNhanVien.Columns[0].Width = 100;
            dgvNhanVien.Columns[1].Width = 150;
            dgvNhanVien.Columns[2].Width = 100;
            dgvNhanVien.Columns[3].Width = 150;
            dgvNhanVien.Columns[4].Width = 100;
            dgvNhanVien.Columns[5].Width = 100;
            dgvNhanVien.Columns[6].Width = 100;
            dgvNhanVien.Columns[7].Width = 100;
            dgvNhanVien.Columns[8].Width = 100;
            dgvNhanVien.Columns[9].Width = 100;
            dgvNhanVien.Columns[10].Width = 100;
            dgvNhanVien.Columns[11].Width = 150;
            dgvNhanVien.AllowUserToAddRows = false;
            dgvNhanVien.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvNhanVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaNhanVien.Focus();
                return;
            }
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (e.RowIndex >= 0) // Đảm bảo chỉ lấy dữ liệu khi hàng được chọn
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];

                // Gán dữ liệu từ các cột của hàng được chọn vào các TextBox tương ứng
                txtMaNhanVien.Text = row.Cells["MaNV"].Value.ToString();
                txtHoNhanVien.Text = row.Cells["HoNV"].Value.ToString();
                txtTenNhanVien.Text = row.Cells["TenNV"].Value.ToString();
                mskNgaySinh.Text = row.Cells["NgaySinh"].Value.ToString();
                if (row.Cells["GioiTinh"].Value.ToString() == "Nam")
                    chkGioiTinh.Checked = true;
                else
                    chkGioiTinh.Checked = false;
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtMaCV.Text = row.Cells["MaCV"].Value.ToString();
                txtSoCa.Text = row.Cells["SoCa"].Value.ToString();
                txtThuong.Text = row.Cells["Thuong"].Value.ToString();
                txtCongViec.Text = row.Cells["TenCV"].Value.ToString();
                mskNgaySinhTuyenDung.Text = row.Cells["NgayTuyenDung"].Value.ToString();

            }

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaNhanVien.Enabled = true;
            txtMaNhanVien.Focus();
        }
        private void ResetValues()
        {
            txtMaNhanVien.Text = "";
            txtHoNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            chkGioiTinh.Checked = false;
            txtDiaChi.Text = "";
            mskNgaySinh.Text = "";
            txtSDT.Text = "";
            txtMaCV.Text = "";
            txtSoCa.Text = "";
            txtThuong.Text = "";
            mskNgaySinhTuyenDung.Text = "";

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql, gt;
            if (txtMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                return;
            }
            if (txtHoNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Ho nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoNhanVien.Focus();
                return;
            }
            if (txtTenNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhanVien.Focus();
                return;
            }
            if (txtDiaChi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (txtMaCV.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã công việc", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaCV.Focus();
                return;
            }
            if (txtSoCa.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số ca", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoCa.Focus();
                return;
            }
            if (txtThuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập Thưởng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtThuong.Focus();
                return;
            }
            if (txtSDT.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSDT.Focus();
                return;
            }

            gt = (chkGioiTinh.Checked) ? "Nam" : "Nữ";
/*            sql = "SELECT MaNV FROM NHAN_VIEN WHERE MaNV=N'" + txtMaNhanVien.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNhanVien.Focus();
                txtMaNhanVien.Text = "";
                return;
            }*/

            try
            {

                SqlCommand cmd = new SqlCommand("proc_AddEmployee", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@manv", txtMaNhanVien.Text.Trim());
                cmd.Parameters.AddWithValue("@ho", txtHoNhanVien.Text.Trim());
                cmd.Parameters.AddWithValue("@ten", txtTenNhanVien.Text.Trim());
                cmd.Parameters.AddWithValue("@ns", mskNgaySinh.Value);
                cmd.Parameters.AddWithValue("@gt", gt);
                cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text.Trim());
                cmd.Parameters.AddWithValue("@sdt", txtSDT.Text.Trim());
                cmd.Parameters.AddWithValue("@macv", txtMaCV.Text.Trim());
                cmd.Parameters.AddWithValue("@soca", txtSoCa.Text.Trim());
                cmd.Parameters.AddWithValue("@thuong", txtThuong.Text.Trim());
                cmd.Parameters.AddWithValue("@ntd", mskNgaySinhTuyenDung.Value);

                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Thêm nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            string manv = txtMaNhanVien.Text.Trim();
            string honv = txtHoNhanVien.Text.Trim();
            string tennv = txtTenNhanVien.Text.Trim();
            DateTime ngaysinh = mskNgaySinh.Value;
            string gioitinh = chkGioiTinh.Checked ? "Nam" : "Nữ";
            string diachi = txtDiaChi.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string macv = txtMaCV.Text.Trim();
            int soca, thuong;
            DateTime ngaytuyendung;

            if (!int.TryParse(txtSoCa.Text.Trim(), out soca))
            {
                MessageBox.Show("Số ca không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(txtThuong.Text.Trim(), out thuong))
            {
                MessageBox.Show("Số tiền thưởng không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ngaytuyendung = mskNgaySinhTuyenDung.Value;

            try
            {

                SqlCommand cmd = new SqlCommand("proc_EditEmployee", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@manv", manv);
                cmd.Parameters.AddWithValue("@honv", honv);
                cmd.Parameters.AddWithValue("@tennv", tennv);
                cmd.Parameters.AddWithValue("@ngaysinh", ngaysinh);
                cmd.Parameters.AddWithValue("@gioitinh", gioitinh);
                cmd.Parameters.AddWithValue("@diachi", diachi);
                cmd.Parameters.AddWithValue("@sdt", sdt);
                cmd.Parameters.AddWithValue("@macv", macv);
                cmd.Parameters.AddWithValue("@soca", soca);
                cmd.Parameters.AddWithValue("@thuong", thuong);
                cmd.Parameters.AddWithValue("@ngaytuyendung", ngaytuyendung);

                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Sửa thông tin nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                      ResetValues();
            btnBoQua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string manv = txtMaNhanVien.Text.Trim();
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (manv == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {

                SqlCommand cmd = new SqlCommand("proc_DeleteEmployee", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@MaNV", manv);

                // Thực thi câu lệnh SQL
                cmd.ExecuteNonQuery();
                MessageBox.Show("Xóa nhân viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
                ResetValues();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            //txtMaNhanVien.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
                this.Close();
                    }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            btnBoQua.Enabled = true;
            string sql = "";
            if ((txtMaNhanVien.Text == "") && (txtTenNhanVien.Text == "") && (txtSDT.Text == "")&&(txtCongViec.Text ==""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xóa dữ liệu hiển thị trên DataGridView trước khi tải dữ liệu mới
            dgvNhanVien.DataSource = null;

            if (txtMaNhanVien.Text != "")
            {
                sql = "EXEC pro_SearchByMaNV @MaNV='" + txtMaNhanVien.Text + "'";
            }
            else if (txtTenNhanVien.Text != "")
            {
                sql = "EXEC pro_SearchByTenNV @TenNV=N'" + txtTenNhanVien.Text + "'";
            }
            else if (txtSDT.Text != "")
            {
                sql = "EXEC pro_SearchBySDT @SDT='" + txtSDT.Text + "'";
            }
            else if (txtCongViec.Text != "")
            {
                sql = "EXEC pro_SearchByTenCV @TenCV='" + txtCongViec.Text + "'";
            }
            else
            {
                MessageBox.Show("Không có điều kiện tìm kiếm phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dữ liệu từ CSDL
            tblNV = Functions.GetDataToTable(sql);

            // Hiển thị dữ liệu lên DataGridView
            dgvNhanVien.DataSource = tblNV;

            // Kiểm tra số bản ghi trả về
            if (tblNV.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có " + tblNV.Rows.Count + " bản ghi phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Đặt lại các giá trị và trạng thái
            ResetValues();
            

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mskNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            Luong frm = new Luong();
            frm.Show();
        }
    }
    
}
