using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FrameworksProjekt
{
    public static class Conn
    {
        private static string connectionString = "Data Source=DatabaseMagi.db; Version=3";
        private static SQLiteConnection con = new SQLiteConnection(connectionString);

        public static SQLiteConnection CreateConnection()
        {
            var _con = con;
            _con.Open();
            return _con;
        }
    }
}
