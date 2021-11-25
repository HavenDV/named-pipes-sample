using H.Pipes;
using NamedPipesSample.Common;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace NamedPipesSample.TrayIcon
{
    public class NamedPipesClient : IAsyncDisposable
    {
        const string pipeName = "samplepipe";

        private PipeClient<PipeMessage> client;

        public NamedPipesClient()
        {
            client = new PipeClient<PipeMessage>(pipeName);
            client.MessageReceived += (sender, args) => OnMessageReceivedAsync(args.Message);
            client.Disconnected += (o, args) => MessageBox.Show("Disconnected from server");
            client.Connected += (o, args) => MessageBox.Show("Connected to server");
            client.ExceptionOccurred += (o, args) => OnExceptionOccurred(args.Exception);
        }

        public async Task InitializeAsync()
        {
            await client.ConnectAsync();

            await client.WriteAsync(new PipeMessage
            {
                Action = ActionType.SendText,
                Text = "Hello from client",
            });
        }

        public async Task ShowTrayIconAsync()
        {
            await client.WriteAsync(new PipeMessage
            {
                Action = ActionType.ShowTrayIcon
            });
        }

        public async Task HideTrayIconAsync()
        {
            await client.WriteAsync(new PipeMessage
            {
                Action = ActionType.HideTrayIcon
            });
        }

        private void OnMessageReceivedAsync(PipeMessage message)
        {
            switch (message.Action)
            {
                case ActionType.SendText:
                    MessageBox.Show(message.Text);
                    break;
                default:
                    MessageBox.Show($"Method {message.Action} not implemented");
                    break;
            }
        }

        private void OnExceptionOccurred(Exception exception)
        {
            MessageBox.Show($"An exception occured: {exception}");
        }

        public async ValueTask DisposeAsync()
        {
            await client.DisposeAsync().ConfigureAwait(false);
        }
    }
}