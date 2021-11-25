﻿using H.Pipes;
using H.Pipes.Args;
using NamedPipesSample.Common;

namespace NamedPipesSample.WindowsService
{
    public class NamedPipesServer : IAsyncDisposable
    {
        const string PIPE_NAME = "samplepipe";

        private PipeServer<PipeMessage> server;
        private TrayIconService trayIconService;

        public NamedPipesServer()
        {
            trayIconService = new TrayIconService();

            server = new PipeServer<PipeMessage>(PIPE_NAME);
            server.ClientConnected += async (o, args) => await OnClientConnectedAsync(args);
            server.ClientDisconnected += (o, args) => OnClientDisconnected(args);
            server.MessageReceived += (sender, args) => OnMessageReceived(args.Message);
            server.ExceptionOccurred += (o, args) => OnExceptionOccurred(args.Exception);
        }

        public async Task InitializeAsync()
        {
            await server.StartAsync();
        }

        private async Task OnClientConnectedAsync(ConnectionEventArgs<PipeMessage> args)
        {
            Console.WriteLine($"Client {args.Connection.Id} is now connected!");

            await args.Connection.WriteAsync(new PipeMessage
            {
                Action = ActionType.SendText,
                Text = "Hi from server"
            });
        }

        private void OnClientDisconnected(ConnectionEventArgs<PipeMessage> args)
        {
            Console.WriteLine($"Client {args.Connection.Id} disconnected");
        }

        private void OnMessageReceived(PipeMessage? message)
        {
            if (message == null) 
                return;

            switch (message.Action)
            {
                case ActionType.SendText:
                    Console.WriteLine($"Text from client: {message.Text}");
                    break;

                case ActionType.ShowTrayIcon:
                    trayIconService.ShowTrayIcon();
                    break;

                case ActionType.HideTrayIcon:
                    trayIconService.HideTrayIcon();
                    break;

                default:
                    Console.WriteLine($"Unknown Action Type: {message.Action}");
                    break;
            }
        }

        private void OnExceptionOccurred(Exception ex)
        {
            Console.WriteLine($"Exception occured in pipe: {ex}");
        }

        public async ValueTask DisposeAsync()
        {
            await server.DisposeAsync().ConfigureAwait(false);
        }
    }
}
