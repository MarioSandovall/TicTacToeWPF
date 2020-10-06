using Prism.Events;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TiTacToe.WPF.Contracts.Services;
using TiTacToe.WPF.Event;
using TiTacToe.WPF.Models;
using TiTacToe.WPF.Utilities;
using TiTacToe.WPF.Wrappers;

namespace TiTacToe.WPF.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<SquareWrapper> Board { get; set; }

        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        private readonly IDialogServices _dialogServices;
        private readonly IEventAggregator _eventAggregator;
        public MainViewModel(
            IDialogServices dialogServices,
            IEventAggregator eventAggregator)
        {
            _dialogServices = dialogServices;
            _eventAggregator = eventAggregator;

            Player1 = new Player();
            Player2 = new Player();

            Board = new ObservableCollection<SquareWrapper>();
            _eventAggregator.GetEvent<MoveEvent>().Subscribe(OnMove);
        }

        public void Load()
        {
            InitializePlayers();
            InitializeBoard();
        }

        private void InitializePlayers()
        {
            Player1.Name = "Player 1";
            Player1.Type = PlayerType.Humman;

            Player2.Name = "Player 2";
            Player2.Type = PlayerType.Circle;
        }

        private void InitializeBoard()
        {
            Board.Clear();
            foreach (var i in _board)
            {
                var square = new Square(i);
                Board.Add(new SquareWrapper(square, _eventAggregator));
            }

            _round = 0;
            Helpers.CurrentPlayer = Player1;
        }

        private async void OnMove(Player player)
        {
            _round++;
            var aux = new List<Square>();
            Board.Select(x => x.Model).ToList().ForEach(s => aux.Add(new Square(s)));

            if (Winning(aux, player.Type))
            {
                await _dialogServices.ShowMessageAsync("TicTacToe", $"{player.Name} wins !!!");
                InitializeBoard();
                return;
            }
            else if (_round > 8)
            {
                await _dialogServices.ShowMessageAsync("TicTacToe", "TIE !!!");
                InitializeBoard();
                return;
            }
            else
            {
                aux.Clear();
                Board.Select(x => x.Model).ToList().ForEach(s => aux.Add(new Square(s)));

                var index = Minimax(aux, PlayerType.Compuer).Index;
                var item = Board.Single(x => x.Position == index);
                item.Available = false;
                item.Color = SelectedColor;
                item.Type = PlayerType.Compuer;
                item.Image = CircleImage;


                if (Winning(Board.Select(x => x.Model).ToList(), PlayerType.Compuer))
                {
                    await _dialogServices.ShowMessageAsync("TicTacToe", $"Computer wins !!!");
                    InitializeBoard();
                    return;
                }
                else if (_round > 8)
                {
                    await _dialogServices.ShowMessageAsync("TicTacToe", "TIE !!!");
                    InitializeBoard();
                    return;
                }
            }

            //Helpers.CurrentPlayer = Helpers.CurrentPlayer.Type == PlayerType.Circle
            //   ? Player1 : Player2;
        }

        private bool Winning(IList<Square> board, PlayerType player)
        {
            return (board[0].Type == player && board[1].Type == player && board[2].Type == player) ||
                   (board[3].Type == player && board[4].Type == player && board[5].Type == player) ||
                   (board[6].Type == player && board[7].Type == player && board[8].Type == player) ||
                   (board[0].Type == player && board[3].Type == player && board[6].Type == player) ||
                   (board[1].Type == player && board[4].Type == player && board[7].Type == player) ||
                   (board[2].Type == player && board[5].Type == player && board[8].Type == player) ||
                   (board[0].Type == player && board[4].Type == player && board[8].Type == player) ||
                   (board[2].Type == player && board[4].Type == player && board[6].Type == player);
        }

        private Move Minimax(IList<Square> reboard, PlayerType player)
        {
            _interaction++;
            var availableSquares = reboard.Where(x => x.Available).ToList();

            if (Winning(reboard, PlayerType.Humman))
            {
                return new Move() { Index = 0, Score = -10 };
            }
            else if (Winning(reboard, PlayerType.Compuer))
            {
                return new Move() { Index = 0, Score = 10 };
            }
            else if (availableSquares.Count == 0)
            {
                return new Move() { Index = 0, Score = 0 };
            }

            var moves = new List<Move>();
            for (var i = 0; i < availableSquares.Count; i++)
            {
                var move = new Move();
                var index = availableSquares[i].Position;

                reboard[index].Type = player;
                reboard[index].Available = false;
                move.Index = reboard[index].Position;

                if (player == PlayerType.Compuer)
                {
                    var m = Minimax(reboard, PlayerType.Humman);
                    move.Score = m.Score;
                }
                else
                {
                    var m = Minimax(reboard, PlayerType.Compuer);
                    move.Score = m.Score;
                }

                reboard[index].Position = move.Index;
                moves.Add(move);
            }

            int bestMove = 0;
            if (player == PlayerType.Compuer)
            {
                var bestScore = -10000;
                for (var i = 0; i < moves.Count; i++)
                {
                    if (moves[i].Score > bestScore)
                    {
                        bestScore = moves[i].Score;
                        bestMove = i;
                    }
                }
            }
            else
            {
                var bestScore = 10000;
                for (var i = 0; i < moves.Count; i++)
                {
                    if (moves[i].Score < bestScore)
                    {
                        bestScore = moves[i].Score;
                        bestMove = i;
                    }
                }
            }

            return moves[bestMove];
        }

        private readonly int[] _board = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        private const string SelectedColor = "#FF1585B5";
        private const string CrossImage = "/TiTacToe.WPF;component/Resources/Images/Cross.png";
        private const string CircleImage = "/TiTacToe.WPF;component/Resources/Images/Circle.png";
        private int _round = 0;
        private int _interaction = 0;
    }
}



