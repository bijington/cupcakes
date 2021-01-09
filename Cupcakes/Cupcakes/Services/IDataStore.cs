using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cupcakes.Services
{
    public interface IDataStore<T>
    {
        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<List<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
