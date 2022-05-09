using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MainFuncitonClass;
using System.Data.SqlClient;

namespace WindowsFormsApp14
{
    public partial class Form1 : Form
    {
        ConnectionDataBase DATABASE = new ConnectionDataBase();
        public Form1()
        {
            InitializeComponent();
        }
        private DataTable TruyVan(string command )
        {
            SqlDataAdapter adapter = new SqlDataAdapter(command,DATABASE.connection());
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }
        //private DataTable Load ()
        //{

        //}
        public void LoadTreeview ()
        {
            string commandNodecha = "SELECT * from lop";    
            DataTable tablecha = TruyVan(commandNodecha);
            try
            {
             for(int i = 0; i< tablecha.Rows.Count;i++ )
                {
                    treeView1.Nodes.Add(tablecha.Rows[i]["Tenlop"].ToString());
                    DataTable tablecon = new DataTable(); 
                    tablecon= TruyVan(String.Format("GetNameByclass {0}", tablecha.Rows[i]["malop"].ToString()));
                    for(int j = 0; j < tablecon.Rows.Count;j++ )
                    {
                        treeView1.Nodes[i].Nodes.Add(tablecon.Rows[j]["hoten"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void LopHocComboBox()
        {
            string sql = "select * from lop";
            cbx_lophoc.DataSource = TruyVan(sql);
            cbx_lophoc.DisplayMember = "Tenlop";
            cbx_lophoc.ValueMember = "Malop";
        }
        private void LoadDataGridView(string id)
        {
            string command = String.Format("GridByName N'{0}'", id);
            DataTable table = TruyVan(command);
            dataGridView1.DataSource = table;

        }
        private void CountStudent()
        {
            int sinhvien = 0;
            int sinhviennu = 0;
            sinhvien = dataGridView1.RowCount-1;
            for(int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if(dataGridView1.Rows[i].Cells[3].Value.ToString() == "Nữ")
                {
                    sinhviennu += 1;
                }    
            }
   
            txt_tongsinhvien.Text = sinhvien.ToString();
            txt_tongsinhviennu.Text = sinhviennu.ToString();
        }
        private void AddStudent()
        {
            string hoten = txt_hoten.Text;
            string quequan = txt_quequan.Text;
            string ngaysinh = txt_date.Value.ToString();
            string masv = txt_masv.Text;
            string lophoc = cbx_lophoc.SelectedValue.ToString();
            string gioitinh = "";
            if(checkBox1.Checked)
            {
                gioitinh = "1";
            }
            else
            {
                gioitinh = "0";
            }
            string command = String.Format("AddStuden '{0}',N'{1}','{2}','{3}','{4}','{5}'", masv, hoten, ngaysinh, gioitinh, quequan,lophoc) ;
            SqlCommand sqlCommand = new SqlCommand(command,DATABASE.connection());
            sqlCommand.Connection.Open();
            if(sqlCommand.ExecuteNonQuery()>0)
            {
                MessageBox.Show("Them Sinh Vien THanh cong");
                sqlCommand.Connection.Close();

            }
            else
            {
                MessageBox.Show("Them Sinh Vien That Bai");
                sqlCommand.Connection.Close();
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTreeview();
            LopHocComboBox();
        }
      
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            LoadDataGridView(treeView1.SelectedNode.Text);
            CountStudent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStudent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
