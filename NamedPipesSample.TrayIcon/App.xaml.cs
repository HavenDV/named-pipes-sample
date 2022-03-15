using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Windows;

namespace NamedPipesSample.TrayIcon
{
    public partial class App : Application
    {
        private TaskbarIcon? notifyIcon;
        public NamedPipesClient client;

        public App()
        {
            client = new NamedPipesClient();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");

            try
            {
                await client.InitializeAsync();
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Error while connecting to pipe server: {exception}");
            }
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await client.DisposeAsync();
            notifyIcon?.Dispose();

            base.OnExit(e);
        }
    }
}