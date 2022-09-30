using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OrderService.Data.IRepositories;
using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Orders;
using OrderService.Service.DTOs.Orders;
using OrderService.Service.Exceptions;
using OrderService.Service.Extensions;
using OrderService.Service.Interfaces.IOrderService;
using System.Linq.Expressions;

namespace OrderService.Service.Services.OrderServices;

public class OrdersService : IOrderService
{
    protected readonly IMapper mapper;
    protected readonly IUnitOfWork unitOfWork;

    public OrdersService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async ValueTask<Order> CreateAsync(OrderForCreationDto orderDto)
    {
        var order = 
            await this.unitOfWork.Orders.GetAsync(orer => 
                orer.UserId.Equals(orderDto.UserId));

        var products = order.OrderProducts.Select(p => p.Product);
        foreach(var product in products)
            order.TotalPrice += product.Price;

        if (!order.IsPaid)
            throw new CustomException(401, "Payment is not ready");

        var mappedOrder = this.mapper.Map<Order>(orderDto);
        mappedOrder.Create();

        mappedOrder = await this.unitOfWork.Orders.CreateAsync(mappedOrder);
        await this.unitOfWork.SaveChangesAsync();

        return mappedOrder;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Order, bool>> expression)
    {
        var order = await this.unitOfWork.Orders.GetAsync(expression);

        if (order is null)
            throw new CustomException(404, "Couldn't find order for given id");

        order.Delete();
        await this.unitOfWork.SaveChangesAsync();

        return true;
    }

    public IEnumerable<Order> GetAll(
        PaginationParams @params,
        Expression<Func<Order, bool>> expression = null)
    {
        var orders =
            this.unitOfWork.Orders.Where(expression).ToPaged(@params);

        return orders.ToList();
    }

    public async ValueTask<Order> GetAsync(Expression<Func<Order, bool>> expression)
    {
        var order = await this.unitOfWork.Orders.GetAsync(expression);

        if (order is null)
            throw new CustomException(404, "Couldn't find order for given id");

        return order;
    }

    public async ValueTask<Order> UpdateAsync(long id, OrderForCreationDto orderDto)
    {
        var order = 
            await this.unitOfWork.Orders.GetAsync(order => order.Id.Equals(id));

        if (order is null)
            throw new CustomException(404, "Couldn't find order for given id");

        var mappedOrder = this.mapper.Map(orderDto, order);
        mappedOrder.Update();

        mappedOrder = await this.unitOfWork.Orders.UpdateAsync(mappedOrder);
        await this.unitOfWork.SaveChangesAsync();

        return mappedOrder;
    }
}
