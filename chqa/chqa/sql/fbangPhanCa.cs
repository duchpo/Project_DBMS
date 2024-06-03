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
    public partial class fbangPhanCa : Form
    {
        DataTable tblbangPhanCa;
        public fbangPhanCa()
        {
            InitializeComponent();

        }

        private void fbangPhanCa_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * from v_LoadEmployee";
            tblbangPhanCa = Functions.GetDataToTable(sql);
            gridV_BangPhanCaNV.DataSource = tblbangPhanCa; //Hiển thị vào dataGridView
            gridV_BangPhanCaNV.AllowUserToAddRows = false;
            gridV_BangPhanCaNV.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
    }
}
