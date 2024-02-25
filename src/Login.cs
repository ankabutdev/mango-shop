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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public static string EmployeeName = "";
        private void label4_Click(object sender, EventArgs e)
        {
            AdminLogin Obj = new AdminLogin();
            Obj.Show();
            this.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonFilePath = "Assets/EmployeeJson.json";
                string jsonContent = File.ReadAllText(jsonFilePath);
                var currency = JsonConvert.DeserializeObject<List<EmployeesJson>>(jsonContent);

                foreach (var item in currency)
                {
                    if (item.EmpName == UnameTb.Text && item.EmpPass == PasswordTb.Text)
                    {
                        EmployeeName = item.EmpName;
                        Billing billing = new Billing();
                        billing.Show();
                        this.Hide();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void CreateBtn_Click(object sender, EventArgs e)
        {
            UsersRegister usersRegister = new UsersRegister();
            usersRegister.Show();
            this.Hide();
        }
    }
}
