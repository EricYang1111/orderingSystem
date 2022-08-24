using Microsoft.AspNetCore.Mvc;
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

            if (success)
            {
                return Ok("update success");
            }
            return Ok("update fail");
        }
    }
}
