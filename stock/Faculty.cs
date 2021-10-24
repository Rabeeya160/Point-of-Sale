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
    public partial class Faculty : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

        public Faculty()
        {
            InitializeComponent();

          
        }


        DataTable Table = new DataTable();

        public bool ValidateStringName()
        {
            bool status = true;
            if (!Regex.IsMatch(F_name.Text , @"^[\p{L}]+$"))
            {
                errorProvider1.SetError(F_name, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(F_name, "");
                status = true;
            }
            return status;
        }

        public bool ValidateStringEmail()
        {
            bool status = true;
            if (!Regex.IsMatch(F_Id.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errorProvider1.SetError(F_Id, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(F_Id , "");
                status = true;
            }
            return status;
        }
        public bool ValidateStringRank()
        {
            bool status = true;
            if (!Regex.IsMatch(Rank.Text, @"^[\p{L}]+$"))
            {
                errorProvider1.SetError(Rank, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(Rank, "");
                status = true;
            }
            return status;
        }
  


        private void Faculty_Load(object sender, EventArgs e)
        {
            Account_Id.Items.Clear();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Max(AccountId) from InvAccount ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Account_Id.Items.Add(dr[0].ToString());
            }
            con.Close();


        }

        private void F_add_Click(object sender, EventArgs e)
        {
            if (ValidateStringName() && ValidateStringEmail() && ValidateStringRank())
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "Insert into InvCustomer values('" + F_name.Text + "' , '" + F_Id.Text + "' , '" + facultycontact.Text + "' , '" + Rank.Text + "' , '" + is_faculty.Checked + "' , '" + Account_Id.Text + "')";
                cmd.ExecuteNonQuery();


                F_name.Text = " ";
                F_Id.Text = " ";
                facultycontact.Text = " ";
                Rank.Text = " ";
                Account_Id.Text = " ";
                is_faculty.Checked = false; 
                con.Close();

                MessageBox.Show("record inserted");
            }

        }

        public void display()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvCustomer ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Faculty_View.DataSource = dt;


            con.Close();


        }

        private void faculty_delete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "delete from InvCustomer where Name = '" + F_name.Text + "'" ;
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("record deleted");

            display();
        }
       
        private void faculty_update_Click(object sender, EventArgs e)
        {
            

           /* if (Faculty_View.SelectedRows.Count > 0)
            {
                F_add.Visible = false;
                search.Visible = true;
                r = Faculty_View.CurrentRow.Index;
                F_name.Text = Faculty_View.Rows[r].Cells[0].Value.ToString();
                F_Id.Text = Faculty_View.Rows[r].Cells[1].Value.ToString();
            
                facultycontact.Text = Faculty_View.Rows[r].Cells[3].Value.ToString();
            }
            else
            {
                F_add.Visible = true;
                search.Visible = false;
                MessageBox.Show("select a row");
            }*/
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void F_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void deleteAll_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "delete from InvCustomer ";
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show(" All record deleted");

            display();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvCustomer where Name = '" + F_name.Text + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Faculty_View.DataSource = dt;


            con.Close();
            /* Faculty_View.Rows[r].Cells[0].Value = F_name.Text;
             Faculty_View.Rows[r].Cells[1].Value = F_Id.Text;
             Faculty_View.Rows[r].Cells[2].Value = facultycnic.Text;
             Faculty_View.Rows[r].Cells[3].Value = facultycontact.Text;*/
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            display();
        }

        private void F_name_Validating(object sender, CancelEventArgs e)
        {
            ValidateStringName(); 
        }

        private void F_Id_TextChanged(object sender, EventArgs e)
        {

        }

        private void F_Id_Validating(object sender, CancelEventArgs e)
        {
            ValidateStringEmail(); 
        }

        private void Rank_Validating(object sender, CancelEventArgs e)
        {
            ValidateStringRank();
        }

        private void facultycontact_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Account_Id_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void is_faculty_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Rank_TextChanged(object sender, EventArgs e)
        {

        }

        private void facultycontact_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void Faculty_View_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvCustomer  where Name = '"+F_name.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Faculty_View.DataSource = dt;


            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Manager m = new Manager();
            m.Show();
            this.Hide();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form1 f = new Form1();
            f.Show();
            this.Hide();
        }
    }
}
