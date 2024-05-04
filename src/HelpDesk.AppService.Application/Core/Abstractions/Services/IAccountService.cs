using System.Threading.Tasks;
using HelpDesk.AppService.Application.Core.Abstractions.ExternalService.Models;

namespace HelpDesk.AppService.Application.Core.Abstractions.Services
{
    public interface IAccountService
    {
        Task<(bool IsSuccess, ErrorModel[] Errors)> PasswordSignInAsync(string username, string password, bool rememberMe);
    }
}
