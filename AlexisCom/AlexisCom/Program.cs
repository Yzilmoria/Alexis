﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AlexisCom
{
    class Program
    {
        static void Main(string[] args)
        {
            //TClient client = new TClient();
            //client.start();
            AsynchronousClient.StartClient();
        }
    }
}
