using System.Collections.Generic;
using System.Linq;
using TiTacToe.Model.Models;
using TiTacToe.Model.Utils;

namespace TiTacToe.Business.Utils
{
    public class Game
    {

        public int Round { get; private set; }

        public bool IsGameOver { get; private set; }

        public int[] Positions = { 0, 1, 2, 3, 4, 5, 6, 7, 8 };

        public bool IsMoveAllowed => Round < MaximumNumberOfRounds;

        private const int MaximumNumberOfRounds = 8;

        public void GameAgain()
        {
            Round = 0;
            IsGameOver = false;
        }

        public void NextRound()
        {
            Round++;
        }

        public void MarkAsGameOver()
        {
            IsGameOver = true;
        }

        public Move FindBestMove(IList<Square> board)
        {
            return MiniMax(board, PlayerType.Computer);
        }

        private Move MiniMax(IList<Square> board, PlayerType playerType)
        {
            if (HasWon(board, PlayerType.Computer))
            {
                return new Move { Score = 10 };
            }

            if (HasWon(board, PlayerType.Human))
            {
                return new Move { Score = -10 };
            }

            var availableSquares = board.Where(x => x.Available).ToList();
            if (availableSquares.Count == 0)
            {
                return new Move { Score = 0 };
            }

            var moves = new List<Move>();
            foreach (var square in availableSquares)
            {
                var move = new Move();
                var position = square.Position;

                board[position].Available = false;
                board[position].PlayerType = playerType;
                move.Position = board[position].Position;

                if (playerType == PlayerType.Computer)
                {
                    var m = MiniMax(board, PlayerType.Human);
                    move.Score = m.Score;
                }
                else if (playerType == PlayerType.Human)
                {
                    var m = MiniMax(board, PlayerType.Computer);
                    move.Score = m.Score;
                }

                board[position].Position = move.Position;
                moves.Add(move);
            }

            var bestMove = 0;
            if (playerType == PlayerType.Computer)
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
            else if (playerType == PlayerType.Human)
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


        public bool HasWon(IList<Square> board, PlayerType player)
        {
            return
              (board[0].PlayerType == player && board[1].PlayerType == player && board[2].PlayerType == player) ||
              (board[3].PlayerType == player && board[4].PlayerType == player && board[5].PlayerType == player) ||
              (board[6].PlayerType == player && board[7].PlayerType == player && board[8].PlayerType == player) ||
              (board[0].PlayerType == player && board[3].PlayerType == player && board[6].PlayerType == player) ||
              (board[1].PlayerType == player && board[4].PlayerType == player && board[7].PlayerType == player) ||
              (board[2].PlayerType == player && board[5].PlayerType == player && board[8].PlayerType == player) ||
              (board[0].PlayerType == player && board[4].PlayerType == player && board[8].PlayerType == player) ||
              (board[2].PlayerType == player && board[4].PlayerType == player && board[6].PlayerType == player);

        }
    }
}
