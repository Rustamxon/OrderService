using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Configuration;
using OrderService.Service.DTOs.Orders;
using OrderService.Service.Interfaces.IOrderService;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ChefPolicy")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        /// <summary>
        /// Buyurtma yaratish
        /// </summary>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "AllPolicy")]
        public async ValueTask<IActionResult> PostAsycn(OrderForCreationDto orderDto) =>
            Ok(await this.orderService.CreateAsync(orderDto));

        /// <summary>
        /// Barcha buyurtmalarni olish
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "AllPolicy")]
        public IActionResult SelectAllAsycn([FromQuery] PaginationParams @params) =>
            Ok(this.orderService.GetAll(@params));

        /// <summary>
        /// Berilgan id bilan buyurtmani olish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async ValueTask<IActionResult> SelectByidAsync(long id) =>
            Ok(await this.orderService.GetAsync(order => order.Id.Equals(id)));

        /// <summary>
        /// Berilgan id bilan buyurtmani yangilash
        /// </summary>
        /// <param name="id"></param>
        /// <param name="orderDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutAsync(
            long id, [FromBody] OrderForCreationDto orderDto) =>
            Ok(await this.orderService.UpdateAsync(id, orderDto));

        /// <summary>
        /// Berilgan id bilan buyurtmani o'chirish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteAsync(long id) =>
            Ok(await this.orderService.DeleteAsync(order => order.Id.Equals(id)));
    }
}
