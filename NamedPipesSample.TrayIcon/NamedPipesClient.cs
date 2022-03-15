using NamedPipesSample.Common;
using System;
using System.Threading.Tasks;
using System.Windows;
using H.Pipes;

namespace NamedPipesSample.TrayIcon
{
    [H.IpcGenerators.IpcClient]
    public partial class ActionServiceClient : IActionService
    {
    } 

    public class NamedPipesClient : IAsyncDisposable
    {
        const string pipeName = "samplepipe";

        private PipeClient<string> pipeClient;
        private ActionServiceClient service = new();

        public NamedPipesClient()
        {
            pipeClient = new PipeClient<string>(pipeName);
            pipeClient.ExceptionOccurred += static (_, args) => OnExceptionOccurred(args.Exception);
            service.Initialize(pipeClient);
        }

        public async Task InitializeAsync()
        {
            await pipeClient.ConnectAsync();
        }

        public void ShowTrayIcon()
        {
            service.ShowTrayIcon();
        }

        public void HideTrayIcon()
        {
            service.HideTrayIcon();
        }

        private void OnTextReceived(string text)
        {
            MessageBox.Show(text);
        }

        private static void OnExceptionOccurred(Exception exception)
        {
            MessageBox.Show($"An exception occured: {exception}");
        }

        public async ValueTask DisposeAsync()
        {
            await pipeClient.DisposeAsync().ConfigureAwait(false);
        }
    }
}