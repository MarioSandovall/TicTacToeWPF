using TiTacToe.Model.Models;
using TiTacToe.Model.Utils;

namespace TiTacToe.Business.Wrappers
{
    public class PlayerWrapper : ModelWrapper<Player>
    {
        public string Name
        {
            get => GetValue<string>();
            set => SetValue(value);
        }

        public int Score
        {
            get => GetValue<int>();
            set => SetValue(value);
        }

        public PlayerType PlayerType
        {
            get => GetValue<PlayerType>();
            set => SetValue(value);
        }

        public PlayerWrapper(Player model) : base(model)
        {

        }

        public void Win()
        {
            Score++;
        }
    }
}
