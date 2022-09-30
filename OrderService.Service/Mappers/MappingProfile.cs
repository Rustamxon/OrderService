using AutoMapper;
using OrderService.Domain.Entities.Orders;
using OrderService.Domain.Entities.Porducts;
using OrderService.Domain.Entities.Users;
using OrderService.Service.DTOs;
using OrderService.Service.DTOs.Commons;
using OrderService.Service.DTOs.Customers;
using OrderService.Service.DTOs.Orders;
using OrderService.Service.DTOs.Payments;

namespace OrderService.Service.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryForCreationDto, ProductCategory>().ReverseMap();
        CreateMap<ProductForCreationDto, Product>().ReverseMap();
        CreateMap<ProductForViewModel, Product>().ReverseMap();

        CreateMap<OrderForCreationDto, Order>().ReverseMap();
        CreateMap<OrderProductForCreationDto, OrderProduct>().ReverseMap();
        CreateMap<PaymentForCreationDto, Payment>().ReverseMap();

        CreateMap<UserForCreationDto, User>().ReverseMap();
        CreateMap<AddressForCreationDto, Address>().ReverseMap();
    }
}
