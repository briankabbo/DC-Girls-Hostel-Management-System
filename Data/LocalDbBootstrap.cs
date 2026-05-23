using System;
using System.Diagnostics;
using System.IO;

namespace GMS_Kabbo.Data
{
    internal static class LocalDbBootstrap
    {
        public const string InstanceName = "DCGirlsHostel";

        public static void EnsureInstanceRunning()
        {
            var sqllocaldb = FindSqlLocalDb();
            if (sqllocaldb == null)
                return;

            Run(sqllocaldb, $"create {InstanceName}");
            Run(sqllocaldb, $"start {InstanceName}");
        }

        private static string FindSqlLocalDb()
        {
            var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var candidate = Path.Combine(programFiles, @"Microsoft SQL Server\150\Tools\Binn\SqlLocalDB.exe");
            return File.Exists(candidate) ? candidate : null;
        }

        private static void Run(string exe, string arguments)
        {
            try
            {
                using (var process = Process.Start(new ProcessStartInfo(exe, arguments)
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                }))
                {
                    process?.WaitForExit(8000);
                }
            }
            catch
            {
                // Instance may already exist or be running.
            }
        }
    }
}
