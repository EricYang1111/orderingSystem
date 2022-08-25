using OrderingSystem.Models;
using System.Data.SqlClient;

namespace OrderingSystem.Dao
{
    public class MenuDao : Connect
    {
        /// <summary>
        /// 创建 menu
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public bool CreateMenu(Menu menu)
        {
            //拼装执行 sql
            string sql = $"insert into menu values('{menu.name}',{menu.price});";
            return CreateOrDeleteOrUpdate(sql);
        }

        /// <summary>
        /// 根据 menuName 查询 menu
        /// </summary>
        /// <param name="menuName"></param>
        /// <returns></returns>
        public Menu QueryMenuByName(string menuName)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from menu where name='{menuName}';";
            Menu menu = Query<Menu>(sql, GetMenu);
            return menu;
        }

        /// <summary>
        /// 根据 menuId 查询 menu
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public Menu QueryMenuById(int menuId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from menu where id={menuId};";
            Menu menu = Query<Menu>(sql, GetMenu);
            return menu;
        }

        /// <summary>
        /// 查询所有 menu
        /// </summary>
        /// <returns></returns>
        public List<Menu> QueryMenus()
        {
            //拼装sql，执行sql获得数据
            string sql = $"select * from menu;";
            List<Menu> menus = Query<List<Menu>>(sql, GetMenus);
            return menus;
        }

        /// <summary>
        /// 用作回调函数，执行查询操作时的 构建对象(查询单个 menu)
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private Menu GetMenu(SqlDataReader reader)
        {
            //解析数据
            Menu menu = null;
            if (reader != null && reader.Read())
            {
                menu = new Menu();
                menu.name = Convert.ToString(reader["name"]);
                menu.menuId = Convert.ToInt32(reader["id"]);
                menu.price = Convert.ToDouble(reader["price"]);
            }
            return menu;
        }

        /// <summary>
        /// 用作回调函数，执行查询操作时的 构建对象(查询所有 menu)
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private List<Menu> GetMenus(SqlDataReader reader)
        {
            //解析数据
            List<Menu> menus = new List<Menu>();
            while (reader != null && reader.Read())
            {
                Menu menu = new Menu();
                menu.name = Convert.ToString(reader["name"]);
                menu.menuId = Convert.ToInt32(reader["id"]);
                menu.price = Convert.ToDouble(reader["price"]);
                menus.Add(menu);
            }
            return menus;
        }

        /// <summary>
        /// 删除 menu
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool DeleteMenu(int menuId)
        {
            //拼装sql，执行sql获得数据
            string sql = $"delete from menu where id={menuId};";
            return CreateOrDeleteOrUpdate(sql);
        }
    }
}
