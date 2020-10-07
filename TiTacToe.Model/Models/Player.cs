using TiTacToe.Model.Utils;

namespace TiTacToe.Model.Models
{
    public class Player
    {
        public string Name { get; set; }

        public PlayerType PlayerType { get; set; }

        public int Score { get; set; }


        public Player(string name, PlayerType playerType)
        {
            Score = 0;
            Name = name;
            PlayerType = playerType;
        }

        public Player()
        {
            
        }
    }
}
