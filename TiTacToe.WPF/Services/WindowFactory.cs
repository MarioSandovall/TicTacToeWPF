using System.Windows;
using TiTacToe.WPF.Contracts.Services;

namespace TiTacToe.WPF.Services
{
    public class WindowFactory : IWindowFactory
    {
        public Window GetMainWindow() => Application.Current.MainWindow;
    }
}
