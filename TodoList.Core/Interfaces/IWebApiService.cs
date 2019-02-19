using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface IWebApiService
    {
        Task<List<Goal>> RefreshDataAsync();
        Task InsertOrUpdateDataAsync(Goal goal);
        Task DeleteDataAsync(int id);
    }
}