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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        static public List<TabItem> tabs = new List<TabItem>();
        private string ChatContent;
        private Dictionary<string, BLConnection> channels = new Dictionary<string, BLConnection>();
        private int currentChannel = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentChannel = TabContainer.SelectedIndex;
            
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            ConnectWindow connectWindow = new ConnectWindow(this);
            connectWindow.Show();
        }
        public void addTab()
        {
            try
            {
                TabItem newTab = new TabItem();
                Grid newGrid = new Grid();
                ScrollViewer newScrollViewer = new ScrollViewer();
                TextBlock newTextBlock = new TextBlock();
                newTab.Header = BLChannels.connections[0].channelname;
                newGrid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE5, 0xE5, 0xE5));
                newScrollViewer.Content = newTextBlock;
                newGrid.Children.Add(newScrollViewer);
                newTab.Content = newGrid;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            /*
                 <Grid Background="#FFE5E5E5">
                    <ScrollViewer>
                        <TextBlock HorizontalAlignment="Stretch" TextWrapping="Wrap"/>
                    </ScrollViewer>
            */
        }

        private void BtnSend_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var item in BLChannels.connections)
            {
                item.closing = true;
            }
        }
    }
}
