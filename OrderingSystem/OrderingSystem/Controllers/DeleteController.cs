using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderingSystem.Dao;

namespace OrderingSystem.Controllers
{
    [Route("delete")]
    public class DeleteController : Controller
    {
        [HttpGet("menu/{menuId}")]
        public IActionResult DeleteMenu(int menuId)
        {
            MenuDao menuDao = new MenuDao();
            bool success = menuDao.DeleteMenu(menuId);

            return Content(JsonConvert.SerializeObject(new { result = success }, Formatting.Indented));

        }

        [HttpGet("order/{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            OrderDao orderDao = new OrderDao();
            bool success = orderDao.DeleteOrder(orderId);

            return Content(JsonConvert.SerializeObject(new { result = success }, Formatting.Indented));
        }
    }
}
