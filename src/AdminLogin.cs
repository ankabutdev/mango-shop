﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangoShop
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PasswordTb.Text == "")
            {
                MessageBox.Show("Enter Password");
            }
            else if (PasswordTb.Text == "Pass")
            {
                Employees Emp = new Employees();
                Emp.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Wrong Admin Password");
            }

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label9_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
