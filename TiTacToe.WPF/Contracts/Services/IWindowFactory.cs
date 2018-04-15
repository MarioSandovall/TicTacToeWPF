using System.Windows;

namespace TiTacToe.WPF.Contracts.Services
{
    public interface IWindowFactory
    {
        Window GetMainWindow();
    }
}