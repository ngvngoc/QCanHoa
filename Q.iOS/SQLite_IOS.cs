using System;
using System.IO;
using Q.iOS;
using Q.Services;
using SQLite;
using Xamarin.Forms;
[assembly: Dependency(typeof(SQLite_IOS))]
namespace Q.iOS
{
    public class SQLite_IOS:ISQLite
    {
        public SQLiteConnection GetConnection()
        {

                var sqliteFilename = "QCDB.db3";
                string personalFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string libraryFolder = Path.Combine(personalFolder, "..", "Library");
                var path = Path.Combine(libraryFolder, sqliteFilename);
                return new SQLiteConnection(path);

        }
    }
}
