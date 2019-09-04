using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace DataAccess.Library.Interfaces
{
    public interface IDataAccess<T>
    {
        SQLiteConnection Connection { get; }

        DataSet GetDataSet(string sql);

        SQLiteDataReader Reader(string query);

        DataTable GetDataTable(string sql, int table);

        int ExecuteSQL(string sql);
        int Rows_Affect_Execute(string sql);

        T GetInsatnce { get;}
    }
}
