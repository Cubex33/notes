using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notes.Models
{
    public class DatabaseProvider
    {
        public static void Init(string ConnectionString)
        {
            Connection = new SQLiteConnection(ConnectionString);
        }

        public static SQLiteConnection Connection { get; set; }
    }
}
