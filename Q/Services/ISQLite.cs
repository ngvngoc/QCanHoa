using System;
using SQLite;

namespace Q.Services
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }


}
