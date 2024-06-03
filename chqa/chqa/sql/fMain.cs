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
    public partial class fMain : Form
    {
        private string role;

        public fMain(string role)
        {
            InitializeComponent();
            this.role = role;
            UpdateLabelMessage(); // Gọi hàm cập nhật nhãn ngay khi khởi tạo form


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*string backgroundImagePath = "D:\\CSDL_visualstudio\\sql (1)\\sql (1)\\sql\\Anh.png";

            // Kiểm tra xem tệp tin ảnh tồn tại hay không
            if (System.IO.File.Exists(backgroundImagePath))
            {
                // Đặt ảnh nền cho Form
                this.BackgroundImage = System.Drawing.Image.FromFile(backgroundImagePath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }*/
            
        }
        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Class.Functions.Disconnect(); //Đóng kết nối
            Application.Exit(); //Thoát
        }


        private void mnuTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void mnuDanhMuc_Click(object sender, EventArgs e)
        {

        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            fDMNhanVien frm = new fDMNhanVien();
            frm.Show();
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            fDMKhachHang   frm = new fDMKhachHang();
            frm.Show();
        }

        private void mnuHangHoa_Click(object sender, EventArgs e)
        {
            fSanPham frm = new fSanPham();
            frm.Show();
        }

        private void mnuHoaDon_Click(object sender, EventArgs e)
        {
            
        }

        private void mnuFindHoaDon_Click(object sender, EventArgs e)
        {
            fTimKiemHD frm = new fTimKiemHD();
            frm.Show();
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            fHoaDonBanHang frm = new fHoaDonBanHang();
            frm.Show();
        }

        private void phânCaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPhanCa frm = new fPhanCa();
            frm.Show();
        }

        private void bảngPhânCaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fbangPhanCa frm = new fbangPhanCa();
            frm.Show();
        }

        private void hóaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fHoaDonUD frm = new fHoaDonUD();
            frm.Show();
        }

        private void phiếuChiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fPhieuChi frm = new fPhieuChi();
            frm.Show();
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(role == "Quanly")
            {
                Taikhoan frm = new Taikhoan();
                frm.Show();
            }
            else
            {
                MessageBox.Show("Bạn không có quyền này");
            }
                
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void lbTB_Click(object sender, EventArgs e)
        {
            if (role == "Quanly")
            {
                lbTB.Text = "Bạn đang đăng nhập với quyền quản lý";
            }
            else if (role == "Staff")
            {
                lbTB.Text = "Bạn đang đăng nhập với quyền nhân viên";
            }
            else
            {
                lbTB.Text = "Bạn đang đăng nhập với quyền không xác định";
            }
        }

        private void UpdateLabelMessage()
        {
            if (role == "Quanly")
            {
                lbTB.Text = "Bạn đang đăng nhập với quyền quản lý";
            }
            else if (role == "Staff")
            {
                lbTB.Text = "Bạn đang đăng nhập với quyền nhân viên";
            }
            else
            {
                lbTB.Text = "Bạn đang đăng nhập với quyền không xác định";
            }
        }
    }
}
