using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace FrameworksProjekt.Database
{
    class DatabaseMan
    {
        string sql;
        SQLiteConnection con;

        public DatabaseMan()
        {

        }

        // Create database if none exists
        public void SetUp()
        {
            // Minion - Category - Item - Playerinventory
            string[] tableSql = new string[] {
                "CREATE TABLE Minion (ID INTEGER PRIMARY KEY, Speed FLOAT , Strength FLOAT, currentLevelName VARCHAR, targetLevelName VARCHAR, X FLOAT, Y FLOAT, imagePath VARCHAR)",
                "CREATE TABLE Category (ID INTEGER PRIMARY KEY, Name STRING)",
                "CREATE TABLE Item (ID INTEGER PRIMARY KEY, Name STRING, Category INTEGER, FOREIGN KEY(CATEGORY) REFERENCES Category(ID))",
                "CREATE TABLE Inventory (ID INTEGER PRIMARY KEY, Name STRING , Item INTEGER, Count INTEGER, FOREIGN KEY(Item) REFERENCES Item(ID))"
            };

            // items by category
            string[][] items = new string[5][];

            items[0] = new string[]{ "Banana", "Apple", "Pear", "Dragon Fruit", "Grapes" };
            items[1] = new string[] { "McKing", "Pizza Nut", "Sundown Avenue", "CFL", "Boritto Clock" };
            items[2] = new string[] { "Arrmani Sunglasses", "DerpBerry Coat", "Pirada Shoes", "BENZIN Jeans", "LaCostALot Polo" };
            items[3] = new string[] { "Couch", "Chairs", "Desks", "Tables", "Closets" };
            items[4] = new string[] { "Aye-Pad", "yPhone", "HPus Laptops", "B'n'No Stereo", "SamSony TV" };

            if (!System.IO.File.Exists("main.db"))
            {
                SQLiteConnection.CreateFile("main.db");

                using(var conn = new SQLiteConnection(Conn.CreateConnection()))
                {
                    for(int i = 0; i < tableSql.Length; i++)
                    {
                        string sql = tableSql[i];

                        ExecuteCommand(sql, conn);
                    }

                    for(int i = 0; i < items.Length; i++)
                    {
                        string[] categoryItems = items[i];

                        for(int t = 0; t < categoryItems.Length; t++)
                        {
                            string sql = "INSERT INTO Item (ID, Name, Category) VALUES (NULL, \"" + categoryItems[t] + "\", " + i + ")";

                            ExecuteCommand(sql, conn);
                        }
                    }
                } 
            }
            else
            {
                GameWorld.Instance.ShouldLoad = true;
            }
        }

        public void ExecuteCommand(string sql, SQLiteConnection conn)
        {
            SQLiteCommand command = new SQLiteCommand(sql, conn);
            command.ExecuteNonQuery();
        }

        public void RecreateMinionTable()
        {
            using (var conn = new SQLiteConnection(Conn.CreateConnection()))
            {
                string sql = "DROP TABLE Minion";

                ExecuteCommand(sql, conn);

                sql = "CREATE TABLE Minion (ID INTEGER PRIMARY KEY, Speed FLOAT , Strength FLOAT, currentLevelName VARCHAR, targetLevelName VARCHAR, X FLOAT, Y FLOAT, imagePath VARCHAR)";

                ExecuteCommand(sql, conn);
            }
        }

        public void RecreateInventoryTable()
        {
            using (var conn = new SQLiteConnection(Conn.CreateConnection()))
            {
                string sql = "DROP TABLE Inventory";

                ExecuteCommand(sql, conn);

                sql = "CREATE TABLE Inventory (ID INTEGER PRIMARY KEY, Name STRING, Item INTEGER, Count INTEGER, FOREIGN KEY(Item) REFERENCES Item(ID))";

                ExecuteCommand(sql, conn);
            }
        }
    }
}
