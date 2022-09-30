using System.ComponentModel.DataAnnotations;

namespace OrderService.Service.DTOs;

public class CategoryForCreationDto
{
    [Required]
    public string Name { get; set; }
}
