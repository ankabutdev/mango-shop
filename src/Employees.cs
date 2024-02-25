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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Collections;
using System.Data.Common;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using MangoShop.Assets;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MangoShop
{
    public partial class Employees : Form
    {
        public Employees()
        {
            InitializeComponent();
            populate();
        }

        private void populate()
        {
            try
            {
                string jsonFilePath = "Assets/EmployeeJson.json";
                string jsonContent = File.ReadAllText(jsonFilePath);
                var currency = JsonConvert.DeserializeObject<List<EmployeesJson>>(jsonContent);

                n = 0;
                foreach (var item in currency)
                {
                    EmployeesDGV.Rows.Add(n + 1, item.EmpName, item.EmpPhone, item.EmpAdd, item.EmpPass);
                    n++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")
                {
                    MessageBox.Show("Missing Information");
                }
                else
                {

                    string jsonFilePath = "Assets/EmployeeJson.json";
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var currency = JsonConvert.DeserializeObject<List<EmployeesJson>>(jsonContent);

                    EmployeesJson employeesJson = new EmployeesJson()
                    {
                        EmpName = EmpNameTb.Text,
                        EmpPhone = Convert.ToInt64(EmpPhoneTb.Text),
                        EmpAdd = EmpAddTb.Text,
                        EmpPass = EmpPassTb.Text,
                    };

                    currency.Add(employeesJson);
                    var jsoncontent = JsonConvert.SerializeObject(currency);
                    File.WriteAllText(jsonFilePath, jsoncontent);

                    EmployeesDGV.Rows.Clear();
                    n = 0;
                    foreach (var item in currency)
                    {
                        EmployeesDGV.Rows.Add(n + 1, item.EmpName, item.EmpPhone, item.EmpAdd, item.EmpPass);
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
        private void Clear()
        {
            EmpNameTb.Text = "";
            EmpPassTb.Text = "";
            EmpPhoneTb.Text = "";
            EmpAddTb.Text = "";
        }
        private void ClearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }
        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonFilePath = "Assets/EmployeeJson.json";
                string jsonContent = File.ReadAllText(jsonFilePath);
                var currency = JsonConvert.DeserializeObject<List<EmployeesJson>>(jsonContent);

                var row = EmployeesDGV.CurrentCell.RowIndex;
                bool bol = true;
                foreach (var item in currency)
                {
                    if (EmpNameTb.Text == item.EmpName && EmpPhoneTb.Text == item.EmpPhone.ToString() && EmpAddTb.Text == item.EmpAdd && EmpPassTb.Text == item.EmpPass)
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
                    foreach (var item in currency)
                    {
                        if (EmployeesDGV.Rows[row].Cells[1].Value.ToString() == item.EmpName && EmployeesDGV.Rows[row].Cells[2].Value.ToString() == item.EmpPhone.ToString() && EmployeesDGV.Rows[row].Cells[3].Value.ToString() == item.EmpAdd && EmployeesDGV.Rows[row].Cells[4].Value.ToString() == item.EmpPass)
                        {
                            currency.Remove(item);
                            break;
                        }
                    }
                    var jsoncontent = JsonConvert.SerializeObject(currency);
                    File.WriteAllText(jsonFilePath, jsoncontent);
                    EmployeesDGV.Rows.Clear();
                    populate();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (EmpNameTb.Text == "" || EmpPhoneTb.Text == "" || EmpAddTb.Text == "" || EmpPassTb.Text == "")
                {
                    MessageBox.Show("Select The Employee To Be Updated");
                }
                else
                {
                    string jsonFilePath = "Assets/EmployeeJson.json";
                    string jsonContent = File.ReadAllText(jsonFilePath);
                    var currency = JsonConvert.DeserializeObject<List<EmployeesJson>>(jsonContent);
                    var row = EmployeesDGV.CurrentCell.RowIndex;
                    foreach (var item in currency)
                    {
                        if (EmployeesDGV.Rows[row].Cells[1].Value.ToString() == item.EmpName && EmployeesDGV.Rows[row].Cells[2].Value.ToString() == item.EmpPhone.ToString() && EmployeesDGV.Rows[row].Cells[3].Value.ToString() == item.EmpAdd && EmployeesDGV.Rows[row].Cells[4].Value.ToString() == item.EmpPass)
                        {
                            item.EmpName = EmpNameTb.Text;
                            item.EmpPhone = Convert.ToInt64(EmpPhoneTb.Text);
                            item.EmpAdd = EmpAddTb.Text;
                            item.EmpPass = EmpPassTb.Text;
                            break;
                        }
                    }
                    var jsoncontent = JsonConvert.SerializeObject(currency);
                    File.WriteAllText(jsonFilePath, jsoncontent);
                    EmployeesDGV.Rows.Clear();
                    populate();
                    Clear();

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        private void label9_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }
        private void label2_Click(object sender, EventArgs e)
        {
            Items items = new Items();
            items.Show();
            this.Hide();
        }
        int n = 0;
        private void BillDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var row = EmployeesDGV.CurrentCell.RowIndex;
                EmpNameTb.Text = EmployeesDGV.Rows[row].Cells[1].Value.ToString();
                EmpPhoneTb.Text = EmployeesDGV.Rows[row].Cells[2].Value.ToString();
                EmpAddTb.Text = EmployeesDGV.Rows[row].Cells[3].Value.ToString();
                EmpPassTb.Text = EmployeesDGV.Rows[row].Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }  
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
