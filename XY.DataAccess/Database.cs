using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YunChengLK.Framework.Data;
using YunChengLK.Framework.Data.Configuration;

namespace XY.DataAccess
{
    public static class Database
    {
        public static SqlServer ApplicationDB
        {
            get
            {
                return new SqlServer(ConnectionConfig.Connections["ApplicationDB_ConnectionString"]);
            }
        }
        public static SqlServer Test
        {
            get
            {
                return new SqlServer(ConnectionConfig.Connections["Test"]);
            }
        }
    }
}
