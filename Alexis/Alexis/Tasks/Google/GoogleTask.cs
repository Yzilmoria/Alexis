using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alexis.Enums;

namespace Alexis.Tasks
{
    class GoogleTask : iTask
    {
        public static TaskType googleTask = TaskType.GOOGLE;
        public string connection { get; set; }
        public bool foreground { get; set; }

        public GoogleTask()
        {
        }

        public bool doGoogle()
        {

            Process myProcess = new Process();
            try
            {
                myProcess.StartInfo.UseShellExecute = true;
                myProcess.StartInfo.FileName = "C:\\Users\\Irida\\Documents\\Visual Studio 2017\\Projects\\GoogleTask\\GoogleTask\\bin\\Debug\\GoogleTask.exe";
                myProcess.StartInfo.CreateNoWindow = false;
                myProcess.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if (!myProcess.HasExited) { myProcess.Kill(); }
            return true;
        }

        public bool terminate()
        {
            return false;
        }

        public bool isTerminated()
        {
            return false;
        }

        public bool addCommand(string command)
        {
            string[] searchwords = command.Split(' ');
            ProcessStartInfo startInfo = new ProcessStartInfo("chrome.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            startInfo.Arguments = "http://www.google.com/search?q=" + String.Join("+", searchwords.ToArray());
            Process.Start(startInfo);
            Thread.Sleep(TimeSpan.FromSeconds(10));
            return true;
        }

        public bool hasReply()
        {
            return false;
        }

        public string getReply()
        {
            return "Let me google that for you!";
        }

        public bool start()
        {
            return true;
        }
    }
}
