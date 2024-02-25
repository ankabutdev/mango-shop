using MangoShop.Assets;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangoShop
{
    public partial class Billing : Form
    {
        public Billing()
        {
            InitializeComponent();
            BillingName.Text = "";
            populate();
        }
        private void populate()
        {
            try
            {
                string jsonFilePath = "Assets/ItemJson.json";
                string jsonContent = File.ReadAllText(jsonFilePath);
                var currency = JsonConvert.DeserializeObject<List<ItemJson>>(jsonContent);
                n = 0;
                foreach (var item in currency)
                {
                    ItemDGV.Rows.Add(n + 1, item.ItName, item.ItQty, item.ItPrice, item.ItCat);
                    n++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        int n = 0, GrdTotal = 0, Amount;
        private void AddToBillBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var row = ItemDGV.CurrentCell.RowIndex;

                if (!(Convert.ToInt64(ItQtyTb.Text) > Convert.ToInt64(ItemDGV.Rows[row].Cells[2].Value.ToString())))
                {
                    if (ItQtyTb.Text == "" || ItNameTb.Text == "")
                    {
                        MessageBox.Show("Enter Quantity");
                    }
                    else
                    {
                        int total = (int)(Convert.ToInt64(ItQtyTb.Text) * Convert.ToInt64(ItPriceTb.Text));

                        string jsonFilePath = "Assets/ItemJson.json";
                        string jsonContent = File.ReadAllText(jsonFilePath);
                        var currency = JsonConvert.DeserializeObject<List<ItemJson>>(jsonContent);

                        foreach (var item in currency)
                        {
                            if(item.ItName == ItemDGV.Rows[row].Cells[1].Value.ToString() && item.ItQty.ToString() == ItemDGV.Rows[row].Cells[2].Value.ToString() && item.ItPrice.ToString() == ItemDGV.Rows[row].Cells[3].Value.ToString())
                            {
                                BillDGV.Rows.Add(ItemDGV.Rows[row].Cells[0].Value.ToString(), item.ItName, item.ItPrice, ItQtyTb.Text, total);
                                item.ItQty = (int)(item.ItQty - Convert.ToInt64(ItQtyTb.Text));
                                if(item.ItQty <= 0)
                                {
                                    currency.Remove(item);
                                }
                                break;
                            }
                        }
                        var jsoncontent = JsonConvert.SerializeObject(currency);
                        File.WriteAllText(jsonFilePath, jsoncontent);

                        GrdTotal = GrdTotal + total;
                        Amount = GrdTotal;
                        TotalLbl.Text = "Rs " + GrdTotal;
                        n++;

                        ItemDGV.Rows.Clear();
                        populate();
                        Reset();
                    }
                }
                else MessageBox.Show("Quentity not enough");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Reset()
        {
            try
            {
                ItPriceTb.Text = "";
                ItQtyTb.Text = "";
                ItNameTb.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            Reset();
            ClientNameTb.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (ClientNameTb.Text == "")
            {
                MessageBox.Show("Please Write Client Name");
            }
            else
            {
                try
                {
                    MessageBox.Show("Bill Saved Successfully");
                    ClientNameTb.Text = "";
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.Print();
                }
            }
            
        }
        private void Billing_Load(object sender, EventArgs e)
        {
            BillingName.Text = Login.EmployeeName;
        }

        long prodid, prodprice, prodqty, tottal, pos = 60;

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ItemDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var row = ItemDGV.CurrentCell.RowIndex;
            ItNameTb.Text = ItemDGV.Rows[row].Cells[1].Value.ToString();
            ItPriceTb.Text = ItemDGV.Rows[row].Cells[3].Value.ToString();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        string prodname;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int x = 300;
            int y = 100;
            int lineHeight = 20;

            e.Graphics.DrawString("Mango Shop", new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Green, new Point(x, y));
            y += lineHeight;
            e.Graphics.DrawString("ID PRODUCT PRICE QUANTITY TOTAL", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Green, new Point(x, y));
            y += lineHeight;

            foreach (DataGridViewRow row in BillDGV.Rows)
            {
                prodid = Convert.ToInt64(row.Cells["Column1"].Value);
                prodname = "" + row.Cells["Column2"].Value;
                prodprice = Convert.ToInt64(row.Cells["Column3"].Value);
                prodqty = Convert.ToInt64(row.Cells["Column4"].Value);
                tottal = Convert.ToInt64(row.Cells["Column5"].Value);
                e.Graphics.DrawString("" + prodid, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Green, new Point(x, y));
                e.Graphics.DrawString("" + prodname, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Green, new Point(x + 100, y));
                e.Graphics.DrawString("" + prodprice, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Green, new Point(x + 150, y));
                e.Graphics.DrawString("" + prodqty, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Green, new Point(x + 200, y));
                e.Graphics.DrawString("" + tottal, new Font("Century Gothic", 8, FontStyle.Bold), Brushes.Green, new Point(x + 250, y));
                y += lineHeight;
            }

            e.Graphics.DrawString("Grand Total: Result " + Amount, new Font("Century Gothic", 12, FontStyle.Bold), Brushes.Green, new Point(x, y));
            y += lineHeight;
            e.Graphics.DrawString("********** ManogoShop **********", new Font("Century Gothic", 10, FontStyle.Bold), Brushes.Green, new Point(x, y));
            BillDGV.Rows.Clear();
            BillDGV.Refresh();
            pos = 100;
            Amount = 0;
        }

        int stock = 0, Key = 0;
        private void ItemDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                DataGridViewRow row = ItemDGV.Rows[i];

                ItNameTb.Text = row.Cells[1].Value.ToString();
                ItPriceTb.Text = row.Cells[3].Value.ToString();
                if (ItNameTb.Text == "")
                {
                    stock = 0;
                    Key = 0;
                }
                else
                {
                    stock = (int)Convert.ToInt64(row.Cells[2].Value.ToString());
                    Key = (int)Convert.ToInt64(row.Cells[0].Value.ToString());
                }        
            }
            catch (Exception) { }
        }
    }
}
