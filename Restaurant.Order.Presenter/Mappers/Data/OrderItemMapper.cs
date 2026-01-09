using AutoMapper;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Data.Model;

namespace Restaurant.Order.Presenter.Mappers.Data;

public class OrderItemMapper : Profile
{

    public OrderItemMapper()
    {
        CreateMap<OrderItemDto, OrderItemData>()
            .ReverseMap();
    }

}
