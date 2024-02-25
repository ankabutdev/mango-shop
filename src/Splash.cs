using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangoShop
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        int startPos = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                startPos += 2;
                progressBar1.Value = startPos;
                pratsentlbl.Text = startPos.ToString() + "%";

                if (progressBar1.Value >= 100)
                {
                    progressBar1.Value = 0;
                    timer1.Stop();
                    Login log = new Login();
                    log.Show();
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }


}
