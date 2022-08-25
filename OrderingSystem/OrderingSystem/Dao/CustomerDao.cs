using OrderingSystem.Models;
using System.Data.SqlClient;

namespace OrderingSystem.Dao
{
    public class CustomerDao : Connect
    {
        /// <summary>
        /// 根据 customerName 查询 customer
        /// </summary>
        /// <param name="customerName"></param>
        /// <returns></returns>
        public Customer QueryCustomerByName(string customerName)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from customer where name='{customerName}'";
            Customer customer = Query<Customer>(sql, GetCustomer);
            return customer;
        }

        /// <summary>
        /// 根据 customerId 查询 customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public Customer QueryCustomerById(int customerId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from customer where id={customerId}";
            Customer customer = Query<Customer>(sql, GetCustomer);
            return customer;
        }

        public List<Customer> QueryAllCustomer()
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from customer";
            List<Customer> customers = Query<List<Customer>>(sql, GetAllCustomer);
            return customers;
        }


        /// <summary>
        /// 作为回调函数，根据获得的 SqlDataReader 对象 构造 Customer 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Customer GetCustomer(SqlDataReader reader)
        {
            //解析数据
            Customer customer = null;
            if (reader != null && reader.Read())
            {
                customer = new Customer();
                customer.name = Convert.ToString(reader["name"]);
                customer.id = Convert.ToInt32(reader["id"]);
                customer.status = Convert.ToBoolean(reader["status"]);
                customer.password = Convert.ToString(reader["password"]);
            }
            return customer;
        }

        /// <summary>
        /// 获取 所有 customer
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<Customer> GetAllCustomer(SqlDataReader reader)
        {
            //解析数据
            List<Customer> customers = new List<Customer>();
            if (reader != null && reader.Read())
            {
                Customer customer = new Customer();
                customer.name = Convert.ToString(reader["name"]);
                customer.id = Convert.ToInt32(reader["id"]);
                customer.status = Convert.ToBoolean(reader["status"]);
                customer.password = Convert.ToString(reader["password"]);
                customers.Add(customer);
            }
            return customers;
        }

        /// <summary>
        /// 创建新 customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool CreateCustomer(Customer customer)
        {
            //拼装执行 sql
            string sql = $"insert into customer values('{customer.name}','{customer.password}','{customer.status}');";
            return CreateOrDeleteOrUpdate(sql);
        }
    }
}
