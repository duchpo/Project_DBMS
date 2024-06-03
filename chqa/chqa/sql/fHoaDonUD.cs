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

using COMExcel = Microsoft.Office.Interop.Excel;
namespace sql
{
    public partial class fHoaDonUD : Form
    {
        private DataTable tblHDUng_Dung;
        public fHoaDonUD()
        {
            InitializeComponent();
        }

        private void fHoaDonUD_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;

            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * From v_HoaDonUngDung";
            tblHDUng_Dung = Functions.GetDataToTable(sql); //lấy dữ liệu
            dgvHDBanHang.DataSource = tblHDUng_Dung;
            dgvHDBanHang.Columns[0].HeaderText = "Mã hóa đơn ";
            dgvHDBanHang.Columns[1].HeaderText = "Ngày mua";
            dgvHDBanHang.Columns[2].HeaderText = "Tên ứng dụng";
            dgvHDBanHang.Columns[3].HeaderText = "Mã ứng dụng";
            dgvHDBanHang.Columns[4].HeaderText = "Mã nhân viên";
            dgvHDBanHang.Columns[4].HeaderText = "Mã sản phẩm";
            dgvHDBanHang.Columns[5].HeaderText = "Số lượng";
            dgvHDBanHang.Columns[6].HeaderText = "Đơn giá";
            dgvHDBanHang.Columns[7].HeaderText = "Giá trị hóa đơn";
            dgvHDBanHang.Columns[0].Width = 150;
            dgvHDBanHang.Columns[1].Width = 130;
            dgvHDBanHang.Columns[2].Width = 100;
            dgvHDBanHang.Columns[3].Width = 80;
            dgvHDBanHang.Columns[4].Width = 100;
            dgvHDBanHang.Columns[5].Width = 100;
            dgvHDBanHang.Columns[6].Width = 100;
            dgvHDBanHang.Columns[7].Width = 100;
            dgvHDBanHang.AllowUserToAddRows = false;
            dgvHDBanHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }







        private void ResetValues()
        {
            txtMaHDBan.Text = "";
            txtNgayBan.Text = DateTime.Now.ToShortDateString();
            cboMaNhanVien.Text = "";
            cboMaKhach.Text = "";

            cboMaSanPham.Text = "";
            txtSoLuong.Text = "";

            txtThanhTien.Text = "0";
        }




     



