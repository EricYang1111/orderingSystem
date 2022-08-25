using OrderingSystem.Models;
using System.Data.SqlClient;

namespace OrderingSystem.Dao
{
    public class OrderItemDao : Connect
    {
        /// <summary>
        /// 创建 order 的 orderitem
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        public bool CreateOrderItems(int orderId, List<OrderItem> orderItems)
        {
            int count = orderItems.Count;
            foreach(OrderItem item in orderItems)
            {
                //拼装执行 sql
                string sql = $"insert into orderitem values({orderId},{item.menuId}," +
                    $"{item.quantity},{item.price});";
                if(CreateOrDeleteOrUpdate(sql))
                {
                    count--;
                }
            }
            return count == 0;
        }


        /// <summary>
        /// 根据 orderId 查询 orderItem
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderItem> QueryOrderItems(int orderId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from orderitem where orderid={orderId};";
            List<OrderItem> orderItems = Query<List<OrderItem>>(sql, GetOrderItems);
            return orderItems;
        }

        /// <summary>
        /// 删除订单的菜品 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool DeleteItems(int orderId)
        {
            string sql = $"delete from orderitem where orderid={orderId};";
            return CreateOrDeleteOrUpdate(sql);
        }

        /// <summary>
        /// 回调函数使用，用来构建从数据库获得的数据对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<OrderItem> GetOrderItems(SqlDataReader reader)
        {
            //解析数据
            List<OrderItem> orderItems = new List<OrderItem>();
            while (reader != null && reader.Read())
            {
                OrderItem orderItem = new OrderItem();
                orderItem.id = Convert.ToInt32(reader["id"]);
                orderItem.orderId = Convert.ToInt32(reader["orderid"]);
                orderItem.menuId = Convert.ToInt32(reader["menuid"]);
                orderItem.name = new MenuDao().QueryMenuById(orderItem.menuId).name;
                orderItem.quantity = Convert.ToInt32(reader["quantity"]);
                orderItem.price = Convert.ToInt32(reader["price"]);

                orderItems.Add(orderItem);
            }
            return orderItems;
        }
    }
}
