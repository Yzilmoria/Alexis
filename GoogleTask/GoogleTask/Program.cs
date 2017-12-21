using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string searchline = Console.ReadLine();
            Console.WriteLine("Let me google that for you");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            string[] searchwords = searchline.Split(' ');
            ProcessStartInfo startInfo = new ProcessStartInfo("chrome.exe");
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            startInfo.Arguments = "http://www.google.com/search?q=" + String.Join("+", searchwords.ToArray());

            Process google = Process.Start(startInfo);
            Console.WriteLine("Searching: " + "http://www.google.com/search?q=" + String.Join("+", searchwords.ToArray()));
            //Process.Start("http://www.google.com/search?q=" + String.Join("+", searchwords.ToArray()));
            Thread.Sleep(TimeSpan.FromSeconds(10));
        }
    }
}
