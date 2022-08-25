using OrderingSystem.Dao;

namespace OrderingSystem.Models
{
    public class OrderInfo
    {
        public int id = 0;
        public int cuid = 0;
        public double totalPrice = 0;
        public DateTime date = DateTime.MinValue;
        public int status = 0;
        public int addressId = 0;
        public int telephoneId = 0;

        public OrderToClient ToOrderToClient()
        {
            OrderToClient orderToClient = new OrderToClient();
            orderToClient.id = this.id;
            orderToClient.cuid = this.cuid;
            orderToClient.totalPrice = this.totalPrice;
            orderToClient.date = this.date;
            orderToClient.status = this.status;
            orderToClient.address = new AddressDao().QueryAddressByAddressId(this.addressId).address;
            orderToClient.telephone = new TelephoneDao().QueryTelephoneByTelephoneId(this.telephoneId).telephone;

            return orderToClient;
        }
    }
}
