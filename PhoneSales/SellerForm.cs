using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Markup;

namespace PhoneSales
{
    public partial class SellerForm : Form
    {
        SqlDataAdapter data;
        DataTable tb;
        public SellerForm()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection("initial catalog = PhoneManagement; data source = DESKTOP-27VJS9E; integrated security = true");

        private void populate()
        {
            cn.Open();
            string query = "select * from Sellers";
            data = new SqlDataAdapter(query, cn);
            SqlCommandBuilder buidler = new SqlCommandBuilder(data);
            var ds = new DataSet();
            data.Fill(ds);
            sellgrd.DataSource = ds.Tables[0];
            cn.Close();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            ProductForm Prod = new ProductForm();
            Prod.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CategoryForm Cat = new CategoryForm();  
            Cat.Show(); 
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                string query = "insert into Sellers values ('"+SellID.Text+ "', '"+SellName.Text+"', "+SellAge.Text+", '"+SellPhone.Text+"', '"+SellPassword.Text+"')";
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Seller added successfully!");
                cn.Close();
                populate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void sellgrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SellID.Text = sellgrd.SelectedRows[0].Cells[0].Value.ToString();
            SellName.Text = sellgrd.SelectedRows[0].Cells[1].Value.ToString();
            SellAge.Text = sellgrd.SelectedRows[0].Cells[2].Value.ToString();
            SellPhone.Text = sellgrd.SelectedRows[0].Cells[3].Value.ToString();
            SellPassword.Text = sellgrd.SelectedRows[0].Cells[4].Value.ToString();
        }

        private void SellerForm_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellID.Text == "" || SellName.Text == "")
                {
                    MessageBox.Show("Missing Information!");
                }
                else
                {
                    cn.Open();
                    string query = "update Sellers set SellerName='" + SellName.Text + "', SellerAge=" + SellAge.Text + ", SellerPhone=" + SellPhone.Text + ", SellerPass='" + SellPassword.Text + "' where SellerID='" + SellID.Text + "';";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller updated succsessfully");
                    cn.Close();
                    populate();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (SellID.Text == "")
                {
                    MessageBox.Show("Select the Seller to delete");
                }
                else
                {
                    cn.Open();
                    string query = "delete from Sellers where SellerID = " + SellID.Text + "";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Seller deleted successfully");
                    cn.Close();
                    populate();
                    SellID.Text = "";
                    SellName.Text = "";
                    SellAge.Text = "";
                    SellPhone.Text = "";
                    SellPassword.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SellingForm selling = new SellingForm();
            selling.Show();
            this.Hide();
        }
    }
    
}
