using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexis.Tasks
{
    public interface iTask
    {
        // readonly tasktype 
        string connection { get; set; } // private for external communication with raspberries

        bool start();
        bool terminate();
        bool isTerminated();

        bool foreground { get; set; }

        bool addCommand(string command);
        bool hasReply();
        string getReply();
    }
}
