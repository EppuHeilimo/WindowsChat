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

        private Dictionary<string, TcpClient> connectedUsers = new Dictionary<string, TcpClient>();
        private string defaultName;
        private Int32 port;
        private Thread server;
        private List<NetworkStream> streams = new List<NetworkStream>(); 
        private bool closing = false;
        TcpListener listener;

        public Server()
        {
            
        }

        public Server(Int32 port, string defaultname)
        {
            this.port = port;
            defaultName = defaultname;
            //server = new Thread(new ThreadStart(run));
            //server.Start();
            run();
        }

        public void run()
        {
            try
            {
                IPAddress address = IPAddress.Parse("127.0.0.1");
                listener = new TcpListener(IPAddress.Any, port);
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
            if (!closing)
            {
                try
                {
                    listener.BeginAcceptTcpClient(HandleAsyncConnection, listener);
                }
                catch (Exception ex)
                {

                }
            }

        }

        private void HandleAsyncConnection(IAsyncResult result)
        {
            if (!closing)
            {
                StartAccept();
                TcpClient client = listener.EndAcceptTcpClient(result);
                NetworkStream stream = client.GetStream();
                streams.Add(stream);
                while (true)
                {
                    try
                    {
                        byte[] message = new byte[255];

                        int count = stream.Read(message, 0, message.Length);

                        if (count > 0)
                        {
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
                                    closing = true;
                                    break;
                                case PacketType.MESSAGE:
                                    foreach (KeyValuePair<string, TcpClient> item in connectedUsers)
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
                        }
                        if (closing)
                        {
                            break;
                        }

                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.StackTrace);
                    }

                }

                
            }
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
                    NetworkStream stream = user.Value.GetStream();
                    stream.Write(data, 0, data.Length);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.StackTrace);
                }
            }

        }

        public void close()
        {
            closing = true;
            listener.Stop();
        }
    }

}
