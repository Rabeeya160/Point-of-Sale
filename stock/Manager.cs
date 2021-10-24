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
    public partial class Manager : Form
    {


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

        public Manager()


        {
            InitializeComponent();

            M_password.PasswordChar = '*';
        }
        

        public void login_search()
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

            string query = "select * from InvManager where  Email ='" + M_email.Text + "' and PasswordHash = '" + M_password.Text + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                Stock_Managment sm = new Stock_Managment();
                sm.Show();

            }
            else
            {
                MessageBox.Show("invalid");

            }
        }

        public void Login_Insert()
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

            string query = " insert into Login_User values ( '"+ M_email.Text+"' , '"+M_password.Text+"') ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                Stock_Managment sm = new Stock_Managment();
                sm.Show();

            }
            else
            {
                MessageBox.Show("invalid");

            }
        }

        public void Delete()
        {

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

            string query = " delete from Login_User ";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                Stock_Managment sm = new Stock_Managment();
                sm.Show();

            }
            else
            {
                MessageBox.Show("invalid");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            login_search();

            Login_Insert(); 






        }

        private void button2_Click(object sender, EventArgs e)
        {
            RegistrationPage rp = new RegistrationPage();
            rp.Show(); 
        }

        public bool ValidateStringEmail()
        {
            bool status = true;
            if (!Regex.IsMatch(M_email.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errorProvider1.SetError(M_email, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(M_email, "");
                status = true;
            }
            return status;
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
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
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }

        private void M_email_Validated(object sender, EventArgs e)
        {
            ValidateStringEmail();
        }

        private void M_password_Validated(object sender, EventArgs e)
        {
           
    }

        
            private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(M_password.Text))
            {
                errorProvider1.SetError(M_password, "Password required!");
            }
            else if (!Regex.IsMatch(M_password.Text, @"[A-Za-z][A-Za-z0-9]{2,7}"))
            {
                errorProvider1.SetError(M_password, "Password invalid!");
            }
            else
            {
                errorProvider1.SetError(M_password, null);
            }
        }

        private void M_password_TextChanged(object sender, EventArgs e)
        {

        }
    }
    }

