using NamedPipesSample.WindowsService;

namespace NamedPipesSample.Common
{
    [H.IpcGenerators.IpcServer]
    public partial class ActionService : IActionService
    {
        private TrayIconService trayIconService = new();

        public void ShowTrayIcon()
        {
            trayIconService.ShowTrayIcon();
        }

        public void HideTrayIcon()
        {
            trayIconService.HideTrayIcon();
        }
    }
}
