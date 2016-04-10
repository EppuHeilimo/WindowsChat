using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace WindowsChat
{
    public class Server
    {
        enum PacketType
        {
            LOGIN = 0,
            MESSAGE = 1,
            DISCONNECT = 2,
            INVALID = 3

        }

        private Dictionary<string, Socket> connectedUsers = new Dictionary<string, Socket>();
        private string defaultName;
        private Int32 port;
        private Thread server;
        private bool closing = false;
        TcpListener listener;

        public Server()
        {
            
        }

        public Server(Int32 port, string defaultname)
        {
            this.port = port;
            defaultName = defaultname;
            server = new Thread(new ThreadStart(run));
            server.Start();
        }

        public void run()
        {
            try
            {
                IPAddress address = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(address, port);
                listener.Start();
                MessageBox.Show("Server created");
                StartAccept();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void StartAccept()
        {
            listener.BeginAcceptSocket(HandleAsyncConnection, listener);

        }

        private void HandleAsyncConnection(IAsyncResult result)
        {
            
            StartAccept();
            Socket client = listener.EndAcceptSocket(result);

            while (true)
            {
                try
                {
                    byte[] message = new byte[255];
                    int count = client.Receive(message);
                    PacketType type = parsePacketType(message);
                    string msg = parseMessage(message);

                    switch (type)
                    {
                        case PacketType.LOGIN:
                            connectedUsers.Add(msg, client);
                            broadcast(msg + " connected!");
                            break;
                        case PacketType.DISCONNECT:
                            connectedUsers.Remove(msg);
                            broadcast(msg + " disconnected!");
                            break;
                        case PacketType.MESSAGE:
                            foreach (KeyValuePair<string, Socket> item in connectedUsers)
                            {
                                if (item.Value == client)
                                {
                                    broadcast(item.Key + ": " + msg);
                                }
                            }
                            break;
                        case PacketType.INVALID:
                            MessageBox.Show("INVALID PACKET: " + msg);
                            break;
                    }

                    if (closing)
                    {
                        closeSockets();
                        break;
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show(e.StackTrace);
                }


            }
            listener.Stop();

        }

        private PacketType parsePacketType(byte[] data)
        {
            try
            {
                string message = System.Text.Encoding.Default.GetString(data);
                string type = message.Substring(0, 1);
                int packet = Convert.ToInt32(type);
                switch (packet)
                {
                    case 0:
                        return PacketType.LOGIN;
                    case 1:
                        return PacketType.MESSAGE;
                    case 2:
                        return PacketType.DISCONNECT;
                }
                return PacketType.INVALID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return PacketType.INVALID;
            }

        }

        private string parseMessage(byte[] data)
        {
            string message = System.Text.Encoding.Default.GetString(data);
            int end = message.IndexOf("\0") - 1;
            return message.Substring(1, end);
        }

        public void broadcast(string msg)
        {
            foreach (var user in connectedUsers)
            {
                try
                {
                    ASCIIEncoding ascii = new ASCIIEncoding();
                    byte[] data = ascii.GetBytes(msg);
                    user.Value.Send(data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }

        }
        private void closeSockets()
        {
            foreach (var user in connectedUsers)
            {
                user.Value.Close();               
            }
            connectedUsers.Clear();
            listener.Stop();
        }

        public void close()
        {
            closing = true;
        }
    }

}
