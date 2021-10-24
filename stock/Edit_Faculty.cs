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

namespace stock
{
    public partial class Edit_Faculty : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

        public Edit_Faculty()
        {
            InitializeComponent();
        }

        public void name()
        {
            con.Open();
            SqlCommand cmdai = con.CreateCommand();
            cmdai.CommandType = CommandType.Text;
            cmdai.CommandText = " select Name , Email , Contact , Desigination , Is_faculty from InvAccount join InvCustomer on InvAccount.AccountId = InvCustomer.AccountId where InvCustomer.AccountId = '"+Faculty_Id.Text+"' ";
            cmdai.ExecuteNonQuery();
            SqlDataReader DR = cmdai.ExecuteReader();

            while (DR.Read())
            {
                F_name.Text = DR.GetString(0);
                F_Id.Text = DR.GetString(1);
                facultycontact.Text = DR.GetString(2);
                Rank.Text = DR.GetString(3);
           

            }
            con.Close();
        }
      

        private void Edit_Faculty_Load(object sender, EventArgs e)
        { 
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select distinct InvAccount.AccountId from InvAccount where AccountType = 'Customer'  ";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                Faculty_Id.Items.Add(dtr["AccountId"].ToString());

            }
            dtr.Close();
            con.Close();
        }

        private void Faculty_Id_SelectedIndexChanged(object sender, EventArgs e)
        {
            name();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmdai = con.CreateCommand();
                cmdai.CommandType = CommandType.Text;
                cmdai.CommandText = "update InvCustomer set Name = '" + F_name.Text + "' ,  Email = '" + F_Id.Text + "' , Contact = '" + facultycontact.Text + "' , Desigination = '" + Rank.Text + "' , Is_faculty ='" + is_faculty.Checked + "' where InvCustomer.AccountId = '" + Faculty_Id.Text + "' ";
                cmdai.ExecuteNonQuery();
                MessageBox.Show("Data Updated");
           
                con.Close();
            }
            catch(Exception es)
            {
                MessageBox.Show("Invalid");
            }
        }

        private void search_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdai = con.CreateCommand();
            cmdai.CommandType = CommandType.Text;
            cmdai.CommandText = " delete from InvCustomer where InvCustomer.AccountId = '" + Faculty_Id.Text + "'  ";
            cmdai.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");
            
            con.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void search_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Account a = new Account();
            a.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Faculty f = new Faculty();
            f.Show();
            this.Hide();
        }
    }
}
