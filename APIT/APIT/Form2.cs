using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace APIT
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            ShowEmployees();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\ApitDB.mdf;Integrated Security=True;Connect Timeout=30");


        
        private void ShowEmployees()
        {
            try
            {
                Con.Open();
                string Query = "Select * from EmployeeTbl";
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

        private void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
        }


        private void button6_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox2.SelectedIndex == -1 ||
                comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Please Insert Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into EmployeeTbl(EmpName,EmpAdd,EmpGen,EmpDOB,EmpJob,EmpGd,EmpMob,EmpApp) values(@EN,@EA,@EG,@ED,@EJ,@EGD,@EM,@EAP)", Con);
                    cmd.Parameters.AddWithValue("@EN", textBox1.Text);
                    cmd.Parameters.AddWithValue("@EA", textBox2.Text);
                    cmd.Parameters.AddWithValue("@EG", comboBox2.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ED", DOBdate.Value.Date);
                    cmd.Parameters.AddWithValue("@EJ", comboBox3.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EGD", comboBox4.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EM", textBox3.Text);
                    cmd.Parameters.AddWithValue("@EAP", AppDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration Success");
                    Con.Close();
                    ShowEmployees();
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

                textBox1.Text = selectedRow.Cells[1].Value.ToString();
                textBox2.Text = selectedRow.Cells[2].Value.ToString();
                comboBox2.SelectedItem = selectedRow.Cells[3].Value.ToString();
                DOBdate.Value = Convert.ToDateTime(selectedRow.Cells[4].Value); // Update DOBdate.Value
                comboBox3.SelectedItem = selectedRow.Cells[5].Value.ToString();
                comboBox4.SelectedItem = selectedRow.Cells[6].Value.ToString();
                textBox3.Text = selectedRow.Cells[7].Value.ToString();
                AppDate.Value = Convert.ToDateTime(selectedRow.Cells[8].Value); // Update AppDate.Value

                Key = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || comboBox2.SelectedIndex == -1 ||
               comboBox3.SelectedIndex == -1 || comboBox4.SelectedIndex == -1)
            {
                MessageBox.Show("Please Insert Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update EmployeeTbl set EmpName=@EN,EmpAdd=@EA,EmpGen=@EG,EmpDOB=@ED,EmpJob=@EJ,EmpGd=@EGD,EmpMob=@EM,EmpApp=@EAP where EmpId=@EmpKey", Con);
                    cmd.Parameters.AddWithValue("@EN", textBox1.Text);
                    cmd.Parameters.AddWithValue("@EA", textBox2.Text);
                    cmd.Parameters.AddWithValue("@EG", comboBox2.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ED", DOBdate.Value.Date);
                    cmd.Parameters.AddWithValue("@EJ", comboBox3.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EGD", comboBox4.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@EM", textBox3.Text);
                    cmd.Parameters.AddWithValue("@EAP", AppDate.Value.Date);
                    cmd.Parameters.AddWithValue("@EmpKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Edited");
                    Con.Close();
                    ShowEmployees();
                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (Key==0)
            {
                MessageBox.Show("Please Insert Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from EmployeeTbl where EmpId=@EmpKey", Con);
                    cmd.Parameters.AddWithValue("@EmpKey", Key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Successfully Deleted");
                    Con.Close();
                    ShowEmployees();
                    clear();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
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

        private void button5_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form5 obj = new Form5();
            obj.Show();
            this.Hide();
        }
    }
}

