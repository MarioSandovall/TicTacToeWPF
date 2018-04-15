using Prism.Events;
using TiTacToe.WPF.Models;

namespace TiTacToe.WPF.Event
{
    public class MoveEvent :PubSubEvent<Player> {}

}
