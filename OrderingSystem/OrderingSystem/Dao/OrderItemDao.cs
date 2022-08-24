using OrderingSystem.Models;

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
    }
}
