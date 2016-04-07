using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Windows;

namespace WindowsChat
{
    public class BLConnection
    {
        private string ip { get; set; }
        private string port { get; set; }

        public string channelname;

        private string nick { get; set; }

        public bool closing { get; set; }

        public BLConnection(string ip, string port, string channelname, string nick)
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
            }
            return true;
        }

        private bool createServer()
        {
            return true;
        }

        public void appIsClosing()
        {

        }

        public void receiveTCP()
        {
     
            while (true)
            {
                try
                {
                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                if (this.closing)
                {
                    break;
                }
            }
        }

    }
}
