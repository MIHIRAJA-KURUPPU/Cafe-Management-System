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

namespace CafeCraze__Cafe_Management_System
{
    public partial class ViewOrders : Form
    {
        public ViewOrders()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\mihir\Documents\Cafedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            Con.Open();
            string query = "select * from OrdersTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            OrdersGV.DataSource = ds.Tables[0];

            Con.Close();

        }
        private void ViewOrders_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void OrdersGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(printPreviewDialog1.ShowDialog() == DialogResult.OK)
            { 
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("======CafeCraze Software======", new Font("Century", 20, FontStyle.Bold), Brushes.Blue, new Point(200,40));
            e.Graphics.DrawString("======Order Summery======",new Font("Century",20, FontStyle.Bold),Brushes.Red,new Point(240,70));
            e.Graphics.DrawString("Order Number : " + OrdersGV.SelectedRows[0].Cells[0].Value.ToString(), new Font("Century", 15, FontStyle.Regular), Brushes.Black, new Point(120, 135));
            e.Graphics.DrawString("Order Date : " + OrdersGV.SelectedRows[0].Cells[1].Value.ToString(), new Font("Century", 15, FontStyle.Regular), Brushes.Black, new Point(120, 170));
            e.Graphics.DrawString("Seller Name : " + OrdersGV.SelectedRows[0].Cells[2].Value.ToString(), new Font("Century", 15, FontStyle.Regular), Brushes.Black, new Point(120, 205));
            e.Graphics.DrawString("Amount (Rs.) : " + OrdersGV.SelectedRows[0].Cells[3].Value.ToString(), new Font("Century", 15, FontStyle.Regular), Brushes.Black, new Point(120, 240));
            e.Graphics.DrawString("======Order Summery======", new Font("Century", 20, FontStyle.Bold), Brushes.Red, new Point(240, 340));
        }
    }
}
