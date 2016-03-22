using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FrameworksProjekt
{
    public class AutoFac<T> where T : new()
    {
        private string table;
        private Mapper<T> mapper = new Mapper<T>();

        public AutoFac()
        {
            table = typeof(T).Name;
        }

        public T Get(int ID)
        {
            using (var cmd = new SQLiteCommand("SELECT * FROM " + table + " WHERE ID=@ID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@ID", ID);

                var r = cmd.ExecuteReader();
                T type = new T();

                if(r.Read())
                {
                    type = mapper.Map(r);
                }

                r.Close();
                cmd.Connection.Close();
                return type;
            }
        }

        public List<T> GetAll()
        {
            using (var cmd = new SQLiteCommand("SELECT * FROM " + table, Conn.CreateConnection()))
            {
                List<T> list = mapper.MapList(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }
        }

        public List<T> GetBy(string field, string value)
        {
            using (var cmd = new SQLiteCommand("SELECT * FROM " + table + " WHERE " + field + " =@KID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@KID", value);

                List<T> list = mapper.MapList(cmd.ExecuteReader());
                cmd.Connection.Close();
                return list;
            }
        }

        public void Delete(int ID)
        {
            using (var cmd = new SQLiteCommand("DELETE FROM " + table + " WHERE ID=@ID", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("ID", ID);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public void DeleteBy(string field, int value)
        {
            using (var cmd = new SQLiteCommand("DELETE FROM " + table + " WHERE " + field + "=@value", Conn.CreateConnection()))
            {
                cmd.Parameters.AddWithValue("@value", value);
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public void Insert(T pro)
        {
            string parms = "";
            string fields = "";

            var mappings = mapper.CreateMap();

            foreach(var map in mappings)
            {
                if(map.Key.ToLower() != "id")
                {
                    fields += map.Value + ", ";
                    parms += "@" + map.Key + ", ";
                } 
            }

            fields = fields.Substring(0, fields.Length - 2);
            parms = parms.Substring(0, parms.Length - 2);

            using (var cmd = new SQLiteCommand("INSERT INTO " + table + " (" + fields + ") VALUES (" + parms + ")", Conn.CreateConnection()))
            {
                foreach(var prop in mappings)
                {
                    if(prop.Key.ToLower() != "id")
                    {
                        cmd.Parameters.AddWithValue(prop.Key, pro.GetType().GetProperty(prop.Key).GetValue(pro, null));
                    }
                }

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }

        public void Update(T pro)
        {
            string fAndP = "";
            var mappings = mapper.CreateMap();

            foreach(var map in mappings)
            {
                if(map.Key.ToLower() != "id")
                {
                    fAndP += map.Value + "=@" + map.Key + ",";
                }
            }

            fAndP = fAndP.Substring(0, fAndP.Length - 2);

            using (var cmd = new SQLiteCommand("UPDATE " + table + " SET " + fAndP + " WHERE ID=@ID", Conn.CreateConnection()))
            {
                foreach(var prop in mappings)
                {
                    cmd.Parameters.AddWithValue(prop.Key, pro.GetType().GetProperty(prop.Key).GetValue(pro, null));
                }

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
            }
        }
    }
}
