using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using ScanDni.Models;
using SQLite;

namespace ScanDni
{
    public  class Database
    {
        private readonly SQLiteAsyncConnection database;

        public Database(string path) 
        {
            database = new SQLiteAsyncConnection(path);
            database.CreateTableAsync<Item>();
        }

        public Task<List<Item>> GetAllDniAsync()
        {
            return database.Table<Item>().ToListAsync();
        }

        public Task<int> SaveDniAsync(Item dni)
        {
            return database.InsertAsync(dni);
        }

        public Task<int> UpdateDniAsync(Item dni)
        {
            return database.UpdateAsync(dni);
        }

        public Task<int> DeleteDniAsync(Item dni)
        {
            return database.DeleteAsync(dni);
        }

        public Item ExistsDniAsync(Item dni)
        {
            return database.Table<Item>().FirstOrDefaultAsync(i => i.Numero == dni.Numero).Result;
        }
    }
}
