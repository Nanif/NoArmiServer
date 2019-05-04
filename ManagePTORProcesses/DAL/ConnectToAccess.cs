using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DAL
{
    public class ConnectToAccess
    {
        private DataSet dataset;
        private OleDbConnection connection;

        Properties.Settings settings = new Properties.Settings();

        public ConnectToAccess()
        {
            dataset = new DataSet();
            if (File.Exists(settings.DBPath))
            {
                connection = new OleDbConnection(settings.DBPath);
            }
            else
                throw new Exception("מסד הנתונים לא נמצא בנתיב שצוין");
        }

        private void AddTable(string tableName)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from " + tableName, connection);
            adapter.Fill(dataset, tableName);

        }

        public DataTable GetTable(string tableName)
        {
            if (!dataset.Tables.Contains(tableName))
                AddTable(tableName);
            return dataset.Tables[tableName];
        }


        public void SaveTable(string tableName)
        {
            OleDbDataAdapter adapter = new OleDbDataAdapter("select * from " + tableName, connection);
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.Update(dataset, tableName);
        }

        public void SaveAllTables()
        {
            foreach (DataTable item in dataset.Tables)
            {
                UpdateTable(item.TableName);
            }
        }

    }
}
