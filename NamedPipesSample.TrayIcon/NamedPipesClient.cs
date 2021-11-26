using H.ProxyFactory;
using NamedPipesSample.Common;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace NamedPipesSample.TrayIcon
{
    public class NamedPipesClient : IAsyncDisposable
    {
        const string pipeName = "samplepipe";

        private PipeProxyFactory factory;
        private IActionService? service;

        public NamedPipesClient()
        {
            factory = new PipeProxyFactory();
            factory.ExceptionOccurred += (o, exception) => OnExceptionOccurred(exception);
        }

        public async Task InitializeAsync()
        {
            await factory.InitializeAsync(pipeName);

            service = await factory.CreateInstanceAsync<ActionService, IActionService>();
            service.TextReceived += (sender, text) => OnTextReceived(text);
            service.SendText("Hello from client");
        }

        public void ShowTrayIcon()
        {
            service?.ShowTrayIcon();
        }

        public void HideTrayIcon()
        {
            service?.HideTrayIcon();
        }

        private void OnTextReceived(string text)
        {
            MessageBox.Show(text);
        }

        private void OnExceptionOccurred(Exception exception)
        {
            MessageBox.Show($"An exception occured: {exception}");
        }

        public async ValueTask DisposeAsync()
        {
            await factory.DisposeAsync().ConfigureAwait(false);
        }
    }
}