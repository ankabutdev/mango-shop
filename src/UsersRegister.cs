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

namespace MangoShop
{
    public partial class UsersRegister : Form
    {
        public UsersRegister()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ClearRegister();
        }
        private void ClearRegister()
        {
            try
            {
                txtUserName.Focus();
                txtAdress.Clear();
                txtPhoneNumber.Clear();
                txtPassword.Clear();
                txtConfirmPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        public void button1_Click(object sender, EventArgs e)
        {

            try
            {
                if (Convert.ToInt64(txtPhoneNumber.Text) > 0)
                {
                    if (txtUserName.Text == "" || txtPhoneNumber.Text == "" || txtAdress.Text == "" || txtPassword.Text == "" || txtConfirmPassword.Text == "")
                    {
                        MessageBox.Show("Please Enter Information");
                    }
                    else
                    {
                        if (txtPassword.Text == txtConfirmPassword.Text)
                        {
                            try
                            {
                                string jsonFilePath = "Assets/EmployeeJson.json";
                                string jsonContent = File.ReadAllText(jsonFilePath);
                                var currency = JsonConvert.DeserializeObject<List<EmployeesJson>>(jsonContent);

                                EmployeesJson employeesJson = new EmployeesJson()
                                {
                                    EmpName = txtUserName.Text,
                                    EmpPhone = Convert.ToInt64(txtPhoneNumber.Text),
                                    EmpAdd = txtAdress.Text,
                                    EmpPass = txtPassword.Text
                                };

                                currency.Add(employeesJson);
                                var jsoncontent = JsonConvert.SerializeObject(currency);
                                File.WriteAllText(jsonFilePath, jsoncontent);                                          
                                ClearRegister();
                                MessageBox.Show("You have successfully registered");
                                Login login = new Login();
                                login.Show();
                                this.Hide();
                            }
                            catch (Exception Ex)
                            {
                                MessageBox.Show(Ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Wrong Password");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
