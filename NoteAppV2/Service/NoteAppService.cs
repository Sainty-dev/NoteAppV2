using NoteAppV2.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NoteAppV2.Service
{
    public class NoteAppService : INoteAppService
    {

        readonly SQLiteAsyncConnection _database;

        //constructor
        public NoteAppService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NoteSQLite.db");
            _database = new SQLiteAsyncConnection(dbPath);

            //create db
            _database.CreateTableAsync<NoteItem>().Wait();

        }

        public Task DeleteItemAsync(NoteItem item)
        {
            return _database.DeleteAsync(item);
        }

        public Task<List<NoteItem>> GetItemAsync()
        {
            return _database.Table<NoteItem>().ToListAsync();
        }

        public Task InsertItemAsync(NoteItem item)
        {
            return _database.InsertAsync(item);
        }

        public Task UpdateItemAsync(NoteItem item)
        {
            return _database.UpdateAsync(item);
        }
    }
}
