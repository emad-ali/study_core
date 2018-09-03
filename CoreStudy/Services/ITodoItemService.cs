using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreStudy.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetIncompleteItemAsync();
        Task<bool> AddItemAsync(TodoItem item);
        Task<bool> MarkDoneAsync(Guid id);
    }
}
