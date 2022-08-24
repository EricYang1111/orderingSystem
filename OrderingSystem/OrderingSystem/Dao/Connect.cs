using OrderingSystem.Models;
using System.Data.SqlClient;

namespace OrderingSystem.Dao
{
    public abstract class Connect
    {
        protected SqlConnection connect = null;
        private string connStr = "Data Source=.;Initial Catalog=OrderingSystem;Integrated Security=True";


        /// <summary>
        /// 没有返回数据的数据库操作
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected bool CreateOrDeleteOrUpdate(string sql)
        {
            //创建连接
            CreateConnect();

            int count = 0;
            try
            {
                //执行sql
                SqlCommand cmd = new SqlCommand(sql, this.connect);
                count = cmd.ExecuteNonQuery();
            }
            catch
            {
                return false;
            }
            finally
            {
                //关闭连接
                this.connect.Close();
            }

            //返回执行结果
            return count != 0;
        }


        /// <summary>
        /// 查询操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        protected T Query<T>(string sql, Func<SqlDataReader, T> func)
        {
            //创建连接
            CreateConnect();

            T t = default(T);
            try
            {
                //执行sql
                SqlCommand cmd = new SqlCommand(sql, this.connect);
                SqlDataReader dataReader = cmd.ExecuteReader();
                t = func(dataReader);
            }
            catch
            {
                return t;
            }
            finally
            {
                //关闭连接
                this.connect.Close();
            }
            //返回执行结果
            return t;
        }


        /// <summary>
        /// 创建 数据库连接
        /// </summary>
        private void CreateConnect()
        {
            this.connect = new SqlConnection(this.connStr);
            this.connect.Open();
        }
    }
}
