using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;
namespace stock
{

    public partial class Stock_Managment : Form
    {

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");


        public Stock_Managment()
        {
            InitializeComponent();
        }


        DataTable table = new DataTable();



        public void displayAllData()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "  select * from InvOrderDetails join InvOrder on InvOrder.Id=InvOrderDetails.OrderId join InvOrderType on InvOrderType.Id = InvOrder.OrderType ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            con.Close();
        }

        private void s_issue_Click(object sender, EventArgs e)
        {
            displayAllData();
        }

        private void s_add_Click(object sender, EventArgs e)
        {

        }

        private void s_report_Click(object sender, EventArgs e)
        {
            try
            {
                string constring = null;
                SqlConnection connection;
                SqlCommand command;
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataSet ds = new DataSet();

                string sql = null;


                SqlConnection con = new SqlConnection("Data Source=DESKTOP-0R3JA26;Initial Catalog=Inventory;Integrated Security=True");

                sql = "select CategoryId , Name , Quantity from InvItem left join InvOrderDetails on InvItem.Id = InvOrderDetails.ProductId order by Id asc";
                connection = new SqlConnection(constring);
                connection.Open();
                command = new SqlCommand(sql, connection);
                adapter.SelectCommand = command;
                adapter.Fill(ds);
                connection.Close();

            }
            catch (Exception er)
            {
                MessageBox.Show("invlaid");
                return;
            }

        }



