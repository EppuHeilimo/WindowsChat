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
                //Add all the received messages to one string seperated by \n
                string str = "";
                List<string> ChatContent = connections[currentChannel].getChatContent();
                foreach (string s in ChatContent)
                {
                    str += s + "\n";
                }
                //Lets find the textblock in the currentchannel, this can be done because we know what the tab contains so everything can be casted to right controls.
                Grid grid = (Grid)connections[currentChannel].tab.Content;
                ScrollViewer scroll = (ScrollViewer)grid.Children[0];
                TextBlock block = (TextBlock)scroll.Content;
                block.Text = str;
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

        public static void disconnect()
        {
            MainWindow.TabContainer.Items.Remove(connections[currentChannel].tab);
            connections[currentChannel].disconnect();
            connections.RemoveAt(currentChannel);
        }
    }
}
