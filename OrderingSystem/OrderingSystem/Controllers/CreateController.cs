using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderingSystem.Dao;
using OrderingSystem.Models;

namespace OrderingSystem.Controllers
{
    [Route("create")]
    [ApiController]
    public class CreateController : ControllerBase
    {
        [HttpPost("customer")]
        public IActionResult CreateCustomer([FromBody] Object obj)
        {    
            string s = obj.ToString();
            Customer customer = JsonConvert.DeserializeObject<Customer>(s);

            CustomerDao customerDao = new CustomerDao();
            bool success = customerDao.CreateCustomer(customer);

            if (success)
            {
                return Ok("create success");
            }
            return Ok("create fail");
        }

        [HttpPost("menu")]
        public IActionResult CreateMenu([FromBody] Object obj)
        {
            string s = obj.ToString();
            Menu menu = JsonConvert.DeserializeObject<Menu>(s);

            MenuDao menuDao = new MenuDao();
            bool success = menuDao.CreateMenu(menu);

            if (success)
            {
                return Ok("create success");
            }
            return Ok("create fail");
        }

        [HttpPost("address")]
        public IActionResult CreateAddress([FromBody] Object obj)
        {
            string s = obj.ToString();
            Address address = JsonConvert.DeserializeObject<Address>(s);

            AddressDao addressDao = new AddressDao();
            bool success = addressDao.CreateAddress(address);

            if (success)
            {
                return Ok("create success");
            }
            return Ok("create fail");
        }

        [HttpPost("telephone")]
        public IActionResult CreateTelephone([FromBody] Object obj)
        {
            string s = obj.ToString();
            Telephone telephone = JsonConvert.DeserializeObject<Telephone>(s);

            TelephoneDao telephoneDao = new TelephoneDao();
            bool success = telephoneDao.CreateTelephone(telephone);

            if (success)
            {
                return Ok("create success");
            }
            return Ok("create fail");
        }

        [HttpPost("order")]
        public IActionResult CreateOrder([FromBody] Object obj)
        {
            string s = obj.ToString();
            FullOrder fullOrder = JsonConvert.DeserializeObject<FullOrder>(s);

            OrderDao orderDao = new OrderDao();
            bool success = orderDao.CreateOrder(fullOrder);

            if (success)
            {
                return Ok("create success");
            }
            return Ok("create fail");
        }
    }
}
