using OrderService.Domain.Commons;
using OrderService.Domain.Enums;

namespace OrderService.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Gender Gender { get; set; }
    public UserRole Role { get; set; }
}
