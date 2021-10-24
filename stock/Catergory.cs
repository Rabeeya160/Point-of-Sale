using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace stock
{


    public partial class Catergory : Form
    {


        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

        public Catergory()
        {
            InitializeComponent();
        }

        public void Duplicate()
        {


            con.Open();
          
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select Count(*) from InvCategory where Name = '"+CName.Text+"'";
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Record Already Exists.");
                  
                }
                reader.Close();
                reader.Dispose();

            }
            con.Close();
        }
        DataTable dt = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {

            if (ValidateCName())
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();

                cmd.CommandType = CommandType.Text;



                {
                    cmd.CommandText = "Insert into InvCategory values('" + CName.Text + "' , '" + dateTimePicker1.Text + "')";


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("record inserted");


                    CName.Text = " ";

                    con.Close();









                }
            }
        }
        public void display()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvCategory ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt; 
           
            con.Close();

          
        }

       
        

        private void Catergory_Load(object sender, EventArgs e)
        {
            

            display();

         



        }

        private void Delete_Button_Click(object sender, EventArgs e)
        {
            /* delete with run time inputs
            int rowindex = dataGridView1.CurrentCell.RowIndex;
            dataGridView1.Rows.RemoveAt(rowindex); 
      */
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "delete from InvCategory where Name = '" + CName.Text + "'";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show("record deleted");

                display();
            }
            catch (Exception a)
            {
                MessageBox.Show("please delete the item First");


            }
        }
       
        private void Update_Button_Click(object sender, EventArgs e)
        {
            /* for run time update value 
                        if (dataGridView1.SelectedRows.Count > 0)
                        {
                            AddButton.Visible = false;
                            button2.Visible = true;
                            r = dataGridView1.CurrentRow.Index;
                            C_Id.Text = dataGridView1.Rows[r].Cells[0].Value.ToString();
                            CName.Text = dataGridView1.Rows[r].Cells[1].Value.ToString();
                        }
                        else
                        {
                            AddButton.Visible = true;
                            button2.Visible = false;
                            MessageBox.Show("select a row"); 
                        }         
             */


           
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


  

           
        }

        public void savebutton_Click(object sender, EventArgs e)
        {
            

            
           


            
           
        }

        private void CName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /* delete full grid through run time   int rowCount = dataGridView1.Rows.Count;

              for (int i = 0; i < rowCount; i++)

              {

                  dataGridView1.Rows.RemoveAt(i);

                  --rowCount;
                  */

            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "delete from InvCategory ";
                cmd.ExecuteNonQuery();

                con.Close();

                MessageBox.Show(" All record deleted");

                display();
            }
            catch(Exception a)
            {
                MessageBox.Show("Please Date Product First" ); 
            }
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            display();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvCategory where Name = '"+CName.Text+"' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            con.Close();


        }

        private void CName_Validating(object sender, CancelEventArgs e)
        {
            ValidateCName();

        }

        public bool ValidateCName()
        {
            bool status = true;
            if (!Regex.IsMatch(CName.Text, @"^[\p{L}]+$"))
            {
                errorProvider1.SetError(CName, "abc");
                status = false;
            }
            else
            {
                errorProvider1.SetError(CName, "" );
                status = true;
            }
            return status; 

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvCategory  where Name = '" + CName.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


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
            Items i = new Items();
            i.Show();
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

      
    

