using AutoMapper;
using Restaurant.Order.Application.DTO;
using Restaurant.Order.Data.Model;

namespace Restaurant.Order.Presenter.Mappers.Data;

public class ProductTypeMapper : Profile
{

    public ProductTypeMapper()
    {
        CreateMap<ProductTypeData, ProductTypeDto>()
            .ReverseMap();
    }

}
