using NamedPipesSample.WindowsService;

namespace NamedPipesSample.Common
{
    public class ActionService : IActionService
    {
        private TrayIconService trayIconService = new();

        public void SendText(string text)
        {
            Console.WriteLine($"Text from client: {text}");

            TextReceived?.Invoke(this, "Hi from server");
        }

        public void ShowTrayIcon()
        {
            trayIconService.ShowTrayIcon();
        }

        public void HideTrayIcon()
        {
            trayIconService.HideTrayIcon();
        }

        public event EventHandler<string>? TextReceived;
    }
}
