using sql.Class;
using System;
using System.Collections;
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
    public partial class Dangnhap : Form
    {
        DataTable tblDN; //Bảng đăng nhập

        public Dangnhap()
        {
            InitializeComponent();
        }

        private void btnDN_Click(object sender, EventArgs e)
        {
            string username = txtTK.Text.Trim();
            string password = txtMK.Text.Trim();

            var authResult = AuthenticateUser(username, password);

            if (authResult.Item1)
            {
                MessageBox.Show("Đăng nhập thành công!");

                string role = authResult.Item2;
                Class.Functions.Connect(username, password);
                // Open the main form with appropriate permissions based on the role
                fMain mainForm = new fMain(role);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
            }
        }

        private Tuple<bool, string> AuthenticateUser(string username, string password)
        {

            using (SqlCommand cmd = new SqlCommand("dbo.proc_CheckLogin", Functions.ConAdmin))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                SqlParameter outputParamResult = new SqlParameter("@result", SqlDbType.Bit);
                outputParamResult.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParamResult);

                SqlParameter outputParamRole = new SqlParameter("@role", SqlDbType.NVarChar, 50);
                outputParamRole.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParamRole);

                cmd.ExecuteNonQuery();

                bool result = (bool)outputParamResult.Value;
                string role = outputParamRole.Value.ToString();

                return Tuple.Create(result, role);
            }
        }


        private void Dangnhap_Load(object sender, EventArgs e)
        {
            Class.Functions.ConnectAdmin(); //Mở kết nối

        }

        private void btnTTK_Click(object sender, EventArgs e)
        {
            
        }

        ////////////


        public class Roles
        {
            public const string Staff = "Staff";
            public const string Quanly = "Quanly";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }

}





