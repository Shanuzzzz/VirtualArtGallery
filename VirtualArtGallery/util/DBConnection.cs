using System;
using System.Data.SqlClient;
using System.IO;

namespace VirtualArtGallery.util
{
    public class DBConnection
    {
        private static SqlConnection connection;

        public static SqlConnection GetConnection()
        {
            if (connection == null)
            {
                var props = PropertyUtil.LoadProperties("dbconfig.properties");
                string server = props["server"];
                string database = props["database"];
                string trusted = props["trusted_connection"];

                string connectionString = $"Server={server};Database={database};Trusted_Connection={trusted};";

                connection = new SqlConnection(connectionString);
            }
            return connection;
        }
    }
}
