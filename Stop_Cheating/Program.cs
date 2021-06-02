using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Stop_Cheating
{
    static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PythonResource pyt = new PythonResource();
            pyt.CreateDat();
            pyt.CreateDat2();
            pyt.CreateDat3();
            pyt.CreateDat4();
            pyt.CreateDir();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Student_Form());
        }
    }
}
