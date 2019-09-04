using DataAccess.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace DataAccess.Library
{
    public enum providers
    {
        SQLite = 0,
        MSSQL = 1,
        MySQL = 2,
        Oracle = 3
    }
    public class DataAccess: IDataAccess<DataAccess>
    {
        private string _ConnectionString = null;

        private static SQLiteConnection liteConnection = null;
        public static SQLiteDataReader reader = null;
        public static SQLiteDataAdapter adapter = null;
        public static SQLiteCommand command = null;
        public DataAccess(string connectionString)
        {
            //var provider = Enum.GetName(typeof(providers), Selection);
            this._ConnectionString = connectionString;
        }

        public SQLiteConnection Connection
        {
            get
            {
                if (liteConnection == null)
                {
                    liteConnection = new SQLiteConnection(_ConnectionString);

                    liteConnection.Open();
                    return liteConnection;
                }
                else if (liteConnection.State == System.Data.ConnectionState.Closed)
                {
                    liteConnection.Open();
                    return liteConnection;
                }
                else
                {
                    return liteConnection;
                }
            }
        }

        public DataAccess GetInsatnce => this;

        public DataSet GetDataSet(string sql)
        {
            command = new SQLiteCommand(sql, Connection);
            adapter = new SQLiteDataAdapter(command);

            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            Connection.Close();

            return dataSet;
        }
        public SQLiteDataReader Reader(string query)
        {
            try
            {
                command = new SQLiteCommand(query, Connection);
                reader = command.ExecuteReader();
            }
            catch (Exception)
            {
                Debug.WriteLine("--Error Exceuting Reader--", "Data Access Library");
            }
            return reader;
        }

        public DataTable GetDataTable(string sql,int table)
        {
            Console.WriteLine(sql);
            DataSet ds = GetDataSet(sql);

            if (ds.Tables.Count >= table)
                return ds.Tables[table];
            return null;
        }

        public  int ExecuteSQL(string sql)
        {
            command = new  SQLiteCommand(sql, Connection);
            return command.ExecuteNonQuery();
        }
        public int Rows_Affect_Execute(string sql)
        {
             command = new  SQLiteCommand(sql, Connection);
            return Convert.ToInt32(command.ExecuteScalar());
        }
    }

}
