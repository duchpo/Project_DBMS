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
    public partial class fSanPham : Form
    {
        DataTable tblsp;
        public fSanPham()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoQua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaSP.Enabled = true;
            txtMaSP.Focus();
        }
        private void ResetValues()
        {
            txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtDonGia.Text = "";
            txtTinhTrang.Text = "";
            txtMaLoaiSP.Text = "";
            txtTenLoaiSP.Text = "";
        }

        private void fSanPham_Load(object sender, EventArgs e)
        {
            //txtMaSP.Enabled = false;
            btnLuu.Enabled = false;
            btnBoQua.Enabled = false;
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = " SELECT * FROM view_danhSachSanPham";
            tblsp = Class.Functions.GetDataToTable(sql); //Đọc dữ liệu từ bảng
            dgvSanPham.DataSource = tblsp; //Nguồn dữ liệu            
            dgvSanPham.Columns[0].HeaderText = "Mã sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[2].HeaderText = "Đơn Giá";
            dgvSanPham.Columns[3].HeaderText = "Tình Trạng";
            dgvSanPham.Columns[4].HeaderText = "Mã loại sản phẩm";
            dgvSanPham.Columns[5].HeaderText = "Tên loại sản phẩm";

            dgvSanPham.Columns[0].Width = 100;
            dgvSanPham.Columns[1].Width = 300;
            dgvSanPham.Columns[2].Width = 100;
            dgvSanPham.Columns[3].Width = 100;
            dgvSanPham.Columns[4].Width = 100;
            dgvSanPham.Columns[5].Width = 100;

            dgvSanPham.AllowUserToAddRows = false; //Không cho người dùng thêm dữ liệu trực tiếp
            dgvSanPham.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvSanPham_Click(object sender, EventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (tblsp.Rows.Count == 0) //Nếu không có dữ liệu
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaSP.Text = dgvSanPham.CurrentRow.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = dgvSanPham.CurrentRow.Cells["TenSP"].Value.ToString();
            txtDonGia.Text = dgvSanPham.CurrentRow.Cells["DonGia"].Value.ToString();
            txtTinhTrang.Text = dgvSanPham.CurrentRow.Cells["TinhTrang"].Value.ToString();
            txtMaLoaiSP.Text = dgvSanPham.CurrentRow.Cells["MaLoaiSP"].Value.ToString();
            txtTenLoaiSP.Text = dgvSanPham.CurrentRow.Cells["TenLoaiSP"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoQua.Enabled = true;
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSP.Focus();
                return;
            }
            if (txtDonGia.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập giá sản phẩm
            {
                MessageBox.Show("Bạn phải nhập giá sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGia.Focus();
                return;
            }

            if (txtTinhTrang.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập tình trạng sản phẩm
            {
                MessageBox.Show("Bạn phải nhập tình trạng sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTinhTrang.Focus();
                return;
            }

            if (txtMaLoaiSP.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập mã loại sản phẩm
            {
                MessageBox.Show("Bạn phải nhập mã loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiSP.Focus();
                return;
            }


            try
            {
                

                SqlCommand cmd = new SqlCommand("proc_themSanPhamMoi", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text.Trim());
                cmd.Parameters.AddWithValue("@DonGia", txtDonGia.Text.Trim());
                cmd.Parameters.AddWithValue("@TinhTrang", txtTinhTrang.Text.Trim());
                cmd.Parameters.AddWithValue("@MaLoaiSP", txtMaLoaiSP.Text.Trim());
                cmd.Parameters.AddWithValue("@TenLoaiSP", txtTenLoaiSP.Text.Trim());




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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblsp.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSP.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xoá không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                /*
                 * sql = "DELETE SAN_PHAM WHERE MaSP=N'" + txtMaSP.Text + "'";
                Class.Functions.RunSQL(sql);
                LoadDataGridView(); */

                try
                {

                    SqlCommand cmd = new SqlCommand("proc_xoaSAN_PHAM", Functions.Con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                    MessageBox.Show("xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    Functions.Disconnect(); // Đảm bảo đóng kết nối sau khi thực hiện thủ tục
                }
            }
            ResetValues();

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            //txtMaSP.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaSP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtTenSP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtDonGia_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }
        private void txtTinhTrang_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void txtMaLoaiSP_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql; //Lưu câu lệnh sql
            if (tblsp.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaSP.Text == "") //nếu chưa chọn bản ghi nào
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtDonGia.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTinhTrang.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaLoaiSP.Text.Trim().Length == 0) //nếu chưa nhập tên chất liệu
            {
                MessageBox.Show("Bạn chưa nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            try
            {
              

                SqlCommand cmd = new SqlCommand("proc_suaSanPham", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text.Trim());
                cmd.Parameters.AddWithValue("@DonGia", txtDonGia.Text.Trim());
                cmd.Parameters.AddWithValue("@TinhTrang", txtTinhTrang.Text.Trim());




                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Sửa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetValues();

            btnBoQua.Enabled = false;
        }

        private void dgvSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtDonGiaNhap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SendKeys.Send("{TAB}");
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void btnLuu_Click_1(object sender, EventArgs e)
        {
            string sql;
            if (txtMaSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaSP.Focus();
                return;
            }
            if (txtTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenSP.Focus();
                return;
            }
            if (txtDonGia.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập giá sản phẩm
            {
                MessageBox.Show("Bạn phải nhập giá sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDonGia.Focus();
                return;
            }

            if (txtTinhTrang.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập tình trạng sản phẩm
            {
                MessageBox.Show("Bạn phải nhập tình trạng sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTinhTrang.Focus();
                return;
            }

            if (txtMaLoaiSP.Text.Trim().Length == 0) // Kiểm tra nếu chưa nhập mã loại sản phẩm
            {
                MessageBox.Show("Bạn phải nhập mã loại sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaLoaiSP.Focus();
                return;
            }


            try
            {
              

                SqlCommand cmd = new SqlCommand("proc_themSanPhamMoi", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@MaSP", txtMaSP.Text.Trim());
                cmd.Parameters.AddWithValue("@TenSP", txtTenSP.Text.Trim());
                cmd.Parameters.AddWithValue("@DonGia", txtDonGia.Text.Trim());
                cmd.Parameters.AddWithValue("@TinhTrang", txtTinhTrang.Text.Trim());
                cmd.Parameters.AddWithValue("@MaLoaiSP", txtMaLoaiSP.Text.Trim());
                cmd.Parameters.AddWithValue("@TenLoaiSP", txtTenLoaiSP.Text.Trim());




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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            btnBoQua.Enabled = true;
            string sql = "";
            if ((txtMaSP.Text == "") && (txtTenSP.Text == "") && (txtMaLoaiSP.Text == "") && (txtTenLoaiSP.Text == ""))
            {
                MessageBox.Show("Bạn hãy nhập điều kiện tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Xóa dữ liệu hiển thị trên DataGridView trước khi tải dữ liệu mới
            dgvSanPham.DataSource = null;

            if (txtMaSP.Text != "")
            {
                sql = "EXEC pro_SearchByMaSP @MaSP='" + txtMaSP.Text + "'";
            }
            else if (txtTenSP.Text != "")
            {
                sql = "EXEC pro_SearchByTenSP @TenSP=N'" + txtTenSP.Text + "'";
            }
            else if (txtMaLoaiSP.Text != "")
            {
                sql = "EXEC pro_SearchByMaLSP @MaLSP='" + txtMaLoaiSP.Text + "'";
            }
            else if (txtTenLoaiSP.Text != "")
            {
                sql = "EXEC pro_SearchByTenLSP @TenLSP='" + txtTenLoaiSP.Text + "'";
            }
            else
            {
                MessageBox.Show("Không có điều kiện tìm kiếm phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy dữ liệu từ CSDL
            tblsp = Functions.GetDataToTable(sql);

            // Hiển thị dữ liệu lên DataGridView
            dgvSanPham.DataSource = tblsp;

            // Kiểm tra số bản ghi trả về
            if (tblsp.Rows.Count == 0)
            {
                MessageBox.Show("Không có bản ghi nào phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Có " + tblsp.Rows.Count + " bản ghi phù hợp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Đặt lại các giá trị và trạng thái
            ResetValues();
        }
    }
}