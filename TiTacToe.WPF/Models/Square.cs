using TiTacToe.WPF.Utilities;

namespace TiTacToe.WPF.Models
{
    public class Square
    {
        public string Color { get; set; }
        public string Image { get; set; }
        public bool Available { get; set; }
        public int Position { get; set; }
        public PlayerType Type { get; set; }

        public Square(int position)
        {
            Position = position;
            Color = DefaultColor;
            Available = true;
        }

        public Square(Square model)
        {
            Color = model.Color;
            Image = model.Image;
            Available = model.Available;
            Position = model.Position;
            Type = model.Type;
        }

        public Square()
        {
            
        }

        private const string DefaultColor = "#FF252525";
    }


}
