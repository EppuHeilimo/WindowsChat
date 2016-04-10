using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WindowsChat
{
    static class BLChannels
    {
        public static List<BLConnection> connections = new List<BLConnection>();
        public static int currentChannel = 0;
        public static Server myServer;
        public static MainWindow MainWindow;

        public static void changeChatContent()
        {
            try
            {
                string str = "";
                List<string> ChatContent = connections[currentChannel].getChatContent();
                foreach (string s in ChatContent)
                {
                    str += s + "\n";
                }

                MainWindow.ChannelTextBlock.Text = str;
                MainWindow.Scroll.ScrollToBottom();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }

        public static void setCurrentChannel(int id)
        {
            currentChannel = id;
        }
    }
}
