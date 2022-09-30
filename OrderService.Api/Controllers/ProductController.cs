using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Configuration;
using OrderService.Service.DTOs.Commons;
using OrderService.Service.Interfaces.IProductServices;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminPolicy")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Mahsulotni yaratish
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async ValueTask<IActionResult> PostAsync([FromForm] ProductForCreationDto productDto) =>
            Ok(await this.productService.CreateAsync(productDto));

        /// <summary>
        /// Barcha mahsulotni olish
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "AllPolicy")]
        public IActionResult SelectAllAsync([FromQuery] PaginationParams @params) =>
            Ok(this.productService.GetAll(@params));

        /// <summary>
        /// Berilgan id bilan mahsulotni olish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "AllPolicy")]
        public async ValueTask<IActionResult> SelectByIdAsync(long id) =>
            Ok(await this.productService.GetAsync(product => product.Id.Equals(id)));

        /// <summary>
        /// Berilgan id bilan mahsulotni malumotini yangilash
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutAsync(
            long id,[FromBody] ProductForCreationDto productDto) =>
            Ok(await this.productService.UpdateAsync(id, productDto));

        /// <summary>
        /// Berilgan id bilan mahsulot o'chirish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteAsync(long id) =>
            Ok(await this.productService.DeleteAsync(product => product.Id.Equals(id)));
    }
}
