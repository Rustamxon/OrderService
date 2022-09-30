using OrderService.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Service.DTOs.Payments;

public class PaymentForCreationDto
{
    [Required]
    public decimal PaidPrice { get; set; }

    [Required]
    public PaymentType PaymentType { get; set; }

    [Required]
    public long OrderId { get; set; }
}
