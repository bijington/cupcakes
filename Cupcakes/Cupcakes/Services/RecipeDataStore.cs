using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cupcakes.Models;

namespace Cupcakes.Services
{
    public class RecipeDataStore : IDataStore<Recipe>
    {
        readonly IList<Recipe> recipes;

        public RecipeDataStore()
        {
            recipes = new List<Recipe>
            {
                new Recipe { Name = "Chocolate Reindeer", Description = "A Festive treat" },
                new Recipe { Name = "Oreo", Description = "" }
            };
        }

        public Task<bool> AddItemAsync(Recipe item)
        {
            recipes.Add(item);

            return Task.FromResult(true);
        }

        public Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = recipes.Where((Recipe arg) => arg.Id == id).FirstOrDefault();
            recipes.Remove(oldItem);

            return Task.FromResult(true);
        }

        public Task<Recipe> GetItemAsync(int id)
        {
            return Task.FromResult(recipes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Recipe>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(recipes);
        }

        public Task<bool> UpdateItemAsync(Recipe item)
        {
            var oldItem = recipes.FirstOrDefault(arg => arg.Id == item.Id);
            recipes.Remove(oldItem);
            recipes.Add(item);

            return Task.FromResult(true);
        }
    }
}
