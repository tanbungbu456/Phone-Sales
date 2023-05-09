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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection cn = new SqlConnection("initial catalog = PhoneManagement; data source = DESKTOP-27VJS9E; integrated security = true");

        private void label9_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static String SellerName = "";
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void clear_Click(object sender, EventArgs e)
        {
            usertb.Text = "";
            passtb.Text = "";
        }

        private void bSubmit_Click(object sender, EventArgs e)
        {
            if(usertb.Text == "" ||  passtb.Text == "")
            {
                MessageBox.Show("Enter your UserName and PassWord please");
            }
            else
            {
                if (rolecb.SelectedIndex > -1)
                {
                    if (rolecb.SelectedItem.ToString() == "STAFF")
                    {
                        if (usertb.Text == "Admin1" && passtb.Text == "admin1123")
                        {
                            ProductForm Prod = new ProductForm();
                            Prod.Show();
                            this.Hide();
                        }
                        else if (usertb.Text == "Admin2" && passtb.Text == "admin2123")
                        {
                            ProductForm Prod = new ProductForm();
                            Prod.Show();
                            this.Hide();
                        }
                        else if (usertb.Text == "Admin3" && passtb.Text == "admin3123")
                        {
                            ProductForm Prod = new ProductForm();
                            Prod.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("If you are the ADMIN, please enter the CORRECT UserName and PassWord");
                        }
                        
                    }
                    else if (rolecb.SelectedItem.ToString() == "AGENT")
                    {
                        //MessageBox.Show("You are an AGENT");
                        cn.Open();
                        SqlDataAdapter sda = new SqlDataAdapter("Select count(8) from Sellers where SellerName='"+usertb.Text+"' and SellerPass='"+passtb.Text+"'", cn);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        if (dt.Rows[0][0].ToString() == "1")
                        {
                            SellerName = usertb.Text;
                            SellingForm Selling = new SellingForm();
                            this.Hide();
                            Selling.Show();
                            cn.Close();
                        }
                        else
                        {
                            MessageBox.Show("Wrong UserName or PassWord");
                        }
                        cn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Select a ROLE");
                }
            }
        }
    }
}
