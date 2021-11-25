using System.ServiceProcess;

namespace NamedPipesSample.WindowsService
{
    internal class ServiceHost : ServiceBase
    {
        private static Thread serviceThread;
        private static bool stopping;

        private static NamedPipesServer pipeServer;

        public ServiceHost()
        {
            ServiceName = "Named Pipes Sample Service";
        }

        protected override void OnStart(string[] args)
        {
            Run(args);
        }

        protected override void OnStop()
        {
            Abort();
        }

        protected override void OnShutdown()
        {
            Abort();
        }

        public static void Run(string[] args)
        {
            serviceThread = new Thread(InitializeServiceThread)
            {
                Name = "Named Pipes Sample Service Thread",
                IsBackground = true
            };
            serviceThread.Start();
        }

        public static void Abort()
        {
            stopping = true;
        }

        private static async void InitializeServiceThread()
        {
            pipeServer = new NamedPipesServer();

            await using (pipeServer)
            {
                await pipeServer.InitializeAsync().ConfigureAwait(false);

                while (!stopping)
                {
                    await Task.Delay(100).ConfigureAwait(false);
                }
            }
        }
    }
}