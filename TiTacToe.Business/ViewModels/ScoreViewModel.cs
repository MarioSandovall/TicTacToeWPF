using Prism.Events;
using Prism.Mvvm;
using TiTacToe.Business.Event;
using TiTacToe.Business.Interfaces;
using TiTacToe.Business.Wrappers;
using TiTacToe.Model.Models;
using TiTacToe.Model.Utils;
using TiTacToe.Service.Interfaces;

namespace TiTacToe.Business.ViewModels
{
    public class ScoreViewModel : BindableBase, IScoreViewModel
    {
        #region Propeties

        private PlayerWrapper _human;
        public PlayerWrapper Human
        {
            get => _human;
            set => SetProperty(ref _human, value);
        }

        private PlayerWrapper _computer;
        public PlayerWrapper Computer
        {
            get => _computer;
            set => SetProperty(ref _computer, value);
        }

        private PlayerWrapper _ties;
        public PlayerWrapper Ties
        {
            get => _ties;
            set => SetProperty(ref _ties, value);
        }

        #endregion

        private readonly IDialogServices _dialogServices;
        public ScoreViewModel(
            IDialogServices dialogServices,
            IEventAggregator eventAggregator)
        {
            _dialogServices = dialogServices;

            eventAggregator.GetEvent<WinEvent>().Subscribe(OnWin);
        }

        public void Initialize()
        {
            var human = new Player("Human", PlayerType.Human);
            Human = new PlayerWrapper(human);

            var computer = new Player("Computer", PlayerType.Computer);
            Computer = new PlayerWrapper(computer);

            var ties = new Player("Ties", PlayerType.Ties);
            Ties = new PlayerWrapper(ties);
        }

        private async void OnWin(PlayerType player)
        {
            var message = "";

            switch (player)
            {
                case PlayerType.Human:
                    Human.Win();
                    message = $"{Human.Name} wins !!!";
                    break;
                case PlayerType.Computer:
                    Computer.Win();
                    message = $"{Computer.Name} wins !!!";
                    break;
                case PlayerType.Ties:
                    Ties.Win();
                    message = "TIE !!!";
                    break;
            }

            await _dialogServices.ShowMessageAsync("TicTacToe", message);
        }

    }
}
