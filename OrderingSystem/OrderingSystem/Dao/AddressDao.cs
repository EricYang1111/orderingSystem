using OrderingSystem.Models;
using System.Data.SqlClient;

namespace OrderingSystem.Dao
{
    public class AddressDao : Connect
    {
        /// <summary>
        /// 创建 address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool CreateAddress(Address address)
        {
            //拼装执行 sql
            string sql = $"insert into address values({address.cuid},'{address.address}');";
            return CreateOrDeleteOrUpdate(sql);
        }

        /// <summary>
        /// 查找 addresses
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<Address> QueryAddresses(int customerId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from address where cuid={customerId}";
            return Query<List<Address>>(sql, GetAddress);
        }

        /// <summary>
        /// 使用 address 查找 address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Address QueryAddressByAddress(string address)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from address where address='{address}';";
            return Query<Address>(sql, GetOneAddress);
        }

        /// <summary>
        /// 使用 addressId 查找 address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public Address QueryAddressByAddressId(int addressId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from address where id='{addressId}';";
            return Query<Address>(sql, GetOneAddress);
        }

        /// <summary>
        /// 作为回调函数，根据获得的 SqlDataReader 对象 构造 Address 对象(可能有多个对象)
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<Address> GetAddress(SqlDataReader reader)
        {
            //解析数据
            List<Address> addresses = new List<Address>();
            while (reader != null && reader.Read())
            {
                Address address = new Address();
                address.address = Convert.ToString(reader["address"]);
                address.id = Convert.ToInt32(reader["id"]);
                address.cuid = Convert.ToInt32(reader["cuid"]);
                addresses.Add(address);
            }
            return addresses;
        }

        /// <summary>
        /// 作为回调函数，根据获得的 SqlDataReader 对象 构造 Address 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Address GetOneAddress(SqlDataReader reader)
        {
            //解析数据
            Address address = null;
            if (reader != null && reader.Read())
            {
                address = new Address();
                address.address = Convert.ToString(reader["address"]);
                address.id = Convert.ToInt32(reader["id"]);
                address.cuid = Convert.ToInt32(reader["cuid"]);
            }
            return address;
        }
    }
}
