using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace KinectMicrophone
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            mainForm mf = new mainForm();
            Application.ThreadException += new ThreadExceptionEventHandler(mf.UnhandledThreadExceptionHandler);
            Application.Run(mf);
        }
    }
}
