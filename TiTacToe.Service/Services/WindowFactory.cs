using System.Windows;
using TiTacToe.Service.Interfaces;

namespace TiTacToe.Service.Services
{
    public class WindowFactory : IWindowFactory
    {
        public Window GetMainWindow() => Application.Current.MainWindow;
    }
}
