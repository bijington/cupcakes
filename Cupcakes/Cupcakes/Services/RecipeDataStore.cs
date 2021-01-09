using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Cupcakes.Models;

namespace Cupcakes.Services
{
    public class RecipeDataStore : IDataStore<Recipe>
    {
        //readonly IList<Recipe> recipes;
        private readonly SQLite.SQLiteAsyncConnection connection;

        public RecipeDataStore()
        {
            //recipes = new List<Recipe>
            //{
            //    new Recipe { Name = "Chocolate Reindeer", Description = "A Festive treat" },
            //    new Recipe { Name = "Oreo", Description = "" }
            //};

            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Cupcakes.db");
            connection = new SQLite.SQLiteAsyncConnection(databasePath);

            Task.Run(async () => await InitialiseAsync()).GetAwaiter().GetResult();
        }

        public Task InitialiseAsync()
        {
            return this.connection.CreateTableAsync<Recipe>();
        }

        public Task<int> AddItemAsync(Recipe item)
        {
            return this.connection.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(int id)
        {
            return this.connection.DeleteAsync<Recipe>(id);
        }

        public Task<Recipe> GetItemAsync(int id)
        {
            return this.connection.FindAsync<Recipe>(id);
        }

        public Task<List<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return this.connection.Table<Recipe>().ToListAsync();
        }

        public Task<int> UpdateItemAsync(Recipe item)
        {
            return this.connection.UpdateAsync(item);
        }
    }
}
