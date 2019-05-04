using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BL.Tables
{
    public class GeneralTable
    {
        private static DAL.ConnectToAccess connectAccess;

        private DataTable table;


        // static ctor
        static GeneralTable()
        {
            connectAccess = new DAL.ConnectToAccess();
        }


        public GeneralTable(string tableName)
        {
            if (table != null)
                table = connectAccess.GetTable(tableName);


        }
    }
}
