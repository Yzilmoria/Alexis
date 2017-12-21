using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace testTask
{
    class Program
    {
        static void Main(string[] args)
        {
            //Process myProcess = Process.Start("C:\\Users\\Irida\\Documents\\Visual Studio 2017\\Projects\\GoogleTask\\GoogleTask\\bin\\Debug\\GoogleTask.exe");
            //Thread.Sleep(TimeSpan.FromSeconds(15));
            //if (!myProcess.HasExited) { myProcess.Kill(); }

            Process myProcess = new Process();
            Console.WriteLine("Starting process");
            try
            {
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = "C:\\Users\\Irida\\Documents\\Visual Studio 2017\\Projects\\GoogleTask\\GoogleTask\\bin\\Debug\\GoogleTask.exe";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
                Console.WriteLine("Started the process: " + "C:\\Users\\Irida\\Documents\\Visual Studio 2017\\Projects\\GoogleTask\\GoogleTask\\bin\\Debug\\GoogleTask.exe");
                Thread.Sleep(TimeSpan.FromSeconds(15));
                if (!myProcess.HasExited) { myProcess.Kill(); }
                // This code assumes the process you are starting will terminate itself. 
                // Given that is is started without a window so you cannot terminate it 
                // on the desktop, it must terminate itself or you can do it programmatically
                // from this application using the Kill method.
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
