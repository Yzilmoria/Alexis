using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alexis.Communicator
{
    public class CommItem
    {
        public int taskType { get; set; } 
        public string commString { get; set; }

        public CommItem()
        {
            this.taskType = 0;
            this.commString = "";
        }

        public CommItem(string commString)
        {
            this.taskType = 0;
            this.commString = commString;
        }

    }
}
