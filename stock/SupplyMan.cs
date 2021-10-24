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
    public partial class SupplyMan : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

        public SupplyMan()
        {
            InitializeComponent();
        }

    /*    public bool ValidateStringName()
        {
            bool status = true;
            if (!Regex.IsMatch(S_name.Text, @"[\p{L}]+$"))
            {
                errorProvider1.SetError(S_name, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(S_name , "");
                status = true;
            }
            return status;
        }

        public bool ValidateStringEmail()
        {
            bool status = true;
            if (!Regex.IsMatch(S_email.Text, @"[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errorProvider1.SetError(S_email, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(S_email, "");
                status = true;
            }
            return status;
        }
        
      */  

        private void SupplyMan_Load(object sender, EventArgs e)
        {
            AccountID.Items.Clear();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Max(AccountId) from InvAccount";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                AccountID.Items.Add(dr[0].ToString());
            }
            con.Close();



        }


        private void F_add_Click(object sender, EventArgs e)
        {

            try
            {
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into InvSupplier values('" + S_name.Text + "' , '" + S_email.Text + "' , '" + S_Contact.Text + "' , '" + S_address.Text + "' , '" + AccountID.Text + "') ";

                

                
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("record inserted");
            } 
            catch (Exception a){
                MessageBox.Show("Invalid");
            }
            }


        
        public void display()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvSupplier ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            S_View.DataSource = dt;
            con.Close();


        }

        private void faculty_delete_Click(object sender, EventArgs e)
        {
            try
            {
                notifyIcon1.BalloonTipTitle = "Icon There ";
                SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into InvSupplier values('" + S_name.Text + "' , '" + S_email.Text + "' , '" + S_Contact.Text + "' , '" + S_address.Text + "' , '" + AccountID.Text + "') ";




                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("record inserted");
            }
            catch (Exception a)
            {
                MessageBox.Show("Invalid");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void deleteAll_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "delete from InvSupplier ";
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show(" All record deleted");

            display();
        }

        private void search_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvSupplier where Name = '" + S_name.Text + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            S_View.DataSource = dt;


            con.Close();
        }

        private void Type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AccountID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void S_name_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void S_Contact_Validating(object sender, CancelEventArgs e)
        {
        }

        private void S_email_TextChanged(object sender, EventArgs e)
        {

        }

        private void S_email_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void S_address_TextChanged(object sender, EventArgs e)
        {

        }

        private void S_address_Validating(object sender, CancelEventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void S_View_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void S_address_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void S_Contact_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void S_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Manager m = new Manager();
            m.Show();
            this.Hide();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