//private async void OnMove(Player player)
//{
//    _round++;
//    var aux = new List<Square>();
//    Board.Select(x => x.Model).ToList().ForEach(s => aux.Add(new Square(s)));

//    if (Winning(aux, player.Type))
//    {
//        await _dialogServices.ShowMessageAsync("TicTacToe", $"{player.Name} wins !!!");
//        InitializeBoard();
//        return;
//    }
//    else if (_round > 8)
//    {
//        await _dialogServices.ShowMessageAsync("TicTacToe", "TIE !!!");
//        InitializeBoard();
//        return;
//    }
//    else
//    {
//        aux.Clear();
//        Board.Select(x => x.Model).ToList().ForEach(s => aux.Add(new Square(s)));

//        var index = Minimax(aux, PlayerType.Compuer).Index;
//        var item = Board.Single(x => x.Position == index);
//        item.Available = false;
//        item.Color = SelectedColor;
//        item.Type = PlayerType.Compuer;
//        item.Image = CircleImage;


//        if (Winning(Board.Select(x => x.Model).ToList(), PlayerType.Compuer))
//        {
//            await _dialogServices.ShowMessageAsync("TicTacToe", $"Computer wins !!!");
//            InitializeBoard();
//            return;
//        }
//        else if (_round > 8)
//        {
//            await _dialogServices.ShowMessageAsync("TicTacToe", "TIE !!!");
//            InitializeBoard();
//            return;
//        }
//    }

//    //Helpers.CurrentPlayer = Helpers.CurrentPlayer.Type == PlayerType.Circle
//    //   ? Player1 : Player2;
//}

//private bool Winning(IList<Square> board, PlayerType player)
//{
//    return (board[0].Type == player && board[1].Type == player && board[2].Type == player) ||
//           (board[3].Type == player && board[4].Type == player && board[5].Type == player) ||
//           (board[6].Type == player && board[7].Type == player && board[8].Type == player) ||
//           (board[0].Type == player && board[3].Type == player && board[6].Type == player) ||
//           (board[1].Type == player && board[4].Type == player && board[7].Type == player) ||
//           (board[2].Type == player && board[5].Type == player && board[8].Type == player) ||
//           (board[0].Type == player && board[4].Type == player && board[8].Type == player) ||
//           (board[2].Type == player && board[4].Type == player && board[6].Type == player);
//}

//private Move Minimax(IList<Square> reboard, PlayerType player)
//{
//    _interaction++;
//    var availableSquares = reboard.Where(x => x.Available).ToList();

//    if (Winning(reboard, PlayerType.Humman))
//    {
//        return new Move() { Index = 0, Score = -10 };
//    }
//    else if (Winning(reboard, PlayerType.Compuer))
//    {
//        return new Move() { Index = 0, Score = 10 };
//    }
//    else if (availableSquares.Count == 0)
//    {
//        return new Move() { Index = 0, Score = 0 };
//    }

//    var moves = new List<Move>();
//    for (var i = 0; i < availableSquares.Count; i++)
//    {
//        var move = new Move();
//        var index = availableSquares[i].Position;

//        reboard[index].Type = player;
//        reboard[index].Available = false;
//        move.Index = reboard[index].Position;

//        if (player == PlayerType.Compuer)
//        {
//            var m = Minimax(reboard, PlayerType.Humman);
//            move.Score = m.Score;
//        }
//        else
//        {
//            var m = Minimax(reboard, PlayerType.Compuer);
//            move.Score = m.Score;
//        }

//        reboard[index].Position = move.Index;
//        moves.Add(move);
//    }

//    int bestMove = 0;
//    if (player == PlayerType.Compuer)
//    {
//        var bestScore = -10000;
//        for (var i = 0; i < moves.Count; i++)
//        {
//            if (moves[i].Score > bestScore)
//            {
//                bestScore = moves[i].Score;
//                bestMove = i;
//            }
//        }
//    }
//    else
//    {
//        var bestScore = 10000;
//        for (var i = 0; i < moves.Count; i++)
//        {
//            if (moves[i].Score < bestScore)
//            {
//                bestScore = moves[i].Score;
//                bestMove = i;
//            }
//        }
//    }

//    return moves[bestMove];
//}

//private readonly int[] _board = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
//private const string SelectedColor = "#FF1585B5";
//private const string CrossImage = "/TiTacToe.WPF;component/Resources/Images/Cross.png";
//private const string CircleImage = "/TiTacToe.WPF;component/Resources/Images/Circle.png";
//private int _round = 0;
//private int _interaction = 0;