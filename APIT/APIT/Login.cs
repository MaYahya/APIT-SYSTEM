using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APIT
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            

            if (textBox1.Text=="admin"&& textBox2.Text == "admin")
            {
                Form1 obj= new Form1();
                obj.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Please Enter the Username or Password Correctly!");
            }
        }
    }
}
