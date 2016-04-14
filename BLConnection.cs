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
using System.Windows.Controls;
using System.Windows.Threading;

namespace WindowsChat
{
    public class BLConnection
    {
        private string ip { get; set; }
        private Int32 port { get; set; }
        private TcpClient tcpclient;
        public string channelname;
        public TabItem tab;
        public List<string> ChatContent = new List<string>();
        public readonly ConcurrentQueue<string> receivedMessages = new ConcurrentQueue<string>();
        private string nick { get; set; }
        NetworkStream stream;
        Thread tcp;
        public bool closing { get; set; }

        public BLConnection(string ip, Int32 port, string channelname, string nick)
        {

            this.ip = ip;
            this.port = port;
            this.channelname = channelname;
            this.nick = nick;
            tcpclient = new TcpClient(ip, port);
            stream = tcpclient.GetStream();
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
                    while(!receivedMessages.TryDequeue(out str));
                    ChatContent.Add(str);
                }
                
            }
            return ChatContent;
        }
        

        public bool connectTo()
        {
            try
            {
                tcp = new Thread(new ThreadStart(receiveTCP));
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
        // mEvent.Start();
            try
            {
                //tcpclient.Connect(ip, port);
                sendTCP("0" + nick);
                while (true)
                {
                    //s.Listen();
                    byte[] data = new byte[255];   
                    int count = stream.Read(data, 0, data.Length);
                    if (count > 0)
                    {
                        string str = parseMessage(data);
                        receivedMessages.Enqueue(str);
                        //invoke changechatcontent in main thread because it changes data owned by main thread.
                        Application.Current.Dispatcher.Invoke(BLChannels.changeChatContent);
                    }

                    if (this.closing)
                    {
                        break;
                    }    
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void disconnect()
        {
            sendTCP("2" + nick);
            close();
        }

        private string parseMessage(byte[] data)
        {
            string message = System.Text.Encoding.Default.GetString(data);
            int end = message.IndexOf("\0");
            return message.Substring(0, end);
        }


        public void sendTCP(string msg)
        {
            try
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] message = ascii.GetBytes(msg);
                if (tcpclient.Connected)
                {
                    stream.Write(message, 0, message.Length);
                }  
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

        public void setTab(TabItem tab)
        {
            this.tab = tab;
        }

        public void close()
        {
            tcpclient.Close();
        }
    }
}
