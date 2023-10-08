using ContactBookAPI.Model.ViewModel;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ContactBookAPI.Core.Interfaces
{
    public interface IAuthRepository
    {
        Task<bool> RegisterUserAsync(RegisterViewModel model, ModelStateDictionary modelState, string role);
        Task<string> LoginAsync(LoginViewModel model);
    }
}
