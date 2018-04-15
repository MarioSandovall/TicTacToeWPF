using Prism.Commands;
using Prism.Events;
using System.Windows.Input;
using TiTacToe.WPF.Event;
using TiTacToe.WPF.Models;
using TiTacToe.WPF.Utilities;

namespace TiTacToe.WPF.Wrappers
{
    public class SquareWrapper : ModelWrapper<Square>
    {

        #region Properties

        public string Color
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string Image
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool Available
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public int Position
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public PlayerType Type
        {
            get { return GetValue<PlayerType>(); }
            set { SetValue(value); }
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
            Color = SelectedColor;
            Type = PlayerType.Cross;
            Image = CrossImage;

            _eventAggregator.GetEvent<MoveEvent>()
                .Publish(Helpers.CurrentPlayer);
        }


        private const string SelectedColor = "#FF1585B5";
        private const string CrossImage = "/TiTacToe.WPF;component/Resources/Images/Cross.png";
        private const string CircleImage = "/TiTacToe.WPF;component/Resources/Images/Circle.png";
    }
}
