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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MangoShop
{
    public partial class Items : Form
    {
        public Items()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\iddiu\OneDrive\Документы\MangoDb.mdf;Integrated Security=True;Connect Timeout=30");
        
        bool bu;
        private void populate()
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
        private void Clear()
        {
            ItNameTb.Text = "";
            ItQtyTb.Text = "";
            PriceTb.Text = "";
            CatCb.Text = "";
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (ItNameTb.Text == "" || ItQtyTb.Text == "" || PriceTb.Text == "" || CatCb.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    if (!(Convert.ToInt64(ItQtyTb.Text) <= 0))
                    {
                        string jsonFilePath = "Assets/ItemJson.json";
                        string jsonContent = File.ReadAllText(jsonFilePath);
                        var currency = JsonConvert.DeserializeObject<List<ItemJson>>(jsonContent);

                        ItemJson itemJson = new ItemJson()
                        {
                            ItName = ItNameTb.Text,
                            ItQty = Convert.ToInt64(ItQtyTb.Text),
                            ItPrice = Convert.ToInt64(PriceTb.Text),
                            ItCat = CatCb.Text
                        };
                        currency.Add(itemJson);
                        var jsoncontent = JsonConvert.SerializeObject(currency);
                        File.WriteAllText(jsonFilePath, jsoncontent);

                        ItemDGV.Rows.Clear();
                        n = 0;
                        foreach (var item in currency)
                        {
                            ItemDGV.Rows.Add(n + 1, item.ItName, item.ItQty, item.ItPrice, item.ItCat);
                            n++;
                        }
                        Clear();
                    }
                    
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            string jsonFilePath = "Assets/ItemJson.json";
            string jsonContent = File.ReadAllText(jsonFilePath);
            var currency = JsonConvert.DeserializeObject<List<ItemJson>>(jsonContent);

            var row = ItemDGV.CurrentCell.RowIndex;
            bool bol = true;
            foreach (var item in currency)
            {
                if (ItNameTb.Text == item.ItName && ItQtyTb.Text == item.ItQty.ToString() && PriceTb.Text == item.ItPrice.ToString())
                {
                    bol = false;
                    break;
                }
            }
            if (bol)
            {
                MessageBox.Show("Select The Employee To Be Deleted");
            }
            else
            {
                try
                {
                    foreach (var item in currency)
                    {
                        if (ItemDGV.Rows[row].Cells[1].Value.ToString() == item.ItName && ItemDGV.Rows[row].Cells[2].Value.ToString() == item.ItQty.ToString() && ItemDGV.Rows[row].Cells[3].Value.ToString() == item.ItPrice.ToString())
                        {
                            currency.Remove(item);
                            break;
                        }
                    }
                    var jsoncontent = JsonConvert.SerializeObject(currency);
                    File.WriteAllText(jsonFilePath, jsoncontent);
                    ItemDGV.Rows.Clear();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ItNameTb.Text == "" || ItQtyTb.Text == "" || PriceTb.Text == "" || CatCb.Text == "")
            {
                MessageBox.Show("Select The Item To Be Updated");
            }
            else
            {
                try
                {
                    string jsonFilePath = "Assets/ItemJson.json";
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var currency = JsonConvert.DeserializeObject<List<ItemJson>>(jsonContent);
                    var row = ItemDGV.CurrentCell.RowIndex;
                    foreach (var item in currency)
                    {
                        if (ItemDGV.Rows[row].Cells[1].Value.ToString() == item.ItName && ItemDGV.Rows[row].Cells[2].Value.ToString() == item.ItQty.ToString() && ItemDGV.Rows[row].Cells[3].Value.ToString() == item.ItPrice.ToString())
                        {
                            item.ItName = ItNameTb.Text;
                            item.ItQty = Convert.ToInt64(ItQtyTb.Text);
                            item.ItPrice = Convert.ToInt64(PriceTb.Text);
                            item.ItCat = CatCb.Text;
                            break;
                        }
                    }
                    var jsoncontent = JsonConvert.SerializeObject(currency);
                    File.WriteAllText(jsonFilePath, jsoncontent);
                    ItemDGV.Rows.Clear();
                    populate();
                    Clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }
        private void CatCb_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                e.KeyChar = (char)Keys.None;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Employees Obj = new Employees();
            Obj.Show();
            this.Hide();
        }
        int n = 0;
        private void EmployeesDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var row = ItemDGV.CurrentCell.RowIndex;
                ItNameTb.Text = ItemDGV.Rows[row].Cells[1].Value.ToString();
                ItQtyTb.Text = ItemDGV.Rows[row].Cells[2].Value.ToString();
                PriceTb.Text = ItemDGV.Rows[row].Cells[3].Value.ToString();
                CatCb.Text = ItemDGV.Rows[row].Cells[4].Value.ToString();
            }
            catch (Exception) { }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
