using TiTacToe.Model.Utils;

namespace TiTacToe.Model.Models
{
    public class Square
    {
        public string Color { get; set; }

        public string Image { get; set; }

        public bool Available { get; set; }

        public int Position { get; set; }

        public PlayerType PlayerType { get; set; }


        public Square(int position)
        {
            Available = true;
            Position = position;
            Color = Colors.Default;
        }
    }

}
