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
namespace PhoneSales
{
    public partial class ProductForm : Form
    {
        
        SqlDataAdapter data;
        DataTable tb;

        SqlConnection cn = new SqlConnection("initial catalog = PhoneManagement; data source = DESKTOP-27VJS9E; integrated security = true");
        public ProductForm()
        {
            InitializeComponent();
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void populate()
        {
            cn.Open();
            string query = "select * from Products";
            data = new SqlDataAdapter(query, cn);
            SqlCommandBuilder buidler = new SqlCommandBuilder(data);
            var ds = new DataSet();
            data.Fill(ds);
            prodgrd.DataSource = ds.Tables[0];
            cn.Close();
        }

        public void categoryFilter()
        {
            if (cn.State == ConnectionState.Closed) cn.Open();
            
            string query = "select * from Products where ProdCategory='"+catcb2.SelectedValue.ToString()+"'";
            data = new SqlDataAdapter(query, cn);
            SqlCommandBuilder buidler = new SqlCommandBuilder(data);
            var ds = new DataSet();
            data.Fill(ds);
            prodgrd.DataSource = ds.Tables[0];
            cn.Close();
        }
        

        private void fillCombo()
        {
            //This method will bind the combobox with the database
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select CatName from Categories", cn);
            SqlDataReader dtr;
            dtr = cmd.ExecuteReader();
            DataTable dtb = new DataTable();
            dtb.Columns.Add("CatName", typeof(string));
            dtb.Load(dtr);
            CatCB.ValueMember = "CatName";
            CatCB.DataSource = dtb;
            cn.Close();
        }

        private void fillCombo2()
        {
            cn.Open();
            SqlCommand cmd = new SqlCommand("Select CatName from Categories", cn);
            SqlDataReader dtr;
            dtr = cmd.ExecuteReader();
            DataTable dtb = new DataTable();
            dtb.Columns.Add("CatName", typeof(string));
            dtb.Load(dtr);
            catcb2.ValueMember = "CatName";
            catcb2.DataSource = dtb;
            cn.Close();
        }
        private void ProductForm_Load(object sender, EventArgs e)
        {
            string sql = "initial catalog = PhoneManagement; data source = DESKTOP-27VJS9E; integrated security = true";
            cn = new SqlConnection(sql);
            fillCombo();
            populate();
            fillCombo2();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm Cat = new CategoryForm();
            Cat.Show();
            this.Hide();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                string query = "insert into Products values ('"+ProdID.Text+"', '"+ProdName.Text+"', '"+ProdQuantity.Text+"', '"+CatCB.SelectedValue.ToString()+"', '"+ProdPrice.Text+"')";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product added successfully!");
                cn.Close();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdID.Text = prodgrd.SelectedRows[0].Cells[0].Value.ToString();
            ProdName.Text = prodgrd.SelectedRows[0].Cells[1].Value.ToString();
            ProdQuantity.Text = prodgrd.SelectedRows[0].Cells[2].Value.ToString();
            ProdPrice.Text = prodgrd.SelectedRows[0].Cells[4].Value.ToString();
            CatCB.SelectedValue = prodgrd.SelectedRows[0].Cells[3].Value.ToString();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdID.Text == "")
                {
                    MessageBox.Show("Select the product to delete");
                }
                else
                {
                    cn.Open();
                    string query = "delete from Products where ProdID = " + ProdID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product deleted successfully");
                    cn.Close();
                    populate();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (ProdID.Text == "" || ProdName.Text == "")
                {
                    MessageBox.Show("Missing Information!");
                }
                else
                {
                    cn.Open();
                    string query = "update Products set ProdName='"+ProdName.Text+"', ProdQuantity="+ProdQuantity.Text+", ProdPrice="+ProdPrice.Text+", ProdCategory='"+CatCB.SelectedValue.ToString()+"' where ProdID='" + ProdID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product updated succsessfully");
                    cn.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SellerForm Sell = new SellerForm();
            Sell.Show();
            this.Hide();
        }

        private void catcb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            categoryFilter();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            populate();
            catcb2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SellingForm Sell = new SellingForm();
            Sell.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }
    }
}