        public void displayAccountCustomer()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select InvAccount.AccountId from InvAccount join InvCustomer on InvAccount.AccountId = InvCustomer.AccountId  where InvCustomer.Name = '" + account.Text + "' ";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                customer_name.Text = dtr.GetValue(0).ToString();

            }
            dtr.Close();
            con.Close();


            con.Open();
            SqlCommand cmdid = con.CreateCommand();
            cmdid.CommandType = CommandType.Text;

            cmdid.CommandText = " select Id from InvAccount join InvCustomer on InvAccount.AccountId = InvCustomer.AccountId  where InvCustomer.Name = '" + account.Text + "' ";
            SqlDataReader dtrid = cmdid.ExecuteReader();
            while (dtrid.Read())
            {
                Customer_Id.Text = dtrid.GetValue(0).ToString();

            }
            dtrid.Close();
            con.Close();



        }

        public void displayNAMESupplier()
        {
            con.Open();
            SqlCommand cmdai = con.CreateCommand();
            cmdai.CommandType = CommandType.Text;
            cmdai.CommandText = " select InvAccount.AccountId from InvAccount join InvSupplier on InvAccount.AccountId = InvSupplier.AccountId  where InvSupplier.Name = '" + sup_account.Text + "' ";
            cmdai.ExecuteNonQuery();
            SqlDataReader DR = cmdai.ExecuteReader();

            while (DR.Read())
            {
                supplier_name.Text = DR.GetValue(0).ToString();

            }
            con.Close();

            con.Open();
            SqlCommand cmdid = con.CreateCommand();
            cmdid.CommandType = CommandType.Text;

            cmdid.CommandText = " select Id from InvAccount join InvSupplier on InvAccount.AccountId = InvSupplier.AccountId  where InvSupplier.Name = '" + sup_account.Text + "' ";
            SqlDataReader dtrid = cmdid.ExecuteReader();
            while (dtrid.Read())
            {
                sup_name_id.Text = dtrid.GetValue(0).ToString();

            }
            dtrid.Close();
            con.Close();

        }


        public void displayAccountTypeSupplier()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "   select  AccountType from InvAccount join InvSupplier on InvSupplier.AccountId = InvAccount.AccountId  where InvSupplier.Name='" + sup_account.Text + "' ";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sup_accountType.Text = dtr.GetString(0);

            }
            dtr.Close();
            con.Close();
        }

        public void displayProductCategory()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "    select Name from InvCategory where InvCategory.Id in (select CategoryId from InvItem where InvItem.Name='" + product.Text + "' ) ";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                textBox6.Text = dtr.GetString(0);

            }
            dtr.Close();
            con.Close();


        }


        public int countproductId()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "  select count(*) from InvOrderDetails  join InvItem on InvItem.Id = InvOrderDetails.ProductId where InvItem.Name = '" + product.SelectedItem.ToString() + "' ";
            SqlDataReader dtr = cmd.ExecuteReader();
            int count = 0;
            while (dtr.Read())
            {
                count = Int32.Parse(dtr.GetValue(0).ToString());

            }
            dtr.Close();
            con.Close();
            return count;
        }




        public int OrderStock()
        {
            int remain = 0;
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "  select sum(Quantity) as quantity from InvOrderDetails join InvOrder on InvOrder.Id = InvOrderDetails.OrderId join InvOrderType on InvOrderType.Id = InvOrder.OrderType join InvItem on InvItem.Id = InvOrderDetails.ProductId   where  InvOrderType.TypeName = 'Add'  and InvItem.Name = '" + product.SelectedItem.ToString() + "' ";
                SqlDataReader dtr = cmd.ExecuteReader();

                while (dtr.Read())
                {
                    remain = Int32.Parse(dtr.GetValue(0).ToString());

                }
                dtr.Close();
                con.Close();
                return remain;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Product is not available more");
            }

            return remain;




        }


        public int OrderStockIssue()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "  select sum(Quantity) as quantity from InvOrderDetails join InvOrder on InvOrder.Id = InvOrderDetails.OrderId join InvOrderType on InvOrderType.Id = InvOrder.OrderType join InvItem on InvItem.Id = InvOrderDetails.ProductId   where  InvOrderType.TypeName = 'Issue'  and InvItem.Name = '" + product.SelectedItem.ToString() + "' ";
            SqlDataReader dtr = cmd.ExecuteReader();
            int Issue = 0;
            while (dtr.Read())
            {
                Issue = Int32.Parse(dtr.GetValue(0).ToString());

            }
            dtr.Close();
            con.Close();
            return Issue;
        }

        public void displayAccountTypeCustomer()
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "   select  AccountType from InvAccount join InvCustomer on InvCustomer.AccountId = InvAccount.AccountId where Name='" + account.Text + "' ";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                textBox5.Text = dtr.GetString(0).ToString();

            }
            dtr.Close();
            con.Close();

        }



        public void displayAccountNameCUSTOMER()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select InvCustomer.Name from InvCustomer join InvAccount on InvAccount.AccountId=InvCustomer.AccountId where AccountType = 'Customer'  ";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                account.Items.Add(dtr["Name"].ToString());

            }
            dtr.Close();
            con.Close();
        }


        public void displayAccountNameSupplier()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "  select  InvSupplier.Name from InvAccount join InvSupplier on InvAccount.AccountId = InvSupplier.AccountId  where AccountType = 'Supplier'";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                sup_account.Items.Add(dtr["Name"].ToString());

            }
            dtr.Close();
            con.Close();



        }




        public void displayProduct()
        {

            con.Open();
            string strSQL1 = "SELECT Name FROM InvItem where Id = '" + product.Text + "'";
            SqlCommand cmd1 = new SqlCommand(strSQL1, con);
            SqlDataReader dtr1 = cmd1.ExecuteReader();
            while (dtr1.Read())
            {
                sup_accountType.Text = dtr1.GetString(0);

            }
            dtr1.Close();
            con.Close();
        }
        public void displayMonthly()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select InvOrderDetails.OrderId , InvItem.Name As ProductName , InvCategory.Name As CategoryName , InvOrder.OrderDate , InvOrderDetails.Quantity   from InvOrderDetails join InvOrder on InvOrderDetails.OrderId = InvOrder.Id join InvOrderType on InvOrder.OrderType = InvOrderType.Id join InvItem on InvItem.Id = InvOrderDetails.ProductId  join InvCategory on InvCategory.Id = InvItem.CategoryId where InvOrder.OrderDate between '" + date_from.Text + "' and '" + date_to.Text + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;



            con.Close();


        }



        public void displayStockCustomer()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select top 1 InvOrderDetails.OrderId ,InvCategory.Name As Category_Name ,     InvItem.Name As Product_Name ,  InvOrder.OrderDate, InvOrderType.TypeName As Status ,   InvCustomer.Name AS Customer_Name , InvOrderDetails.Quantity from InvOrderDetails join InvOrder on InvOrderDetails.OrderId = InvOrder.Id join InvOrderType on InvOrder.OrderType = InvOrderType.Id join InvItem on InvOrderDetails.ProductId = InvItem.Id join InvCategory on InvCategory.Id= InvItem.CategoryId  join InvCustomer on InvOrder.AccountId = InvCustomer.AccountId order by OrderId  desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            con.Close();

        }

        public void displayStockSupplier()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select top 1 InvOrderDetails.OrderId ,InvCategory.Name As Category_Name ,     InvItem.Name As Product_Name ,  InvOrder.OrderDate, InvOrderType.TypeName As Status ,   InvSupplier.Name AS Customer_Name , InvOrderDetails.Quantity from InvOrderDetails join InvOrder on InvOrderDetails.OrderId = InvOrder.Id join InvOrderType on InvOrder.OrderType = InvOrderType.Id join InvItem on InvOrderDetails.ProductId = InvItem.Id join InvCategory on InvCategory.Id= InvItem.CategoryId  join InvSupplier on InvOrder.AccountId = InvSupplier.AccountId order by OrderId  desc";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            dataGridView1.DataSource = dt;


            con.Close();




        }







        private void Stock_Managment_Load(object sender, EventArgs e)
        {
            {
                //textBox3.Visible = false;
                //customer_name.Visible = false;
                //Customer_Id.Visible = false;
                //Order_Type_ID.Visible = false;
                //ManagerId.Visible = false;
                //product_id_cus.Visible = false;
                //order_id_cus.Visible = false;
                //label1.Visible = false;
                //label33.Visible = false;
                //label21.Visible = false;
                //label22.Visible = false;
                //label20.Visible = false;
                //label27.Visible = false;
                //label19.Visible = false;
                //label3.Visible = false;
                //label23.Visible = false;
                //label24.Visible = false;
                //label16.Visible = false;
                //label12.Visible = false;
                //label26.Visible = false;
                //supplier_name.Visible = false;
                //sup_name_id.Visible = false;
                //Order_type_sup.Visible = false;
                //manager_Id_sup.Visible = false;
                //Order_type_sup.Visible = false;
                //product_id_sup.Visible = false;




            }




            groupBox11.Visible = false;
            Order_type_sup.Text = "Add";

            {

                if (tabControl1.SelectedTab.Text == "Customer")
                {
                    order_type_name.Text = "Issue";
                }


            }
            displayAccountNameCUSTOMER();
            displayAccountNameSupplier();

            this.order_type_name.AutoCompleteCustomSource.AddRange
(new string[] {"Add", "Issue",
});


            this.Order_type_sup.AutoCompleteCustomSource.AddRange
(new string[] {"Add", "Issue",
});
            this.Order_type_sup.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.Order_type_sup.AutoCompleteSource = AutoCompleteSource.CustomSource;
            // order details




            { // for  ManagerName 
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select FullName from Login_User join  InvManager on Login_User.Email = InvManager.Email";
                SqlDataReader dtr = cmd.ExecuteReader();
                while (dtr.Read())
                {
                    Manager_Name.Text = dtr.GetString(0);
                }
                dtr.Close();
                con.Close();
            }

            { // for  ManagerName
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;

                cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Id from   InvManager where FullName = '" + Manager_Name.Text + "'";
                SqlDataReader dtr = cmd.ExecuteReader();
                while (dtr.Read())
                {
                    ManagerId.Text = dtr.GetValue(0).ToString();
                    manager_Id_sup.Text = dtr.GetValue(0).ToString();
                }
                dtr.Close();
                con.Close();
            }









            // for  product name
            con.Open();
            SqlCommand cmda = con.CreateCommand();
            cmda.CommandType = CommandType.Text;
            cmda = con.CreateCommand();
            cmda.CommandText = "select Name From InvItem ";
            DataTable dta = new DataTable();
            SqlDataReader sdr = cmda.ExecuteReader();
            dta.Load(sdr);
            product.DisplayMember = "Name";
            product.DroppedDown = true;
            List<string> list = new List<string>();

            foreach (DataRow dr in dta.Rows)
            {
                list.Add(dr.Field<string>("Name"));

            }
            this.product.Items.AddRange(list.ToArray<string>());
            product.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            product.AutoCompleteSource = AutoCompleteSource.ListItems;
            con.Close();

        }




        private void button1_Click(object sender, EventArgs e)
        {

        }


        public void countProductId()

        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = " select count(*) from InvOrderDetails  join InvItem on InvItem.Id = InvOrderDetails.ProductId where InvItem.Name = '" + product.SelectedItem.ToString() + "'";
            SqlDataReader dtr = cmd.ExecuteReader();
            while (dtr.Read())
            {
                textBox3.Text = dtr.GetValue(0).ToString();
            }
            dtr.Close();
            con.Close();
        }

        private void date_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }








        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {


            displayOrderType1();
            /*con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "Insert into InvOrderType values('" + type.Text + "' )";
            cmd.ExecuteNonQuery();

            con.Close();

            MessageBox.Show("record inserted");
            */
        }
        public void displayOrder()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvOrder ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            con.Close();


        }
        public void displayOrderType1()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "select * from InvOrderType ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            con.Close();


        }
        public void displayOrderDetails()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            con.Close();


        }



        private void button1_Click_1(object sender, EventArgs e)
        {

            CustomerOrder();

            OrderIdIntCustomer();






            // displayOrder();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (validatequantity())
            {
                CustomerOrderDetails();
                displayStockCustomer();


                int r = OrderStock() - OrderStockIssue();
                textBox1.Text = r.ToString();


            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {



        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Customer")
            {
                displayStockCustomer();

            }
            else if (tabControl1.SelectedTab.Text == "Supplier")
            {
                displayStockSupplier();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            displayMonthly();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void type_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            table.Clear();
        }



        private void tabPage1_Click(object sender, EventArgs e)
        {







        }

        private void tabPage2_Click(object sender, EventArgs e)
        {



            displayNAMESupplier();
            //    displayAccountIDSupplier();




        }




        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }



        private void product_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayProductCategory();
            ProductIdInt();

            countProductId();


            if (Int32.Parse(textBox3.Text) > 1)
            {
                int r = OrderStock() - OrderStockIssue();
                textBox1.Text = r.ToString();
                textBox2.Text = r.ToString();
                

            }
            else

            {
                MessageBox.Show("Product Is not available ");

                textBox7.Text = product_id_cus.Text;

                groupBox11.Visible = true;

            












            }







        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayAccountTypeSupplier();
            displayNAMESupplier();


        }

        private void account_SelectedIndexChanged(object sender, EventArgs e)
        {
            displayAccountTypeCustomer();
            displayAccountCustomer();


        }



        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        public void CustomerOrderTypeId()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select Id from InvOrderType where TypeName = '" + order_type_name.Text + "'";
            SqlDataReader dtrid = cmd.ExecuteReader();
            while (dtrid.Read())
            {
                Order_Type_ID.Text = dtrid.GetValue(0).ToString();

            }
            dtrid.Close();
            con.Close();
        }


        public void SupplierOrderTypeId()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select Id from InvOrderType where TypeName = '" + Order_type_sup.Text + "'";
            SqlDataReader dtrid = cmd.ExecuteReader();
            while (dtrid.Read())
            {
                Order_Type_supp_Id.Text = dtrid.GetValue(0).ToString();

            }
            dtrid.Close();
            con.Close();
        }

        public void ManagerIdInt()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select Id from InvManager where FullName = '" + Manager_Name.Text + "'";
            SqlDataReader dtrid = cmd.ExecuteReader();
            while (dtrid.Read())
            {
                ManagerId.Text = dtrid.GetValue(0).ToString();

            }
            dtrid.Close();
            con.Close();


        }
        public void ProductIdInt()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select Id from InvItem where Name = '" + product.SelectedItem.ToString() + "'";
            SqlDataReader dtrid = cmd.ExecuteReader();
            while (dtrid.Read())
            {

                product_id_sup.Text = dtrid.GetValue(0).ToString();

                product_id_cus.Text = dtrid.GetValue(0).ToString();

            }
            dtrid.Close();
            con.Close();


        }


        public void OrderIdIntCustomer()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select Id from InvOrder where OrderType = '" + Order_Type_ID.Text + "'";
            SqlDataReader dtrid = cmd.ExecuteReader();
            while (dtrid.Read())
            {
                order_id_cus.Text = dtrid.GetValue(0).ToString();

            }
            dtrid.Close();
            con.Close();


        }



        public void OrderIdIntSup()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select Id from InvOrder where OrderType = '" + Order_Type_supp_Id.Text + "'";
            SqlDataReader dtrid = cmd.ExecuteReader();
            while (dtrid.Read())
            {
                OrderId_Sup.Text = dtrid.GetValue(0).ToString();


            }
            dtrid.Close();
            con.Close();


        }

        public void CustomerOrderType()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "Insert into InvOrderType values('" + order_type_name.Text + "' ) ";
            cmd.ExecuteNonQuery();
            MessageBox.Show("record inserted");
            con.Close();
        }

        public void SupplierOrderType()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "Insert into InvOrderType values('" + Order_type_sup.Text + "' ) ";
            cmd.ExecuteNonQuery();
            MessageBox.Show("record inserted");
            con.Close();


        }

        public void CustomerOrder()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " insert into InvOrder values ('" + customer_name.Text + "' , '" + Order_Type_ID.Text + "' , '" + ManagerId.Text + "' , '" + dateTimePicker1.Text + "') ";
            cmd.ExecuteNonQuery();
            MessageBox.Show("record inserted");
            con.Close();


        }

        public void SupplierOrder()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " insert into InvOrder values ('" + supplier_name.Text + "' , '" + Order_Type_supp_Id.Text + "' , '" + ManagerId.Text + "' , '" + dateTimePicker2.Text + "') ";
            cmd.ExecuteNonQuery();
            MessageBox.Show("record inserted");
            con.Close();


        }

        public void CustomerOrderDetails()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " insert into InvOrderDetails values ('" + order_id_cus.Text + "' , '" + product_id_cus.Text + "' , '" + maskedTextBox1.Text + "' ) ";
            cmd.ExecuteNonQuery();
            MessageBox.Show("record inserted");
            con.Close();
        }



        public void SupplierOrderDetails()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " insert into InvOrderDetails values ('" + OrderId_Sup.Text + "' , '" + product_id_sup.Text + "' , '" + quantity.Text + "' ) ";
            cmd.ExecuteNonQuery();
            MessageBox.Show("record inserted");
            con.Close();
        }


        private void button10_Click(object sender, EventArgs e)
        {




            CustomerOrderType();
            CustomerOrderTypeId();





        }

        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            SupplierOrderType();
            SupplierOrderTypeId();
        }

        public void Report_Customer()
        {
            try
            {
                PdfPTable pdfTableBlank = new PdfPTable(1);
                PdfPTable pdfTable1 = new PdfPTable(1);//Here 1 is Used For Count of Column 
                PdfPTable pdfTable2 = new PdfPTable(1);
                PdfPTable pdfTable3 = new PdfPTable(1);

                //Font Style 
                System.Drawing.Font fontH1 = new System.Drawing.Font("Currier", 16);

                //pdfTable1.DefaultCell.Padding = 5; 
                pdfTable1.WidthPercentage = 80;
                pdfTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable1.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                //pdfTable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170); 
                pdfTable1.DefaultCell.BorderWidth = 0;


                //pdfTable1.DefaultCell.Padding = 5; 
                pdfTable2.WidthPercentage = 80;
                pdfTable2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable2.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                //pdfTab2e1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170); 
                pdfTable2.DefaultCell.BorderWidth = 0;

                //pdfTable1.DefaultCell.Padding = 5; 
                pdfTable3.WidthPercentage = 80;
                pdfTable3.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable3.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                //pdfTab2e1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170); 
                pdfTable3.DefaultCell.BorderWidth = 0;


                Paragraph par = new Paragraph();
                par.Add(" ");

                Chunk c1 = new Chunk("HR INVENTORY SYSTEM", FontFactory.GetFont("Times New Roman"));
                c1.Font.Color = new iTextSharp.text.BaseColor(192, 0, 0);
                c1.Font.SetStyle(0);
                c1.Font.Size = 14;
                Phrase p1 = new Phrase();
                p1.Add(c1);
                pdfTable1.AddCell(p1);
                Chunk c2 = new Chunk("UET LAHORE", FontFactory.GetFont("Times New Roman"));
                c2.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c2.Font.SetStyle(0);//0 For Normal Font 
                c2.Font.Size = 11;
                Phrase p2 = new Phrase();
                p2.Add(c2);
                pdfTable2.AddCell(p2);
                Chunk c3 = new Chunk("Customer Care CONTACT : 03XX-XXYYZZA", FontFactory.GetFont("Times New Roman"));
                c3.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c3.Font.SetStyle(0);
                c3.Font.Size = 11;

                Phrase p3 = new Phrase();
                p3.Add(c3);
                pdfTable2.AddCell(p3);

                Paragraph p = new Paragraph("  ");


                #region Section-1 
                PdfPTable pdfTable4 = new PdfPTable(4);
                pdfTable4.DefaultCell.Padding = 5;
                pdfTable4.WidthPercentage = 80;
                pdfTable4.DefaultCell.BorderWidth = 0.0f;



                pdfTable4 = new PdfPTable(dataGridView1.Columns.Count);
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    pdfTable4.AddCell(new Phrase(dataGridView1.Columns[j].HeaderText));

                }
                pdfTable4.HeaderRows = 1;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        if (dataGridView1[k, i].Value != null)
                        {



                            pdfTable4.AddCell(new Phrase(dataGridView1[k, i].Value.ToString()));





                        }
                    }
                }

                Chunk c4 = new Chunk("Current Date:    " + DateTime.Now.ToString("dd/MM/yyyy") + " ", FontFactory.GetFont("Times New Roman"));
                c4.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c4.Font.SetStyle(0);
                c4.Font.Size = 11;
                Phrase p4 = new Phrase();
                p4.Add(c4);


                Chunk c5 = new Chunk("Manager Name:   " + Manager_Name.Text + " ", FontFactory.GetFont("Times New Roman"));
                c5.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c5.Font.SetStyle(0);
                c5.Font.Size = 11;
                Phrase p5 = new Phrase();
                p5.Add(c5);

                Chunk c6 = new Chunk("Employee Name:  " + customer_name.Text + " ", FontFactory.GetFont("Times New Roman"));
                c6.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c6.Font.SetStyle(0);
                c6.Font.Size = 11;
                Phrase p6 = new Phrase();
                p6.Add(c6);


                Chunk c7 = new Chunk("Status:   " + order_type_name.Text + " ", FontFactory.GetFont("Times New Roman"));
                c7.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c7.Font.SetStyle(0);
                c7.Font.Size = 11;
                Phrase p7 = new Phrase();
                p7.Add(c7);


                Chunk c8 = new Chunk("Date From:   " + date_from.Text + " ", FontFactory.GetFont("Times New Roman"));
                c8.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c8.Font.SetStyle(0);
                c8.Font.Size = 11;
                Phrase p8 = new Phrase();
                p8.Add(c8);


                Chunk c9 = new Chunk("Date To:  " + date_to.Text + " ", FontFactory.GetFont("Times New Roman"));
                c9.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c9.Font.SetStyle(0);
                c9.Font.Size = 11;
                Phrase p9 = new Phrase();
                p9.Add(c9);

                pdfTable3.AddCell(p4);
                pdfTable3.AddCell(p5);
                pdfTable3.AddCell(p6);
                pdfTable3.AddCell(p7);
                pdfTable3.AddCell(p8);
                pdfTable3.AddCell(p9);


                string folderPath = "D:\\PDF\\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                //File Name 
                int fileCount = Directory.GetFiles("D:\\PDF").Length;
                string strFileName = "Invoice" + (fileCount + 1) + ".pdf";

                using (FileStream stream = new FileStream(folderPath + strFileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    pdfDoc.Add(pdfTable1);

                    pdfDoc.Add(pdfTable2);

                    pdfDoc.Add(par);
                    pdfDoc.Add(pdfTableBlank);

                    pdfDoc.Add(pdfTable3);

                    pdfDoc.Add(pdfTable4);

                    pdfDoc.Close();
                    stream.Close();
                }

                System.Diagnostics.Process.Start(folderPath + "\\" + strFileName);


            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public void Report_Supplier()
        {
            try
            {
                PdfPTable pdfTableBlank = new PdfPTable(1);
                PdfPTable pdfTable1 = new PdfPTable(1);//Here 1 is Used For Count of Column 
                PdfPTable pdfTable2 = new PdfPTable(1);
                PdfPTable pdfTable3 = new PdfPTable(1);

                //Font Style 
                System.Drawing.Font fontH1 = new System.Drawing.Font("Currier", 16);

                //pdfTable1.DefaultCell.Padding = 5; 
                pdfTable1.WidthPercentage = 80;
                pdfTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable1.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                //pdfTable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170); 
                pdfTable1.DefaultCell.BorderWidth = 0;


                //pdfTable1.DefaultCell.Padding = 5; 
                pdfTable2.WidthPercentage = 80;
                pdfTable2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable2.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                //pdfTab2e1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170); 
                pdfTable2.DefaultCell.BorderWidth = 0;

                //pdfTable1.DefaultCell.Padding = 5; 
                pdfTable3.WidthPercentage = 80;
                pdfTable3.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable3.DefaultCell.VerticalAlignment = Element.ALIGN_CENTER;
                //pdfTab2e1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170); 
                pdfTable3.DefaultCell.BorderWidth = 0;


                Paragraph par = new Paragraph();
                par.Add(" ");

                Chunk c1 = new Chunk("HR INVENTORY SYSTEM", FontFactory.GetFont("Times New Roman"));
                c1.Font.Color = new iTextSharp.text.BaseColor(192, 0, 0);
                c1.Font.SetStyle(0);
                c1.Font.Size = 14;
                Phrase p1 = new Phrase();
                p1.Add(c1);
                pdfTable1.AddCell(p1);
                Chunk c2 = new Chunk("UET LAHORE", FontFactory.GetFont("Times New Roman"));
                c2.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c2.Font.SetStyle(0);//0 For Normal Font 
                c2.Font.Size = 11;
                Phrase p2 = new Phrase();
                p2.Add(c2);
                pdfTable2.AddCell(p2);
                Chunk c3 = new Chunk("Customer Care CONTACT : 03XX-XXYYZZA", FontFactory.GetFont("Times New Roman"));
                c3.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c3.Font.SetStyle(0);
                c3.Font.Size = 11;

                Phrase p3 = new Phrase();
                p3.Add(c3);
                pdfTable2.AddCell(p3);

                Paragraph p = new Paragraph("  ");


                #region Section-1 
                PdfPTable pdfTable4 = new PdfPTable(4);
                pdfTable4.DefaultCell.Padding = 5;
                pdfTable4.WidthPercentage = 80;
                pdfTable4.DefaultCell.BorderWidth = 0.0f;



                pdfTable4 = new PdfPTable(dataGridView1.Columns.Count);
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    pdfTable4.AddCell(new Phrase(dataGridView1.Columns[j].HeaderText));

                }
                pdfTable4.HeaderRows = 1;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int k = 0; k < dataGridView1.Columns.Count; k++)
                    {
                        if (dataGridView1[k, i].Value != null)
                        {
                            pdfTable4.AddCell(new Phrase(dataGridView1[k, i].Value.ToString()));

                        }
                    }
                }

                Chunk c4 = new Chunk("Current Date:    " + DateTime.Now.ToString("M/d/yyyy") + " ", FontFactory.GetFont("Times New Roman"));
                c4.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c4.Font.SetStyle(0);
                c4.Font.Size = 11;
                Phrase p4 = new Phrase();
                p4.Add(c4);


                Chunk c5 = new Chunk("Manager Name:   " + Manager_Name.Text + " ", FontFactory.GetFont("Times New Roman"));
                c5.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c5.Font.SetStyle(0);
                c5.Font.Size = 11;
                Phrase p5 = new Phrase();
                p5.Add(c5);

                Chunk c6 = new Chunk("Supplier Name:  " + supplier_name.Text + " ", FontFactory.GetFont("Times New Roman"));
                c6.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c6.Font.SetStyle(0);
                c6.Font.Size = 11;
                Phrase p6 = new Phrase();
                p6.Add(c6);


                Chunk c7 = new Chunk("Status:   " + Order_type_sup.Text + " ", FontFactory.GetFont("Times New Roman"));
                c7.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c7.Font.SetStyle(0);
                c7.Font.Size = 11;
                Phrase p7 = new Phrase();
                p7.Add(c7);


                Chunk c8 = new Chunk("Date From:   " + date_from.Text + " ", FontFactory.GetFont("Times New Roman"));
                c8.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c8.Font.SetStyle(0);
                c8.Font.Size = 11;
                Phrase p8 = new Phrase();
                p8.Add(c8);


                Chunk c9 = new Chunk("Date To:  " + date_to.Text + " ", FontFactory.GetFont("Times New Roman"));
                c9.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
                c9.Font.SetStyle(0);
                c9.Font.Size = 11;
                Phrase p9 = new Phrase();
                p9.Add(c9);

                pdfTable3.AddCell(p4);
                pdfTable3.AddCell(p5);
                pdfTable3.AddCell(p6);
                pdfTable3.AddCell(p7);
                pdfTable3.AddCell(p8);
                pdfTable3.AddCell(p9);


                string folderPath = "D:\\PDF\\";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                //File Name 
                int fileCount = Directory.GetFiles("D:\\PDF").Length;
                string strFileName = "DescriptionForm" + (fileCount + 1) + ".pdf";

                using (FileStream stream = new FileStream(folderPath + strFileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();

                    pdfDoc.Add(pdfTable1);

                    pdfDoc.Add(pdfTable2);

                    pdfDoc.Add(par);
                    pdfDoc.Add(pdfTableBlank);

                    pdfDoc.Add(pdfTable3);

                    pdfDoc.Add(pdfTable4);

                    pdfDoc.Close();
                    stream.Close();
                }

                System.Diagnostics.Process.Start(folderPath + "\\" + strFileName);


            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private void button4_Click_2(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Customer")
            {
                Report_Customer();
            }

            if (tabControl1.SelectedTab.Text == "Supplier")
            {
                Report_Supplier();
            }
        }

        public void Search_Cus()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select  InvOrderDetails.OrderId ,InvCategory.Name As Category_Name ,     InvItem.Name As Product_Name ,  InvOrder.OrderDate, InvOrderType.TypeName As Status ,   InvCustomer.Name AS Customer_Name , InvOrderDetails.Quantity from InvOrderDetails join InvOrder on InvOrderDetails.OrderId = InvOrder.Id join InvOrderType on InvOrder.OrderType = InvOrderType.Id join InvItem on InvOrderDetails.ProductId = InvItem.Id join InvCategory on InvCategory.Id= InvItem.CategoryId  join InvCustomer on InvOrder.AccountId = InvCustomer.AccountId where InvOrderDetails.OrderId = '" + OrderId_Sup.Text + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                customer_name.Text = dr["Customer_Name"].ToString();
                order_type_name.Text = dr["Status"].ToString();

            }


            con.Close();
        }


        public void Search_Sup()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = " select  InvOrderDetails.OrderId ,InvCategory.Name As Category_Name ,     InvItem.Name As Product_Name ,  InvOrder.OrderDate, InvOrderType.TypeName As Status ,   InvSupplier.Name AS Supplier , InvOrderDetails.Quantity from InvOrderDetails join InvOrder on InvOrderDetails.OrderId = InvOrder.Id join InvOrderType on InvOrder.OrderType = InvOrderType.Id join InvItem on InvOrderDetails.ProductId = InvItem.Id join InvCategory on InvCategory.Id= InvItem.CategoryId  join InvSupplier on InvOrder.AccountId = InvSupplier.AccountId where InvOrderDetails.OrderId = '" + OrderId_Sup.Text + "' ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            foreach (DataRow dr in dt.Rows)
            {
                supplier_name.Text = dr["Supplier"].ToString();
                Order_type_sup.Text = dr["Status"].ToString();

            }


            con.Close();
        }
        private void button5_Click_1(object sender, EventArgs e)
        {

            if (tabControl1.SelectedTab.Text == "Customer")
            {
                Search_Cus();
            }

            if (tabControl1.SelectedTab.Text == "Supplier")
            {
                Search_Sup();
            }


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void date_to_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void product_id_sup_TextChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void OrderId_Sup_TextChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text == "Customer")
            {
                textBox8.Text = order_id_cus.Text;
            }
            else
         if (tabControl1.SelectedTab.Text == "Supplier")
            {
                textBox8.Text = OrderId_Sup.Text;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SupplierOrderType();
            SupplierOrderTypeId();
        }

        private void button4_Click_3(object sender, EventArgs e)
        {
            SupplierOrder();
            OrderIdIntSup();


        }

        private void button8_Click(object sender, EventArgs e)
        {
            int r = (OrderStock() - OrderStockIssue()) + (Int32.Parse(quantity.Text));

            textBox2.Text = r.ToString();

            SupplierOrderDetails();
            displayStockSupplier();

            





        }

        private void order_type_name_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox10_Enter(object sender, EventArgs e)
        {

        }

        private void product_id_cus_TextChanged(object sender, EventArgs e)
        {

        }

        private void quantity_Validating(object sender, CancelEventArgs e)
        {

        }
        private void supplier_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void Order_type_sup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {

        }
      //  internal static bool mLogout = false;
        private void button11_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmdai = con.CreateCommand();
            cmdai.CommandType = CommandType.Text;
            cmdai.CommandText = " delete from Login_User  ";
            cmdai.ExecuteNonQuery();
            Form1 f = new Form1();
            f.Show();
            this.Hide();
          
            con.Close();





            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            cmd.CommandType = CommandType.Text;

            {
                cmd.CommandText = "Insert into InvOrderDetails values('" + textBox8.Text + "' , '" + textBox7.Text + "'  , '" + textBox4.Text + "')";


                cmd.ExecuteNonQuery();
                MessageBox.Show("record inserted");




                con.Close();
            }
        }

        private void quantity_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            
        }

        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            validatequantity();

        }
        public bool validatequantity(){
          bool  status = false;
            if (Int32.Parse(maskedTextBox1.Text) > Int32.Parse (textBox1.Text))
            {
                errorProvider1.SetError(maskedTextBox1, "kam value daluuuuu");
                status = false;
            }
            else
            {
                errorProvider1.SetError(maskedTextBox1, "");
                status = true;
            }
            return status;
        }
    }
}

#endregion
#endregion