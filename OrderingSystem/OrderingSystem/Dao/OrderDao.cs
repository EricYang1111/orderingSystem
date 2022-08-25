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

        /// <summary>
        /// 查找某个 order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderToClient QureyOrder(int orderId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from orderinfo where id={orderId}";
            OrderInfo orderInfo = Query<OrderInfo>(sql, GetOrderInfo);

            return orderInfo.ToOrderToClient();
        }

        /// <summary>
        /// 删除 order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool DeleteOrder(int orderId)
        {
            //先删除 orderitem
            OrderItemDao orderItemDao = new OrderItemDao();
            orderItemDao.DeleteItems(orderId);

            //再删除 orderinfo
            string sql = $"delete from orderinfo where id={orderId};";
            return CreateOrDeleteOrUpdate(sql);
        }

        /// <summary>
        /// 更改 order 状态 status
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool UpdateStatus(int orderId)
        {
            //先获取 order 的 status 属性
            // 0:创建了订单，还没开始做
            // 1:正在做
            // 2:已上菜
            // 3:已完成订单
            int status = new OrderDao().QureyOrder(orderId).status;
            if(status >= 3)
            {
                return false;
            }

            //更改数据
            string sql = $"update orderinfo set status={status+1} where id={orderId};";
            return CreateOrDeleteOrUpdate(sql);
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

        /// <summary>
        /// 作为回调函数，根据获得的 SqlDataReader 对象 构造 OrderInfo 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private OrderInfo GetOrderInfo(SqlDataReader reader)
        {
            //解析数据
            OrderInfo orderInfo = null;
            while (reader != null && reader.Read())
            {
                orderInfo = new OrderInfo();
                orderInfo.totalPrice = Convert.ToDouble(reader["totalprice"]);
                orderInfo.id = Convert.ToInt32(reader["id"]);
                orderInfo.cuid = Convert.ToInt32(reader["cuid"]);
                orderInfo.date = Convert.ToDateTime(reader["date"]);
                orderInfo.status = Convert.ToInt32(reader["status"]);
                orderInfo.addressId = Convert.ToInt32(reader["addressid"]);
                orderInfo.telephoneId = Convert.ToInt32(reader["telephoneid"]);
            }
            return orderInfo;
        }
    }
}
