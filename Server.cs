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
        public Server(Int32 port, string defaultname)
        {
            this.port = port;
            defaultName = defaultname;
            server = new Thread(new ThreadStart(run));
        }

        public void run()
        {
            try
            {
                IPAddress address = IPAddress.Parse("127.0.0.1");
                TcpListener listener = new TcpListener(address, port);
                listener.Start();
                while (true)
                {
                    try
                    {
                        Socket s = listener.AcceptSocket();
                        byte[] message = new byte[255];
                        int count = s.Receive(message);
                        PacketType type = parsePacketType(message);
                        string msg = parseMessage(message);

                        switch (type)
                        {
                            case PacketType.LOGIN:
                                connectedUsers.Add(msg, s);
                                broadcast(msg + " connected!");
                                break;
                            case PacketType.DISCONNECT:
                                connectedUsers.Remove(msg);
                                broadcast(msg + " disconnected!");
                                break;
                            case PacketType.MESSAGE:
                                foreach (KeyValuePair<string, Socket> item in connectedUsers)
                                {
                                    if (item.Value == s)
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
                        MessageBox.Show(e.Message);
                    }

                   
                }
                listener.Stop();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private PacketType parsePacketType(byte[] data)
        {
            string message = Convert.ToString(data);
            string type = message.Substring(0, 1);
            int packet = Int32.Parse(type);
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

        private string parseMessage(byte[] data)
        {
            string message = Convert.ToString(data);
            return message.Substring(1);
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
                    MessageBox.Show(ex.Message);
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
        }
    }

}
