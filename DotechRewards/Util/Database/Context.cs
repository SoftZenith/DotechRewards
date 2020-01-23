using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace DotechRewards.Util.Database
{
    public class Context
    {
        public static SqlConnection Connect(string connName = "DBConnection") {
            string connStr = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
            return new SqlConnection(connStr);
        }

    }
}