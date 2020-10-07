using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using Prism.Events;
using TiTacToe.Business.Event;
using TiTacToe.Business.Interfaces;
using TiTacToe.Business.Utils;
using TiTacToe.Business.Wrappers;
using TiTacToe.Model.Models;
using TiTacToe.Model.Utils;

namespace TiTacToe.Business.ViewModels
{
    public class BoardViewModel : IBoardViewModel
    {

        #region Properties

        private readonly Game _tiTacToe;

        public ObservableCollection<SquareWrapper> Board { get; set; }

        #endregion

        private readonly IMapper _mapper;
        private readonly IEventAggregator _eventAggregator;

        public BoardViewModel(
            IMapper mapper,
            IEventAggregator eventAggregator)
        {
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            _tiTacToe = new Game();

            Board = new ObservableCollection<SquareWrapper>();

            _eventAggregator.GetEvent<MoveEvent>().Subscribe(OnMove);
        }


        public void Initialize()
        {
            Board.Clear();
            _tiTacToe.GameAgain();
            foreach (var position in _tiTacToe.Positions)
            {
                var square = new Square(position);
                Board.Add(new SquareWrapper(square, _eventAggregator));
            }
        }

        private void OnMove()
        {
            CheckGame(PlayerType.Human);

            if (!_tiTacToe.IsGameOver)
            {
                _tiTacToe.NextRound();

                ComputerMove();

                _tiTacToe.NextRound();

            }
        }

        private void CheckGame(PlayerType player)
        {
            var board = _mapper.Map<IList<Square>>(Board);
            if (_tiTacToe.HasWon(board, player))
            {
                MarkAsGameOver();
                _eventAggregator.GetEvent<WinEvent>().Publish(player);
            }
            else if (!_tiTacToe.IsMoveAllowed)
            {
                MarkAsGameOver();
                _eventAggregator.GetEvent<WinEvent>().Publish(PlayerType.Ties);
            }
        }

        private void ComputerMove()
        {
            var board = _mapper.Map<IList<Square>>(Board);
            var bestMove = _tiTacToe.FindBestMove(board);
            var square = Board.Single(x => x.Position == bestMove.Position);

            square.MarkAsBusy();

            CheckGame(PlayerType.Computer);
        }

        private void MarkAsGameOver()
        {
            _tiTacToe.MarkAsGameOver();
            foreach (var square in Board)
            {
                square.Available = false;
            }
        }

    }
}
