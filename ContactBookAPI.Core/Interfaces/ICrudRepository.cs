using ContactBookAPI.Model;
using ContactBookAPI.Model.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContactBookAPI.Core.Interfaces
{
    public interface ICrudRepository
    {
        Task<bool> CreateNewUserAsync(PostNewUserViewModel model, ModelStateDictionary modelState);
        Task<bool> UpdateUserAsync(string userId, PutViewModel model);
        Task<PaginatedViewModel> GetAllUserAsync(int page, int pagesize);
        Task<bool> DeleteUserAsync(string userId);
        Task<User> GetUserByidAsync(string userId);
        Task<User> GetUserByEmailAsync(string email);
    }
}
