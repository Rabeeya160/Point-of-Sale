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
    public partial class Edit_Supplier : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");
       
        public Edit_Supplier()
        {
            InitializeComponent();
        }
        public void name()
        {
            con.Open();
            SqlCommand cmdai = con.CreateCommand();
            cmdai.CommandType = CommandType.Text;
            cmdai.CommandText = " select Name , Email , Cell , Address  from InvAccount join InvSupplier on InvAccount.AccountId = InvSupplier.AccountId where InvSupplier.AccountId = '" + AccountID.Text + "' ";
            cmdai.ExecuteNonQuery();
            SqlDataReader DR = cmdai.ExecuteReader();

            while (DR.Read())
            {
                S_name.Text = DR.GetString(0);
                S_email.Text = DR.GetString(1);
                S_Contact.Text = DR.GetString(2);
                S_address.Text = DR.GetString(3);


            }
            con.Close();
        }


        private void Edit_Supplier_Load(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select distinct InvAccount.AccountId from InvAccount where AccountType = 'Supplier'  ";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                AccountID.Items.Add(dtr["AccountId"].ToString());

            }
            dtr.Close();
            con.Close();
        }

        private void deleteAll_Click(object sender, EventArgs e)
        {

        }

        private void F_add_Click(object sender, EventArgs e)
        {

        }

        private void faculty_delete_Click(object sender, EventArgs e)
        {

        }

        private void AccountID_SelectedIndexChanged(object sender, EventArgs e)
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
                cmdai.CommandText = "update InvSupplier set Name = '" + S_name.Text + "' ,  Email = '" + S_email.Text + "' , Cell = '" + S_Contact.Text + "' , Address = '" + S_address.Text + "'  where InvSupplier.AccountId = '" + AccountID.Text + "' ";
                cmdai.ExecuteNonQuery();
                MessageBox.Show("Data Updated");

                con.Close();
            }
            catch (Exception es)
            {
                MessageBox.Show("Invalid");
            }
        }

        private void search_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdai = con.CreateCommand();
            cmdai.CommandType = CommandType.Text;
            cmdai.CommandText = " delete from InvSupplier where InvSupplier.AccountId = '" + AccountID.Text + "'  ";
            cmdai.ExecuteNonQuery();
            MessageBox.Show("Data Deleted");

            con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void S_address_TextChanged(object sender, EventArgs e)
        {

        }

        private void search_MouseHover(object sender, EventArgs e)
        {
            toolTip3.SetToolTip(this.search, "Click Here to Delete Your Data");
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            toolTip2.SetToolTip(this.button1, "Click here to Update Your Date");
        }

        private void search_MouseClick(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void S_name_Validating(object sender, CancelEventArgs e)
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
            SupplyMan sm = new SupplyMan();
            sm.Show();
            this.Hide();
        }
    }
}
