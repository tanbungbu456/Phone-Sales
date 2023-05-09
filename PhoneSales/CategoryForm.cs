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
    public partial class CategoryForm : Form
    {
        SqlConnection cn;
        SqlDataAdapter data;
        DataTable tb;
        public CategoryForm()
        {
            InitializeComponent();
        }
        
        private void populate()
        {
            cn.Open();
            string query = "select * from Categories";
            data = new SqlDataAdapter(query, cn);
            SqlCommandBuilder buidler = new SqlCommandBuilder(data);
            var ds = new DataSet();
            data.Fill(ds);
            catgrd.DataSource = ds.Tables[0];   
            cn.Close();
        }
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            string sql = "initial catalog = PhoneManagement; data source = DESKTOP-27VJS9E; integrated security = true";
            cn = new SqlConnection(sql);
            populate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                string query = "insert into Categories values ('" + CatID.Text + "', '" + CatName.Text + "', '" + CatDes.Text + "')";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Category added successfully!");
                cn.Close();
                populate();
            }catch (Exception ex) 
                {
                MessageBox.Show(ex.Message);
                }
        }

        private void catgrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (catgrd.SelectedRows.Count > 0)
            {
                CatID.Text = catgrd.SelectedRows[0].Cells[0].Value.ToString();
                CatName.Text = catgrd.SelectedRows[0].Cells[1].Value.ToString();
                CatDes.Text = catgrd.SelectedRows[0].Cells[2].Value.ToString();

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatID.Text == "")
                {
                    MessageBox.Show("Select the category to delete");
                }
                else
                {
                    cn.Open();
                    string query = "delete from Categories where CatID = " + CatID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category deleted successfully");
                    cn.Close();
                    populate();
                }
            }catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if(CatID.Text=="" || CatName.Text=="" || CatDes.Text=="")
                {
                    MessageBox.Show("Missing Information!");
                }
                else
                {
                    cn.Open();
                    string query = "update Categories set CatName='" + CatName.Text + "', CatDes='" + CatDes.Text + "' where CatID='" + CatID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Category updated succsessfully");
                    cn.Close();
                    populate();
                }
                
            }catch (Exception ex) 
            { 
                MessageBox.Show (ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm Prod = new ProductForm();
            Prod.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SellerForm Seller = new SellerForm();
            Seller.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SellingForm Selling = new SellingForm();
            Selling.Show();
            this.Hide();
        }
    }
}
