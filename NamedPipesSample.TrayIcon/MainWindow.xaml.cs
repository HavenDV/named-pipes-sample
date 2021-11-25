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

        private async void ShowTrayIconBtn_Click(object sender, RoutedEventArgs e)
        {
            await ((App)Application.Current).client.ShowTrayIconAsync();
        }

        private async void HideTrayIconBtn_Click(object sender, RoutedEventArgs e)
        {
            await ((App)Application.Current).client.HideTrayIconAsync();
        }
    }
}
