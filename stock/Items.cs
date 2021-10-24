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
    public partial class Items : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

        public Items()
        {
            InitializeComponent();
           
        }
        
        DataTable table = new DataTable();

        public void name()
        {
            con.Open();
            SqlCommand cmdai = con.CreateCommand();
            cmdai.CommandType = CommandType.Text;
            cmdai.CommandText = " select InvCategory.Id  from InvCategory where InvCategory.Name = '" + IdSelection.Text + "' ";
            cmdai.ExecuteNonQuery();
            SqlDataReader DR = cmdai.ExecuteReader();

            while (DR.Read())
            {

                C_Id.Text = DR.GetValue(0).ToString();

            }
            con.Close();
        }

        private void Items_Load(object sender, EventArgs e)
        {
            IdSelection.DisplayMember = "Name";
            IdSelection.DroppedDown = true;
            IdSelection.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            IdSelection.AutoCompleteSource = AutoCompleteSource.ListItems;

            IdSelection.Items.Clear();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Distinct Name from InvCategory ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
           SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
          

                foreach (DataRow dr in dt.Rows)
                {
                    IdSelection.Items.Add(dr["Name"].ToString());
                }
                con.Close();

            

        }

        private void AddProduct_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "Insert into InvItem values('" + ProductName.Text + "' , '" + C_Id.Text + "' , '" + ProductDate.Text + "')";
            cmd.ExecuteNonQuery();

            ProductName.Text = " ";
            C_Id.Text = " "; 
            con.Close();

            MessageBox.Show("record inserted");
            
            display();
        }

        public void display()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvItem ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Product_view.DataSource = dt;


            con.Close();


        }

        private void DeleteProduct_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "delete from InvItem where Name = '" + ProductName.Text + "'";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("record deleted");

                display();
            }
            catch(Exception a)
            {
                MessageBox.Show("please Delete OrderDetails First"); 
            }
        }

        private void UpdateProduct_Click(object sender, EventArgs e)
        {
        }

        private void DeleteAllProduct_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "delete from InvItem ";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show(" All record deleted");

                display();
            }
            catch(Exception a)
            {
                MessageBox.Show("please Delete the OrderDetails First");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            display(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvItem where Name = '" + ProductName.Text + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            Product_view.DataSource = dt;


            con.Close();
        }

        private void IdSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            name();
        }

        private void ProductName_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProductName_Validating(object sender, CancelEventArgs e)
        {
            ValidateName();
        }
        public bool ValidateName()
         {
            bool status = true;
            if (!Regex.IsMatch(ProductName.Text, @"^[\p{L}]+$"))
            {
                errorProvider1.SetError(ProductName, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(ProductName , "");
                status = true;
            }
            return status;
        }

        private void Product_view_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Catergory c = new Catergory();
            c.Show();
            this.Hide();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Stock_Managment sm = new Stock_Managment();
            sm.Show();
            this.Hide();
        }
    }
}
