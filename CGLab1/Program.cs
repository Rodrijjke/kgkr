using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CGLab1.Components;

namespace CGLab1
{
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            new MainWindow().Run(60);
        }
    }
}
