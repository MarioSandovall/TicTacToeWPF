using Autofac;
using System.Windows;
using TiTacToe.WPF.Startup;
using TiTacToe.WPF.ViewModels;

namespace TiTacToe.WPF
{

    public partial class App : Application
    {
        private MainViewModel _mainViewModel;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new Bootstrapper();
            var container = bootstrapper.BootStrap();

            var mainWindow = new MainWindow();
            _mainViewModel = container.Resolve<MainViewModel>();

            mainWindow.Loaded +=MainWindowOnLoaded;
            mainWindow.DataContext = _mainViewModel;

            mainWindow.Show();
        }

        private void MainWindowOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            _mainViewModel.Load();
        }
    }
}
