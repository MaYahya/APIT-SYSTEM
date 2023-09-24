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

namespace APIT
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            ShowSalary();
            GetEmpId();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\ApitDB.mdf;Integrated Security=True;Connect Timeout=30");

        private void ShowSalary()
        {
            try
            {
                Con.Open();
                string Query = "Select * from EmpSalaryTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var empdata = new DataSet();
                sda.Fill(empdata);
                DataGridView1.DataSource = empdata.Tables[0];
                DataGridView1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                if (Con.State == ConnectionState.Open)
                {
                    Con.Close(); // Ensure the connection is closed even in case of an exception.
                }
            }
        }
        private void GetEmpId()
        {
            Con.Open();

            SqlCommand cmd = new SqlCommand("Select * from EmployeeTbl", Con);
            SqlDataReader Rdr = cmd.ExecuteReader();
            DataTable td = new DataTable();
            td.Columns.Add("EmpId", typeof(int));
            td.Load(Rdr);
            comboBox1.ValueMember = "EmpId";
            comboBox1.DataSource = td;

          Con.Close();
        }

        private void GetEmpName()
        {
            Con.Open();
            string Query = "Select * from EmployeeTbl where EmpId=" + comboBox1.SelectedValue.ToString()+"";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                textBox1.Text = dr["EmpName"].ToString();
            }


            Con.Close();

        }

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            Key = 0;
           
           
        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.SelectedIndex == -1 ||
                textBox4.Text == "" || textBox5.Text == "" ) 
            {
                MessageBox.Show("Please Insert Information");
            }
            else
            {
                try
                {

                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmpSalaryTbl(EmpId,EmpName,EmpSal,EmpCola,EmpHealth,EmpOver) values(@EI,@EN,@EB,@EC,@EH,@EO)", Con);
                    cmd.Parameters.AddWithValue("@EI", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@EN", textBox1.Text);
                    cmd.Parameters.AddWithValue("@EB", textBox2.Text);
                    cmd.Parameters.AddWithValue("@EC", textBox3.Text);
                    cmd.Parameters.AddWithValue("@EH", textBox4.Text);
                    cmd.Parameters.AddWithValue("@EO", textBox5.Text);




                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration Success");
                    Con.Close();
                    ShowSalary();
                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox1.SelectedIndex == -1
                || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Please Insert Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmpSalaryTbl set EmpId=@EI,EmpName=@EN,EmpSal=@EB,EmpCola=@EC,EmpHealth=@EH,EmpOver=@EO where SalaryId=@EmpSalKey", Con);
                    cmd.Parameters.AddWithValue("@EI", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@EN", textBox1.Text);
                    cmd.Parameters.AddWithValue("@EB", textBox2.Text);
                    cmd.Parameters.AddWithValue("@EC", textBox3.Text);
                    cmd.Parameters.AddWithValue("@EH", textBox4.Text);
                    cmd.Parameters.AddWithValue("@EO", textBox5.Text);
                    cmd.Parameters.AddWithValue("@EmpSalKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Edited");
                    Con.Close();
                    ShowSalary();
                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        int Key = 0;
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < DataGridView1.Rows.Count)
            {
                DataGridViewRow selectedRow = DataGridView1.Rows[e.RowIndex];
               

                textBox1.Text = selectedRow.Cells[2].Value.ToString();
                comboBox1.SelectedItem = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[3].Value.ToString();
                textBox3.Text = selectedRow.Cells[4].Value.ToString();
                textBox4.Text = selectedRow.Cells[5].Value.ToString();
                textBox5.Text = selectedRow.Cells[6].Value.ToString();

                Key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 obj = new Form2();
            obj.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 obj = new Form3();
            obj.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 obj = new Form5();
            obj.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetEmpName();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Please Insert Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from EmpSalaryTbl where SalaryId=@EmpSalKey", Con);
                    cmd.Parameters.AddWithValue("@EmpSalKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Deleted");
                    Con.Close();
                    ShowSalary();
                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
    }
}
