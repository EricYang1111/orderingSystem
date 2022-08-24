using OrderingSystem.Models;
using System.Data.SqlClient;

namespace OrderingSystem.Dao
{
    public class TelephoneDao : Connect
    {
        /// <summary>
        /// 创建 telephone
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public bool CreateTelephone(Telephone telephone)
        {
            //拼装执行 sql
            string sql = $"insert into telephone values({telephone.cuid},'{telephone.telephone}');";
            return CreateOrDeleteOrUpdate(sql);
        }

        /// <summary>
        /// 查找 telephone
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<Telephone> QueryTelephone(int customerId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from telephone where cuid={customerId}";
            return Query<List<Telephone>>(sql, GetTelephone);
        }


        /// <summary>
        /// 根据 telephone 查找 telephone
        /// </summary>
        /// <param name="telephone"></param>
        /// <returns></returns>
        public Telephone QueryTelephoneByTelephone(string telephone)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from telephone where telephone='{telephone}'";
            return Query<Telephone>(sql, GetOneTelephone);
        }

        /// <summary>
        /// 作为回调函数，根据获得的 SqlDataReader 对象 构造 Telephone 对象(可能有多个对象)
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<Telephone> GetTelephone(SqlDataReader reader)
        {
            //解析数据
            List<Telephone> telephones = new List<Telephone>();
            while (reader != null && reader.Read())
            {
                Telephone telephone = new Telephone();
                telephone.telephone = Convert.ToString(reader["telephone"]);
                telephone.id = Convert.ToInt32(reader["id"]);
                telephone.cuid = Convert.ToInt32(reader["cuid"]);
                telephones.Add(telephone);
            }
            return telephones;
        }

        /// <summary>
        /// 作为回调函数，根据获得的 SqlDataReader 对象 构造 Telephone 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Telephone GetOneTelephone(SqlDataReader reader)
        {
            //解析数据
            Telephone telephone = null;
            while (reader != null && reader.Read())
            {
                telephone = new Telephone();
                telephone.telephone = Convert.ToString(reader["telephone"]);
                telephone.id = Convert.ToInt32(reader["id"]);
                telephone.cuid = Convert.ToInt32(reader["cuid"]);
            }
            return telephone;
        }
    }
}
