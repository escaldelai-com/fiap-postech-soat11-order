using AutoMapper;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Data.Model;

namespace Restaurant.Order.Presenter.Mappers.Data;

public class OrderInfoMapper : Profile
{

    public OrderInfoMapper()
    {
        CreateMap<OrderInfoDto, OrderInfoData>()
            .ForPath(d => d.Cliente, o => o.MapFrom(s => s.Cliente!.Id))
            .ReverseMap()
            .ForPath(d => d.Cliente!.Id, o => o.MapFrom(s => s.Cliente));
    }

}
