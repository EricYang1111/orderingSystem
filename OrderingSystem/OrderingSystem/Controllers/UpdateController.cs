using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderingSystem.Dao;

namespace OrderingSystem.Controllers
{
    [Route("update")]
    public class UpdateController : Controller
    {
        [HttpGet("orderStatus/{orderId}")]
        public IActionResult UpdateOrderStatus(int orderId)
        {
            OrderDao orderDao = new OrderDao();
            bool success = orderDao.UpdateStatus(orderId);

            return Content(JsonConvert.SerializeObject(new { result = success }, Formatting.Indented));
        }
    }
}
