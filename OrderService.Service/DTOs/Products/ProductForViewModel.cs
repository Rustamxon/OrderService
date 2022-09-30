using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Service.DTOs.Commons;

public class ProductForViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    public decimal Price { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public long CategoryId { get; set; }
    public IFormFile File { get; set; }
}
