using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.DLL
{
    public class BaseRepository
    {
        public SqlConnection Conn;
        public SqlTransaction trans;
        /// <summary>
        /// Connections
        /// </summary>
        public void connection()
        {
            Conn = new SqlConnection(Util.AppSetting.Get("dbstring"));
        }
    }
}
