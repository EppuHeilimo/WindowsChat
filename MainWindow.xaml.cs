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

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<TabItem> tabs = new List<TabItem>();
        private string ChatContent;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void tabDynamic_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            ConnectWindow connect = new ConnectWindow();
            connect.Show();
            addTab();
        }

        private void addTab()
        {
            try
            {
                TabItem newTab = new TabItem();
                Grid newGrid = new Grid();
                ScrollViewer newScrollViewer = new ScrollViewer();
                TextBlock newTextBlock = new TextBlock();
                tabs.Add(newTab);
                newTab.Header = "Channel " + tabs.Count;
                newGrid.Background = new SolidColorBrush(Color.FromArgb(0xFF, 0xE5, 0xE5, 0xE5));
                newScrollViewer.Content = newTextBlock;
                newGrid.Children.Add(newScrollViewer);
                newTab.Content = newGrid;
                TabContainer.Items.Insert(0, newTab);
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
            ChatContent += MessageBox.Text;
        }
    }
}
