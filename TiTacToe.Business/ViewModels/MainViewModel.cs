using Prism.Commands;
using System.Windows.Input;
using TiTacToe.Business.Interfaces;

namespace TiTacToe.Business.ViewModels
{
    public class MainViewModel
    {
        #region ViewModels

        public IBoardViewModel BoardViewModel { get; set; }

        public IScoreViewModel ScoreViewModel { get; set; }

        #endregion

        #region Commands

        public ICommand RestartCommand { get; set; }

        public ICommand PlayAgainCommand { get; set; }

        #endregion
        public MainViewModel(
            IBoardViewModel boardViewModel,
            IScoreViewModel scoreViewModel)
        {

            BoardViewModel = boardViewModel;
            ScoreViewModel = scoreViewModel;

            RestartCommand = new DelegateCommand(Load);
            PlayAgainCommand = new DelegateCommand(OnPlayAgainExecute);
        }

        public void Load()
        {
            BoardViewModel.Initialize();
            ScoreViewModel.Initialize();
        }

        private void OnPlayAgainExecute()
        {
            BoardViewModel.Initialize();
        }

    }
}