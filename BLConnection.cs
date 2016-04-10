using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Net.Sockets;
using System.Windows;
using System.Net;
using System.IO;

namespace WindowsChat
{
    public class BLConnection
    {
        private string ip { get; set; }
        private Int32 port { get; set; }
        private Socket s;
        public string channelname;
        public readonly ConcurrentQueue<string> receivedMessages = new ConcurrentQueue<string>();
        private readonly AutoResetEvent _signal = new AutoResetEvent(true);
        private string nick { get; set; }

        public bool closing { get; set; }

        public BLConnection(string ip, Int32 port, string channelname, string nick)
        {
            
            this.ip = ip;
            this.port = port;
            this.channelname = channelname;
            this.nick = nick;

            if (ip == "localhost" || ip == "127.0.0.1")
            {
                createServer();
            }
            else
            {
                connectTo();
            }

        }

        public BLConnection()
        {
            
        }
        

        public bool connectTo()
        {
            try
            {
                Thread tcp = new Thread(new ThreadStart(receiveTCP));

                tcp.Start();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        private void createServer()
        {
            Server server = new Server(port, channelname);
        }


        public void receiveTCP()
        {
            sendTCP("0"+nick);
            while (true)
            {
                try
                {
                    IPAddress address = IPAddress.Parse("127.0.0.1");
                    TcpListener listener = new TcpListener(address, port);
                    listener.Start();
                    s = listener.AcceptSocket();
                    while (true)
                    {
                        byte[] data = new byte[255];
                        int count = s.Receive(data);
                        string str = System.Text.Encoding.Default.GetString(data);

                         receivedMessages.Enqueue(str);

                        _signal.Set();

                        if (this.closing)
                        {
                            break;
                        }

                    }
                    listener.Stop();
                    s.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void sendTCP(string msg)
        {
            try
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] message = ascii.GetBytes(msg);
                s.Send(message);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
