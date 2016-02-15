using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandPrompt
{
    class Program
    {
        static void Main(string[] args)
        {
            //var proc1 = new ProcessStartInfo();

            string command = @" C:\sindows\sysWOW64\inetsrv\appcmd.exe list modules " + "\"RewriteModule\"";
            string command2 = @" C:\myscripts\reverseproxy\IV-Request.txt";
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;

            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "/c " + command2;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            process.StartInfo = startInfo;
            process.Start();

            var results = process.StandardOutput.ReadLine();
            Console.WriteLine(results);
            Console.ReadLine();

        }
    }
}
