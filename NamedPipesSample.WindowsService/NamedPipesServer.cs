using H.ProxyFactory;

namespace NamedPipesSample.WindowsService
{
    public class NamedPipesServer : IAsyncDisposable
    {
        const string PIPE_NAME = "samplepipe";

        private PipeProxyServer server = new();

        public NamedPipesServer()
        {
            server.ExceptionOccurred += (o, exception) => OnExceptionOccurred(exception);
        }

        public async Task InitializeAsync()
        {
            await server.InitializeAsync(PIPE_NAME);
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
