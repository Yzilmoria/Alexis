using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Alexis.Communicator.Server
{
    // https://www.codeproject.com/Articles/745134/csharp-async-socket-server
    // https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-server-socket-example
    // https://docs.microsoft.com/en-us/dotnet/framework/network-programming/asynchronous-client-socket-example
    // Make sure it talks to client that send '<EOF>'-tag
    class AsyncServer
    {
        private static Socket listener;
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        public const int _buffersize = 1024;
        public const int _port = 50000;
        public static bool _isRunning = true;

        private IPHostEntry ipHostInfo;
        private IPAddress ipAddress;
        private IPEndPoint localEndPoint;
        

        class StateObject
        {
            public Socket workSocket = null;
            public byte[] buffer = new byte[_buffersize];
            public StringBuilder sb = new StringBuilder();
        }

        static bool isSocketConnected(Socket s)
        {
            return !((s.Poll(1000, SelectMode.SelectRead) && (s.Available == 0)) || !s.Connected);
        }

        public bool start()
        {
            ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            ipAddress = ipHostInfo.AddressList[0];
            localEndPoint = new IPEndPoint(ipAddress, _port);

            listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                allDone.Reset();

                //while (_isRunning)
                //{
                //    // Set the event to nonsignaled state.  
                //    allDone.Reset();

                //    // Start an asynchronous socket to listen for connections.  
                //    Console.WriteLine("Waiting for a connection...");
                //    listener.BeginAccept(
                //        new AsyncCallback(acceptCallback),
                //        listener);

                //    // Wait until a connection is made before continuing.  wait 5 min?
                //    bool isRequest = allDone.WaitOne(new TimeSpan(0, 5,0));

                //    if (!isRequest)
                //    {
                //        allDone.Set();
                //    }
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        public bool HttpListener()
        {
            try
            {
                // Set the event to nonsignaled state.  
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.  
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(acceptCallback),
                    listener);

                // Wait until a connection is made before continuing.  wait 5 min?
                bool isRequest = allDone.WaitOne(new TimeSpan(0, 5, 0));

                if (!isRequest)
                {
                    allDone.Set();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            return true;
        }

        public bool close()
        {
            try
            {
                listener.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
            return true;
        }

        static void acceptCallback(IAsyncResult ar)
        {
            // Get the listener that handles the client request.
            Socket listener = (Socket)ar.AsyncState;

            if (listener != null)
            {
                Socket handler = listener.EndAccept(ar);

                // Signal main thread to continue
                allDone.Set();

                // Create state
                StateObject state = new StateObject();
                state.workSocket = handler;
                handler.BeginReceive(state.buffer, 0, _buffersize, 0, new AsyncCallback(readCallback), state);
            }
        }

        public static void readCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                content = state.sb.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    // All the data has been read from the   
                    // client. Display it on the console.  
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);
                    // Echo the data back to the client.  
                    Send(handler, content);
                    if (content.Contains("bey"))
                    {
                        _isRunning = false;
                    }
                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, _buffersize, 0,
                    new AsyncCallback(readCallback), state);
                }
            }
        }

        private static void Send(Socket handler, String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                Socket handler = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
