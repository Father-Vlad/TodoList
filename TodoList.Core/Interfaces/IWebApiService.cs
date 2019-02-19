using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface IWebApiService
    {
        Task<List<Goal>> RefreshDataAsync(Action<List<Goal>> OnRefreshGoalsCompleted = null);
        Task InsertOrUpdateDataAsync(Goal goal);
        Task DeleteDataAsync(int id);
    }
}