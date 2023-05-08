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
using System.Windows.Markup;
using System.Data.SqlClient;
namespace PhoneSales
{
    public partial class SellingForm : Form
    {
        SqlDataAdapter data;
        DataTable tb;
        public SellingForm()
        {
            InitializeComponent();
        }
        SqlConnection cn = new SqlConnection("initial catalog = PhoneManagement; data source = DESKTOP-27VJS9E; integrated security = true");
        private void populate()
        {
            cn.Open();
            string query = "select ProdName, ProdPrice from Products";
            data = new SqlDataAdapter(query, cn);
            SqlCommandBuilder buidler = new SqlCommandBuilder(data);
            var ds = new DataSet();
            data.Fill(ds);
            prodgrd2.DataSource = ds.Tables[0];
            cn.Close();
        }
        private void populateBills()
        {
            cn.Open();
            string query = "select * from Bills";
            data = new SqlDataAdapter(query, cn);
            SqlCommandBuilder buidler = new SqlCommandBuilder(data);
            var ds = new DataSet();
            data.Fill(ds);
            sellgrd.DataSource = ds.Tables[0];
            cn.Close();
        }

        int flag = 0;
        private void SellingForm_Load(object sender, EventArgs e)
        {
            populate();
            populateBills();
            fillCombo();
            SellerName.Text = Form1.SellerName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void prodgrd2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ProdName.Text = prodgrd2.SelectedRows[0].Cells[0].Value.ToString();
            ProdPrice.Text = prodgrd2.SelectedRows[0].Cells[1].Value.ToString();
            flag = 1;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            labelDate.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }
        int Grdtotal = 0, n=0;

        private void button4_Click(object sender, EventArgs e)
        {
            if (BillID.Text == "")
            {
                MessageBox.Show("Missing Bill ID");
            }
            else
            {
                try
                {
                    cn.Open();
                    string query = "insert into Bills values ('" + BillID.Text + "', '" + SellerName.Text + "', '" + labelDate.Text + "', " + usdlbl.Text + ")";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order added successfully!");
                    cn.Close();
                    populateBills();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void sellgrd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            flag = 1;
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            if (sellgrd.SelectedRows.Count > 0)
            {
                string value1 = sellgrd.SelectedRows[0].Cells[0].Value.ToString();
                //string value2 = sellgrd.SelectedRows[1].Cells[0].Value.ToString();

                // use value variable here
                e.Graphics.DrawString("Phone Total Bill", new Font("Century Gothic", 25, FontStyle.Bold), Brushes.Red, new Point(230));
                e.Graphics.DrawString("Bill ID: " + value1, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));
                //e.Graphics.DrawString("Seller Name: " + value2, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));

            }
            else
            {
                // handle the case where no rows are selected
                MessageBox.Show("Error in Data, please choose again");
            }

            if (sellgrd.SelectedRows.Count > 0)
            {
                string value2 = sellgrd.SelectedRows[0].Cells[1].Value.ToString();
                //string value2 = sellgrd.SelectedRows[1].Cells[0].Value.ToString();

                // use value variable here               
                e.Graphics.DrawString("Seller Name: " + value2, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 100));
                //e.Graphics.DrawString("Seller Name: " + value2, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));

            }
            else
            {
                // handle the case where no rows are selected
                MessageBox.Show("Error in Data, please choose again");
            }

            if (sellgrd.SelectedRows.Count > 0)
            {
                string value2 = sellgrd.SelectedRows[0].Cells[2].Value.ToString();
                //string value2 = sellgrd.SelectedRows[1].Cells[0].Value.ToString();

                // use value variable here               
                e.Graphics.DrawString("Bill Date: " + value2, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 130));
                //e.Graphics.DrawString("Seller Name: " + value2, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));

            }
            else
            {
                // handle the case where no rows are selected
                MessageBox.Show("Error in Data, please choose again");
            }

            if (sellgrd.SelectedRows.Count > 0)
            {
                string value2 = sellgrd.SelectedRows[0].Cells[3].Value.ToString();
                //string value2 = sellgrd.SelectedRows[1].Cells[0].Value.ToString();

                // use value variable here               
                e.Graphics.DrawString("Total Amount: " + value2, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 160));
                //e.Graphics.DrawString("Seller Name: " + value2, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));

            }
            else
            {
                // handle the case where no rows are selected
                MessageBox.Show("Error in Data, please choose again");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void sellgrd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            flag = 1;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            populate();
            CatCB.Text = "";
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

        private void CatCB_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cn.Open();
            string query = "select ProdName, ProdPrice from Products where ProdCategory='" + CatCB.SelectedValue.ToString() + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, cn);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            prodgrd2.DataSource = ds.Tables[0];
            cn.Close();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 login = new Form1();
            login.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (sellgrd.SelectedRows.Count > 0)
                {
                    string value1 = sellgrd.SelectedRows[0].Cells[0].Value.ToString();
                    //string value2 = sellgrd.SelectedRows[1].Cells[0].Value.ToString();

                    // use value variable here

                    //e.Graphics.DrawString("Seller Name: " + value2, new Font("Century Gothic", 20, FontStyle.Bold), Brushes.Blue, new Point(100, 70));
                    cn.Open();
                    string query = "delete from Bills where BillID = " + value1 + "";
                    SqlCommand cmd = new SqlCommand(query, cn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Bill deleted successfully");
                    cn.Close();
                    populateBills();
                }
                else
                {
                    // handle the case where no rows are selected
                    MessageBox.Show("Error in Data, please choose again");
                }


                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ordergrd.SelectedCells != null && ordergrd.SelectedCells.Count > 0)
            {
                DataGridViewCell selectedCell = ordergrd.SelectedCells[0];
                DataGridViewRow selectedRow = selectedCell.OwningRow;
                ordergrd.Rows.Remove(selectedRow);
            }
            else
            {
                MessageBox.Show("No more order to delete");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (ProdName.Text == "" || ProdQuantity.Text == "")
            {
                MessageBox.Show("Missing Data");
            }
            else
            {
                int total = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQuantity.Text);
                DataGridViewRow nrow = new DataGridViewRow();
                nrow.CreateCells(ordergrd);
                nrow.Cells[0].Value = n + 1;
                nrow.Cells[1].Value = ProdName.Text;
                nrow.Cells[2].Value = ProdPrice.Text;
                nrow.Cells[3].Value = ProdQuantity.Text;
                nrow.Cells[4].Value = Convert.ToInt32(ProdPrice.Text) * Convert.ToInt32(ProdQuantity.Text);
                ordergrd.Rows.Add(nrow);
                n++;
                Grdtotal = Grdtotal + total;
                usdlbl.Text = Grdtotal.ToString();
            }
        }
    }
}
