using System;

namespace NamedPipesSample.Common
{
    public interface IActionService
    {
        void SendText(string text);
        void ShowTrayIcon();
        void HideTrayIcon();

        event EventHandler<string> TextReceived;
    }

    public class ActionService { }
}
