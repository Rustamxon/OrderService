using AutoMapper;
using OrderService.Data.IRepositories;
using OrderService.Domain.Configuration;
using OrderService.Domain.Entities.Orders;
using OrderService.Service.DTOs.Payments;
using OrderService.Service.Exceptions;
using OrderService.Service.Extensions;
using OrderService.Service.Interfaces.IOrderService;
using OrderService.Service.Interfaces.IPaymentService;
using System.Linq.Expressions;

namespace OrderService.Service.Services.PaymentServices;

public class PaymentService : IPaymentService
{
    protected readonly IMapper mapper;
    protected readonly IUnitOfWork unitOfWork;
    protected readonly IOrderService orderService;

    public PaymentService(
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IOrderService orderService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.orderService = orderService;
    }

    public async ValueTask<Payment> CreateAsync(PaymentForCreationDto paymentDto)
    {
        var order = 
            await this.unitOfWork.Orders.GetAsync(order => 
                order.Id.Equals(paymentDto.OrderId));

        if (order is null && order.IsPaid)
            throw new CustomException(404, "Couldn't find order");

        order.IsPaid = true;
        order.Update();

        var mappedPayment = this.mapper.Map<Payment>(paymentDto);
        mappedPayment.Create();

        mappedPayment = 
            await this.unitOfWork.Payments.CreateAsync(mappedPayment);

        await this.unitOfWork.Orders.UpdateAsync(order);
        await this.unitOfWork.SaveChangesAsync();

        return mappedPayment;
    }

    public async ValueTask<bool> DeleteAsync(Expression<Func<Payment, bool>> expression)
    {
        var payment = await this.unitOfWork.Payments.GetAsync(expression);

        if (payment is null)
            throw new CustomException(404, "Couldn't find payment");

        payment.Delete();
        await this.unitOfWork.SaveChangesAsync();

        return true;
    }

    public IEnumerable<Payment> GetAll(
        PaginationParams @params,
        Expression<Func<Payment, bool>> expression = null)
    {
        var payments =
            this.unitOfWork.Payments.Where(expression).ToPaged(@params);

        return payments.ToList();
    }

    public async ValueTask<Payment> GetAsync(Expression<Func<Payment, bool>> expression)
    {
        var payment = await this.unitOfWork.Payments.GetAsync(expression);

        if (payment is null)
            throw new CustomException(404, "Couldn't find payment");

        return payment;
    }

    public async ValueTask<Payment> UpdateAsync(long id, PaymentForCreationDto paymentDto)
    {
        var payment =
            await this.unitOfWork.Payments.GetAsync(payment =>
                payment.OrderId.Equals(paymentDto.OrderId));

        if (payment is null)
            throw new CustomException(404, "Couldn't find payment");

        var mappedPayment = this.mapper.Map(paymentDto, payment);
        mappedPayment.Update();

        mappedPayment = 
            await this.unitOfWork.Payments.UpdateAsync(mappedPayment);
        await this.unitOfWork.SaveChangesAsync();

        return mappedPayment;
    }
}
