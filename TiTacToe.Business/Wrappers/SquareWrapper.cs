using Prism.Commands;
using Prism.Events;
using System.Windows.Input;
using TiTacToe.Business.Event;
using TiTacToe.Model.Models;
using TiTacToe.Model.Utils;

namespace TiTacToe.Business.Wrappers
{
    public class SquareWrapper : ModelWrapper<Square>
    {

        #region Properties

        public string Color
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public string Image
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public bool Available
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        public int Position
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public PlayerType PlayerType
        {
            get => GetValue<PlayerType>();
            set => SetValue(value);
        }

        #endregion


        #region Commands

        public ICommand SelectCommand { get; set; }

        #endregion

        private readonly IEventAggregator _eventAggregator;
        public SquareWrapper(Square model, IEventAggregator eventAggregator) : base(model)
        {
            _eventAggregator = eventAggregator;
            SelectCommand = new DelegateCommand(OnSelectExecute);
        }

        private void OnSelectExecute()
        {
            if (!Available) return;

            Available = false;
            Image = Images.Cross;
            Color = Colors.Selected;
            PlayerType = PlayerType.Human;

            _eventAggregator.GetEvent<MoveEvent>().Publish();
        }

        public void MarkAsBusy()
        {
            if (!Available) return;

            Available = false;
            Image = Images.Circle;
            Color = Colors.Selected;
            PlayerType = PlayerType.Computer;

        }


    }
}
