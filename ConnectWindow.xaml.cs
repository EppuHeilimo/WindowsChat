using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WindowsChat
{
    /// <summary>
    /// Interaction logic for ConnectWindow.xaml
    /// </summary>
    public partial class ConnectWindow : Window
    {
        MainWindow parentwindow;
        public ConnectWindow(MainWindow parent)
        {
            InitializeComponent();

            parentwindow = parent;
        }

        private void BtnConnect_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ipBox.Text != "" && portBox.Text != "" && channelBox.Text != "" && nickBox.Text != "")
                {
                    BLChannels.connections.Insert(0, new BLConnection(ipBox.Text, Convert.ToInt32(portBox.Text), channelBox.Text, nickBox.Text));
                    BLChannels.connections[0].setTab(parentwindow.addTab());
                    Close();
                }
                else
                {
                    MessageBox.Show("Missing information");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }

        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
          //  Application.Current.MainWindow = new ConnectWindow();
          //  Application.Current.MainWindow.Show();
            Close();
        }

        private void BtnNewServer_OnClick(object sender, RoutedEventArgs e)
        {
            BLChannels.myServer = new Server(Convert.ToInt32(portBox.Text), channelBox.Text);
        }
    }
}
