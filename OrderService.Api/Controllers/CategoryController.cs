using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Configuration;
using OrderService.Service.DTOs;
using OrderService.Service.Interfaces.ICategorieService;

namespace OrderService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize (Policy = "AdminPolicy")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService categoryService;
    public CategoryController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    /// <summary>
    /// Categoryni yaratish
    /// </summary>
    /// <param name="categoryDto"></param>
    /// <returns></returns>
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(CategoryForCreationDto categoryDto) =>
        Ok(await this.categoryService.CreateAsync(categoryDto));

    /// <summary>
    /// Barcha categorylarni olish
    /// </summary>
    /// <param name="params"></param>
    /// <returns></returns>
    [HttpGet]
    [Authorize(Policy = "AllPolicy")]
    public IActionResult SelectAllAsycn([FromQuery] PaginationParams @params) =>
        Ok(this.categoryService.GetAll(@params));

    /// <summary>
    /// Berilgan id bilan categoryni olish
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async ValueTask<IActionResult> SelectByIdAsync(long id) =>
        Ok(await this.categoryService.GetAsync(category => category.Id.Equals(id)));

    /// <summary>
    /// Berilgan id bilan categoryni yangilash
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoryDto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async ValueTask<IActionResult> PutAsync(
        long id,[FromBody] CategoryForCreationDto categoryDto) =>
        Ok(await this.categoryService.UpdateAsync(id, categoryDto));

    /// <summary>
    /// Berilgan id bilan categoryni o'chirish
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async ValueTask<IActionResult> DeleteAsync(long id) =>
        Ok(await this.categoryService.DeleteAsync(category => category.Id.Equals(id)));
}
