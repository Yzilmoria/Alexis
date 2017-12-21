using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexis.Communicator;
using Alexis.Server;
using System.Collections;
using Alexis.Communicator.Server;

public static class Control
{
    public static void Delegate()
    {
        AsyncServer server = new AsyncServer();
        // TcpServer server = new TcpServer();
        server.start(); // Waits until a connection is established
        string input = "";
        CommItem comm = new CommItem();
        List<Task> taskList = new List<Task>();

        while (server.isConnected() && comm.taskType != 1)
        {
            input = server.readMessage();
            if (input != "") Console.WriteLine("Got: " + input);
            comm = Conversation.Interpret(input);
            if (comm.taskType != 0)
            {
                server.writeMessage("Alexis: " + comm.commString);
            }
        }
        server.close();
    }
}
