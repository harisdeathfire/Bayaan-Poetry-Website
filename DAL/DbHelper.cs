using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DBHelper
    {
        public static SqlConnection GetConnection() 
        {
            return new SqlConnection("Data Source=ACER-NITRO5\\SQLEXPRESS;Initial Catalog=UrduDB;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}