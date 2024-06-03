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
    public partial class TaoTK : Form
    {
        public TaoTK()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("proc_ThemTaiKhoan", Functions.ConAdmin);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@manv", txtMaNV.Text);
            cmd.Parameters.AddWithValue("@tk", txtTK.Text);
            cmd.Parameters.AddWithValue("@mk", txtMK.Text);
            if (cmd.ExecuteNonQuery() > 0)
            {
                MessageBox.Show("Tạo tài khoản thành công");
               
            }
            else
            {
                MessageBox.Show("Tạo tài khoản thất bại, vui lòng nhập lại");
                
            }
        }

        private void TaoTK_Load(object sender, EventArgs e)
        {

        }
    }
}
