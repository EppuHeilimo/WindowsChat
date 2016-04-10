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
        private IPAddress ip { get; set; }
        private Int32 port { get; set; }
        private Socket s;
        public string channelname;
        public List<string> ChatContent = new List<string>();
        public readonly ConcurrentQueue<string> receivedMessages = new ConcurrentQueue<string>();
        private string nick { get; set; }

        public bool closing { get; set; }

        public BLConnection(string ip, Int32 port, string channelname, string nick)
        {
            
            this.ip = IPAddress.Parse(ip);
            this.port = port;
            this.channelname = channelname;
            this.nick = nick;
            s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            connectTo();

        }

        public BLConnection()
        {
            
        }

        public List<string> getChatContent()
        {

            if (!receivedMessages.IsEmpty)
            {
                for (int i = 0; i < receivedMessages.Count; i++)
                {
                    string str;
                    receivedMessages.TryDequeue(out str);
                    ChatContent.Add(str);
                }
                
            }
            return ChatContent;
        }
        

        public bool connectTo()
        {
            try
            {
                Thread tcp = new Thread(new ThreadStart(receiveTCP));
                tcp.Start();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
                return false;
            }
            return true;
        }


        public void receiveTCP()
        {
            
            while (true)
            {
                try
                {
                    s.Connect(ip, port);
                    MessageBox.Show("Connected to " + ip);
                    sendTCP("0" + nick);
                    
                    while (true)
                    {
                        //s.Listen();
                        byte[] data = new byte[255];
                        int count = s.Receive(data);
                        string str = parseMessage(data);
                        receivedMessages.Enqueue(str);

                        if (this.closing)
                        {
                            break;
                        }

                    }
                    s.Close();

                }
                catch (SocketException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private string parseMessage(byte[] data)
        {
            string message = System.Text.Encoding.Default.GetString(data);
            int end = message.IndexOf("\0") - 1;
            return message.Substring(1, end);
        }


        public void sendTCP(string msg)
        {
            try
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] message = ascii.GetBytes(msg);
                if(s.Connected)
                    s.Send(message);
                else
                {
                    MessageBox.Show("No connection");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }

    }
}
