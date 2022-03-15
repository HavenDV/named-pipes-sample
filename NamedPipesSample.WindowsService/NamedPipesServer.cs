using H.Pipes;
using H.Pipes.AccessControl;
using NamedPipesSample.Common;

namespace NamedPipesSample.WindowsService
{
    public class NamedPipesServer : IAsyncDisposable
    {
        const string PIPE_NAME = "samplepipe";

        private PipeServer<string> server = new PipeServer<string>(PIPE_NAME);
        private ActionService service = new();

        public NamedPipesServer()
        {
            server.AllowUsersReadWrite();
            server.ExceptionOccurred += static (_, args) => OnExceptionOccurred(args.Exception);
            service.Initialize(server);
        }

        public async Task InitializeAsync()
        {
            await server.StartAsync();
        }

        private static void OnExceptionOccurred(Exception ex)
        {
            Console.WriteLine($"Exception occured in pipe: {ex}");
        }

        public async ValueTask DisposeAsync()
        {
            await server.DisposeAsync().ConfigureAwait(false);
        }
    }
}
