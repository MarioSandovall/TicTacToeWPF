using System.Threading.Tasks;
using MahApps.Metro.Controls.Dialogs;

namespace TiTacToe.Service.Interfaces
{
    public interface IDialogServices
    {
        Task<MessageDialogResult> AskQuestionAsync(string title, string message);
        Task<ProgressDialogController> ShowProgressAsync(string message);
        Task ShowMessageAsync(string title, string message);
    }
}