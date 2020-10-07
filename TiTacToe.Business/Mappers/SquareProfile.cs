using AutoMapper;
using TiTacToe.Business.Wrappers;
using TiTacToe.Model.Models;

namespace TiTacToe.Business.Mappers
{
    public class SquareProfile : Profile
    {
        public SquareProfile()
        {
            CreateMap<SquareWrapper, Square>();
        }
    }
}
