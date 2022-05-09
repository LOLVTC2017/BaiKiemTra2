using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace MainFuncitonClass
{
    public class ConnectionDataBase
    {
        public  SqlConnection connection ()
        {
            SqlConnection DataBase = new SqlConnection("Data Source=phu;Initial Catalog=QL_SVCSDL;Integrated Security=True");
            return DataBase;
        }
    }
}
