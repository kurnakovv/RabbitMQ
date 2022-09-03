using Microsoft.AspNetCore.Mvc;
using Producer.Models;
using Producer.Services;

namespace Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RabbitMqController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMqService;

        public RabbitMqController(
            IRabbitMqService rabbitMqService)
        {
            _rabbitMqService = rabbitMqService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] MessageModel model)
        {
            _rabbitMqService.SendMessage(model.Text);
            return Ok("Ok");
        }
    }
}
