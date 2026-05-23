using System;
using System.IO;
using System.Windows.Forms;
using GMS_Kabbo.Data;
using GMS_Kabbo.Forms;

namespace GMS_Kabbo
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            var dataDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dataDir);
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDir);

            LocalDbBootstrap.EnsureInstanceRunning();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new login());
        }
    }
}
