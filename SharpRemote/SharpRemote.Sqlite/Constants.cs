using System.IO;

namespace SharpRemote.Sqlite
{
    public static class Constants
    {
        public const string DatabaseFileName = "RemoteDatabase.db3";

        public const SQLite.SQLiteOpenFlags Flags = 
            SQLite.SQLiteOpenFlags.ReadWrite | 
            SQLite.SQLiteOpenFlags.Create | 
            SQLite.SQLiteOpenFlags.SharedCache |
            SQLite.SQLiteOpenFlags.NoMutex | 
            SQLite.SQLiteOpenFlags.ProtectionNone;

        public static string DatabasePath(string basePath) => Path.Combine(basePath, DatabaseFileName);
    }
}
