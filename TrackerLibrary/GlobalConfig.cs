﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.DataAccess;

namespace TrackerLibrary
{
    public static class GlobalConfig
    {
        public static List<IDataConnection> Connections { get; private set; } = new List<IDataConnection>();
        public static IDataConnection Connection { get; private set; }

        public static void InitializeConnections(bool database, bool textFiles)
        {
            if (database)
            {
                var sql = new SqlConnector();
                Connections.Add(sql);
            }

            if (textFiles)
            {
                var text = new TextConnector();
                Connections.Add(text);
            }
        }

        public static void InitializeConnection(DatabaseType dbType)
        {
            if (dbType == DatabaseType.Sql)
            {
                Connection = new SqlConnector();
            }

            if (dbType == DatabaseType.Text)
            {
                Connection = new TextConnector();
            }
        }

        public static string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
