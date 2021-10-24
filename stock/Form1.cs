using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Catergory c = new Catergory();
            c.Show(); 

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Items i = new Items();
            i.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Faculty f = new Faculty();
            f.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SupplyMan s = new SupplyMan();
            s.Show(); 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Manager m = new Manager();
            m.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Account aa = new Account();
            aa.Show();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
