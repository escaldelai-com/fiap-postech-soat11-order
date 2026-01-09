using AutoMapper;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Data.Model;

namespace Restaurant.Order.Presenter.Mappers.Data;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<ProductData, ProductDto>()
            .ReverseMap();
    }

}
