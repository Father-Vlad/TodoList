using System;
using System.Threading.Tasks;
using TodoList.Core.Models;

namespace TodoList.Core.Interfaces
{
    public interface IWebApiService
    {
        Task<bool> RefreshDataAsync();
        Task InsertOrUpdateDataAsync(Goal goal);
        Task DeleteDataAsync(int id);
        Action OnRefreshDoneGoalsHandler { get; set; }
        Action OnRefreshNotDoneGoalsHandler { get; set; }
    }
}