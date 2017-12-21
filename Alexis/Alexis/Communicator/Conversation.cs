using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Alexis.Communicator
{
    public static class Conversation
    {
        private static Regex nameRegex = new Regex(@"\b(Alex|Alexis|Darling|Sweetie)\b", RegexOptions.IgnoreCase);
        private static Regex beyRegex = new Regex(@"^\A(bye|see you)", RegexOptions.IgnoreCase);
        private static Regex hiCheck = new Regex(@"^\A(hi|hello)", RegexOptions.IgnoreCase);
        private static Regex googleCheck = new Regex(@"\b(google)\b", RegexOptions.IgnoreCase);

        public static CommItem Interpret(string input) // Find out what kind of task a command is for
        {
            CommItem output = new CommItem();
            if(input == "")
            {
                return output;
            }
            bool withName = nameRegex.IsMatch(input);
            if (beyRegex.IsMatch(input))
            {
                output.commString = withName? nameRegex.Replace(input, "Jinx") : input;
                output.taskType = 1;
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                return output;
            }
            else if (hiCheck.IsMatch(input))
            {
                output.commString = sayHi.on(withName);
                output.taskType = 2;
            }
            else if (googleCheck.IsMatch(input))
            {
                Tasks.GoogleTask g = new Tasks.GoogleTask();
                g.doGoogle();
                output.commString = "Googling";
                output.taskType = 4;
            }
            else // Command - send to Controller.
            {
                output.commString = ("Got command: " + input);
            }
            output.taskType = 3;
            return output;
        }
    }
}
