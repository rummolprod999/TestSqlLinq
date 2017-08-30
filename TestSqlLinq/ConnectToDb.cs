using System;
using MySql.Data.MySqlClient;
namespace TestSqlLinq
{
    public class ConnectToDb
    {
        public static MySqlConnection GetDbConnection()
        {
            // Connection String.
            String connString =
                $"Server=localhost;port=3306;Database=tender2;User Id=root;password=1234;CharSet=utf8;Convert Zero Datetime=True;default command timeout=3600;Connection Timeout=3600";

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}