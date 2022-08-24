using OrderingSystem.Models;
using System.Data.SqlClient;

namespace OrderingSystem.Dao
{
    public class OrderDao : Connect
    {
        /// <summary>
        /// 创建 order 
        /// </summary>
        /// <param name="fullOrder"></param>
        /// <returns></returns>
        public bool CreateOrder(FullOrder fullOrder)
        {
            //先添加 orderinfo
            //FullOrder 转 OrderInfo
            OrderInfo orderInfo = ToOrderInfo(fullOrder); 
            //拼装执行 sql
            string sql = $"insert into orderinfo values('{orderInfo.cuid}','{orderInfo.totalPrice}'," +
                $"'{orderInfo.date}','{orderInfo.status}','{orderInfo.addressId}','{orderInfo.telephoneId}');";
            if (!CreateOrDeleteOrUpdate(sql))
            {
                return false;
            }

            //再添加 orderitem
            //获取刚添加的 orderinfo 的 id
            int orderId = GetOrderId(fullOrder.cuid, orderInfo.date);
            if (orderId == 0)
            {
                return false;
            }
            //添加 orderitem
            OrderItemDao orderItemDao = new OrderItemDao();
            return orderItemDao.CreateOrderItems(orderId, fullOrder.menu); 
        }

        /// <summary>
        /// 查找某个用户的所有 order
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<OrderInfo> QureyOrders(int customerId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from orderinfo where cuid={customerId}";
            List<OrderInfo> orderInfos = Query<List<OrderInfo>>(sql, GetOrderInfos);
            return orderInfos;
        }

        public bool DeleteOrder(int orderId)
        {
            //先删除 orderitem
            string sql1 = $"delete from orderitem where orderid={orderId};";
            CreateOrDeleteOrUpdate(sql1);

            //再删除 orderinfo
            string sql2 = $"delete from orderinfo where id={orderId};";
            return CreateOrDeleteOrUpdate(sql2);
        }

        /// <summary>
        /// FullOrder对象 转 OrderInfo对象
        /// </summary>
        /// <param name="fullOrder"></param>
        /// <returns></returns>
        private OrderInfo ToOrderInfo(FullOrder fullOrder)
        {
            OrderInfo info = new OrderInfo();
            info.cuid = fullOrder.cuid;
            info.totalPrice = fullOrder.totalPrice;
            info.date = DateTime.Now;
            info.status = 0;
            
            Address address = new AddressDao().QueryAddressByAddress(fullOrder.address);
            if(address == null)
            {
                new AddressDao().CreateAddress(new Address(){
                    cuid = fullOrder.cuid,
                    address = fullOrder.address
                });
                address = new AddressDao().QueryAddressByAddress(fullOrder.address);
            }
            info.addressId = address.id;

            Telephone telephone = new TelephoneDao().QueryTelephoneByTelephone(fullOrder.telephone);
            if (telephone == null)
            {
                new TelephoneDao().CreateTelephone(new Telephone()
                {
                    cuid = fullOrder.cuid,
                    telephone = fullOrder.telephone
                });
                telephone = new TelephoneDao().QueryTelephoneByTelephone(fullOrder.telephone);
            }
            info.telephoneId = telephone.id;
            return info;
        }


        /// <summary>
        /// 获取 orderinfo对象的 id
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private int GetOrderId(int customerId, DateTime dateTime)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from orderinfo where cuid={customerId} and date='{dateTime}';";
            return Query<Int32>(sql, (reader) =>
            {
                if (reader.Read())
                {
                    return Convert.ToInt32(reader["id"]);
                }
                return 0;
            });
        }


        /// <summary>
        /// 作为回调函数，根据获得的 SqlDataReader 对象 构造 OrderInfo 对象(可能有多个对象)
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<OrderInfo> GetOrderInfos(SqlDataReader reader)
        {
            //解析数据
            List<OrderInfo> orderInfos = new List<OrderInfo>();
            while (reader != null && reader.Read())
            {
                OrderInfo orderInfo = new OrderInfo();
                orderInfo.totalPrice = Convert.ToDouble(reader["totalprice"]);
                orderInfo.id = Convert.ToInt32(reader["id"]);
                orderInfo.cuid = Convert.ToInt32(reader["cuid"]);
                orderInfo.date = Convert.ToDateTime(reader["date"]);
                orderInfo.status = Convert.ToInt32(reader["status"]);
                orderInfo.addressId = Convert.ToInt32(reader["addressid"]);
                orderInfo.telephoneId = Convert.ToInt32(reader["telephoneid"]);

                orderInfos.Add(orderInfo);
            }
            return orderInfos;
        }
    }
}
