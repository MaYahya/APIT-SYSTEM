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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            ShowApit();
            GetEmpId();
            textBox1.Visible = false;
            label3.Visible = false;
            textBox2.Visible = false;
            label5.Visible = false;
            textBox4.Visible = false;
            textBox3.Visible = false;
            textBox5.Visible = false;
            label8.Visible = false;
            label7.Visible = false;
            label6.Visible = false;
        }


        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\Documents\ApitDB.mdf;Integrated Security=True;Connect Timeout=30");


        private void ShowApit() {
            try
            {
                Con.Open();
                string Query = "Select * From ApitTbl";
                SqlDataAdapter sda = new SqlDataAdapter(Query,Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(sda);
                var empdata = new DataSet();
                sda.Fill(empdata);
                DataGridView1.DataSource = empdata.Tables[0];
                DataGridView1.Refresh();

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error");
            }
            finally
            {
                if(Con.State==ConnectionState.Open){
                    Con.Close();
                  
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
            comboBox1.SelectedIndex = -1;
            Con.Close();
        }

        private void GetEmpName()
        {
            Con.Open();
            string Query = "Select * from EmployeeTbl where EmpId=" + comboBox1.SelectedValue.ToString() + "";
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
        int sal = 0;
        private void GetEmpSal()
        {
            Con.Open();
            string Query = "Select * from EmpSalaryTbl where EmpId=" + comboBox1.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                sal = Convert.ToInt32(dr["EmpSal"]);
            }


            Con.Close();

        }
        int a = 0; int b = 0; int c = 0;
        private void GetEmpTot1()
        {
           
            Con.Open();
            
            string Query = "Select EmpCola from EmpSalaryTbl where EmpId=" + comboBox1.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                a = Convert.ToInt32(dr["EmpCola"]);
            }
          
          
            Con.Close();

        }
        private void GetEmpTot2()
        {

            Con.Open();

            string Query = "Select EmpHealth from EmpSalaryTbl where EmpId=" + comboBox1.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                b = Convert.ToInt32(dr["EmpHealth"]);
            }

          
            Con.Close();

        }
        private void GetEmpTot3()
        {

            Con.Open();

            string Query = "Select EmpOver from EmpSalaryTbl where EmpId=" + comboBox1.SelectedValue.ToString() + "";
            SqlCommand cmd = new SqlCommand(Query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                c = Convert.ToInt32(dr["EmpOver"]);
            }

           
            Con.Close();

        }
        int tot = 0;
        private void Gettot()
        {
           
            GetEmpTot3(); GetEmpTot2(); GetEmpTot1(); GetEmpSal();
            tot = (a + b + c + sal);
            textBox4.Text = tot.ToString();
            textBox3.Text = (a + b + c).ToString();
            if((sal+a+b+c) > 100000)
            {
                textBox5.Text = (tot - 100000).ToString();
            }
            else
            {
                textBox5.Text = "No tax";
                MessageBox.Show("No tax ");
            }
        }
        
        
        float tb1 = 0.0f;
        float tb2 = 0.0f;
        float tb3 = 0.0f;
        float tb4 = 0.0f;
        float tb5 = 0.0f;
        float tb6 = 0.0f;
        float t6 = 0.0f;
        float t12 = 0.0f;
        float t18 = 0.0f;
        float t24 = 0.0f;
        float t30 = 0.0f;
        float t36 = 0.0f;
        float tt = 0.0f;
        private void Calculate()
        {
            Gettot();
            if (tot > 100000)
            {
                tb1 = (tot - 100000) * 12;
            }
            if (tb1 >= 500000)
            {
                tb2 = (tb1 - 500000);
                t6 = (500000 / 100 * 6) / 12;
                textBox6.Text = t6.ToString();
               

            }
            else
            {
                t6 = (tb1 / 100 * 6) / 12;
                textBox6.Text = t6.ToString();
                float tb7 = tb1 / 12;


            }
            if (tb2 >= 500000)
            {
                tb3 = (tb2 - 500000);
                t12 = (500000 / 100 * 12) / 12;
                textBox7.Text = t12.ToString();
               
              
            }
            else
            {
                t12 = (tb2 / 100 * 12) / 12;
                textBox7.Text = t12.ToString();
                float tb8 = tb2 / 12;
               
            }

            if (tb3 >= 500000)
            {
                tb4 = (tb3 - 500000);
                t18 = (500000 / 100 * 18) / 12;
                textBox8.Text = t18.ToString();  
              
              
            }
            else
            {
                t18 = (tb3 / 100 * 18) / 12;
                textBox8.Text = t18.ToString();
                float tb9 = tb3 / 12;
               
            }

            if (tb4 >= 500000)
            {
                tb5 = (tb4 - 500000);
                t24 = (500000 / 100 * 24) / 12;
                textBox11.Text = t24.ToString(); 
               
              
            }
            else
            {
                t24 = (tb4 / 100 * 24) / 12;
                textBox11.Text = t24.ToString();
                float tb10 = tb4 / 12;
             
            }

            if (tb5 >= 500000)
            {
                tb6 = (tb5 - 500000);
                t30 = (500000 / 100 * 30) / 12;
                textBox10.Text = t30.ToString(); 
            
                
            }
            else
            {
                t30 = (tb5 / 100 * 30) / 12;
                textBox10.Text = t30.ToString();
                float tb11 = tb5 / 12;
               
            }

           
            t36 = (tb6 / 100 * 36) / 12;
            textBox9.Text = t36.ToString();
            float tb12 = tb6 / 12;
         

             tt = (t6 + t12 + t18 + t24 + t30 + t36);
           
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                textBox1.Visible = true;
                label3.Visible = true; textBox2.Visible = true;
                label5.Visible = true; textBox4.Visible = true; textBox3.Visible = true; textBox5.Visible = true;
                label8.Visible = true;
                label7.Visible = true; label6.Visible = true;

            }
        }
 

        private void comboBox1_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            GetEmpName();

            GetEmpSal();
            textBox2.Text = sal.ToString(); 
            Gettot();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {

            label9.Visible = true; label10.Visible = true; label11.Visible = true; label13.Visible = true; label14.Visible = true;
            label15.Visible = true; textBox8.Visible = true; textBox7.Visible = true; textBox6.Visible = true;
            textBox9.Visible = true; textBox10.Visible = true; textBox11.Visible = true;
            Calculate();


            try
            {

                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into ApitTbl(EmpId,EmpName,EmpSal,TotalAllow,TotalInco,EmptaxAm,TexSix,TexTwel,TexEig,TexTwen,TexThirty,TexThirtySix,Total_Apit) values(@EI,@EN,@ES,@EA,@EIN,@ET,@E6,@E12,@E18,@E24,@E30,@E36,@ETO)", Con);
                cmd.Parameters.AddWithValue("@EI", comboBox1.Text);
                cmd.Parameters.AddWithValue("@EN", textBox1.Text);
                cmd.Parameters.AddWithValue("@ES", textBox2.Text);
                cmd.Parameters.AddWithValue("@EA", textBox3.Text);
                cmd.Parameters.AddWithValue("@EIN", textBox4.Text);
                cmd.Parameters.AddWithValue("@ET", textBox5.Text);
                cmd.Parameters.AddWithValue("@E6", textBox6.Text);
                cmd.Parameters.AddWithValue("@E12", textBox7.Text);
                cmd.Parameters.AddWithValue("@E18", textBox8.Text);
                cmd.Parameters.AddWithValue("@E24", textBox11.Text);
                cmd.Parameters.AddWithValue("@E30", textBox10.Text);
                cmd.Parameters.AddWithValue("@E36", textBox9.Text);
                cmd.Parameters.AddWithValue("@ETO",tt.ToString() );




                cmd.ExecuteNonQuery();
                MessageBox.Show("Registration Success");
                Con.Close();
                ShowApit();
               

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Image logo = Properties.Resources.medical;

            int desiredWidth = 150; 
            int desiredHeight = 150; 

            e.Graphics.DrawImage(logo, new Rectangle(50, 30, desiredWidth, desiredHeight));

            e.Graphics.DrawString("Open Innovation Pvt Ltd", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Red, new Point(220, 80));
            e.Graphics.DrawString("Colombo-07", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Red, new Point(250, 100));
            e.Graphics.DrawString("APIT System Version 1.0", new Font("Segoe UI", 10, FontStyle.Bold), Brushes.Red, new Point(160, 750));

            if (DataGridView1.SelectedRows.Count > 0)
            {

                DataGridViewRow selectedRow = DataGridView1.SelectedRows[0];
                string EmpId = selectedRow.Cells["EmpId"].Value.ToString();
                string EmpName = selectedRow.Cells["EmpName"].Value.ToString();
                string EmpSal = selectedRow.Cells["EmpSal"].Value.ToString();
                string TotalAllowance = selectedRow.Cells["TotalAllow"].Value.ToString();
                string TotalIncome = selectedRow.Cells["TotalInco"].Value.ToString();
                string TaxAmount = selectedRow.Cells["EmptaxAm"].Value.ToString();
                string TaxSix = selectedRow.Cells["TexSix"].Value.ToString();
                string TaxTwelve = selectedRow.Cells["TexTwel"].Value.ToString();
                string TaxEighteen = selectedRow.Cells["TexEig"].Value.ToString();
                string TaxTwenty = selectedRow.Cells["TexTwen"].Value.ToString();
                string Taxthirty = selectedRow.Cells["TexThirty"].Value.ToString();
                string TaxThirtysix = selectedRow.Cells["TexThirtySix"].Value.ToString();
                string TotalAp = selectedRow.Cells["Total_Apit"].Value.ToString();


                string[] headers = {
        "Employee Id",
        "Employee Name",
        "Salary",
        "Total Allowance",
        "Total Income",
        "Tax Amount",
        "Tax Six",
        "Tax Twelve",
        "Tax Eighteen",
        "Tax Twenty",
        "Tax Thirty",
        "Tax Thirty-Six",
        "Total AP"
    };

                string[] data = {
        EmpId,
        EmpName,
        EmpSal,
        TotalAllowance,
        TotalIncome,
        TaxAmount,
        TaxSix,
        TaxTwelve,
        TaxEighteen,
        TaxTwenty,
        Taxthirty,
        TaxThirtysix,
        TotalAp
    };


             

                int yPos = 250;

                for (int i = 0; i < headers.Length; i++)
                {
                    // Draw header
                    e.Graphics.DrawString(headers[i] , new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Blue, new Point(50, yPos));
                yPos += 30;
                  
                      }
                int xPos = 250;
                for (int i = 0; i < data.Length; i++)
                {
                    // Draw header
                    e.Graphics.DrawString(data[i], new Font("Segoe UI", 12, FontStyle.Bold), Brushes.Blue, new Point(300, xPos));
                    xPos += 30;

                }

            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView1.Rows[e.RowIndex].Selected = true;
                DataGridView1.Refresh();

                printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("Letter", 500, 800);
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
        }
    }
}