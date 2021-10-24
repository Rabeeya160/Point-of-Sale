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
using System.Text.RegularExpressions;

namespace stock
{
    public partial class Account : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

        public Account()
        {
            InitializeComponent();
        }

        private void Account_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("select a type of Account"); 
            }
             else   if (radioButton1.Checked == true)
                {
                con.Open();
                 
                    SqlCommand cmdc = con.CreateCommand();
                    cmdc.CommandType = CommandType.Text;

                    cmdc.CommandText = "Insert into InvAccount values('" + radioButton1.Text + "')";
                    cmdc.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("record inserted");
                    SupplyMan sm = new SupplyMan();
                    sm.Show(); 

                }
                else if (radioButton2.Checked == true)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "Insert into InvAccount values('" + radioButton2.Text + "')";
                    cmd.ExecuteNonQuery();

                    con.Close();
                    MessageBox.Show("record inserted");
                    Faculty f = new Faculty();
                    f.Show();
                }
              
            }
           

        

        private void account_type_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                Edit_Faculty ef = new Edit_Faculty();
                ef.Show();

            }
            else
                if (radioButton1.Checked == true)
            {
                Edit_Supplier es = new Edit_Supplier();
                es.Show();
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            if (radioButton2.Checked == true)
            {
               
            }
            else
                if (radioButton1.Checked == true)
            {
                
            }
        }

        private void Account_MouseDown(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip2.SetToolTip(this.button1 , "Click here to Save Your Selected Choice");
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(this.button3, "Click here to Edit or Delete Your Choice!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stock_Managment sm = new Stock_Managment();
            sm.Show();
            this.Hide();
        }
    }
}
