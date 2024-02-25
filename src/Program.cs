using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MangoShop
{
    internal static class Program
    {
        /// <summary>
        /// App  uchun asosiy kirish.
        /// MAIN
        /// </summary> 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(mainForm: new Splash());
        }
    }
}
