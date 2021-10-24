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
    public partial class RegistrationPage : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

        

        public RegistrationPage()
        {
            InitializeComponent();

            New_Password.PasswordChar = '*';
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateStringEmail() && ValidateStringName())
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "Insert into InvManager values('" + New_Email.Text + "' , '" + New_Name.Text + "' , '" + New_Password.Text + "')";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("record inserted");
            }

        }


        public bool ValidateStringEmail()
        {
            bool status = true;
            if (!Regex.IsMatch(New_Email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errorProvider1.SetError(New_Email, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(New_Email, "");
                status = true;
            }
            return status;
        }

        public bool ValidateStringName()
        {
            bool status = true;
            if (!Regex.IsMatch(New_Name.Text, @"^[\p{L}]+$"))
            {
                errorProvider1.SetError(New_Name, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(New_Name, "");
                status = true;
            }
            return status;
        }

        private void RegistrationPage_Load(object sender, EventArgs e)
        {

        }

        private void New_Name_Validating(object sender, CancelEventArgs e)
        {
            ValidateStringName();
        }

        private void New_Email_Validating(object sender, CancelEventArgs e)
        {
            ValidateStringEmail();
        }

        private void New_Name_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void New_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
          
        }

        private void New_Password_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(New_Password.Text))
            {
                errorProvider1.SetError(New_Password, "Password required!");
            }
            else if (!Regex.IsMatch(New_Password.Text, @"[A-Za-z][A-Za-z0-9]{2,7}"))
            {
                errorProvider1.SetError(New_Password, "Password invalid!");
            }
            else
            {
                errorProvider1.SetError(New_Password, null);
            }

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            Manager m = new Manager();
            m.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
