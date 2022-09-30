using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Configuration;
using OrderService.Service.DTOs.Payments;
using OrderService.Service.Interfaces.IPaymentService;

namespace OrderService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminPolicy")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }

        /// <summary>
        /// To'lovni yaratish
        /// </summary>
        /// <param name="paymentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = "AllPolicy")]
        public async ValueTask<IActionResult> PostAsync(PaymentForCreationDto paymentDto) =>
            Ok(await this.paymentService.CreateAsync(paymentDto));

        /// <summary>
        /// Barcha to'lovlarni olish
        /// </summary>
        /// <param name="params"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Policy = "AllPolicy")]
        public IActionResult SelectAllAsync([FromQuery] PaginationParams @params) =>
            Ok(this.paymentService.GetAll(@params));

        /// <summary>
        /// Berilgan id bilan to'lovni olish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize(Policy = "ChefPolicy")]
        public async ValueTask<IActionResult> SelectByIdAsync(long id) =>
            Ok(await this.paymentService.GetAsync(payment => payment.Id.Equals(id)));

        /// <summary>
        /// Berilgan id bilan to'lovni yangilash
        /// </summary>
        /// <param name="id"></param>
        /// <param name="paymentDto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async ValueTask<IActionResult> PutAsync(
            long id, [FromBody] PaymentForCreationDto paymentDto) =>
            Ok(await this.paymentService.UpdateAsync(id, paymentDto));

        /// <summary>
        /// Berilgan id bilan to'lovni o'chirish
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async ValueTask<IActionResult> DeleteAsync(long id) =>
            Ok(await this.paymentService.DeleteAsync(payment => payment.Id.Equals(id)));
    }
}
