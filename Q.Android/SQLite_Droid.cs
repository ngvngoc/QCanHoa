using System;
using System.IO;
using Q.Services;
using SQLite;
using Xamarin.Forms;
using Q.Droid;

[assembly: Dependency(typeof(SQLite_Droid))]
namespace Q.Droid
{
    public class SQLite_Droid:ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var Dbname = "QCDB.db3";
            var Dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(Dbpath, Dbname);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}
