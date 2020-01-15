using SQLite;

namespace SharpRemote.Sqlite.Models
{
    public class BaseSqliteItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
