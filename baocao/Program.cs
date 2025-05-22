using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baocao
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            //Application.Run(new quenmatkhau());
            //Application.Run(new dangky());
           
            //Application.Run(new doimatkhau("admin"));

            
            Application.Run(new dangky());
            



        }
    }
}
