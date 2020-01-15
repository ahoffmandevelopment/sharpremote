using SharpRemote.Sqlite.Extensions;
using SharpRemote.Sqlite.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SharpRemote.Sqlite
{
    public class RemoteDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath(basePath), Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;
        static string basePath;

        public RemoteDatabase(string basePath)
        {
            if (string.IsNullOrWhiteSpace(basePath))
            {
                throw new ArgumentException("The base path cannot be null, empty, or whitespace.", nameof(basePath));
            }

            RemoteDatabase.basePath = basePath;

            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!doesTableExist<RemoteButton>())
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(RemoteButton)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public async Task<int> ClearTableAsync<T>() where T: BaseSqliteItem, new()
        {
            return await Database.DeleteAllAsync<T>();
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : BaseSqliteItem, new()
        {
            return await Database.Table<T>().ToListAsync();
        }

        public async Task<int> SaveItemAsync<T>(T item) where T : BaseSqliteItem
        {
            return item.ID == 0
                ? await Database.InsertAsync(item)
                : await Database.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T: BaseSqliteItem
        {
            return await Database.DeleteAsync(item);
        }

        bool doesTableExist<T>()
        {
            return Database.TableMappings.Any(m => m.MappedType.Name == typeof(T).Name);
        }
    }
}
