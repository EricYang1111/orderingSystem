using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OrderingSystem.Dao;
using OrderingSystem.Models;

namespace OrderingSystem.Controllers
{
    [Route("query")]
    public class QueryController : Controller
    {
        [HttpGet("customerByName/{customerName}")]
        public ActionResult QueryCustomer(string customerName)
        {
            CustomerDao customerDao = new CustomerDao();
            Customer customer = customerDao.QueryCustomerByName(customerName);

            return Content(JsonConvert.SerializeObject(customer == null ? (new { }) : customer, Formatting.Indented));
            //return Json(customer);
        }

        [HttpGet("customerById/{customerId}")]
        public ActionResult QueryCustomer(int customerId)
        {
            CustomerDao customerDao = new CustomerDao();
            Customer customer = customerDao.QueryCustomerById(customerId);

            return Content(JsonConvert.SerializeObject(customer==null?(new {}):customer, Formatting.Indented));
            //return Json(customer);
        }

        [HttpGet("menuByName/{menuName}")]
        public ActionResult QueryMenuByName(string menuName)
        {
            MenuDao menuDao = new MenuDao();
            Menu menu = menuDao.QueryMenuByName(menuName);

            return Content(JsonConvert.SerializeObject(menu == null ? (new { }) : menu, Formatting.Indented));

        }

        [HttpGet("menuById/{menuId}")]
        public ActionResult QueryMenuById(int menuId)
        {
            MenuDao menuDao = new MenuDao();
            Menu menu = menuDao.QueryMenuById(menuId);

            return Content(JsonConvert.SerializeObject(menu == null ? (new { }) : menu, Formatting.Indented));

        }

        [HttpGet("menu")]
        public ActionResult QueryMenu()
        {
            MenuDao menuDao = new MenuDao();
            List<Menu> menus = menuDao.QueryMenus();

            return Content(JsonConvert.SerializeObject((menus == null || menus.Count == 0 )? (new List<Menu>()) : menus, Formatting.Indented));
        }

        [HttpGet("addresses/{customerId}")]
        public ActionResult QueryAddresses(int customerId)
        {
            AddressDao addressDao = new AddressDao();
            List<Address> addresses = addressDao.QueryAddresses(customerId);

            return Content(JsonConvert.SerializeObject((addresses == null || addresses.Count == 0) ? (new List<Address>()) : addresses, Formatting.Indented));
        }

        [HttpGet("address/{addressId}")]
        public ActionResult QueryAddress(int addressId)
        {
            AddressDao addressDao = new AddressDao();
            Address address = addressDao.QueryAddressByAddressId(addressId);

            return Content(JsonConvert.SerializeObject(address == null ? (new { }) : address, Formatting.Indented));
        }

        [HttpGet("telephones/{customerId}")]
        public ActionResult QueryTelephones(int customerId)
        {
            TelephoneDao telephoneDao = new TelephoneDao();
            List<Telephone> telephones = telephoneDao.QueryTelephones(customerId);

            return Content(JsonConvert.SerializeObject((telephones == null || telephones.Count == 0) ? (new List<Address>()) : telephones, Formatting.Indented));
        }

        [HttpGet("telephone/{telephoneId}")]
        public ActionResult QueryTelephone(int telephoneId)
        {
            TelephoneDao telephoneDao = new TelephoneDao();
            Telephone telephone = telephoneDao.QueryTelephoneByTelephoneId(telephoneId);

            return Content(JsonConvert.SerializeObject(telephone == null ? (new { }) : telephone, Formatting.Indented));
        }

        [HttpGet("orders/{customerId}")]
        public ActionResult QueryOrders(int customerId)
        {
            OrderDao orderDao = new OrderDao();
            List<OrderInfo> orderInfos = orderDao.QureyOrders(customerId);

            return Content(JsonConvert.SerializeObject((orderInfos == null || orderInfos.Count == 0) ? (new List<Address>()) : orderInfos, Formatting.Indented));

        }

        [HttpGet("order/{orderId}")]
        public ActionResult QueryOrder(int orderId)
        {
            OrderDao orderDao = new OrderDao();
            OrderInfo orderInfo = orderDao.QureyOrder(orderId);

            return Content(JsonConvert.SerializeObject(orderInfo == null ? (new { }) : orderInfo, Formatting.Indented));

        }

        [HttpGet("orderItems/{orderId}")]
        public ActionResult QueryOrderItem(int orderId)
        {
            OrderItemDao orderItemDao = new OrderItemDao();
            List<OrderItem> orderItems = orderItemDao.QueryOrderItems(orderId);

            return Content(JsonConvert.SerializeObject((orderItems == null || orderItems.Count == 0) ? (new List<OrderItem>()) : orderItems, Formatting.Indented));

        }
    }
}
