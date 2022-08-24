using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderingSystem.Dao;
using OrderingSystem.Models;

namespace OrderingSystem.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [HttpPost]
        public IActionResult Login([FromBody] Object obj)
        {
            string s = obj.ToString();
            Customer newCustomer = JsonConvert.DeserializeObject<Customer>(s);

            Customer customer = new CustomerDao().QueryCustomerByName(newCustomer.name);

            if(customer == null || !customer.password.Equals(newCustomer.password))
            {
                return Content(JsonConvert.SerializeObject(new { }, Formatting.Indented));
            }
            return Content(JsonConvert.SerializeObject(customer, Formatting.Indented));
        }
    }
}
