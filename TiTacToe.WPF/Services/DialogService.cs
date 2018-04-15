using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using TiTacToe.WPF.Contracts.Services;

namespace TiTacToe.WPF.Services
{
    public class DialogService : IDialogServices
    {

        private readonly MetroWindow _window;
        public DialogService(IWindowFactory windowFactory)
        {
            _window = (MetroWindow)windowFactory.GetMainWindow();
        }

        public Task<MessageDialogResult> AskQuestionAsync(string title, string message)
        {
            return _window.ShowMessageAsync(title, message, MessageDialogStyle.AffirmativeAndNegative,
                new MetroDialogSettings()
                {
                    AffirmativeButtonText = "Ok",
                    NegativeButtonText = "Cancel"
                });
        }

        public Task<ProgressDialogController> ShowProgressAsync(string message)
        {
            return _window.ShowProgressAsync("Un momento por favor...", message);
        }

        public Task ShowMessageAsync(string title, string message)
        {
            return _window.ShowMessageAsync(title, message);
        }

    }
}