        private void dgvHDBanHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaHDBan.Focus();
                return;
            }

            if (tblHDUng_Dung.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (e.RowIndex >= 0) // Đảm bảo chỉ lấy dữ liệu khi hàng được chọn
            {
                DataGridViewRow row = dgvHDBanHang.Rows[e.RowIndex];

                // Gán dữ liệu từ các cột của hàng được chọn vào các TextBox tương ứng
                txtMaHDBan.Text = row.Cells["MaHD_UD"].Value.ToString();
                cboMaNhanVien.Text = row.Cells["MaNV"].Value.ToString();
                cboMaKhach.Text = row.Cells["MaUngDung"].Value.ToString();
                txtNgayBan.Text = row.Cells["NgayDatHang"].Value.ToString();
                cboMaSanPham.Text = row.Cells["MaSP"].Value.ToString();
                txtSoLuong.Text = row.Cells["SL"].Value.ToString();
                txtDonGia.Text = row.Cells["DonGia"].Value.ToString();
                txtThanhTien.Text = row.Cells["TriGiaDH"].Value.ToString();
            }

            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnInHoaDon.Enabled = false;
            btnThem.Enabled = false;
            btnBoQua.Enabled = true;
            ResetValues();
/*            txtMaHDBan.Text = Functions.CreateKey("HD");*/
            LoadDataGridView();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaHDBan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hóa đơn ứng dụng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaHDBan.Focus();
                return;
            }
            if (cboMaSanPham.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaSanPham.Focus();
                return;
            }
            if (txtSoLuong.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng sản phẩm ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Focus();
                return;
            }
            if (cboMaKhach.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã ứng dụng ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKhach.Focus();
                return;
            }
            if (cboMaNhanVien.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNhanVien.Focus();
                return;
            }

            try
            {

                SqlCommand cmd = new SqlCommand("por_AddHoaDonUngDung", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@MaHD_UD", txtMaHDBan.Text.Trim());
                cmd.Parameters.AddWithValue("@MaSP", cboMaSanPham.Text.Trim());
                cmd.Parameters.AddWithValue("@SL", txtSoLuong.Text.Trim());
                cmd.Parameters.AddWithValue("@NgayDatHang", txtNgayBan.Value);
                cmd.Parameters.AddWithValue("@MaUngDung", cboMaKhach.Text.Trim());
                cmd.Parameters.AddWithValue("@MaNV", cboMaNhanVien.Text.Trim());
                cmd.Parameters.AddWithValue("@DonGia", txtDonGia.Text.Trim());
                cmd.Parameters.AddWithValue("@TriGiaDH", txtThanhTien.Text.Trim());


                cmd.ExecuteNonQuery(); // Thực thi câu lệnh SQL
                MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            /*            btnSua.Enabled = true;*/
            btnBoQua.Enabled = false;
            btnLuu.Enabled = false;
            btnInHoaDon.Enabled = true;
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaHDBan.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần in!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook;
            COMExcel.Worksheet exSheet;
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;

            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];

            // Code phần định dạng chung, như đã thấy trong mã cũ
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Shop DĐKV";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Linh Trung - Thủ Đức";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (04)38526419";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN ỨNG DỤNG";

            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT * From v_HoaDonUngDung WHERE MaHD_UD = '" + txtMaHDBan.Text + "'";
            tblThongtinHD = Functions.GetDataToTable(sql);
            // Tiếp tục với phần code, như đã thấy trong mã cũ
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Ứng dụng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][2].ToString();



            //Lấy thông tin các mặt hàng từ hóa đơn đã chọn
            sql = "SELECT * From v_HoaDonUngDung WHERE MaHD_UD = '" + txtMaHDBan.Text + "'";
            tblThongtinHang = Functions.GetDataToTable(sql);

            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:J11"].Font.Bold = true;
            exRange.Range["A11:J11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:J11"].ColumnWidth = 12;
            exRange.Range["C11:C11"].ColumnWidth = 15;
            exRange.Range["D11:D11"].ColumnWidth = 15;
            exRange.Range["K11:K11"].ColumnWidth = 15;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Mã hóa đơn";
            exRange.Range["C11:C11"].Value = "Ngày đặt";
            exRange.Range["D11:D11"].Value = "Tên ứng dụng";
            exRange.Range["E11:E11"].Value = "Mã ứng dụng";
            exRange.Range["F11:F11"].Value = "Mã nhân viên";
            exRange.Range["G11:G11"].Value = "Mã sản phẩm";
            exRange.Range["H11:H11"].Value = "Số lượng";
            exRange.Range["I11:I11"].Value = "Đơn giá";
            exRange.Range["J11:J11"].Value = "Thành tiền";
            
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][7].ToString();

            exRange = exSheet.Cells[6][hang + 17]; //Ô A1 
            exRange.Range["A1:D1"].MergeCells = true;
            exRange.Range["A1:D1"].MergeCells = true;
            exRange.Range["A1:D1"].Font.Italic = true;
            exRange.Range["A1:D1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "HCM, ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;
            exRange.Range["A2:D2"].MergeCells = true;
            exRange.Range["A2:D2"].Font.Italic = true;
            exRange.Range["A2:D2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;

            exApp.Visible = true;
            btnBoQua.Enabled = true;
        }


        private void btnBoQua_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
            btnBoQua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            /*btnSua.Enabled = true;*/
            btnLuu.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {


                    SqlCommand command = new SqlCommand("por_XoaHoaDonUngDung", Functions.Con);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@MaHD_UD", txtMaHDBan.Text);
                    command.ExecuteNonQuery();


                    ResetValues();
                    LoadDataGridView();
                    btnXoa.Enabled = false;
                    btnInHoaDon.Enabled = false;
                    MessageBox.Show("Đã xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {

            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand("proc_TimKiemHoaDonUngDung", Functions.Con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Thêm các tham số vào SqlCommand
                cmd.Parameters.AddWithValue("@NgayBatDau", dtpBatDau.Value.Date);
                cmd.Parameters.AddWithValue("@NgayKetThuc", dtpKetThuc.Value.Date);

                dataAdapter.SelectCommand = cmd;
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                // Gán DataTable vào DataGridView
                dgvHDBanHang.DataSource = dataTable;

                MessageBox.Show("Tìm kiếm hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnInHoaDon.Enabled = true;
            btnBoQua.Enabled = true;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    

}
