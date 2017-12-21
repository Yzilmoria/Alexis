using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Alexis.Server
{
    class TcpServer
    {
        public TcpClient AlexisComm;
        TcpListener server;
        NetworkStream ns;

        public TcpServer()
        {
            server = new TcpListener(Dns.GetHostEntry("localhost").AddressList[0], 50000);
        }

        public void start()
        {
            server.Stop();
            server.Start();  // this will start the server
            AlexisComm = new TcpClient();

            while (!AlexisComm.Connected)   //we wait for a connection
            {
                AlexisComm = server.AcceptTcpClient();  //if a connection exists, the server will accept it
                ns = AlexisComm.GetStream(); //networkstream is used to send/receive messages
            }
        }

        public void close()
        {
            server.Stop();
        }

        public string readMessage()
        {
            if (!ns.DataAvailable)
            {
                return "";
            }
            byte[] msg = new byte[1024];     //the messages arrive as byte array
            ns.Read(msg, 0, msg.Length);   //the same networkstream reads the message sent by the client
            return Encoding.Default.GetString(msg).Trim('\0', ' '); //now , we write the message as string, cut off extra stuff
        }

        public void writeMessage(string msg)
        {
            byte[] message = new byte[100];   //any message must be serialized (converted to byte array)
            message = Encoding.Default.GetBytes(msg.Trim(' '));  //conversion string => byte array

            ns.Write(message, 0, message.Length);     //sending the message
        }

        public bool isConnected()
        {
            return AlexisComm.Connected;
        }
    }
}
