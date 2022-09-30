using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Configuration;
using OrderService.Service.DTOs.Customers;
using OrderService.Service.Interfaces.CustomerService;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize(Policy = "SuperAdminPolicy")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    public UserController(IUserService userService)
    {
        this.userService = userService;
    }

    /// <summary>
    /// Userni yaratish
    /// </summary>
    /// <param name="userDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async ValueTask<IActionResult> InsertAsync([FromBody] UserForCreationDto userDto) =>
        Ok(await this.userService.CreateAsync(userDto));

    /// <summary>
    /// Barcha userlarni malumotini olish
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public IActionResult SelectAllsync([FromQuery] PaginationParams @params) =>
        Ok(this.userService.GetAll(@params));

    /// <summary>
    /// Berilgan id bilan userni malumotini olish
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
  //  [Authorize(Policy = "AllPolicy")]
    public async ValueTask<IActionResult> SelectByIdAsync(long id) =>
        Ok(await this.userService.GetAsync(user => user.Id.Equals(id)));

    /// <summary>
    /// Berilgan id bilan userni malumotlarini yangilash
    /// </summary>
    /// <param name="id"></param>
    /// <param name="userDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async ValueTask<IActionResult> UpdateAsync(
        long id, [FromBody] UserForCreationDto userDto) =>
        Ok(await this.userService.UpdateAsync(id, userDto));

    /// <summary>
    /// Berilgan id bilan userni o'chirib yuborish
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteAsync(long id) =>
        Ok(await this.userService.DeleteAsync(user => user.Id.Equals(id)));
}
