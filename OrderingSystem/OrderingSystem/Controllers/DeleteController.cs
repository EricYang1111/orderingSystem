using Microsoft.AspNetCore.Mvc;
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

            if (success)
            {
                return Ok("delete success");
            }
            return Ok("delete fail");
        }

        [HttpGet("order/{orderId}")]
        public IActionResult DeleteOrder(int orderId)
        {
            OrderDao orderDao = new OrderDao();
            bool success = orderDao.DeleteOrder(orderId);

            if (success)
            {
                return Ok("delete success");
            }
            return Ok("delete fail");
        }
    }
}
