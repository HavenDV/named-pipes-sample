using System.Windows;

namespace NamedPipesSample.TrayIcon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowTrayIconBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).client.ShowTrayIcon();
        }

        private void HideTrayIconBtn_Click(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).client.HideTrayIcon();
        }
    }
}
