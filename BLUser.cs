using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsChat
{
    class BLUser
    {
        private string username;
        private Socket socket;
        public BLUser(string username, Socket socket)
        {
            this.username = username;
            this.socket = socket;

        }

        public void setSocket(Socket socket)
        {
            this.socket = socket;
        }

        public string getUserName()
        {
            return username;
        }

        public Socket getSocket()
        {
            return socket;
        }
    }
}
