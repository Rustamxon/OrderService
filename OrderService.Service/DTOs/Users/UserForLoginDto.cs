using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Service.DTOs.Users;

public class UserForLoginDto
{
    [Required]
    public string Login { get; set; }

    [Required]
    public string Password { get; set; }
}
